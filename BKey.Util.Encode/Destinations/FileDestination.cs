using System.IO;

namespace BKey.Util.Encode.Destinations;
public class FileDestination : IDestination
{
    private readonly string _filePath;

    public FileDestination(string filePath)
    {
        _filePath = filePath;
    }

    public void Write(string content)
    {
        File.WriteAllText(_filePath, content);
    }
}
