namespace Codecool.MarsExploration.MapExplorer.Logger;

public class FileLogger : ILogger
{
    private readonly string _filePath;

    public FileLogger(string filePath)
    {
        _filePath = filePath;
    }

    public void Log(string message)
    {
        using (var writer = new StreamWriter(_filePath, true))
        {
            writer.WriteLine(message);
        }
    }
}