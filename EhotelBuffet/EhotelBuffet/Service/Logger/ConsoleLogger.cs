namespace EhotelBuffet.Service.Logger;

public class ConsoleLogger : ILogger
{
    public void LogInfo(string message)
    {
        Console.WriteLine($@"INFO:{DateTime.Now} - 
                                                                                                                {message} ");
    }
    
    public void LogError(string message)
    {
        Console.WriteLine($"ERROR: {DateTime.Now} - {message}");
    }
}