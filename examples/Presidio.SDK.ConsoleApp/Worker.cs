using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Presidio.Enums;
using Presidio.Models;
using Presidio.PatternRecognizers;
using Presidio.Types;

namespace Presidio.SDK.ConsoleApp;

internal class Worker(IPresidioAnalyzer analyzerService, IPresidioAnonymizer anonymizerService, ILogger<Worker> logger)
{
    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        var text =
            """
            John Smith (john@test.com) lives in 127.0.0.1 at postcode: 6100 AA / Zip code: 10023 and his drivers license is AC432223.
            
            BSN = 123456782
            
            And for Jane it's AC439999

            """;

        var supportedEntities = await analyzerService.GetSupportedEntitiesAsync("en", cancellationToken);
        logger.LogWarning("SupportedEntities : {items}", string.Join(',', supportedEntities));

        // Step 1: Analyze text for PII
        var analyzeRequest = new AnalyzeRequest
        {
            Text = text,
            Language = "en",
            CorrelationId = Guid.NewGuid().ToString(),
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
                AdditionalPatternRecognizers.DutchPostCode,
                AdditionalPatternRecognizers.DutchBSN
            ]
        };

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
                // [PIIEntityTypes.PERSON] = new Replace { NewValue = "ANONYMIZED_PERSON" },
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