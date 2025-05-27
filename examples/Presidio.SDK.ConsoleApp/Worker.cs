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
        var text = "John Smith drivers license is AC432223 and for Jane it's AC439999";

        // Step 1: Analyze text for PII
        var analyzeRequest = new AnalyzeRequest
        {
            Text = text,
            Language = "en"
        };

        var analysisResults = await analyzerService.AnalyzeAsync(analyzeRequest);

        int x = 0;
        string Get()
        {
            return "ANONYMIZED_PERSON" + x++;
        }

        // Step 2: Anonymize the detected PII
        var anonymizeRequest = new AnonymizeRequest
        {
            Text = text,
            Anonymizers = new Dictionary<string, IAnonymizer>
            {
                ["PERSON"] = new Replace { NewValue = Get() },
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
        Console.WriteLine($"Anonymized text: {anonymizeResponse.Text}");
    }
}