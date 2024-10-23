
namespace BKey.Util.Encode.Encodings;

[Encoder("Base64UrlDecode")]
public class Base64UrlDecode : IEncoder
{
    private readonly IEncoder _base64Decoder;
    private readonly IEncoder _urlDecoder;

    public Base64UrlDecode(IEncoder base64Decoder, IEncoder urlDecoder)
    {
        _base64Decoder = base64Decoder;
        _urlDecoder = urlDecoder;
    }

    public Base64UrlDecode() : this (new Base64Decode(), new UrlDecode())
    {
    }

    public string Process(string input)
    {
        // For decoding, we need to reverse the process
        // First, we need to replace URL-safe characters back to Base64 characters
        var urlDecoded = _urlDecoder.Process(input.Replace('-', '+').Replace('_', '/'));

        // Add padding if necessary
        while (urlDecoded.Length % 4 != 0)
        {
            urlDecoded += '=';
        }

        // Then decode using Base64
        return _base64Decoder.Process(urlDecoded);
    }
}
