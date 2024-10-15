using System;

namespace BKey.Util.Encode.Encodings;
[Encoder("XmlStringDecode")]
public class XmlStringDecode : IEncoder
{
    public string Process(string input)
    {
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        return XmlDecode(input);
    }

    private string XmlDecode(string input)
    {
        return input.Replace("&lt;", "<")
                    .Replace("&gt;", ">")
                    .Replace("&amp;", "&")
                    .Replace("&apos;", "'")
                    .Replace("&quot;", "\"");
    }
}