using Newtonsoft.Json;
using Presidio.Enums;

namespace Presidio.Models;

public class RecognizerResult
{
    /// <summary>
    /// Where the PII starts
    /// </summary>
    public int Start { get; set; }

    /// <summary>
    /// Where the PII ends
    /// </summary>
    public int End { get; set; }

    /// <summary>
    /// The length of the PII.
    /// </summary>
    [JsonIgnore]
    public int Length => End - Start;

    /// <summary>
    /// The PII detection score
    /// </summary>
    public double Score { get; set; }

    /// <summary>
    /// The detected entity type
    /// </summary>
    public required PIIEntityTypes EntityType { get; init; }

    /// <summary>
    /// Recognition metadata
    /// </summary>
    public RecognizedMetadata? RecognitionMetadata { get; set; }
}