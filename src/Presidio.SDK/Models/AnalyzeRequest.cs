namespace Presidio.Models;

public class AnalyzeRequest
{
    public required string Text { get; init; }

    public required string Language { get; init; }

    public string? CorrelationId { get; init; }

    public double? ScoreThreshold { get; init; }

    public string[]? Entities { get; init; }

    public bool? ReturnDecisionProcess { get; init; }

    public PatternRecognizer[]? AdHocRecognizers { get; init; }

    public string[]? Context { get; init; }
}