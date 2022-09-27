namespace Bowling.ExternalServices.Interfaces;

public interface IConsoleActions
{
    public void WriteLine(string s);

    public void Write(string s);
    
    public string ReadLine();
}