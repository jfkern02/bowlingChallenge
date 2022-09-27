using Bowling.ExternalServices.Interfaces;

namespace Bowling.ExternalServices;

public class ConsoleActions : IConsoleActions
{
    public void WriteLine(string s)
    {
        Console.WriteLine(s);
    }

    public void Write(string s)
    {
        Console.Write(s);
    }

    public string ReadLine()
    {
        return Console.ReadLine();
    }
}