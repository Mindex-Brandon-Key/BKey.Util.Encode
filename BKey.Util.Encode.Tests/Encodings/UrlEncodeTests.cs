using BKey.Util.Encode.Encodings;
using Xunit;

namespace BKey.Util.Encode.Tests.Encodings;
public class UrlEncodeTests
{
    [Fact]
    public void UrlEncode_ShouldEncodeSpecialCharacters()
    {
        // Arrange
        var encoder = new UrlEncode();
        string input = "hello world";
        string expectedOutput = "hello+world";

        // Act
        string actualOutput = encoder.Process(input);

        // Assert
        Assert.Equal(expectedOutput, actualOutput);
    }
}