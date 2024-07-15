using CommandLine;

namespace BKey.Util.Encode;
public class Options
{
    [Option('i', "inputPath", Required = false, HelpText = "Input file path.")]
    public string? InputPath { get; set; }

    [Option('o', "outputPath", Required = false, HelpText = "Output file path.")]
    public string? OutputPath { get; set; }

    [Option('e', "encodingType", Required = true, HelpText = "Encoding type.")]
    public string EncodingType { get; set; }
}


