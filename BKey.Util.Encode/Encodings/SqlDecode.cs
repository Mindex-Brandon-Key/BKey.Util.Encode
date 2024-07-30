using System.Text;

namespace BKey.Util.Encode.Encodings;

[Encoder("SqlDecode")]
public class SqlDecode : IEncoder
{
    public string Process(string input)
    {
        // Unescape single quotes by reducing double single quotes to one
        return input.Replace("''", "'");
    }
}

