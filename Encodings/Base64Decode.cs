using System;
using System.Text;

namespace BKey.Util.Encode.Encodings;

[Encoder("Base64Decode")]
public class Base64Decode : IEncoder
{
    public string Process(string input)
    {
        byte[] data = Convert.FromBase64String(input);
        return Encoding.UTF8.GetString(data);
    }
}

