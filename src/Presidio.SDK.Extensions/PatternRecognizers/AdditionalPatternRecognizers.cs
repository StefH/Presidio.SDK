using Presidio.Models;

namespace Presidio.Extensions.PatternRecognizers;

/// <summary>
/// Additional AdditionalPatternRecognizers.
/// </summary>
public static class AdditionalPatternRecognizers
{
    /// <summary>
    /// Recognizer for Dutch postcodes (NL_POSTCODE).
    /// Matches 4 digits (not starting with 0), optional space, and 2 uppercase letters (excluding SA, SD, SS).
    /// </summary>
    public static readonly PatternRecognizer DutchPostCode = new()
    {
        Name = "Dutch postcode recognizer",
        SupportedEntity = "NL_POSTCODE",
        SupportedLanguage = "en",
        Patterns =
        [
            new Pattern
            {
                Name = "Dutch PostCode",
                Regex = @"(\s[1-9][0-9]{3}\s?(?!SA|SD|SS)[A-Z]{2}\s)",
                Score = 1
            }
        ],
        Context = ["postcode"]
    };

    /// <summary>
    /// Recognizer for Dutch date (NL_DATE).
    /// </summary>
    public static readonly PatternRecognizer DutchDate = new()
    {
        Name = "NL Date",
        SupportedEntity = "NL_DATE",
        SupportedLanguage = "en",
        Patterns =
        [
            new Pattern
            {
                Name = "Dutch Date",
                Regex = @"(\b\d{1,2}\s+(januari|februari|maart|april|mei|juni|juli|augustus|september|oktober|november|december)\s+\d{4}\b)",
                Score = 1
            }
        ],
        Context = ["date"]
    };

    /// <summary>
    /// Recognizer for Dutch date (NL_DATE_TIME).
    /// </summary>
    public static readonly PatternRecognizer DutchDateTime = new()
    {
        Name = "NL DateTime",
        SupportedEntity = "NL_DATE_TIME",
        SupportedLanguage = "en",
        Patterns =
        [
            new Pattern
            {
                Name = "Dutch DateTime",
                Regex = @"\b\d{1,2}\s+(januari|februari|maart|april|mei|juni|juli|augustus|september|oktober|november|december)\s+\d{4}\s+om\s+\d{2}:\d{2}:\d{2}\b",
                Score = 1
            }
        ],
        Context = ["date", "datetime", "time"]
    };

    /// <summary>
    /// Provides a recognizer for identifying Dutch Burgerservicenummer (BSN) patterns in text.
    /// </summary>
    public static readonly PatternRecognizer DutchBSN = new()
    {
        Name = "Dutch Burgerservicenummer (BSN) recognizer",
        SupportedEntity = "NL_BSN",
        SupportedLanguage = "en",
        Patterns =
        [
            new Pattern
            {
                Name = "Dutch BSN",
                Regex = @"(\s[1-9][0-9]{8}\s)",
                Score = 0.75
            }
        ],
        Context = ["bsn", "Burgerservicenummer"]
    }; 
}