using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace BKey.Util.Encode.Encodings;

[Encoder("JwtDecode")]
public class JwtDecode : IEncoder
{
    private readonly IEncoder _base64UrlDecoder;
    private readonly IEncoder _jsonPrettifier;

    public JwtDecode(IEncoder base64UrlDecoder, IEncoder jsonPrettifier)
    {
        _base64UrlDecoder = base64UrlDecoder;
        _jsonPrettifier = jsonPrettifier;
    }

    public JwtDecode() : this(new Base64UrlDecode(), new JsonPrettify())
    {
    }

    public string Process(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return "Invalid JWT token";
        }

        try
        {
            var parts = input.Split('.');
            if (parts.Length != 3)
            {
                return "Invalid JWT format";
            }

            var decodedHeader = _base64UrlDecoder.Process(parts[0]);
            var decodedPayload = _base64UrlDecoder.Process(parts[1]);
            var signature = parts[2];

            var output = new StringBuilder();

            output.AppendLine(_jsonPrettifier.Process(decodedHeader));
            output.AppendLine(".");

            output.AppendLine(_jsonPrettifier.Process(decodedPayload));
            output.AppendLine(".");

            output.AppendLine(signature);

            return output.ToString();
        }
        catch (Exception ex)
        {
            return $"Error decoding JWT: {ex.Message}";
        }
    }
}

