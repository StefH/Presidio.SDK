using Microsoft.Extensions.DependencyInjection;
using Presidio.Options;

namespace Presidio.Http;

internal static class HttpClientBuilderExtensions
{
    internal static IHttpClientBuilder AddPresidioHttpLoggingHandler(this IHttpClientBuilder builder, PresidioSDKOptions options)
    {
        if (options.LogRequest || options.LogResponse)
        {
            return builder.AddHttpMessageHandler<PresidioHttpLoggingHandler>();
        }

        return builder;
    }
}