using BKey.Util.Encode.Encodings;
using Xunit;

namespace BKey.Util.Encode.Tests.Encodings;
public class JsonDecodeTests
{
    [Fact]
    public void JsonDecode_ShouldConvertJsonToString()
    {
        // Arrange
        var encoder = new JsonDecode();
        string input = "\"test\"";
        string expectedOutput = "test";

        // Act
        string actualOutput = encoder.Process(input);

        // Assert
        Assert.Equal(expectedOutput, actualOutput);
    }
}
