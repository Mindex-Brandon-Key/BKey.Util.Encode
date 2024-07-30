using System.Text;
using System.Text.Json;

namespace BKey.Util.Encode.Encodings;

[Encoder("JsonMinify")]
public class JsonMinify : IEncoder
{
    public string Process(string input)
    {
        var jsonDocument = JsonDocument.Parse(input);
        return JsonSerializer.Serialize(jsonDocument, new JsonSerializerOptions { WriteIndented = false });
    }
}
