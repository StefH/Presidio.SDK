namespace Presidio.Models;

public class OperatorResult
{
    public required string Operator { get; set; }

    public required string EntityType { get; set; }

    public int Start { get; set; }

    public int End { get; set; }

    public required string Text { get; set; }
}