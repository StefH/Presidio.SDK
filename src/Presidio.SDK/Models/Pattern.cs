namespace Presidio.Models;
            
/// <summary>
/// Represents a regular expression pattern with a detection confidence score.
/// </summary>
public class Pattern
{
    /// <summary>
    /// Name of regular expression pattern.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Regex pattern string.
    /// </summary>
    public required string Regex { get; init; }

    /// <summary>
    /// Detection confidence of this pattern (0.01 if very noisy, 0.6-1.0 if very specific).
    /// </summary>
    public double Score { get; set; }
}