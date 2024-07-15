using System.IO;

namespace BKey.Util.Encode.Sources;
public class FileSource : ISource
{
    private readonly string _filePath;

    public FileSource(string filePath)
    {
        _filePath = filePath;
    }

    public string Read()
    {
        return File.ReadAllText(_filePath);
    }
}
