namespace Presidio.Models;

public class Replace : IAnonymizer
{
    public string Type => "replace";

    public required string NewValue { get; init; }
}