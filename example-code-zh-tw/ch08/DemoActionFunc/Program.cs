// 示範 Action<T> 與 Func<T, TResult>

Console.WriteLine("=== Action 與 Func 範例 ===\n");

// --------------------------------------------------------------
// 1. Action：無回傳值的委派
// --------------------------------------------------------------
Console.WriteLine("1. Action：無回傳值的委派");
Console.WriteLine(new string('-', 40));

// 無參數
Action greet = () => Console.WriteLine("Hello!");
greet();

// 一個參數
Action<string> sayHello = name => Console.WriteLine($"Hello, {name}!");
sayHello("Alice");

// 兩個參數
Action<string, int> repeat = (text, count) =>
{
    for (int i = 0; i < count; i++)
        Console.WriteLine($"  {text}");
};
repeat("Hi", 3);

// --------------------------------------------------------------
// 2. Func：有回傳值的委派
// --------------------------------------------------------------
Console.WriteLine("\n2. Func：有回傳值的委派");
Console.WriteLine(new string('-', 40));

// 無參數，傳回 int
Func<int> getRandomNumber = () => Random.Shared.Next(1, 100);
Console.WriteLine($"隨機數：{getRandomNumber()}");

// 一個參數，傳回 bool
Func<string, bool> isEmpty = s => string.IsNullOrEmpty(s);
Console.WriteLine($"空字串檢查：isEmpty(\"\") = {isEmpty("")}");

// 兩個參數，傳回 int
Func<int, int, int> add = (x, y) => x + y;
Console.WriteLine($"add(10, 20) = {add(10, 20)}");

// 三個參數，傳回 string
Func<string, string, string, string> joinWithSeparator =
    (s1, s2, separator) => $"{s1}{separator}{s2}";
Console.WriteLine($"joinWithSeparator: {joinWithSeparator("Hello", "World", " - ")}");

// --------------------------------------------------------------
// 3. 配置驅動的行為
// --------------------------------------------------------------
Console.WriteLine("\n3. 配置驅動的行為");
Console.WriteLine(new string('-', 40));

var items = new[] { "apple", "BANANA", "Cherry", "date" };
Console.WriteLine($"測試資料：{string.Join(", ", items)}");

// 配置 1：處理大寫字串
var processor1 = new DataProcessor(
    shouldProcess: s => s.Any(char.IsUpper),
    onProcessed: s => Console.WriteLine($"  處理大寫項目: {s}")
);
Console.WriteLine("\n處理包含大寫的項目：");
processor1.Process(items);

// 配置 2：處理長度大於 5 的字串
var processor2 = new DataProcessor(
    shouldProcess: s => s.Length > 5,
    onProcessed: s => Console.WriteLine($"  處理長項目: {s}")
);
Console.WriteLine("\n處理長度 > 5 的項目：");
processor2.Process(items);

// --------------------------------------------------------------
// 4. 泛型委派的協變與逆變
// --------------------------------------------------------------
Console.WriteLine("\n4. 泛型委派的協變與逆變");
Console.WriteLine(new string('-', 40));

// 協變範例（Func 的 TResult 是 out）
Func<string> getString = () => "hello";
Func<object> getObject = getString;  // 合法：string 可轉為 object
Console.WriteLine($"協變：getObject() = {getObject()}");

// 逆變範例（Action 的 T 是 in）
Action<object> actOnObject = o => Console.WriteLine($"  處理：{o}");
Action<string> actOnString = actOnObject;  // 合法：可以用更寬鬆的處理器
actOnString("測試逆變");

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 輔助類別
// ============================================================

public class DataProcessor
{
    private readonly Func<string, bool> _shouldProcess;
    private readonly Action<string> _onProcessed;

    public DataProcessor(Func<string, bool> shouldProcess, Action<string> onProcessed)
    {
        _shouldProcess = shouldProcess;
        _onProcessed = onProcessed;
    }

    public void Process(IEnumerable<string> items)
    {
        foreach (var item in items)
        {
            if (_shouldProcess(item))
            {
                _onProcessed(item);
            }
        }
    }
}
