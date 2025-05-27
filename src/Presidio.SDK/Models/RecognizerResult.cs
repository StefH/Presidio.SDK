using Newtonsoft.Json;
using Presidio.Json;

namespace Presidio.Models;

public class RecognizerResult
{
    public int Start { get; set; }

    public int End { get; set; }

    [JsonIgnore]
    public int Length => End - Start;

    public double Score { get; set; }

    [JsonConverter(typeof(SafeEnumConverter<PIIEntityTypes>))]
    public required PIIEntityTypes EntityType { get; init; }

    public RecognizedMetadata RecognitionMetadata { get; set; } = null!;    
}