namespace ConcurrencyLab.Infrastructure;

public sealed class AppLogger
{
    public void Info(string message)
    {
        var ts = DateTime.Now.ToString("HH:mm:ss.fff");
        var threadId = Environment.CurrentManagedThreadId;
        Console.WriteLine($"[{ts}] [T:{threadId}] {message}");
    }
}
