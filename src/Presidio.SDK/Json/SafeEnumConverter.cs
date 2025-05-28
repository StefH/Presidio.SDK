using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Presidio.Json;

/// <summary>
/// A generic JSON converter for enums that falls back to an UNKNOWN/DEFAULT value when conversion fails.
/// This converter attempts to parse string values to enum values, and if parsing fails,
/// it returns the UNKNOWN/DEFAULT enum value instead of throwing an exception.
/// </summary>
/// <typeparam name="TEnum">The enum type to convert</typeparam>
internal class SafeEnumConverter<TEnum> : StringEnumConverter
    where TEnum : struct, Enum
{
    private readonly TEnum _defaultValue;

    /// <summary>
    /// Initializes a new instance of the SafeEnumConverter class.
    /// Assumes the enum has an DEFAULT value.
    /// </summary>
    public SafeEnumConverter()
    {
        // Try to get the DEFAULT value from the enum
        if (Enum.TryParse<TEnum>("DEFAULT", true, out var defaultValue))
        {
            _defaultValue = defaultValue;
        }
        else if (Enum.TryParse<TEnum>("UNKNOWN", true, out var unknownValue))
        {
            _defaultValue = unknownValue;
        }
        else
        {
            // If no DEFAULT or UNKNOWN value exists, use the first enum value as fallback
            var values = Enum.GetValues(typeof(TEnum)).OfType<TEnum>().ToArray();
            _defaultValue = values.Length > 0 ? values[0] : default;
        }
    }

    /// <summary>
    /// Initializes a new instance of the SafeEnumConverter class with a specific DEFAULT value.
    /// </summary>
    /// <param name="defaultValue">The enum value to use when conversion fails</param>
    public SafeEnumConverter(TEnum defaultValue)
    {
        _defaultValue = defaultValue;
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

                return _defaultValue;
            }

            // Handle string values
            if (reader.TokenType == JsonToken.String)
            {
                var enumString = reader.Value?.ToString();

                if (string.IsNullOrWhiteSpace(enumString))
                {
                    return _defaultValue;
                }

                // Try to parse the string to enum (case-insensitive)
                if (Enum.TryParse<TEnum>(enumString, true, out var result))
                {
                    return result;
                }

                // If parsing fails, return the DEFAULT value
                return _defaultValue;
            }

            // Handle integer values
            if (reader.TokenType == JsonToken.Integer)
            {
                var intValue = Convert.ToInt32(reader.Value);
                if (Enum.IsDefined(typeof(TEnum), intValue))
                {
                    return Enum.ToObject(typeof(TEnum), intValue);
                }

                return _defaultValue;
            }

            // For any other token type, return DEFAULT value
            return _defaultValue;
        }
        catch
        {
            // If any exception occurs during conversion, return the DEFAULT value
            return _defaultValue;
        }
    }

    /// <summary>
    /// Determines whether this instance can convert the specified object type.
    /// </summary>
    /// <param name="objectType">Type of the object</param>
    /// <returns>true if this instance can convert the specified object type; otherwise, false</returns>
    public override bool CanConvert(Type objectType)
    {
        var actualType = IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType;
        return actualType is { IsEnum: true } && actualType == typeof(TEnum);
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