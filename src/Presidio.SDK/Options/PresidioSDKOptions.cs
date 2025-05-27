using System.ComponentModel.DataAnnotations;

namespace Presidio.Options;

[PublicAPI]
public class PresidioSDKOptions
{
    /// <summary>
    /// The required BaseAddress for Analyzer.
    /// </summary>
    [Required]
    public Uri AnalyzerBaseAddress { get; set; } = new("http://localhost:5002");

    /// <summary>
    /// The required BaseAddress for Anonymizer.
    /// </summary>
    [Required]
    public Uri AnonymizerBaseAddress { get; set; } = new("http://localhost:5001");

    /// <summary>
    /// This timeout in seconds defines the timeout on the HttpClient which is used to call the BaseAddress.
    /// Default value is 60 seconds.
    /// </summary>
    [Range(1, int.MaxValue)]
    public int TimeoutInSeconds { get; set; } = 60;

    /// <summary>
    /// The maximum number of retries.
    /// </summary>
    [Range(0, 99)]
    public int MaxRetries { get; set; } = 3;

    /// <summary>
    /// In addition to Network failures, TaskCanceledException, HTTP 5XX and HTTP 408. Also retry these <see cref="HttpStatusCode"/>s. [Optional]
    /// </summary>
    public HttpStatusCode[]? HttpStatusCodesToRetry { get; set; }
}