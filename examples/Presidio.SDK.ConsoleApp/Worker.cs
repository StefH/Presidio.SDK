using Microsoft.Extensions.Logging;
using Presidio.Enums;
using Presidio.Extensions;
using Presidio.Models;
using Presidio.Types;

namespace Presidio.SDK.ConsoleApp;

internal class Worker(IPresidioAnalyzer analyzerService, IPresidioAnonymizer anonymizerService, ILogger<Worker> logger)
{
    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        var text =
            """
            Geachte heer/mevrouw,
            Op 10 maart 2024 heb ik bij TechHome een oven van het merk Siemens en type HB674GBS1 gekocht.
            Helaas werkt de oven sinds 25 maart 2024 niet naar behoren.
            Het probleem is als volgt: de oven wordt niet warm en geeft constant een foutmelding op het display.
            Dit belemmert het gebruik en zorgt ervoor dat ik geen gerechten kan bereiden.
            Conform de wettelijke garantie en het recht op een deugdelijk product, verzoek ik u om een passende oplossing.
            Ik zou graag willen weten of het mogelijk is om het apparaat te laten repareren, vervangen of, indien nodig, een terugbetaling te ontvangen.
            Ik verzoek u vriendelijk om binnen 14 dagen te reageren met een voorstel voor een oplossing.
            Als bijlage vindt u een kopie van mijn aankoopbewijs ter verificatie.
            Bij voorbaat dank voor uw medewerking. 
            Ik zie uw reactie met belangstelling tegemoet.
            
            Met vriendelijke groet,
            Peter Jansen
            Dorpsstraat 10, 5678 CD Utrecht
            06-87654321
            peter.jansen@email.com
            
            ---
            
            Jan de Vries
            Hoofdstraat 25
            1234 AB Amsterdam
            
            PostCode = 1200 AA
            PostCode = 1200 aa
            
            Date1: January 04, 2025 at 06:38 PM
            Date2: 30 maart 2025 11:12:13
            Date3: 30 maart 2025 om 11:12:13
            
            John Smith (john@test.com) lives in 127.0.0.1 or FE80::0202:B3FF:FFFF:8329 at postcode: 9000 AB / Zip code: 10023 and his drivers license is AC432223.
            
            BSN = 123456782
            
            And for Jane it's AC439999.
            
            UTC = 2020-07-30T18:00:00.000Z
            UTC = 2020-07-30T14:00:00.000-04
            """;

        var supportedEntities = await analyzerService.GetSupportedEntitiesAsync("nl", cancellationToken);
        logger.LogWarning("SupportedEntities : {items}", string.Join(',', supportedEntities));

        // Step 1: Analyze text for PII
        var analyzeRequest = new AnalyzeRequest
        {
            Text = text,
            Language = "nl",
            CorrelationId = Guid.NewGuid().ToString(),
            ReturnDecisionProcess = true,
            AdHocRecognizers =
            [
                new PatternRecognizer
                {
                    Name = "US zip code recognizer",
                    SupportedEntity = "US_ZIP",
                    SupportedLanguage = "en",
                    Patterns =
                    [
                        new Pattern
                        {
                            Name = "zip code (weak)",
                            Regex = @"(\b\d{5}(?:\-\d{4})?\b)",
                            Score = 0.5
                        }
                    ],
                    Context = [ "zip", "code" ]
                },
                AdditionalPatternRecognizers.DutchDate,
                AdditionalPatternRecognizers.DutchPostCode,
                AdditionalPatternRecognizers.DutchBSN,
                AdditionalPatternRecognizers.DutchStreet
            ]
        };
        analyzeRequest.AdHocRecognizers.AddDateTimeISO8601();

        var analysisResults = await analyzerService.AnalyzeAsync(analyzeRequest, cancellationToken);

        var sortedPersonResults = analysisResults
            .Where(r => r.EntityType == PIIEntityTypes.PERSON)
            .OrderByDescending(r => r.Start)
            .ToArray();

        // Step 2a: Replace in original text
        var modified = text;
        var map = new Dictionary<string, string>();
        foreach (var r in sortedPersonResults)
        {
            var replacement = $"`{Guid.NewGuid()}`";
            var originalValue = text.Substring(r.Start, r.Length);

            map.Add(replacement, originalValue);
            modified = modified.Substring(0, r.Start) + replacement + modified.Substring(r.End);
        }

        logger.LogWarning("Modified text: {Text}", modified);

        // Step 2b: Reverse
        var reversed = modified;
        foreach (var (key, value) in map)
        {
            reversed = reversed.Replace(key, value);
        }
        logger.LogWarning("Reversed text: {Text}", reversed);


        // Step 3: Anonymize the detected PII using service
        var anonymizeRequest = new AnonymizeRequest
        {
            Text = text,
            Anonymizers = new Dictionary<string, IAnonymizer>
            {
                [PIIEntityTypes.EMAIL_ADDRESS] = new Encrypt { Key = "3t6w9z$C.F)J@NcR" },
                [PIIEntityTypes.US_DRIVER_LICENSE] = new Mask { MaskingChar = "*", CharsToMask = 4, FromEnd = true }
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