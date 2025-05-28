namespace Presidio.Models;

public class RecognizedMetadata
{
    /// <summary>
    /// Name of recognizer that made the decision
    /// </summary>
    public required string RecognizerName { get; init; }
}