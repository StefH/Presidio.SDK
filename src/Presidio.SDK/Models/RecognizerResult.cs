using Newtonsoft.Json;

namespace Presidio.Models;

public class RecognizerResult
{
    public int Start { get; set; }

    public int End { get; set; }

    [JsonIgnore]
    public int Length => End - Start;

    public double Score { get; set; }

    public required string EntityType { get; set; }

    public RecognizedMetadata RecognitionMetadata { get; set; } = null!;    
}