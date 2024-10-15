using System;
using System.Security;

namespace BKey.Util.Encode.Encodings;
[Encoder("XmlStringEncode")]
public class XmlStringEncode : IEncoder
{
    public string Process(string input)
    {
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        return SecurityElement.Escape(input);
    }
}
