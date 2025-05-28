using Presidio.Models;

namespace Presidio.PatternRecognizers;

/// <summary>
/// Additional AdditionalPatternRecognizers.
/// </summary>
public static class AdditionalPatternRecognizers
{
    /// <summary>
    /// Recognizer for Dutch postcodes (NL_POSTCODE).
    /// Matches 4 digits (not starting with 0), optional space, and 2 uppercase letters (excluding SA, SD, SS).
    /// </summary>
    public static readonly PatternRecognizer DutchPostCode = new PatternRecognizer
    {
        Name = "Dutch postcode recognizer",
        SupportedEntity = "NL_POSTCODE",
        SupportedLanguage = "en",
        Patterns =
        [
            new Pattern
            {
                Name = "Dutch PostCode",
                Regex = "([1-9][0-9]{3}\\s?(?!SA|SD|SS)[A-Z]{2})",
                Score = 1
            }
        ],
        Context = ["postcode"]
    };

    /// <summary>
    /// Provides a recognizer for identifying Dutch Burgerservicenummer (BSN) patterns in text.
    /// </summary>
    public static readonly PatternRecognizer DutchBSN = new PatternRecognizer
    {
        Name = "Dutch Burgerservicenummer (BSN) recognizer",
        SupportedEntity = "NL_BSN",
        SupportedLanguage = "en",
        Patterns =
        [
            new Pattern
            {
                Name = "Dutch BSN",
                Regex = "([1-9][0-9]{8})",
                Score = 0.75
            }
        ],
        Context = ["bsn", "Burgerservicenummer"]
    };
}