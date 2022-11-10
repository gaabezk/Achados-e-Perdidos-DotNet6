using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace com.achadoseperdidos.Api.Validations;

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private const string Format = "dd/MM/yyyy";

    public override DateOnly Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        if (reader.GetString().Equals(""))
            return DateOnly.Parse("01/01/0001");
        
        if (DateOnly.TryParseExact(
                reader.GetString(),
                Format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None, 
                out DateOnly date))
        {
            return date;
        }
        throw new JsonException("A data deve estar no seguinte formato: dd/MM/yyyy Ex: 27/09/2022");
    }
    
    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
    }
}