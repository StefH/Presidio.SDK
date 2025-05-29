using Presidio.Enums;

namespace Presidio.Models;

/// <summary>
/// Explanation of how a recognizer made a decision.
/// </summary>
public class AnalysisExplanation
{
    /// <summary>
    /// Name of recognizer that made the decision.
    /// </summary>
    public required string Recognizer { get; init; }

    /// <summary>
    /// Name of pattern (if decision was made by a PatternRecognizer).
    /// </summary>
    public string? PatternName { get; init; }

    /// <summary>
    /// Regex pattern that was applied (if PatternRecognizer).
    /// </summary>
    public string? Pattern { get; init; }

    /// <summary>
    /// Regex Flags used.
    /// </summary>
    public RegexFlags? RegexFlags { get; init; }

    /// <summary>
    /// Recognizer's confidence in result.
    /// </summary>
    public double? OriginalScore { get; set; }

    /// <summary>
    /// The PII detection score.
    /// </summary>
    public double Score { get; set; }

    /// <summary>
    /// Free text for describing a decision of a logic or model.
    /// </summary>
    public required string TextualExplanation { get; init; }

    /// <summary>
    /// Difference from the original score.
    /// </summary>
    public double? ScoreContextImprovement { get; set; }

    /// <summary>
    /// The context word which helped increase the score.
    /// </summary>
    public required string SupportiveContextWord { get; init; }

    /// <summary>
    /// Result of a validation (e.g. checksum).
    /// </summary>
    public object? ValidationResult { get; set; }
}