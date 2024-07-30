using BKey.Util.Encode.Destinations;
using BKey.Util.Encode.Encodings;
using BKey.Util.Encode.Sources;
using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BKey.Util.Encode;

class Program
{
    static void Main(string[] args)
    {
        var availableEncoders = GetAvailableEncoders();

        Parser.Default.ParseArguments<Options>(args)
              .WithParsed<Options>(opts => RunOptionsAndReturnExitCode(opts, availableEncoders))
              .WithNotParsed<Options>((errs) => HandleParseError(errs, availableEncoders));
    }

    static void RunOptionsAndReturnExitCode(Options opts, Dictionary<string, Type> availableEncoders)
    {
        ISource source;
        IDestination destination;
        IEncoder encoder = GetEncoder(opts.EncodingType, availableEncoders);

        if (opts.ListEncoders || encoder == null)
        {
            Console.WriteLine("Supported encoding types:");
            foreach (var encoderOption in availableEncoders.Keys)
            {
                Console.WriteLine($"  {encoderOption}");
            }
            return;
        }

        if (!string.IsNullOrEmpty(opts.InputPath) && File.Exists(opts.InputPath))
        {
            source = new FileSource(opts.InputPath);
        }
        else
        {
            source = new StdinSource();
        }

        if (!string.IsNullOrEmpty(opts.OutputPath))
        {
            destination = new FileDestination(opts.OutputPath);
        }
        else
        {
            destination = new StdoutDestination();
        }

        string input = source.Read();
        string result = encoder.Process(input);
        destination.Write(result);
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

    static void HandleParseError(IEnumerable<Error> errs, Dictionary<string, Type> availableEncoders)
    {
        Console.WriteLine("Usage:");
        foreach (var encoder in availableEncoders.Keys)
        {
            Console.WriteLine($"  {encoder}");
        }
    }
}