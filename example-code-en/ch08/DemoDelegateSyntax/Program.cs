// Demo: Basic Syntax of Delegates and Events

// 1. Custom Delegate Type
Console.WriteLine("1. Custom Delegate Type");
Console.WriteLine(new string('-', 40));

// Using delegate (Calculate delegate type is defined at the bottom of the file)
Calculate add = (x, y) => x + y;
Calculate multiply = (x, y) => x * y;

Console.WriteLine($"add(10, 5) = {add(10, 5)}");
Console.WriteLine($"multiply(10, 5) = {multiply(10, 5)}");

// 2. Custom Delegate Instance (Logger)
Console.WriteLine("\n2. Custom Delegate Instance (Logger)");
Console.WriteLine(new string('-', 40));

var logger = new Logger();

// Set the log handler
logger.SetLogHandler((message, level) =>
{
    var color = level switch
    {
        LogLevel.Error => ConsoleColor.Red,
        LogLevel.Warning => ConsoleColor.Yellow,
        _ => ConsoleColor.White
    };
    
    Console.ForegroundColor = color;
    Console.WriteLine($"[{level}] {message}");
    Console.ResetColor();
});

logger.Log("Application started", LogLevel.Info);
logger.Log("High memory usage detected", LogLevel.Warning);

// 3. Using Predicate<T> to Implement Search Functionality
Console.WriteLine("\n3. Using Predicate<T> to Implement Search");
Console.WriteLine(new string('-', 40));

var fruits = new StringList();
fruits.Add("Apple");
fruits.Add("Mango");
fruits.Add("Banana");

// Find a string ending with "go"
string? result = fruits.Find(s => s.EndsWith("go"));
Console.WriteLine($"Fruit ending with 'go': {result}");

// Find a string containing 'a'
result = fruits.Find(s => s.Contains('a'));
Console.WriteLine($"Fruit containing 'a': {result}");

Console.ReadKey();

// ============================================================
// Delegate Type Declarations
// ============================================================

delegate int Calculate(int x, int y);
public delegate void LogHandler(string message, LogLevel level);

// ============================================================
// Helper Classes
// ============================================================

public class StringList
{
    private readonly List<string> _items = new();

    public void Add(string item) => _items.Add(item);

    public string? Find(Predicate<string> match)
    {
        foreach (var item in _items)
        {
            if (match(item))
                return item;
        }
        return null;
    }
}

public enum LogLevel { Info, Warning, Error }

public class Logger
{
    private LogHandler? _logHandler;
    
    public void SetLogHandler(LogHandler handler)
    {
        _logHandler = handler;
    }
    
    public void Log(string message, LogLevel level)
    {
        _logHandler?.Invoke(message, level);
    }
}
