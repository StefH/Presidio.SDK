using Presidio.Enums;

namespace Presidio.Models;

/// <summary>
/// A regular expressions or deny-list based recognizer.
/// </summary>
public record PatternRecognizer
{
    /// <summary>
    /// Name of recognizer.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Language code supported by this recognizer.
    /// </summary>
    public required string SupportedLanguage { get; init; }

    /// <summary>
    /// List of type Pattern containing regex expressions with additional metadata.
    /// </summary>
    public required Pattern[] Patterns { get; init; }

    /// <summary>
    /// The Regex flags to use for this recognizer.
    /// </summary>
    public RegexFlags? GlobalRegexFlags { get; init; }

    /// <summary>
    /// List of words to be returned as PII if found.
    /// </summary>
    public string[]? DenyList { get; init; }

    /// <summary>
    /// List of words to be used to increase confidence if found in the vicinity of detected entities.
    /// </summary>
    public string[]? Context { get; init; }

    /// <summary>
    /// The name of entity this ad hoc recognizer detects.
    /// </summary>
    public required string SupportedEntity { get; init; }
}