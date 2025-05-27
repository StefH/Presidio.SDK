namespace Presidio.Models;

public class RecognizerResult
{
    public int Start { get; set; }

    public int End { get; set; }

    public double Score { get; set; }

    public required string EntityType { get; set; }

    public RecognizedMetadata RecognitionMetadata { get; set; } = null!;    
}