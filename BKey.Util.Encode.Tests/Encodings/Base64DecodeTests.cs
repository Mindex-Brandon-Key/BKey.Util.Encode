using BKey.Util.Encode.Encodings;
using Xunit;

namespace BKey.Util.Encode.Tests.Encodings;
public class Base64DecodeTests
{
    [Fact]
    public void Base64Decode_ShouldConvertBase64ToString()
    {
        // Arrange
        var encoder = new Base64Decode();
        string input = "SGVsbG8sIFdvcmxkIQ==";
        string expectedOutput = "Hello, World!";

        // Act
        string actualOutput = encoder.Process(input);

        // Assert
        Assert.Equal(expectedOutput, actualOutput);
    }

    [Fact]
    public void Base64Decode_ShouldHandleEmptyString()
    {
        // Arrange
        var encoder = new Base64Decode();
        string input = "";
        string expectedOutput = "";

        // Act
        string actualOutput = encoder.Process(input);

        // Assert
        Assert.Equal(expectedOutput, actualOutput);
    }

    [Fact]
    public void Base64Decode_ShouldHandleSpecialCharacters()
    {
        // Arrange
        var encoder = new Base64Decode();
        string input = "U3BlY2lhbCBjaGFyczogIUAjJCVeJiooKV8r";
        string expectedOutput = "Special chars: !@#$%^&*()_+";

        // Act
        string actualOutput = encoder.Process(input);

        // Assert
        Assert.Equal(expectedOutput, actualOutput);
    }

    [Fact]
    public void Base64Decode_ShouldThrowFormatExceptionOnInvalidInput()
    {
        // Arrange
        var encoder = new Base64Decode();
        string input = "InvalidBase64";

        // Act & Assert
        Assert.Throws<FormatException>(() => encoder.Process(input));
    }
}