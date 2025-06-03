using Azure;
using Azure.AI.TextAnalytics;

namespace AzureAIFoundryPIIExample;

class Program
{
    private static async Task RecognizePIIAsync()
    {
        var text =
            """
            Geachte heer/mevrouw,

            Op 10 maart 2024 heb ik een oven gekocht.
            Helaas werkt de oven sinds 25 maart 2024 niet naar behoren, de oven wordt niet warm en geeft een foutmelding op het display.

            Ik zie uw reactie met belangstelling tegemoet.

            Peter Jansen
            Dorpsstraat 10, 5678 CD Utrecht
            peter.jansen@email.com
            """;

        // 0. Define key and endpoints
        var azureKeyCredential = new AzureKeyCredential(Environment.GetEnvironmentVariable("LANGUAGE_KEY")!);
        var languageEndpoint = new Uri("http://localhost:5000");
        var personallyIdentifiableInformationEndpoint = new Uri("http://localhost:5004");

        // 1. Detect the language
        var languageClient = new TextAnalyticsClient(languageEndpoint, azureKeyCredential);
        var lang = (await languageClient.DetectLanguageAsync(text)).Value.Iso6391Name;

        // 2. Analyze and anonymize the PII in the text
        var personallyIdentifiableInformationClient = new TextAnalyticsClient(personallyIdentifiableInformationEndpoint, azureKeyCredential);
        var entities = (await personallyIdentifiableInformationClient.RecognizePiiEntitiesAsync(text, lang)).Value;

        Console.WriteLine($"Redacted Text: {entities.RedactedText}");

        Console.WriteLine($"Recognized {entities.Count} PII entit{(entities.Count > 1 ? "ies" : "y")}:");
        foreach (PiiEntity entity in entities)
        {
            Console.WriteLine($"Text: {entity.Text}, Category: {entity.Category}, SubCategory: {entity.SubCategory}, Confidence score: {entity.ConfidenceScore}");
        }
    }

    static async Task Main(string[] args)
    {
        await RecognizePIIAsync();
    }
}