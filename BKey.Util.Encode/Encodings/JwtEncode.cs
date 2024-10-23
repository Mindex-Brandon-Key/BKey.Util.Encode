using System;
using System.Text;
using System.Text.Json;

namespace BKey.Util.Encode.Encodings;

[Encoder("JwtEncode")]
public class JwtEncode : IEncoder
{
    public string Process(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return "Invalid input for JWT payload";
        }

        try
        {
            // Create a simple header
            var header = new
            {
                alg = "none",
                typ = "JWT"
            };

            // Serialize and encode the header
            string encodedHeader = Base64UrlEncode(JsonSerializer.Serialize(header));

            // Encode the payload (assuming the input is valid JSON)
            string encodedPayload = Base64UrlEncode(input);

            // Combine to form the JWT
            return $"{encodedHeader}.{encodedPayload}.";
        }
        catch (Exception ex)
        {
            return $"Error encoding JWT: {ex.Message}";
        }
    }

    private static string Base64UrlEncode(string input)
    {
        var inputBytes = Encoding.UTF8.GetBytes(input);
        return Convert.ToBase64String(inputBytes)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
    }
}
