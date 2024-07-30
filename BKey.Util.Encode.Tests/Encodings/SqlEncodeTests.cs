using BKey.Util.Encode.Encodings;
using Xunit;

namespace BKey.Util.Encode.Tests.Encodings;
public class SqlEncodeTests
{
    [Fact]
    public void SqlEncode_ShouldEscapeSingleQuotes()
    {
        // Arrange
        var encoder = new SqlEncode();
        string input = "O'Reilly";
        string expectedOutput = "O''Reilly";

        // Act
        string actualOutput = encoder.Process(input);

        // Assert
        Assert.Equal(expectedOutput, actualOutput);
    }
}
