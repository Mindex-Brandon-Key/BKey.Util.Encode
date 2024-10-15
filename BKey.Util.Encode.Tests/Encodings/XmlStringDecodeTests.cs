using BKey.Util.Encode.Encodings;
using Xunit;

namespace BKey.Util.Encode.Tests.Encodings;
public class XmlStringDecodeTests
{
    [Fact]
    public void Process_ValidEncodedString_ShouldReturnDecodedString()
    {
        // Arrange
        var decoder = new XmlStringDecode();
        string input = "&lt;tag&gt;value &amp; &apos;other&apos; &quot;quoted&quot; value&lt;/tag&gt;";
        string expected = "<tag>value & 'other' \"quoted\" value</tag>";

        // Act
        string result = decoder.Process(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Process_NullInput_ShouldThrowArgumentNullException()
    {
        // Arrange
        var decoder = new XmlStringDecode();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => decoder.Process(null));
    }
}
