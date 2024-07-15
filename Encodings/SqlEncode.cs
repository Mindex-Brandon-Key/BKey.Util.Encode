using System.Text;

namespace BKey.Util.Encode.Encodings;

[Encoder("SqlEncode")]
public class SqlEncode : IEncoder
{
    public string Process(string input)
    {
        // Escape single quotes by doubling them up for SQL EXEC calls
        return input.Replace("'", "''");
    }
}
