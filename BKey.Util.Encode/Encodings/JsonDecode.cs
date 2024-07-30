using System.Text;

namespace BKey.Util.Encode.Encodings;
[Encoder("JsonDecode")]
public class JsonDecode : IEncoder
{
    public string Process(string input)
    {
        return System.Text.Json.JsonSerializer.Deserialize<string>(input);
    }
}
