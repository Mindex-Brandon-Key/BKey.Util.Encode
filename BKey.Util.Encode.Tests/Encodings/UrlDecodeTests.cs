using BKey.Util.Encode.Encodings;
using Xunit;

namespace BKey.Util.Encode.Tests.Encodings;
public class UrlDecodeTests
{
    [Fact]
    public void UrlDecode_ShouldDecodeSpecialCharacters()
    {
        // Arrange
        var encoder = new UrlDecode();
        string input = "hello+world";
        string expectedOutput = "hello world";

        // Act
        string actualOutput = encoder.Process(input);

        // Assert
        Assert.Equal(expectedOutput, actualOutput);
    }
}
