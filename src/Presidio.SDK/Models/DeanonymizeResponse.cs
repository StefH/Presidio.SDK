namespace Presidio.Models;

/// <summary>
/// Response object for deanonymization containing the resulting text and array of deanonymized entities.
/// </summary>
public class DeanonymizeResponse
{
    /// <summary>
    /// The deanonymized text.
    /// </summary>
    public required string Text { get; init; }

    /// <summary>
    /// Array of deanonymized entities.
    /// </summary>
    public required OperatorResult[] Items { get; init; }
}