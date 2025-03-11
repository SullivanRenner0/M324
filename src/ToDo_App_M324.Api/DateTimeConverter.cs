
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ToDo_App_M324.Api;

internal class DateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
            throw new JsonException();

        var prefferedFormats = new[] { "dd.MM.yyyy HH:mm:ss zzz", "dd.MM.yyyy HH:mm:ss zz" };
        var text = reader.GetString();
        if (DateTime.TryParseExact(text, prefferedFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var d))
            return d;

        if (DateTime.TryParseExact(text, "dd.MM.yyyy HH:mm:ss", CultureInfo.CurrentCulture, DateTimeStyles.None, out d))
            return d;

        if (DateTime.TryParse(text, out d))
            return d;

        throw new JsonException();
    }
    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("dd.MM.yyyy HH:mm:ss zzz"));
    }
}