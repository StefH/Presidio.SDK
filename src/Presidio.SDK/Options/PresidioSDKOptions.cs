using System.ComponentModel.DataAnnotations;
using Presidio.Options.Validation;

namespace Presidio.Options;

[PublicAPI]
[AtLeastOneBaseAddressRequired]
public class PresidioSDKOptions
{
    /// <summary>
    /// The BaseAddress for Analyzer.
    /// </summary>
    public Uri? AnalyzerBaseAddress { get; set; }

    /// <summary>
    /// The BaseAddress for Anonymizer.
    /// </summary>
    public Uri? AnonymizerBaseAddress { get; set; }

    /// <summary>
    /// This timeout in seconds defines the timeout on the HttpClient which is used to call the BaseAddress.
    /// Default value is <c>60</c> seconds.
    /// </summary>
    [Range(1, int.MaxValue)]
    public int TimeoutInSeconds { get; set; } = 60;

    /// <summary>
    /// The maximum number of retries.
    /// </summary>
    [Range(0, 99)]
    public int MaxRetries { get; set; } = 5;

    /// <summary>
    /// In addition to Network failures, TaskCanceledException, HTTP 5XX and HTTP 408. Also retry these <see cref="HttpStatusCode"/>s. [Optional]
    /// </summary>
    public HttpStatusCode[]? HttpStatusCodesToRetry { get; set; }

    /// <summary>
    /// Log the request as Debug Logging.
    /// Default value is <c>false</c>;
    /// </summary>
    public bool LogRequest { get; set; }

    /// <summary>
    /// Log the response as Debug Logging.
    /// Default value is <c>false</c>;
    /// </summary>
    public bool LogResponse { get; set; }

    /// <summary>
    /// Write the request json as indented.
    /// Default value is <c>false</c>;
    /// </summary>
    public bool WriteJsonIndented { get; set; }
}