using System.Text;
using System.Text.RegularExpressions;

namespace BKey.Util.Encode.Encodings;
[Encoder("JsObjectToJson")]
public class JsObjectToJson : IEncoder
{
    public string Process(string input)
    {
        // Remove any leading/trailing whitespace
        input = input.Trim();

        // Replace single quotes with double quotes
        string jsonString = Regex.Replace(input, @"'", "\"");

        // Ensure keys are in double quotes
        jsonString = Regex.Replace(jsonString, @"(\w+)\s*:", "\"$1\":");

        return jsonString;
    }
}
