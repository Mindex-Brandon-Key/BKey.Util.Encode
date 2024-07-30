using BKey.Util.Encode.Encodings;
using Xunit;

namespace BKey.Util.Encode.Tests.Encodings;
public class JsonPrettifyTests
{
    [Fact]
    public void Prettify_ShouldAddWhitespace()
    {
        // Arrange
        var encoder = new JsonPrettify();
        string input = "{\"key\":\"value\"}";
        string expectedOutput = "{\n  \"key\": \"value\"\n}";

        // Act
        string actualOutput = encoder.Process(input);

        // Normalize line endings
        actualOutput = actualOutput.Replace("\r\n", "\n");

        // Assert
        Assert.Equal(expectedOutput, actualOutput);
    }
}
