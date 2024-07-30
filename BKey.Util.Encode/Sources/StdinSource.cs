using System;
using System.Text;
using System.Threading;

namespace BKey.Util.Encode.Sources;
public class StdinSource : ISource
{
    private static Thread inputThread;
    private static AutoResetEvent getInput, gotInput;
    private static string? input;

    static StdinSource()
    {
        getInput = new AutoResetEvent(false);
        gotInput = new AutoResetEvent(false);
        inputThread = new Thread(Reader);
        inputThread.IsBackground = true;
        inputThread.Start();
    }

    private static void Reader()
    {
        while (true)
        {
            getInput.WaitOne();
            input = Console.ReadLine();
            gotInput.Set();
        }
    }

    public static bool TryReadLine(out string? line, int timeout_ms = 500/*Timeout.Infinite*/)
    {
        getInput.Set();
        bool success = gotInput.WaitOne(timeout_ms);
        if (success)
            line = input;
        else
            line = null;
        return success;
    }

    public static string? ReadLine(int timeout_ms = 500/**Timeout.Infinite**/)
    {
        getInput.Set();
        bool success = gotInput.WaitOne(timeout_ms);
        if (success)
            return input;
        else
            throw new TimeoutException("User did not provide input within the timelimit.");
    }


    public string Read()
    {
        if (Console.IsInputRedirected)
        {
            return Console.In.ReadToEnd();
        }
        else
        {
            var inputBuilder = new StringBuilder();

            if (!TryReadLine(out var line))
            {
                Console.WriteLine("Enter Text:");
                // Wait for initial data
                line = ReadLine(Timeout.Infinite);
            }

            do
            {
                inputBuilder.AppendLine(line);
            } while (TryReadLine(out line));

            Console.WriteLine();

            return inputBuilder.ToString();
        }

    }

}

