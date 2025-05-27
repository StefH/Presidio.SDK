using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            NamingStrategy = new SnakeCaseNamingStrategy()
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

    public static IServiceCollection AddPresidioSDK(this IServiceCollection services, PresidioSDKOptions options)
    {
        Guard.NotNull(services);
        Guard.NotNull(options);

        services.AddOptionsWithDataAnnotationValidation(options);

        services
            .AddHttpClient("Presidio.SDK.Analyzer", httpClient =>
            {
                httpClient.BaseAddress = options.AnalyzerBaseAddress;
                httpClient.Timeout = TimeSpan.FromSeconds(options.TimeoutInSeconds);
            })
            .AddPolicyHandler((serviceProvider, _) => HttpClientRetryPolicies.GetPolicy<IPresidioAnalyzer>(serviceProvider, options.MaxRetries, options.HttpStatusCodesToRetry))
            .UseWithRestEaseClient<IPresidioAnalyzer>(o => o.JsonSerializerSettings = JsonSerializerSettings);

        services
            .AddHttpClient("Presidio.SDK.Anonymizer", httpClient =>
            {
                httpClient.BaseAddress = options.AnonymizerBaseAddress;
                httpClient.Timeout = TimeSpan.FromSeconds(options.TimeoutInSeconds);
            })
            .AddPolicyHandler((serviceProvider, _) => HttpClientRetryPolicies.GetPolicy<IPresidioAnonymizer>(serviceProvider, options.MaxRetries, options.HttpStatusCodesToRetry))
            .UseWithRestEaseClient<IPresidioAnonymizer>(o => o.JsonSerializerSettings = JsonSerializerSettings);

        return services;
    }
}