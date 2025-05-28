// ReSharper disable InconsistentNaming

using Newtonsoft.Json;
using Presidio.Json;

namespace Presidio.Enums;

/// <summary>
/// Defines the set of operations that can be performed on sensitive or secure data.
/// </summary>
/// <remarks>
/// This enumeration represents common data processing operations such as encryption, hashing, and masking.
/// It is typically used to specify the desired operation in data security workflows.
/// </remarks>
[JsonConverter(typeof(SafeEnumConverter<Operators>))]
public enum Operators
{
    Unknown,

    decrypt,

    encrypt,

    hash,

    mask,

    pattern,

    redact,

    replace
}