namespace Presidio.Models;

public class AnalysisExplanation
{
    public string Recognizer { get; set; }

    public string PatternName { get; set; }

    public string Pattern { get; set; }

    public double? OriginalScore { get; set; }

    public double Score { get; set; }

    public string TextualExplanation { get; set; }

    public double? ScoreContextImprovement { get; set; }

    public string SupportiveContextWord { get; set; }

    public double? ValidationResult { get; set; }
}