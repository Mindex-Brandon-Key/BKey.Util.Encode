
namespace BKey.Util.Encode.Encodings;

[Encoder("Base64UrlEncode")]
public class Base64UrlEncode : IEncoder
{
    private readonly IEncoder _base64Encoder;
    private readonly IEncoder _urlEncoder;

    public Base64UrlEncode(IEncoder base64Encoder, IEncoder urlEncoder)
    {
        _base64Encoder = base64Encoder;
        _urlEncoder = urlEncoder;
    }

    public Base64UrlEncode() : this(new Base64Encode(), new UrlEncode())
    {
    }

    public string Process(string input)
    {
        // For decoding, we need to reverse the process
        // First, we need to replace URL-safe characters back to Base64 characters
        var urlDecoded = input.Replace('-', '+').Replace('_', '/');
        
        // Add padding if necessary
        while (urlDecoded.Length % 4 != 0)
        {
            urlDecoded += '=';
        }

        // Then decode using Base64
        return _base64Encoder.Process(urlDecoded);
    }
}
