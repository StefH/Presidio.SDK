namespace Presidio.Models;

public class RecognizerResultWithAnalysisExplanation : RecognizerResult
{
    public required AnalysisExplanation AnalysisExplanation { get; init; }
}