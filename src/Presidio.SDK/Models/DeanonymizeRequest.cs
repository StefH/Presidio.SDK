namespace Presidio.Models;

public class DeanonymizeRequest
{
    /// <summary>
    /// The anonymized text.
    /// </summary>
    public required string Text { get; init; }

    /// <summary>
    /// Object where the key is DEFAULT or the ENTITY_TYPE and the value is <c>decrypt</c> since it is the only one supported.
    /// </summary>
    public Dictionary<string, Decrypt> Deanonymizers { get; set; } = new();

    /// <summary>
    /// Array of anonymized PIIs.
    /// </summary>
    public required OperatorResult[] AnonymizerResults { get; init; }
}