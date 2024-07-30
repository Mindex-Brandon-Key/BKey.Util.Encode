using BKey.Util.Encode.Encodings;
using Xunit;

namespace BKey.Util.Encode.Tests.Encodings;
public class Base64EncodeTests
{
    [Fact]
    public void Base64Encode_ShouldConvertStringToBase64()
    {
        // Arrange
        var encoder = new Base64Encode();
        string input = "Hello, World!";
        string expectedOutput = "SGVsbG8sIFdvcmxkIQ==";

        // Act
        string actualOutput = encoder.Process(input);

        // Assert
        Assert.Equal(expectedOutput, actualOutput);
    }

    [Fact]
    public void Base64Encode_ShouldHandleEmptyString()
    {
        // Arrange
        var encoder = new Base64Encode();
        string input = "";
        string expectedOutput = "";

        // Act
        string actualOutput = encoder.Process(input);

        // Assert
        Assert.Equal(expectedOutput, actualOutput);
    }

    [Fact]
    public void Base64Encode_ShouldHandleSpecialCharacters()
    {
        // Arrange
        var encoder = new Base64Encode();
        string input = "Special chars: !@#$%^&*()_+";
        string expectedOutput = "U3BlY2lhbCBjaGFyczogIUAjJCVeJiooKV8r";

        // Act
        string actualOutput = encoder.Process(input);

        // Assert
        Assert.Equal(expectedOutput, actualOutput);
    }
}
