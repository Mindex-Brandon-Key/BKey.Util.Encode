using System.IO;
using System.Threading.Tasks;

namespace BKey.Util.Encode.Sources;
public class FileSource : ISource
{
    private readonly string _filePath;

    public FileSource(string filePath)
    {
        _filePath = filePath;
    }

    public async Task<string> Read()
    {
        return await File.ReadAllTextAsync(_filePath);
    }
}
