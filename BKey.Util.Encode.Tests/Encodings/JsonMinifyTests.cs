using BKey.Util.Encode.Encodings;
using Xunit;

namespace BKey.Util.Encode.Tests.Encodings;
public class JsonMinifyTests
{
    [Fact]
    public void Minify_ShouldRemoveWhitespace()
    {
        // Arrange
        var encoder = new JsonMinify();
        string input = "{\n  \"key\": \"value\"\n}";
        string expectedOutput = "{\"key\":\"value\"}";

        // Act
        string actualOutput = encoder.Process(input);

        // Assert
        Assert.Equal(expectedOutput, actualOutput);
    }

    [Fact]
    public void Minify_ShouldHandleWindowsNewlines()
    {
        // Arrange
        var encoder = new JsonMinify();
        string input = "{\r\n  \"key\": \"value\"\r\n}";
        string expectedOutput = "{\"key\":\"value\"}";

        // Act
        string actualOutput = encoder.Process(input);

        // Assert
        Assert.Equal(expectedOutput, actualOutput);
    }
}
