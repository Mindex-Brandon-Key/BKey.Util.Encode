using System.Text;
using System.Web;

namespace BKey.Util.Encode.Encodings;

[Encoder("UrlDecode")]
public class UrlDecode : IEncoder
{
    public string Process(string input)
    {
        return HttpUtility.UrlDecode(input);
    }
}

