using System.Threading.Tasks;

namespace BKey.Util.Encode.Sources;
internal interface ISource
{
    Task<string> Read();
}
