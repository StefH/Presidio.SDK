using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Presidio.Options;
using Presidio.RetryPolicies;
using RestEase.HttpClientFactory;
using Stef.Validation;

// ReSharper disable once CheckNamespace
namespace Presidio.DependencyInjection;

[PublicAPI]
public static class ServiceCollectionExtensions
{
    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new SnakeCaseNamingStrategy(),
        }
    };

    public static IServiceCollection AddPresidioSDK(this IServiceCollection services, IConfiguration configuration)
    {
        Guard.NotNull(services);
        Guard.NotNull(configuration);

        return services.AddPresidioSDK(restEaseClientOptions =>
        {
            configuration.GetSection(nameof(PresidioSDKOptions)).Bind(restEaseClientOptions);
        });
    }

    public static IServiceCollection AddPresidioSDK(this IServiceCollection services, IConfigurationSection section, JsonSerializerSettings? jsonSerializerSettings = null)
    {
        Guard.NotNull(services);
        Guard.NotNull(section);

        return services.AddPresidioSDK(section.Bind);
    }

    public static IServiceCollection AddPresidioSDK(this IServiceCollection services, Action<PresidioSDKOptions> configureAction)
    {
        Guard.NotNull(services);
        Guard.NotNull(configureAction);

        var options = new PresidioSDKOptions();
        configureAction(options);

        return services.AddPresidioSDK(options);
    }

    // Add this class to enable logging of all HTTP requests and responses
    public class HttpLoggingHandler : DelegatingHandler
    {
        private readonly ILogger<HttpLoggingHandler> _logger;

        public HttpLoggingHandler(ILogger<HttpLoggingHandler> logger)
        {
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Request: {Method} {Uri}", request.Method, request.RequestUri);
            if (request.Content != null)
            {
                var requestContent = await request.Content.ReadAsStringAsync();
                _logger.LogDebug("Request Content: {Content}", requestContent);
            }

            var response = await base.SendAsync(request, cancellationToken);

            _logger.LogInformation("Response: {StatusCode} {Uri}", response.StatusCode, response.RequestMessage?.RequestUri);
            if (response.Content != null)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                _logger.LogDebug("Response Content: {Content}", responseContent);
            }

            return response;
        }
    }

    // In AddPresidioSDK, register the handler and add it to the HttpClient pipeline
    public static IServiceCollection AddPresidioSDK(this IServiceCollection services, PresidioSDKOptions options)
    {
        Guard.NotNull(services);
        Guard.NotNull(options);

        services.AddOptionsWithDataAnnotationValidation(options);

        // Register the logging handler
        services.AddTransient<HttpLoggingHandler>();

        services
            .AddHttpClient("Presidio.SDK.Analyzer", httpClient =>
            {
                httpClient.BaseAddress = options.AnalyzerBaseAddress;
                httpClient.Timeout = TimeSpan.FromSeconds(options.TimeoutInSeconds);
            })
            .AddPolicyHandler((serviceProvider, _) => HttpClientRetryPolicies.GetPolicy<IPresidioAnalyzer>(serviceProvider, options.MaxRetries, options.HttpStatusCodesToRetry))
            .AddHttpMessageHandler<HttpLoggingHandler>()
            .UseWithRestEaseClient<IPresidioAnalyzer>(o => o.JsonSerializerSettings = JsonSerializerSettings);

        services
            .AddHttpClient("Presidio.SDK.Anonymizer", httpClient =>
            {
                httpClient.BaseAddress = options.AnonymizerBaseAddress;
                httpClient.Timeout = TimeSpan.FromSeconds(options.TimeoutInSeconds);
            })
            .AddPolicyHandler((serviceProvider, _) => HttpClientRetryPolicies.GetPolicy<IPresidioAnonymizer>(serviceProvider, options.MaxRetries, options.HttpStatusCodesToRetry))
            .AddHttpMessageHandler<HttpLoggingHandler>()
            .UseWithRestEaseClient<IPresidioAnonymizer>(o => o.JsonSerializerSettings = JsonSerializerSettings);

        return services;
    }
}