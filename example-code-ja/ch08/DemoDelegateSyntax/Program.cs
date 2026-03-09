// デモ: デリゲートとイベントの基本構文

// 1. カスタムデリゲート型
Console.WriteLine("1. カスタムデリゲート型");
Console.WriteLine(new string('-', 40));

Calculate add = (x, y) => x + y;
Calculate multiply = (x, y) => x * y;

Console.WriteLine($"add(10, 5) = {add(10, 5)}");
Console.WriteLine($"multiply(10, 5) = {multiply(10, 5)}");

// 2. カスタムデリゲート実例（Logger）
Console.WriteLine("\n2. カスタムデリゲート実例（Logger）");
Console.WriteLine(new string('-', 40));

var logger = new Logger();

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

logger.Log("アプリケーション開始", LogLevel.Info);
logger.Log("メモリ使用量が高い", LogLevel.Warning);

// 3. Predicate<T> を使った検索
Console.WriteLine("\n3. Predicate<T> を使った検索");
Console.WriteLine(new string('-', 40));

var fruits = new StringList();
fruits.Add("Apple");
fruits.Add("Mango");
fruits.Add("Banana");

string? result = fruits.Find(s => s.EndsWith("go"));
Console.WriteLine($"'go' で終わる果物: {result}");

result = fruits.Find(s => s.Contains('a'));
Console.WriteLine($"'a' を含む果物: {result}");

Console.ReadKey();

// ============================================================
// デリゲート型宣言
// ============================================================

delegate int Calculate(int x, int y);
public delegate void LogHandler(string message, LogLevel level);

// ============================================================
// ヘルパークラス
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
