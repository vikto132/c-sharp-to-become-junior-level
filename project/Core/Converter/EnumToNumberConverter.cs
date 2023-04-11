using System;
using Newtonsoft.Json;

namespace Core.Converter;

public class EnumToNumberConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        writer.WriteValue((int)value);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (existingValue == null) return null;

        if (!int.TryParse(existingValue.ToString(), out var value))
            return Enum.ToObject(objectType, value);

        throw new ArgumentException($"Value {existingValue} cannot be converted to enum value.");
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType.IsEnum;
    }
}