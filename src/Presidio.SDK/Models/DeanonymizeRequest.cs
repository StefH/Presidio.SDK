namespace Presidio.Models;

public class DeanonymizeRequest
{
    public required string Text { get; init; }

    public Dictionary<string, Decrypt>? Deanonymizers { get; set; }

    public required OperatorResult[] AnonymizerResults { get; init; }
}