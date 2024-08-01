using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BKey.Util.Encode.Encodings;
internal class EncoderFactory : IEncoderFactory
{
    private Lazy<Dictionary<string, Type>> _availableEncoders = new Lazy<Dictionary<string, Type>>(GetAvailableEncoders);
    private Dictionary<string, Type> AvailableEncoders => _availableEncoders.Value;

    public IEncoder? GetEncoder(string encodingType)
    {
        if (AvailableEncoders.TryGetValue(encodingType, out var encoderType))
        {
            return (IEncoder?)Activator.CreateInstance(encoderType);
        }
        return null;
    }

    public IEnumerable<string> ListEncoders()
    {
        return AvailableEncoders.Keys;
    }

    private static Dictionary<string, Type> GetAvailableEncoders()
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

}
