using Presidio.Enums;

namespace Presidio.Models;

/// <summary>
/// Replace with a given value.
/// </summary>
public class Replace : IAnonymizer
{
    /// <summary>
    /// The type of anonymizer. Always "replace".
    /// </summary>
    /// <example>replace</example>
    public Operators Type => Operators.replace;

    /// <summary>
    /// The string to replace with.
    /// </summary>
    /// <example>VALUE</example>
    public required string NewValue { get; init; }
}