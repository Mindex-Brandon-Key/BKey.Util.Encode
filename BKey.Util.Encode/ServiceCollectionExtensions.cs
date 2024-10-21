using BKey.Util.Encode.Encodings;
using Microsoft.Extensions.DependencyInjection;

namespace BKey.Util.Encode;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEncodings(this IServiceCollection services)
    {
        services.AddSingleton<IEncoderFactory, EncoderFactory>();

        return services;
    }
}
