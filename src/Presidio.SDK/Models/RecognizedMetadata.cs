namespace Presidio.Models;

public class RecognizedMetadata
{
    /// <summary>
    /// Name of the recognizer that made the decision.
    /// </summary>
    public required string RecognizerName { get; init; }

    /// <summary>
    /// Identifier of the recognizer that made the decision.
    /// </summary>
    public required string RecognizerIdentifier { get; init; }
}