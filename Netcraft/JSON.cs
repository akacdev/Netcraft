using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;
using Netcraft.Entities;

namespace Netcraft
{
    public class EnumNamingPolicy : JsonNamingPolicy
    {
        public string[] StateNames = Enum.GetNames(typeof(State));
        public string[] FileSortNames = Enum.GetNames(typeof(FileSort));

        public override string ConvertName(string name)
        {
            if (StateNames.Contains(name)) return Extensions.ConvertStateToApi(name);
            else if (FileSortNames.Contains(name)) return Extensions.ConvertFileSortToApi(name);
            else return name.ToSnakeCase();
        }
    }

    public class LongUnixConverter : JsonConverter<DateTime>
    {
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.ToUnixSeconds());
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            return reader.TokenType switch
            {
                JsonTokenType.Number => reader.GetInt64().ToDate(),
                _ => throw new JsonException(),
            };
        }
    }

    public class BoolConverter : JsonConverter<bool>
    {
        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(Convert.ToInt32(value));
        }

        public override bool Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            return reader.TokenType switch
            {
                JsonTokenType.True => true,
                JsonTokenType.False => false,
                JsonTokenType.String => int.Parse(reader.GetString()) == 1,
                JsonTokenType.Number => reader.TryGetInt32(out int l) ? Convert.ToBoolean(l) : reader.TryGetDouble(out double d) && Convert.ToBoolean(d),
                _ => throw new JsonException(),
            };
        }
    }
}