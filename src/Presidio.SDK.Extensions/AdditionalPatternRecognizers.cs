using Presidio.Enums;
using Presidio.Models;

namespace Presidio.Extensions;

/// <summary>
/// Additional AdditionalPatternRecognizers.
/// </summary>
public static class AdditionalPatternRecognizers
{
    private const RegexFlags DefaultGlobalRegexFlags = RegexFlags.Multiline | RegexFlags.DotAll;

    /// <summary>
    /// Recognizer for ISO 8601 Date object with timezone or UTC. 
    /// </summary>
    public static readonly PatternRecognizer DateTimeISO8601Recognizer = new()
    {
        Name = "ISO 8601 DateTime recognizer",
        SupportedEntity = "DATE_TIME",
        SupportedLanguage = "en",
        Patterns =
        [
            new Pattern
            {
                Name = "ISO 8601 DateTime",
                Regex = @"\b(\d{4}(-?\d\d){2})[tT]?((\d\d:?){1,2}(\d\d)?(.\d{3})?([zZ]|[+-](\d\d):?(\d\d)))?\b",
                Score = 1
            }
        ],
        Context = ["date", "time", "datetime", "ISO 8601"]
    };

    /// <summary>
    /// Recognizer for Dutch postcodes (NL_POSTCODE).
    /// Matches 4 digits (not starting with 0), optional space, and 2 uppercase letters (excluding SA, SD, SS).
    /// </summary>
    public static readonly PatternRecognizer NlPostCode = new()
    {
        Name = "Dutch Postcode recognizer",
        SupportedEntity = "NL_POSTCODE",
        SupportedLanguage = "nl",
        GlobalRegexFlags = DefaultGlobalRegexFlags,
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
    public static readonly PatternRecognizer NlDateRecognizer = new()
    {
        Name = "Dutch Date recognizer",
        SupportedEntity = "NL_DATE",
        SupportedLanguage = "nl",
        GlobalRegexFlags = DefaultGlobalRegexFlags,
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
    /// Provides a recognizer for identifying Dutch Burgerservicenummer (BSN) patterns in text.
    /// </summary>
    public static readonly PatternRecognizer NlBSNRecognizer = new()
    {
        Name = "Dutch Burgerservicenummer (BSN) recognizer",
        SupportedEntity = "NL_BSN",
        SupportedLanguage = "nl",
        GlobalRegexFlags = DefaultGlobalRegexFlags,
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
    public static readonly PatternRecognizer NlStreetRecognizer = new()
    {
        Name = "Dutch Street (including house number) recognizer",
        SupportedEntity = "NL_STREET",
        SupportedLanguage = "nl",
        Patterns =
        [
            new Pattern
            {
                Name = "Dutch Street",
                Regex = @"\b(?:(?:[0-9]{1,2}(?:e|ste|de)\s)?(?:(?:van|de|den|der|het|ter|ten|la|le|l’|sint|saint|prof(?:essor)?|dr|mr|mevrouw|jonkheer)\.?[\s\-]*)*)(?:[\w\-']+\s?)+(?:straat|laan|weg|hof|gracht|plein|dreef|singel|pad|steeg|kade|plantsoen|boulevard|ring|markt|akker|gaarde|zoom|oord|veld|kamp|erf|hoek|wal|berg|weide|rotonde|park|stoep|brug|haven|vest|scheg|pier|lei|tuin|kaai|dam|dwarsstraat|dwarsweg|dwarslaan)\b\s*[0-9]{1,5}(?:[\s\-]?[a-zA-Z]{1,3})?(?:\s?[0-9]{1,2})?",
                Score = 0.75
            }
        ],
        Context = ["street", "address"]
    };
}