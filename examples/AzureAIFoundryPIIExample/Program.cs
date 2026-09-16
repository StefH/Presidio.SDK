using Azure;
using Azure.AI.TextAnalytics;

namespace AzureAIFoundryPIIExample;

class Program
{
    private static async Task RecognizePIIAsync()
    {
        var text =
            """
            Dear John Doe,
            
            We are reaching out to confirm your current details in our records. Please find the information below:
            
            Email: john.doe@emailprovider.com
            Driver’s License Number: D1234567
            Bank Account Number: 9876543210
            
            Thank you for ensuring your information is up-to-date.
            
            Best regards,
            mstack
            """;

        // 0. Define key and endpoints
        var azureKeyCredential = new AzureKeyCredential(Environment.GetEnvironmentVariable("AZURE_AI_LANGUAGE_KEY")!);
        var languageEndpoint = new Uri("http://localhost:5000");
        var personallyIdentifiableInformationEndpoint = new Uri("http://localhost:5004");

        // 1. Detect the language
        var languageClient = new TextAnalyticsClient(languageEndpoint, azureKeyCredential);
        var lang = (await languageClient.DetectLanguageAsync(text)).Value.Iso6391Name;

        // 2. Analyze and anonymize the PII in the text
        var personallyIdentifiableInformationClient = new TextAnalyticsClient(personallyIdentifiableInformationEndpoint, azureKeyCredential);
        var entities = (await personallyIdentifiableInformationClient.RecognizePiiEntitiesAsync(text, lang)).Value;

        Console.WriteLine($"Redacted Text: {entities.RedactedText}");

        Console.WriteLine($"\r\nRecognized {entities.Count} PII entit{(entities.Count > 1 ? "ies" : "y")}:");
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