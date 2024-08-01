using System.Collections.Generic;

namespace BKey.Util.Encode.Encodings;
internal interface IEncoderFactory
{
    IEncoder? CreateEncoder(string encodingType);
    IEnumerable<string> ListEncoders();
}