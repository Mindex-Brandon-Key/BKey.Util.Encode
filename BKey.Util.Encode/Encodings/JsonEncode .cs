using System.Text;

namespace BKey.Util.Encode.Encodings;
[Encoder("JsonEncode")]
public class JsonEncode : IEncoder
{
    public string Process(string input)
    {
        return System.Text.Json.JsonSerializer.Serialize(input);
    }
}
