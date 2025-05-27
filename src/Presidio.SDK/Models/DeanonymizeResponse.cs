namespace Presidio.Models;

public class DeanonymizeResponse
{
    public required string Text { get; init; }

    public required OperatorResult[] Items { get; init; }
}