using System.ComponentModel.DataAnnotations;
using Presidio.Options;
using Presidio.Options.Validation;

namespace Presidio.SDK.Tests;

public class ValidationAttributeTests
{
    private static readonly AtLeastOneBaseAddressRequiredAttribute Sut = new();

    [Test]
    public async Task Validate_BothAddressesNull_ReturnsValidationError()
    {
        // Arrange
        var options = new PresidioSDKOptions
        {
            AnalyzerBaseAddress = null,
            AnonymizerBaseAddress = null
        };

        var context = new ValidationContext(options);

        // Act
        var result = Sut.GetValidationResult(options, context);

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result!.ErrorMessage).IsEqualTo("Either AnalyzerBaseAddress or AnonymizerBaseAddress must be defined.");
    }

    [Test]
    public async Task Validate_AnalyzerBaseAddressSet_ReturnsSuccess()
    {
        // Arrange
        var options = new PresidioSDKOptions
        {
            AnalyzerBaseAddress = new Uri("https://analyzer"),
            AnonymizerBaseAddress = null
        };

        var context = new ValidationContext(options);

        // Act
        var result = Sut.GetValidationResult(options, context);

        // Assert
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task Validate_AnonymizerBaseAddressSet_ReturnsSuccess()
    {
        // Arrange
        var options = new PresidioSDKOptions
        {
            AnalyzerBaseAddress = null,
            AnonymizerBaseAddress = new Uri("https://anonymizer")
        };

        var context = new ValidationContext(options);

        // Act
        var result = Sut.GetValidationResult(options, context);

        // Assert
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task Validate_BothAddressesSet_ReturnsSuccess()
    {
        // Arrange
        var options = new PresidioSDKOptions
        {
            AnalyzerBaseAddress = new Uri("https://analyzer"),
            AnonymizerBaseAddress = new Uri("https://anonymizer")
        };

        var context = new ValidationContext(options);

        // Act
        var result = Sut.GetValidationResult(options, context);

        // Assert
        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task Validate_NonOptionsType_ReturnsValidationError()
    {
        // Arrange
        var notOptions = new object();
        var context = new ValidationContext(notOptions);

        // Act
        var result = Sut.GetValidationResult(notOptions, context);

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result!.ErrorMessage).IsEqualTo("Either AnalyzerBaseAddress or AnonymizerBaseAddress must be defined.");
    }
}