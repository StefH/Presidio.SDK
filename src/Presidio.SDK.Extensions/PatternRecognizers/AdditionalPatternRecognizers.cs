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
        SupportedLanguage = "nl",
        Patterns =
        [
            new Pattern
            {
                Name = "Dutch PostCode",
                Regex = @"\b[1-9][0-9]{3}\s?(?!SA|SD|SS)[A-Z]{2}\b",
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
        SupportedLanguage = "nl",
        Patterns =
        [
            new Pattern
            {
                Name = "Dutch Date",
                Regex = @"\b\d{1,2}\s+(januari|februari|maart|april|mei|juni|juli|augustus|september|oktober|november|december)\s+\d{4}\b",
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
        SupportedLanguage = "nl",
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
        SupportedLanguage = "nl",
        Patterns =
        [
            new Pattern
            {
                Name = "Dutch BSN",
                Regex = @"\b[1-9][0-9]{8}\b",
                Score = 0.75
            }
        ],
        Context = ["bsn", "Burgerservicenummer"]
    };

    /// <summary>
    /// Provides a recognizer for identifying a Dutch Street Address (including house number).
    /// </summary>
    public static readonly PatternRecognizer DutchStreet = new()
    {
        Name = "Dutch Street including house number",
        SupportedEntity = "NL_STREET",
        SupportedLanguage = "nl",
        Patterns =
        [
            new Pattern
            {
                Name = "Dutch Street",
                Regex = @"\b(?:(?:[0-9]{1,2}(?:e|ste|de)\s)?(?:(?:van|de|den|der|het|ter|ten|la|le|l’|sint|saint|prof(?:essor)?|dr|mr|mevrouw|jonkheer)\.?[\s\-]*)*)(?:[\w\-']+\s?)+(?:straat|laan|weg|hof|gracht|plein|dreef|singel|pad|steeg|kade|plantsoen|boulevard|ring|markt|akker|gaarde|zoom|oord|veld|kamp|erf|hoek|wal|berg|weide|rotonde|park|stoep|brug|haven|vest|scheg|pier|lei|tuin|kaai|dam|dwarsstraat|dwarsweg|dwarslaan)\b\s*[0-9]{1,5}(?:[\s\-]?[a-zA-Z]{1,3})?(?:\s?[0-9]{1,2})?",
                Score = 0.99
            }
        ],
        Context = ["street", "address"]
    };
}