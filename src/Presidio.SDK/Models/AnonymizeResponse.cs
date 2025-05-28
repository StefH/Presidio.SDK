namespace Presidio.Models;

/// <summary>
/// Represents the response from an anonymization operation.
/// </summary>
public class AnonymizeResponse
{
    /// <summary>
    /// The anonymized text.
    /// </summary>
    public required string Text { get; init; }

    /// <summary>
    /// Array of anonymized entities.
    /// </summary>
    public required OperatorResult[] Items { get; init; }
}