// 示範委派與事件的基本語法

// 1. 自訂委派型別
Console.WriteLine("1. 自訂委派型別");
Console.WriteLine(new string('-', 40));

// 使用委派（Calculate 委派型別定義在檔案底部）
Calculate add = (x, y) => x + y;
Calculate multiply = (x, y) => x * y;

Console.WriteLine($"add(10, 5) = {add(10, 5)}");
Console.WriteLine($"multiply(10, 5) = {multiply(10, 5)}");

// 2. 自訂委派實例 (Logger)
Console.WriteLine("\n2. 自訂委派實例 (Logger)");
Console.WriteLine(new string('-', 40));

var logger = new Logger();

// 設定日誌處理器
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

logger.Log("應用程式啟動", LogLevel.Info);
logger.Log("記憶體使用過高", LogLevel.Warning);

// 3. 使用 Predicate<T> 實現搜尋功能
Console.WriteLine("\n3. 使用 Predicate<T> 實現搜尋");
Console.WriteLine(new string('-', 40));

var fruits = new StringList();
fruits.Add("Apple");
fruits.Add("Mango");
fruits.Add("Banana");

// 尋找以 "go" 結尾的字串
string? result = fruits.Find(s => s.EndsWith("go"));
Console.WriteLine($"以 'go' 結尾的水果：{result}");

// 尋找包含 'a' 的字串
result = fruits.Find(s => s.Contains('a'));
Console.WriteLine($"包含 'a' 的水果：{result}");

Console.ReadKey();

// ============================================================
// 委派型別宣告
// ============================================================

delegate int Calculate(int x, int y);
public delegate void LogHandler(string message, LogLevel level);

// ============================================================
// 輔助類別
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
