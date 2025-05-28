namespace Presidio.Models;

/// <summary>
/// Request object for anonymizing text using specified anonymizers and analyzer results.
/// </summary>
public class AnonymizeRequest
{
    /// <summary>
    /// The text to anonymize.
    /// </summary>
    public required string Text { get; set; }

    /// <summary>
    /// Object where the key is DEFAULT or the ENTITY_TYPE and the value is the anonymizer definition.
    /// </summary>
    /// <remarks>
    /// Example: { "DEFAULT": { "type": "replace", "new_value": "&lt;ENTITY_TYPE&gt;" } }
    /// </remarks>
    public Dictionary<string, IAnonymizer>? Anonymizers { get; init; }

    /// <summary>
    /// Array of analyzer detections.
    /// </summary>
    public required RecognizerResult[] AnalyzerResults { get; init; }
}