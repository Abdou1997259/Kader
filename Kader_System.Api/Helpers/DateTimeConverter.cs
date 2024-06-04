
using System.Text.Json;

namespace Kader_System.Api.Helpers
{
    public class DateTimeConverter : System.Text.Json.Serialization.JsonConverter<DateTime>
    {
        private readonly string _format;

        public DateTimeConverter(string format)
        {
            _format = format;
        }
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Implement custom deserialization if needed
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            // Implement custom serialization if needed
            writer.WriteStringValue(value.ToString(_format));
        }
    }
 }



