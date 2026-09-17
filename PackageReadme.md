## Presidio SDK
Unofficial [RestEase](https://github.com/canton7/RestEase) C# Client for [Microsoft Presidio (dataprivacystack)](https://presidio.dataprivacystack.org/).

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

---

### Sponsors

[Entity Framework Extensions](https://entityframework-extensions.net/?utm_source=StefH) and [Dapper Plus](https://dapper-plus.net/?utm_source=StefH) are major sponsors and proud to contribute to the development of **Presidio.SDK**.

[![Entity Framework Extensions](https://raw.githubusercontent.com/StefH/resources/main/sponsor/entity-framework-extensions-sponsor.png)](https://entityframework-extensions.net/bulk-insert?utm_source=StefH)

[![Dapper Plus](https://raw.githubusercontent.com/StefH/resources/main/sponsor/dapper-plus-sponsor.png)](https://dapper-plus.net/bulk-insert?utm_source=StefH)