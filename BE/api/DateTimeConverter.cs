using System.Text.Json;
using System.Text.Json.Serialization;

public class DateTimeConverter : JsonConverter<DateTime>
{
    private readonly string format = "dd/MM/yyyy HH:mm:ss";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (DateTime.TryParseExact(reader.GetString(), format, null, System.Globalization.DateTimeStyles.None, out DateTime date))
        {
            return date;
        }
        throw new JsonException($"Định dạng ngày giờ không hợp lệ. Vui lòng sử dụng {format} (ví dụ: 28/09/2024 14:30:45).");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(format));
    }
}