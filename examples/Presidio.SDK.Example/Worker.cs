using Microsoft.Extensions.Logging;
using Presidio.Enums;
using Presidio.Models;
using Presidio.Types;

namespace Presidio.SDK.Example;

internal class Worker(IPresidioAnalyzer analyzerService, IPresidioAnonymizer anonymizerService, ILogger<Worker> logger)
{
    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        var text =
            """
            Geachte heer/mevrouw,
            
            Op 10 maart 2024 heb ik een oven gekocht. Helaas werkt de oven sinds 25 maart 2024 niet naar behoren, de oven wordt niet warm en geeft een foutmelding op het display.
            
            Ik zie uw reactie met belangstelling tegemoet.
            
            Peter Jansen
            Dorpsstraat 10, 5678 CD Utrecht
            peter.jansen@email.com
            """;

        // Step 1: Analyze text for PII
        var analyzeRequest = new AnalyzeRequest
        {
            Text = text,
            Language = "nl",
            AdHocRecognizers =
            [
                new PatternRecognizer
                {
                    Name = "Dutch Postcode recognizer",
                    SupportedEntity = "NL_POSTCODE",
                    SupportedLanguage = "nl",
                    GlobalRegexFlags = RegexFlags.Multiline | RegexFlags.DotAll,
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
                }
            ]
        };

        var analysisResults = await analyzerService.AnalyzeAsync(analyzeRequest, cancellationToken);


        // Step 2a: Anonymize the detected PII
        var anonymizeRequest = new AnonymizeRequest
        {
            Text = text,
            Anonymizers = new Dictionary<string, IAnonymizer>
            {
                [PIIEntityTypes.PERSON] = new Hash(),
                [PIIEntityTypes.DATE_TIME] = new Mask { MaskingChar = "*", CharsToMask = 99 },
                [PIIEntityTypes.EMAIL_ADDRESS] = new Encrypt { Key = "3t6w9z$C.F)J@NcR" }
            },
            AnalyzerResults = analysisResults.Select(r => new RecognizerResult
            {
                Start = r.Start,
                End = r.End,
                Score = r.Score,
                EntityType = r.EntityType
            }).ToArray()
        };

        var anonymizeResponse = await anonymizerService.AnonymizeAsync(anonymizeRequest, cancellationToken);
        logger.LogWarning("Anonymized text: {Text}", anonymizeResponse.Text);

        // Step 2b: Deanonymize the detected PII
        var deanonymizeRequest = new DeanonymizeRequest
        {
            Text = anonymizeResponse.Text,
            Deanonymizers = new Dictionary<string, Decrypt>
            {
                [PIIEntityTypes.EMAIL_ADDRESS] = new() { Key = "3t6w9z$C.F)J@NcR" }
            },
            AnonymizerResults = anonymizeResponse.Items
                .Where(r => r.Operator == Operators.encrypt)
                .ToArray()
        };

        var deanonymizeResponse = await anonymizerService.DeanonymizeAsync(deanonymizeRequest, cancellationToken);
        logger.LogWarning("Deanonymized text: {Text}", deanonymizeResponse.Text);
    }
}