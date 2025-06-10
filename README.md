# Presidio SDK
Unofficial [RestEase](https://github.com/canton7/RestEase) C# Client for [Microsoft Presidio](https://microsoft.github.io/presidio/).

## 📦 Presidio.SDK
[![NuGet Badge](https://img.shields.io/nuget/v/Presidio.SDK)](https://www.nuget.org/packages/Presidio.SDK)<br>

## 📦 Presidio.SDK.Extensions
[![NuGet Badge Extensions](https://img.shields.io/nuget/v/Presidio.SDK.Extensions)](https://www.nuget.org/packages/Presidio.SDK.Extensions)

### Extensions

#### `nl` language 
- NlPostCodeRecognizer (*example: 1000 AA*)
- NlStreetRecognizer (*example: Hoofdstraat 12a*)
- NlDateRecognizer (*example: 25 maart 2024*)
- NlBSNRecognizer (*example: 123456782*)


## ⭐ Usage

### Register

``` c#
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile("appsettings.Development.json", optional: true)
    .Build();

services.AddPresidioSDK(configuration);

var serviceProvider = services.BuildServiceProvider();
```

### Use Analyzer

``` c#
IPresidioAnalyzer analyzerService = serviceProvider.GetRequiredService<IPresidioAnalyzer>();

var text =
    """
    Date1: January 04, 2025 at 06:38 PM
    John Smith (john@test.com) lives in 127.0.0.1 and his drivers license is AC432223.
    And for Jane it's AC439999
    """;

       
// Analyze text for PII
var analyzeRequest = new AnalyzeRequest
{
    Text = text,
    Language = "en",
    CorrelationId = Guid.NewGuid().ToString()
};

var analysisResults = await analyzerService.AnalyzeAsync(analyzeRequest, cancellationToken);
```
