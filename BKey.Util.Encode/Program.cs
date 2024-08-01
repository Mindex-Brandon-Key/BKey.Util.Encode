using BKey.Util.Encode.Destinations;
using BKey.Util.Encode.Encodings;
using BKey.Util.Encode.Sources;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BKey.Util.Encode;

class Program
{
    private IEncoderFactory EncoderFactory { get; }

    public Program(IEncoderFactory encoderFactory)
    {
        EncoderFactory = encoderFactory;
    }

    public async Task Run(string[] args)
    {
        var rootCommand = new RootCommand("Encoder application");

        var listCommand = new Command("list-encoders", "List all supported encoding types")
        {
            Handler = CommandHandler.Create(ListEncoders),
        };
        listCommand.AddAlias("list");

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
                    .FromAmong(EncoderFactory.ListEncoders().ToArray()),
            };
        encodeCommand.Handler = CommandHandler.Create<string, string, string[]>(RunOptionsAndReturnExitCode);

        rootCommand.AddCommand(listCommand);
        rootCommand.AddCommand(encodeCommand);

        await rootCommand.InvokeAsync(args);
    }

    static async Task Main(string[] args)
    {

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        // Build the service provider
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Resolve and run the app
        var app = serviceProvider.GetService<Program>();
        await app?.Run(args);

       
    }

    public async Task RunOptionsAndReturnExitCode(string inputPath, string outputPath, string[] encodingTypes)
    {

        if (encodingTypes == null || !encodingTypes.Any())
        {
            Console.WriteLine("At least one encoding type is required. Use the 'list' command to see all supported encoding types.");
            return;
        }

        ISource source;
        IDestination destination;
        var encoders = encodingTypes.Select(et => EncoderFactory.CreateEncoder(et)).ToList();

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
            string input = await source.Read();
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

    private static void ConfigureServices(IServiceCollection services)
    {
        // Register services here
        services.AddSingleton<IEncoderFactory, EncoderFactory>();
        services.AddTransient<Program>();
    }

    public void ListEncoders()
    {
        var availableEncoders = EncoderFactory.ListEncoders();

        Console.WriteLine("Supported encoding types:");
        foreach (var encoder in availableEncoders)
        {
            Console.WriteLine($"  {encoder}");
        }
    }
}