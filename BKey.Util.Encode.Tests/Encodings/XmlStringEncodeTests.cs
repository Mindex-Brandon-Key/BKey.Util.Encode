using BKey.Util.Encode.Encodings;
using Xunit;

namespace BKey.Util.Encode.Tests.Encodings;
public class XmlStringEncodeTests
{
    [Fact]
    public void Process_ValidString_ShouldReturnEncodedString()
    {
        // Arrange
        var encoder = new XmlStringEncode();
        string input = "<tag>value & 'other' \"quoted\" value</tag>";
        string expected = "&lt;tag&gt;value &amp; &apos;other&apos; &quot;quoted&quot; value&lt;/tag&gt;";

        // Act
        string result = encoder.Process(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Process_NullInput_ShouldThrowArgumentNullException()
    {
        // Arrange
        var encoder = new XmlStringEncode();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => encoder.Process(null));
    }
}
