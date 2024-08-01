using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BKey.Util.Encode.Sources;
public class StdinSource : ISource
{

    static StdinSource()
    {
    }

    public async Task<string> Read()
    {
        // Check if there is already data on the console
        if (Console.IsInputRedirected)
        {
            using (var reader = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding))
            {
                return await reader.ReadToEndAsync();
            }
        }
        else
        {
            Console.WriteLine("Please enter data, then press Ctrl+D to finish:");

            var inputBuilder = new StringBuilder();
            var inputPosition = 0;

            while (true)
            {
                var keyInfo = Console.ReadKey(intercept: true);

                if (keyInfo.Key == ConsoleKey.D && keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control))
                {
                    break;
                }

                switch (keyInfo.Key)
                {
                    case ConsoleKey.Backspace:
                        if (inputPosition > 0)
                        {
                            inputBuilder.Remove(inputPosition - 1, 1);
                            inputPosition--;
                            Console.Write("\b \b");
                        }
                        break;
                    case ConsoleKey.Delete:
                        if (inputPosition < inputBuilder.Length)
                        {
                            inputBuilder.Remove(inputPosition, 1);
                            Console.Write(" \b");
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (inputPosition > 0)
                        {
                            inputPosition--;
                            Console.Write("\b");
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (inputPosition < inputBuilder.Length)
                        {
                            inputPosition++;
                            Console.Write(keyInfo.KeyChar);
                        }
                        break;
                    default:
                        inputBuilder.Insert(inputPosition, keyInfo.KeyChar);
                        inputPosition++;
                        Console.Write(keyInfo.KeyChar);
                        break;
                }
            }

            Console.WriteLine();

            return inputBuilder.ToString();
        }
    }

}

