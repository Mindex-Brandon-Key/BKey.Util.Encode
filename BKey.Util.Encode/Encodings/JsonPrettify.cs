using System.Text;
using System.Text.Json;

namespace BKey.Util.Encode.Encodings;

[Encoder("JsonPrettify")]
public class JsonPrettify : IEncoder
{
    public string Process(string input)
    {
        var jsonDocument = JsonDocument.Parse(input);
        return JsonSerializer.Serialize(jsonDocument, new JsonSerializerOptions { WriteIndented = true });
    }
}
