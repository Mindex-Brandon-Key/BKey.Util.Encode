using System.Collections.Generic;

namespace BKey.Util.Encode.Encodings;
public interface IEncoderFactory
{
    IEncoder? CreateEncoder(string encodingType);
    IEnumerable<string> ListEncoders();
}