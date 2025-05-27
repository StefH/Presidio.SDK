using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;

namespace Presidio.RetryPolicies;

internal static class HttpClientRetryPolicies
{
    public static IAsyncPolicy<HttpResponseMessage> GetPolicy<T>(IServiceProvider serviceProvider, int maxRetries, HttpStatusCode[]? statusCodesToRetry) where T : class
    {
        var policyBuilder = HttpPolicyExtensions
            .HandleTransientHttpError();

        if (statusCodesToRetry is { Length: > 0 })
        {
            policyBuilder = policyBuilder.OrResult(httpResponseMessage => statusCodesToRetry.Contains(httpResponseMessage.StatusCode));
        }

        return policyBuilder
            .OrInner<TaskCanceledException>()
            .WaitAndRetryAsync(maxRetries, retryCount => TimeSpan.FromSeconds(Math.Pow(2, retryCount)), (result, timeSpan, retryCount, context) =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<T>>();
                var reason = result?.Result?.StatusCode.ToString() ?? result?.Exception.Message;

                logger.LogWarning("Request failed with '{reason}'. Waiting {timeSpan} before next retry. Retry attempt {retryCount}/{totalRetryCount}.", reason, timeSpan, retryCount, maxRetries);
            });
    }
}