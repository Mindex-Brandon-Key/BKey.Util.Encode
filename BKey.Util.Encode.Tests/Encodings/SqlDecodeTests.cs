using BKey.Util.Encode.Encodings;
using Xunit;

namespace BKey.Util.Encode.Tests.Encodings;
public class SqlDecodeTests
{
    [Fact]
    public void SqlDecode_ShouldUnescapeSingleQuotes()
    {
        // Arrange
        var encoder = new SqlDecode();
        string input = "O''Reilly";
        string expectedOutput = "O'Reilly";

        // Act
        string actualOutput = encoder.Process(input);

        // Assert
        Assert.Equal(expectedOutput, actualOutput);
    }
}
