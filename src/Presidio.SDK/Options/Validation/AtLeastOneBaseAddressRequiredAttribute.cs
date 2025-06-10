using System.ComponentModel.DataAnnotations;

namespace Presidio.Options.Validation;

[AttributeUsage(AttributeTargets.Class)]
internal class AtLeastOneBaseAddressRequiredAttribute : ValidationAttribute
{
    private static readonly string[] Members = [nameof(PresidioSDKOptions.AnalyzerBaseAddress), nameof(PresidioSDKOptions.AnonymizerBaseAddress)];

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is PresidioSDKOptions options && (options.AnalyzerBaseAddress != null || options.AnonymizerBaseAddress != null))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult($"Either {nameof(PresidioSDKOptions.AnalyzerBaseAddress)} or {nameof(PresidioSDKOptions.AnonymizerBaseAddress)} must be defined.", Members);
    }
}