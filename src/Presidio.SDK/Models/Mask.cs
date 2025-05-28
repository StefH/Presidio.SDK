namespace Presidio.Models;

public class Mask : IAnonymizer
{
    /// <summary>
    /// Gets the type of the anonymizer. Always "mask".
    /// </summary>
    public string Type => "mask";

    /// <summary>
    /// The replacement character.
    /// </summary>
    public string MaskingChar { get; set; } = "*";

    /// <summary>
    /// The amount of characters that should be replaced.
    /// </summary>
    public int CharsToMask { get; set; }

    /// <summary>
    /// Whether to mask the PII from its end.
    /// </summary>
    public bool FromEnd { get; set; }
}