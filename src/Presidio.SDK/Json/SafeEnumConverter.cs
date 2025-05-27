using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Presidio.Json;

/// <summary>
/// A generic JSON converter for enums that falls back to an UNKNOWN value when conversion fails.
/// This converter attempts to parse string values to enum values, and if parsing fails,
/// it returns the UNKNOWN enum value instead of throwing an exception.
/// </summary>
/// <typeparam name="TEnum">The enum type to convert</typeparam>
internal class SafeEnumConverter<TEnum> : StringEnumConverter
    where TEnum : struct, Enum
{
    private readonly TEnum _unknownValue;

    /// <summary>
    /// Initializes a new instance of the SafeEnumConverter class.
    /// Assumes the enum has an UNKNOWN value.
    /// </summary>
    public SafeEnumConverter()
    {
        // Try to get the UNKNOWN value from the enum
        if (Enum.TryParse<TEnum>("UNKNOWN", true, out var unknownValue))
        {
            _unknownValue = unknownValue;
        }
        else
        {
            // If no UNKNOWN value exists, use the first enum value as fallback
            var values = Enum.GetValues(typeof(TEnum)).OfType<TEnum>().ToArray();
            _unknownValue = values.Length > 0 ? values[0] : default;
        }
    }

    /// <summary>
    /// Initializes a new instance of the SafeEnumConverter class with a specific unknown value.
    /// </summary>
    /// <param name="unknownValue">The enum value to use when conversion fails</param>
    public SafeEnumConverter(TEnum unknownValue)
    {
        _unknownValue = unknownValue;
    }

    /// <summary>
    /// Reads the JSON representation of the object.
    /// </summary>
    /// <param name="reader">The JsonReader to read from</param>
    /// <param name="objectType">Type of the object</param>
    /// <param name="existingValue">The existing value of object being read</param>
    /// <param name="serializer">The calling serializer</param>
    /// <returns>The object value</returns>
    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        try
        {
            // Handle null values
            if (reader.TokenType == JsonToken.Null)
            {
                if (IsNullableType(objectType))
                {
                    return null;
                }

                return _unknownValue;
            }

            // Handle string values
            if (reader.TokenType == JsonToken.String)
            {
                var enumString = reader.Value?.ToString();

                if (string.IsNullOrWhiteSpace(enumString))
                {
                    return _unknownValue;
                }

                // Try to parse the string to enum (case-insensitive)
                if (Enum.TryParse<TEnum>(enumString, true, out var result))
                {
                    return result;
                }

                // If parsing fails, return the unknown value
                return _unknownValue;
            }

            // Handle integer values
            if (reader.TokenType == JsonToken.Integer)
            {
                var intValue = Convert.ToInt32(reader.Value);
                if (Enum.IsDefined(typeof(TEnum), intValue))
                {
                    return Enum.ToObject(typeof(TEnum), intValue);
                }

                return _unknownValue;
            }

            // For any other token type, return unknown value
            return _unknownValue;
        }
        catch
        {
            // If any exception occurs during conversion, return the unknown value
            return _unknownValue;
        }
    }

    /// <summary>
    /// Writes the JSON representation of the object.
    /// </summary>
    /// <param name="writer">The JsonWriter to write to</param>
    /// <param name="value">The value to write</param>
    /// <param name="serializer">The calling serializer</param>
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        // Use the base StringEnumConverter behavior for writing
        base.WriteJson(writer, value, serializer);
    }

    /// <summary>
    /// Determines whether this instance can convert the specified object type.
    /// </summary>
    /// <param name="objectType">Type of the object</param>
    /// <returns>true if this instance can convert the specified object type; otherwise, false</returns>
    public override bool CanConvert(Type objectType)
    {
        Type actualType = IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType;
        return actualType != null && actualType.IsEnum && actualType == typeof(TEnum);
    }

    /// <summary>
    /// Determines whether the specified type is a nullable type.
    /// </summary>
    /// <param name="type">The type to check</param>
    /// <returns>true if the type is nullable; otherwise, false</returns>
    private static bool IsNullableType(Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
    }
}