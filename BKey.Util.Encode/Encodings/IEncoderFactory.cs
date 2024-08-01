using System.Collections.Generic;

namespace BKey.Util.Encode.Encodings;
internal interface IEncoderFactory
{
    IEncoder? GetEncoder(string encodingType);
    IEnumerable<string> ListEncoders();
}