using System;
using System.Text;

namespace BKey.Util.Encode.Encodings;
[Encoder("Base64Encode")]
public class Base64Encode : IEncoder
{
    public string Process(string input)
    {
        var bytes = Encoding.UTF8.GetBytes(input);
        var data = Convert.ToBase64String(bytes);
        return data;
    }
}
