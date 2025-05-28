using Newtonsoft.Json;
using Presidio.Enums;

namespace Presidio.Models;

public class OperatorResult
{
    /// <summary>
    /// Name of the used operator
    /// </summary>
    public required Operators Operator { get; init; }

    /// <summary>
    /// Type of the PII entity
    /// </summary>
    public required PIIEntityTypes EntityType { get; init; }

    /// <summary>
    /// Start index of the changed text
    /// </summary>
    public int Start { get; init; }

    /// <summary>
    /// End index in the changed text
    /// </summary>
    public int End { get; init; }

    /// <summary>
    /// The length of the changed text.
    /// </summary>
    [JsonIgnore]
    public int Length => End - Start;

    /// <summary>
    /// The new text returned
    /// </summary>
    public required string Text { get; init; }
}