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
        var text = "John Smith lives in Paris and his drivers license is AC432223 and for Jane it's AC439999";

        // Step 1: Analyze text for PII
        var analyzeRequest = new AnalyzeRequest
        {
            Text = text,
            Language = "en"
        };

        var analysisResults = await analyzerService.AnalyzeAsync(analyzeRequest);

        var sortedPersonResults = analysisResults
            .Where(r =>r.EntityType == "PERSON")
            .OrderByDescending(r => r.Start)
            .ToArray();

        // Step 2: Replace in original text
        //var personCount = sortedPersonResults.Length;
        //var modified = text;
        //foreach (var r in sortedPersonResults)
        //{
        //    var replacement = $"ANONYMIZED_PERSON_{--personCount}";
        //    modified = modified.Substring(0, r.Start) + replacement + modified.Substring(r.End);
        //}

        //var personCount = sortedPersonResults.Length;
        var modified = text;
        var d = new Dictionary<string, string>();
        foreach (var r in sortedPersonResults)
        {
            var replacement = $"`{Guid.NewGuid()}`";
            var originalValue = text.Substring(r.Start, r.Length);

            d.Add(replacement, originalValue);
            modified = modified.Substring(0, r.Start) + replacement + modified.Substring(r.End);
        }

        logger.LogWarning("Modified text: {Text}", modified);

        // Reverse
        var reversed = modified;
        foreach (var (key, value) in d)
        {
            reversed = reversed.Replace(key, value);
        }
        logger.LogWarning("Reversed text: {Text}", reversed);


        // Step 3: Anonymize the detected PII using interface
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