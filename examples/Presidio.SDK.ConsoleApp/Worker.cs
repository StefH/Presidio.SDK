using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Presidio.Models;

namespace Presidio.SDK.ConsoleApp;

internal class Worker(IPresidioAnalyzer analyzerService, IPresidioAnonymizer anonymizerService, ILogger<Worker> logger)
{
    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        const string text = "John Smith lives in Paris and his drivers license is AC432223 and for Jane it's AC439999";

        // Step 1: Analyze text for PII
        var analyzeRequest = new AnalyzeRequest
        {
            Text = text,
            Language = "en"
        };

        var analysisResults = await analyzerService.AnalyzeAsync(analyzeRequest);

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
                ["PERSON"] = new Replace { NewValue = "ANONYMIZED_PERSON" },
                ["US_DRIVER_LICENSE"] = new Mask { MaskingChar = "*", CharsToMask = 4, FromEnd = true }
            },
            AnalyzerResults = analysisResults.Select(r => new RecognizerResult
            {
                Start = r.Start,
                End = r.End,
                Score = r.Score,
                EntityType = r.EntityType
            }).ToArray()
        };

        var anonymizeResponse = await anonymizerService.AnonymizeAsync(anonymizeRequest);

        // Result: anonymized text
        logger.LogWarning("Anonymized text: {Text}", anonymizeResponse.Text);
    }
}