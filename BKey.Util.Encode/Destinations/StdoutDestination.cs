using System;

namespace BKey.Util.Encode.Destinations;
public class StdoutDestination : IDestination
{
    public void Write(string content)
    {
        Console.WriteLine(content);
    }
}

