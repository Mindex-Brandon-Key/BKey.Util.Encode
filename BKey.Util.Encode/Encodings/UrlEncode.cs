using System.Text;
using System.Web;

namespace BKey.Util.Encode.Encodings;

[Encoder("UrlEncode")]
public class UrlEncode : IEncoder
{
    public string Process(string input)
    {
        return HttpUtility.UrlEncode(input);
    }
}

