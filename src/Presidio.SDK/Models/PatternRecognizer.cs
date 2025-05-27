namespace Presidio.Models;

public class PatternRecognizer
{
    public string Name { get; set; } = null!;

    public string SupportedLanguage { get; set; } = null!;

    public Pattern[] Patterns { get; set; } = null!;

    public string[] DenyList { get; set; } = null!;

    public string[] Context { get; set; } = null!;

    public string SupportedEntity { get; set; } = null!;
}