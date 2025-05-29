namespace Presidio.Enums;

/// <summary>
/// Enum representing Python Regex flags.
/// </summary>
[Flags]
public enum RegexFlags
{
    None = 0,

    IgnoreCase = 2,

    Locale = 4,

    Multiline = 8,

    DotAll = 16,

    Unicode = 32,

    IgnoreWhitespaceAndComments = 64,

    Debug = 128,

    Ascii = 256
}