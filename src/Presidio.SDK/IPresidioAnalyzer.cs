using Presidio.Models;
using RestEase;

namespace Presidio;

[Header("User-Agent", "sheyenrath/Presidio.SDK")]
public interface IPresidioAnalyzer
{
    /// <summary>
    /// Recognizes PII entities in a given text and returns their types, locations and score
    /// </summary>
    /// <param name="request">The analyze request containing text and parameters.</param>
    /// <param name="cancellationToken">The optional <see cref="CancellationToken"/>.</param>
    /// <returns>A list of analysis results</returns>
    [Post("/analyze")]
    Task<RecognizerResultWithAnalysisExplanation[]> AnalyzeAsync([Body] AnalyzeRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get the available PII recognizers for a given language
    /// </summary>
    /// <param name="language">Two characters for the desired language in ISO_639-1 format. Default value is <c>en</c>.</param>
    /// <param name="cancellationToken">The optional <see cref="CancellationToken"/>.</param>
    /// <returns>A list of supported recognizers</returns>
    [Get("/recognizers")]
    Task<string[]> GetRecognizersAsync([Query] string language = "en", CancellationToken cancellationToken = default);

    /// <summary>
    /// Get the list of PII entities Presidio-Analyzer is capable of detecting
    /// </summary>
    /// <param name="language">Two characters for the desired language in ISO_639-1 format. Default value is <c>en</c>.</param>
    /// <param name="cancellationToken">The optional <see cref="CancellationToken"/>.</param>
    /// <returns>A list of supported entities</returns>
    [Get("/supportedentities")]
    Task<string[]> GetSupportedEntitiesAsync([Query] string language = "en", CancellationToken cancellationToken = default);
}