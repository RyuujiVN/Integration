using System.Text.Json;
using System.Text.Json.Serialization;

public class DateOnlyConverter : JsonConverter<DateTime>
{
    private readonly string format = "dd/MM/yyyy";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (DateTime.TryParseExact(reader.GetString(), format, null, System.Globalization.DateTimeStyles.None, out DateTime date))
        {
            return date;
        }
        throw new JsonException($"Định dạng ngày không hợp lệ. Vui lòng sử dụng {format} (ví dụ: 28/09/2004).");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(format));
    }
}