namespace Presidio.Models;

/// <summary>
/// Replace encrypted PII decrypted text.
/// </summary>
public class Decrypt
{
    /// <summary>
    /// "decrypt"
    /// </summary>
    /// <example>decrypt</example>
    public string Type => "decrypt";

    /// <summary>
    /// Cryptographic key of length 128, 192 or 256 bits, in a string format.
    /// </summary>
    /// <example>3t6w9z$C.F)J@NcR</example>
    public required string Key { get; init; }
}