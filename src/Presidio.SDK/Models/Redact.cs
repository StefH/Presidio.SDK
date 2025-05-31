using Presidio.Enums;

namespace Presidio.Models;


/// <summary>
/// Replace with an empty string
/// </summary>
public class Redact : IAnonymizer
{
    /// <inheritdoc />
    public Operators Type => Operators.redact;
}