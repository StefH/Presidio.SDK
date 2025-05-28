namespace Presidio.Models;

/// <summary>
/// Represents an anonymizer that provides functionality for data anonymization.
/// </summary>
/// <remarks>Implementations of this interface define specific anonymization strategies or techniques.
/// The <see cref="Type"/> property can be used to identify the type or category of the anonymizer.</remarks>
public interface IAnonymizer
{
    string Type { get; }
}