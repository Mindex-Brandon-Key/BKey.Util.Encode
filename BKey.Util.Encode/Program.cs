using BKey.Util.Encode.Destinations;
using BKey.Util.Encode.Encodings;
using BKey.Util.Encode.Sources;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace BKey.Util.Encode;

class Program
{
    static async Task Main(string[] args)
    {

        var rootCommand = new RootCommand("Encoder application");

        var listCommand = new Command("list", "List all supported encoding types")
            {
                new Option<bool>(
                    new[] { "-l", "--list" },
                    "List all supported encoding types.")
            };
        listCommand.Handler = CommandHandler.Create(ListEncoders);

        var encodeCommand = new Command("encode", "Process input with specified encoders")
            {
                new Option<string>(
                    new[] { "--inputPath", "-i" },
                    "Input file path."),
                new Option<string>(
                    new[] { "--outputPath", "-o" },
                    "Output file path."),
                new Option<string[]>(
                    new[] { "--encodingTypes", "-e" },
                    () => Array.Empty<string>(),
                    "Comma-separated list of encoding types.")
                    .FromAmong(GetAvailableEncoders().Keys.ToArray()),
            };
        encodeCommand.Handler = CommandHandler.Create<string, string, string[]>(RunOptionsAndReturnExitCode);

        rootCommand.AddCommand(listCommand);
        rootCommand.AddCommand(encodeCommand);

        rootCommand.InvokeAsync(args).Wait();
    }

    static void RunOptionsAndReturnExitCode(string inputPath, string outputPath, string[] encodingTypes)
    {
        var availableEncoders = GetAvailableEncoders();

        if (encodingTypes == null || !encodingTypes.Any())
        {
            Console.WriteLine("At least one encoding type is required. Use the 'list' command to see all supported encoding types.");
            return;
        }

        ISource source;
        IDestination destination;
        var encoders = encodingTypes.Select(et => GetEncoder(et, availableEncoders)).ToList();

        if (encoders.Any(e => e == null))
        {
            Console.WriteLine("One or more invalid encoding types specified.");
            return;
        }

        if (!string.IsNullOrEmpty(inputPath) && File.Exists(inputPath))
        {
            source = new FileSource(inputPath);
        }
        else
        {
            source = new StdinSource();
        }

        if (!string.IsNullOrEmpty(outputPath))
        {
            destination = new FileDestination(outputPath);
        }
        else
        {
            destination = new StdoutDestination();
        }

        try
        {
            string input = source.Read();
            string result = input;
            foreach (var encoder in encoders)
            {
                result = encoder.Process(result);
            }
            destination.Write(result);
        }
        catch (FormatException)
        {
            Console.WriteLine("Input is not a valid Base64 string.");
        }
        catch (JsonException)
        {
            Console.WriteLine("Input is not a valid JSON string.");
        }
    }

    static IEncoder GetEncoder(string encodingType, Dictionary<string, Type> availableEncoders)
    {
        if (availableEncoders.TryGetValue(encodingType, out var encoderType))
        {
            return (IEncoder)Activator.CreateInstance(encoderType);
        }
        throw new ArgumentException($"Unsupported encoding type '{encoderType}'", nameof(encoderType));
    }

    static Dictionary<string, Type> GetAvailableEncoders()
    {
        var encoderTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => typeof(IEncoder).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToArray();

        var encoders = new Dictionary<string, Type>();

        foreach (var type in encoderTypes)
        {
            var attribute = type.GetCustomAttribute<EncoderAttribute>();
            var name = attribute != null ? attribute.Name : type.Name;
            encoders[name] = type;
        }

        return encoders;
    }

    static void ListEncoders()
    {
        var availableEncoders = GetAvailableEncoders();

        Console.WriteLine("Supported encoding types:");
        foreach (var encoder in availableEncoders.Keys)
        {
            Console.WriteLine($"  {encoder}");
        }
    }
}