using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Presidio.Http;
using Presidio.Options;
using Presidio.RetryPolicies;
using RestEase.HttpClientFactory;
using Stef.Validation;

// ReSharper disable once CheckNamespace
namespace Presidio.DependencyInjection;

[PublicAPI]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresidioSDK(this IServiceCollection services, IConfiguration configuration)
    {
        Guard.NotNull(services);
        Guard.NotNull(configuration);

        return services.AddPresidioSDK(restEaseClientOptions =>
        {
            configuration.GetSection(nameof(PresidioSDKOptions)).Bind(restEaseClientOptions);
        });
    }

    public static IServiceCollection AddPresidioSDK(this IServiceCollection services, IConfigurationSection section)
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

    public static IServiceCollection AddPresidioSDK(this IServiceCollection services, PresidioSDKOptions options)
    {
        Guard.NotNull(services);
        Guard.NotNull(options);

        services.AddOptionsWithDataAnnotationValidation(options);

        if (options.LogRequest || options.LogResponse)
        {
            services.AddTransient<PresidioHttpLoggingHandler>();
        }

        var jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            },
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = options.WriteJsonIndented ? Formatting.Indented : Formatting.None
        };

        services
                .AddHttpClient("Presidio.SDK.Analyzer", httpClient =>
                {
                    httpClient.BaseAddress = options.AnalyzerBaseAddress;
                    httpClient.Timeout = TimeSpan.FromSeconds(options.TimeoutInSeconds);
                })
                .AddPolicyHandler((serviceProvider, _) => HttpClientRetryPolicies.GetPolicy<IPresidioAnalyzer>(serviceProvider, options.MaxRetries, options.HttpStatusCodesToRetry))
                .AddPresidioHttpLoggingHandler(options)
                .UseWithRestEaseClient<IPresidioAnalyzer>(o => o.JsonSerializerSettings = jsonSerializerSettings);

        services
            .AddHttpClient("Presidio.SDK.Anonymizer", httpClient =>
            {
                httpClient.BaseAddress = options.AnonymizerBaseAddress;
                httpClient.Timeout = TimeSpan.FromSeconds(options.TimeoutInSeconds);
            })
            .AddPolicyHandler((serviceProvider, _) => HttpClientRetryPolicies.GetPolicy<IPresidioAnonymizer>(serviceProvider, options.MaxRetries, options.HttpStatusCodesToRetry))
            .AddPresidioHttpLoggingHandler(options)
            .UseWithRestEaseClient<IPresidioAnonymizer>(o => o.JsonSerializerSettings = jsonSerializerSettings);

        return services;
    }
}