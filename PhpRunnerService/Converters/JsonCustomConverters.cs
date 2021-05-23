using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PhpRunnerService.Converters
{
    public class JsonCustomConverters
    {
        public class BoolConverter : JsonConverter<bool>
        {
            public override bool HandleNull => true;

            public override bool Read(
                ref Utf8JsonReader reader,
                Type typeToConvert,
                JsonSerializerOptions options)
            {
                return reader.GetString().Equals("1");
            }

            public override void Write(
                Utf8JsonWriter writer,
                bool value,
                JsonSerializerOptions options)
            {
                writer.WriteStringValue(value ? "1" : "0");
            }

            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(bool);
            }
        }


        public class IntConverter : JsonConverter<int>
        {

            //public override bool HandleNull => true;

            public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    string stringValue = reader.GetString();
                    if (int.TryParse(stringValue, out int value))
                    {
                        return value;
                    }
                }
                else if (reader.TokenType == JsonTokenType.Number)
                {
                    return reader.GetInt32();
                }

                throw new System.Text.Json.JsonException();
            }


            public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
            {
                writer.WriteNumberValue(value);
            }

            //public override bool CanConvert(Type objectType)
            //{
            //    return objectType == typeof(int);
            //}
        }
    }
}
