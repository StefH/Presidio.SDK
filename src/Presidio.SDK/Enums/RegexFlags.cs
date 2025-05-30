namespace Presidio.Enums;

/// <summary>
/// Enum representing Python Regex flags.
/// </summary>
[Flags]
public enum RegexFlags
{
    /// <summary>
    /// No flags.
    /// </summary>
    None = 0,

    /// <summary>
    /// Case-insensitive matching.
    /// </summary>
    IgnoreCase = 2,

    /// <summary>
    /// Locale-dependent matching. (Deprecated in favor of <c>Unicode</c>>)
    /// </summary>
    Locale = 4,

    /// <summary>
    /// ^ and $ match the start/end of each line.
    /// </summary>
    Multiline = 8,

    /// <summary>
    /// Matches any character, including newline.
    /// </summary>
    DotAll = 16,

    /// <summary>
    /// Makes \w, \W, etc., Unicode-aware. (Default in Python 3)
    /// </summary>
    Unicode = 32,

    /// <summary>
    /// Allows for whitespace and comments in regex for readability.
    /// </summary>
    IgnoreWhitespaceAndComments = 64,

    /// <summary>
    /// Output debug information about compiled regex.
    /// </summary>
    Debug = 128,

    /// <summary>
    /// Makes \w, \W, \b, \B, \d, \D, \s, and \S match only ASCII characters.
    /// </summary>
    Ascii = 256
}