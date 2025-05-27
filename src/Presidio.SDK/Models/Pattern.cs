namespace Presidio.Models;

public class Pattern
{
    public string Name { get; set; } = null!;

    public string Regex { get; set; } = null!;

    public double Score { get; set; }
}