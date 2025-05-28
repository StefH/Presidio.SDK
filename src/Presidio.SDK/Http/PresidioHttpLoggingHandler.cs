using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Presidio.Options;

namespace Presidio.Http;

internal class PresidioHttpLoggingHandler(ILogger<PresidioHttpLoggingHandler> logger, IOptions<PresidioSDKOptions> options) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (options.Value.LogRequest)
        {
            logger.LogInformation("Request: {Method} {Uri}", request.Method, request.RequestUri);
            if (request.Content != null)
            {
                var requestContent = await ReadContentAsStringAsync(request.Content, cancellationToken);
                logger.LogDebug("Request Content: {Content}", requestContent);
            }
        }

        if (options.Value.LogResponse)
        {
            var response = await base.SendAsync(request, cancellationToken);
            logger.LogInformation("Response: {StatusCode} {Uri}", response.StatusCode, response.RequestMessage?.RequestUri);

            var responseContent = await ReadContentAsStringAsync(response.Content, cancellationToken);
            logger.LogDebug("Response Content: {Content}", responseContent);

            return response;
        }

        return await base.SendAsync(request, cancellationToken);
    }

    private static async Task<string?> ReadContentAsStringAsync(HttpContent? content, CancellationToken cancellationToken)
    {
        if (content == null)
        {
            return null;
        }

#if NETSTANDARD2_1
        return await content.ReadAsStringAsync();
#else
        return await content.ReadAsStringAsync(cancellationToken);
#endif
    }
}