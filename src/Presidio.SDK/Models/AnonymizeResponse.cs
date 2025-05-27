namespace Presidio.Models;

public class AnonymizeResponse
{
    public required string Text { get; init; }

    public required OperatorResult[] Items { get; init; }
}