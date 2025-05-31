using Presidio.Models;

namespace Presidio.Extensions;

public static class PatternRecognizerExtensions
{
    private static readonly string[] SupportedLanguages = ["en", "nl", "de", "it", "es"];

    public static List<PatternRecognizer>? AddDateTimeISO8601(this List<PatternRecognizer>? patternRecognizers, string[]? supportedLanguages = default)
    {
        if (patternRecognizers == null)
        {
            return null;
        }

        foreach (var language in supportedLanguages ?? SupportedLanguages)
        {
            var patternRecognizer = AdditionalPatternRecognizers.DateTimeISO8601Recognizer;
            if (patternRecognizers.Any(pr => pr.SupportedLanguage == language && pr.Name == patternRecognizer.Name))
            {
                continue;
            }

            patternRecognizers.Add(patternRecognizer with
            {
                Name = $"{patternRecognizer.Name} ({language})",
                SupportedLanguage = language
            });
        }

        return patternRecognizers;
    }
}