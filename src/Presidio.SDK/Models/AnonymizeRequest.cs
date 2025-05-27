namespace Presidio.Models;

public class AnonymizeRequest
{
    public required string Text { get; set; }
    
    public Dictionary<string, IAnonymizer>? Anonymizers { get; init; }
    
    public required RecognizerResult[] AnalyzerResults { get; init; }
}