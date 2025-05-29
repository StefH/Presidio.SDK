namespace Presidio.Models;

/// <summary>
/// Explanation of how a recognizer made a decision.
/// </summary>
public class AnalysisExplanation
{
    private static readonly Dictionary<int, string> PythonRegexNamesMap = new()
    {
        { 0, "None" },
        { 2, "IgnoreCase" },
        { 4, "CultureInvariant" },
        { 8, "Multiline" },
        { 16, "DotAll" },
        { 64, "IgnorePatternWhitespace" },
        { 256, "ECMAScript" }
    };

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
    /// Regex Flags.
    /// </summary>
    public int? RegexFlags { get; init; }

    /// <summary>
    /// Returns a comma separated list of the Python Regex flags based on the RegexFlags property.
    /// </summary>
    public string? RegexFlagsAsString
    {
        get
        {
            if (RegexFlags == null)
            {
                return null;
            }

            var flagNames = new List<string>();
            foreach (var kvp in PythonRegexNamesMap.Skip(1))
            {
                if ((RegexFlags & kvp.Key) == kvp.Key)
                {
                    flagNames.Add(kvp.Value);
                }
            }

            return flagNames.Count > 0 ? string.Join(", ", flagNames) : PythonRegexNamesMap[0];
        }
    }

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