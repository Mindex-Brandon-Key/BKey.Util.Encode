using BKey.Util.Encode.Encodings;
using Xunit;

namespace BKey.Util.Encode.Tests.Encodings;
public class JsObjectToJsonTests
{
    [Fact]
    public void JsObjectToJson_ShouldConvertJsObjectToJson()
    {
        // Arrange
        var encoder = new JsObjectToJson();
        string input = "{ key: 'value' }";
        string expectedOutput = "{ \"key\": \"value\" }";

        // Act
        string actualOutput = encoder.Process(input);

        // Assert
        Assert.Equal(expectedOutput, actualOutput);
    }

    [Fact]
    public void JsObjectToJson_ShouldHandleMultipleProperties()
    {
        // Arrange
        var encoder = new JsObjectToJson();
        string input = "{ key1: 'value1', key2: 'value2' }";
        string expectedOutput = "{ \"key1\": \"value1\", \"key2\": \"value2\" }";

        // Act
        string actualOutput = encoder.Process(input);

        // Assert
        Assert.Equal(expectedOutput, actualOutput);
    }
}
