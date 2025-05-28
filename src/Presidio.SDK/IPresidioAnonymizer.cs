using Presidio.Models;
using RestEase;

namespace Presidio;

[Header("User-Agent", "sheyenrath/Presidio.SDK")]
public interface IPresidioAnonymizer
{
    /// <summary>
    /// Anonymize detected PII text entities with desired values
    /// </summary>
    /// <param name="request">The anonymize request containing text, anonymizers, and analyzer results</param>
    /// <param name="cancellationToken">The optional <see cref="CancellationToken"/>.</param>
    /// <returns>Anonymized text with details about applied operations</returns>
    [Post("/anonymize")]
    Task<AnonymizeResponse> AnonymizeAsync([Body] AnonymizeRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get the list of all built-in supported anonymizers
    /// </summary>
    /// <param name="cancellationToken">The optional <see cref="CancellationToken"/>.</param>
    /// <returns>A list of supported anonymizer names</returns>
    [Get("/anonymizers")]
    Task<string[]> GetAnonymizersAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Deanonymize previously anonymized text (decrypt only)
    /// </summary>
    /// <param name="request">The deanonymize request containing encrypted text and decryption parameters</param>
    /// <param name="cancellationToken">The optional <see cref="CancellationToken"/>.</param>
    /// <returns>Deanonymized text with details about applied operations</returns>
    [Post("/deanonymize")]
    Task<DeanonymizeResponse> DeanonymizeAsync([Body] DeanonymizeRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get the list of all built-in supported deanonymizers
    /// </summary>
    /// <param name="cancellationToken">The optional <see cref="CancellationToken"/>.</param>
    /// <returns>A list of supported deanonymizer names</returns>
    [Get("/deanonymizers")]
    Task<string[]> GetDeanonymizersAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Health check endpoint
    /// </summary>
    /// <param name="cancellationToken">The optional <see cref="CancellationToken"/>.</param>
    /// <returns>Health status message</returns>
    [Get("/health")]
    Task<string> GetHealthAsync(CancellationToken cancellationToken = default);
}