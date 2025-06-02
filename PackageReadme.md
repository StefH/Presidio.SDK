## Presidio SDK
Unofficial [RestEase](https://github.com/canton7/RestEase) C# Client for [Microsoft Presidio](https://microsoft.github.io).

### ⭐ Usage

#### Register

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