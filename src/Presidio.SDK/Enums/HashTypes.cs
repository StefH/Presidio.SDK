// ReSharper disable InconsistentNaming

using Newtonsoft.Json;
using Presidio.Json;

namespace Presidio.Enums;

/// <summary>
/// Specifies the types of cryptographic hash algorithms supported. 
/// </summary>
/// <remarks>Use this enumeration to select the desired hash algorithm for cryptographic operations.</remarks>
[JsonConverter(typeof(SafeEnumConverter<HashTypes>))]
public enum HashTypes
{
    sha256,
    
    sha512
}