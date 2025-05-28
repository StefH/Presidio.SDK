using Presidio.Enums;

namespace Presidio.Models;


/// <summary>
/// Replace with an empty string
/// </summary>
public class Redact : IAnonymizer
{
    /// <summary>
    /// Gets the type of the anonymizer.
    /// </summary>
    /// <example>redact</example>
    public Operators Type => Operators.redact;
}