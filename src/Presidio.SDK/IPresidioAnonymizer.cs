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
    /// <returns>Anonymized text with details about applied operations</returns>
    [Post("/anonymize")]
    Task<AnonymizeResponse> AnonymizeAsync([Body] AnonymizeRequest request);

    /// <summary>
    /// Get the list of all built-in supported anonymizers
    /// </summary>
    /// <returns>A list of supported anonymizer names</returns>
    [Get("/anonymizers")]
    Task<string[]> GetAnonymizersAsync();

    /// <summary>
    /// Deanonymize previously anonymized text (decrypt only)
    /// </summary>
    /// <param name="request">The deanonymize request containing encrypted text and decryption parameters</param>
    /// <returns>Deanonymized text with details about applied operations</returns>
    [Post("/deanonymize")]
    [AllowAnyStatusCode]
    Task<Response<DeanonymizeResponse>> DeanonymizeAsync([Body] DeanonymizeRequest request);

    /// <summary>
    /// Get the list of all built-in supported deanonymizers
    /// </summary>
    /// <returns>A list of supported deanonymizer names</returns>
    [Get("/deanonymizers")]
    Task<string[]> GetDeanonymizersAsync();

    /// <summary>
    /// Health check endpoint
    /// </summary>
    /// <returns>Health status message</returns>
    [Get("/health")]
    Task<string> GetHealthAsync();
}