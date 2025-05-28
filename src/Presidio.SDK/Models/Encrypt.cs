using Presidio.Enums;

namespace Presidio.Models;

/// <summary>
/// Replace with an encrypted value.
/// </summary>
public class Encrypt : IAnonymizer
{
    /// <summary>
    /// Gets the anonymizer type.
    /// </summary>
    public Operators Type => Operators.encrypt;

    /// <summary>
    /// Cryptographic key of length 128, 192 or 256 bits, in a string format.
    /// </summary>
    /// <example>3t6w9z$C.F)J@NcR</example>
    public required string Key { get; init; }
}