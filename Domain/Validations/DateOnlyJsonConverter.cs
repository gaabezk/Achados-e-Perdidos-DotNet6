using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Domain.Validations;

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private const string Format = "yyyy-MM-dd";

    public override DateOnly Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        // if (reader.GetString().Equals(""))
        //     return DateOnly.Parse("01/01/0001");

        if (DateOnly.TryParseExact(
                reader.GetString(),
                Format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var date))
            return date;
        throw new JsonException("A data deve estar no seguinte formato: yyyy-MM-dd Ex: 2023-12-31");
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
    }
}