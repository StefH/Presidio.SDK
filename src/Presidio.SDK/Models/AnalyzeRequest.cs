using Newtonsoft.Json;
using Presidio.Enums;

namespace Presidio.Models;

public class AnalyzeRequest
{
    /// <summary>
    /// The text to analyze.
    /// Example: "hello world, my name is Jane Doe. My number is: 034453334"
    /// </summary>
    public required string Text { get; init; }

    /// <summary>
    /// Two characters for the desired language in ISO_639-1 format.
    /// Example: "en"
    /// </summary>
    public required string Language { get; init; }

    /// <summary>
    /// A correlation id to append to headers and traces.
    /// </summary>
    public string? CorrelationId { get; init; }

    /// <summary>
    /// The minimal detection score threshold.
    /// </summary>
    public double? ScoreThreshold { get; init; }

    /// <summary>
    /// A list of entities to analyze.
    /// </summary>
    public string[]? Entities { get; init; }

    /// <summary>
    /// Whether to include analysis explanation in the response.
    /// </summary>
    public bool? ReturnDecisionProcess { get; init; }

    /// <summary>
    /// List of recognizers to be used in the context of this request only (ad-hoc).
    /// </summary>
    public PatternRecognizer[]? AdHocRecognizers { get; init; }

    /// <summary>
    /// List of context words which may help to raise recognized entities confidence.
    /// </summary>
    public string[]? Context { get; init; }

    /// <summary>
    /// The Regex flags to use.
    /// </summary>
    public RegexFlags? GlobalRegexFlags { get; set; }
}