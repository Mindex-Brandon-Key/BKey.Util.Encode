using BKey.Util.Encode.Encodings;
using Xunit;

namespace BKey.Util.Encode.Tests.Encodings;
public class JsonEncodeTests
{
    [Fact]
    public void JsonEncode_ShouldConvertStringToJson()
    {
        // Arrange
        var encoder = new JsonEncode();
        string input = "test";
        string expectedOutput = "\"test\"";

        // Act
        string actualOutput = encoder.Process(input);

        // Assert
        Assert.Equal(expectedOutput, actualOutput);
    }
}
