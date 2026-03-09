// デモ: Action<T> と Func<T, TResult>

Console.WriteLine("=== Action と Func の例 ===\n");

// --------------------------------------------------------------
// 1. Action: 戻り値なしデリゲート
// --------------------------------------------------------------
Console.WriteLine("1. Action: 戻り値なしデリゲート");
Console.WriteLine(new string('-', 40));

// 引数なし
Action greet = () => Console.WriteLine("Hello!");
greet();

// 引数1つ
Action<string> sayHello = name => Console.WriteLine($"Hello, {name}!");
sayHello("Alice");

// 引数2つ
Action<string, int> repeat = (text, count) =>
{
    for (int i = 0; i < count; i++)
        Console.WriteLine($"  {text}");
};
repeat("Hi", 3);

// --------------------------------------------------------------
// 2. Func: 戻り値ありデリゲート
// --------------------------------------------------------------
Console.WriteLine("\n2. Func: 戻り値ありデリゲート");
Console.WriteLine(new string('-', 40));

Func<int> getRandomNumber = () => Random.Shared.Next(1, 100);
Console.WriteLine($"乱数: {getRandomNumber()}");

Func<string, bool> isEmpty = s => string.IsNullOrEmpty(s);
Console.WriteLine($"空文字判定: isEmpty(\"\") = {isEmpty("")}");

Func<int, int, int> add = (x, y) => x + y;
Console.WriteLine($"add(10, 20) = {add(10, 20)}");

Func<string, string, string, string> joinWithSeparator =
    (s1, s2, separator) => $"{s1}{separator}{s2}";
Console.WriteLine($"joinWithSeparator: {joinWithSeparator("Hello", "World", " - ")}");

// --------------------------------------------------------------
// 3. 設定駆動の振る舞い
// --------------------------------------------------------------
Console.WriteLine("\n3. 設定駆動の振る舞い");
Console.WriteLine(new string('-', 40));

var items = new[] { "apple", "BANANA", "Cherry", "date" };
Console.WriteLine($"テストデータ: {string.Join(", ", items)}");

var processor1 = new DataProcessor(
    shouldProcess: s => s.Any(char.IsUpper),
    onProcessed: s => Console.WriteLine($"  大文字を含む項目を処理: {s}")
);
Console.WriteLine("\n大文字を含む項目を処理:");
processor1.Process(items);

var processor2 = new DataProcessor(
    shouldProcess: s => s.Length > 5,
    onProcessed: s => Console.WriteLine($"  長い項目を処理: {s}")
);
Console.WriteLine("\n長さ > 5 の項目を処理:");
processor2.Process(items);

// --------------------------------------------------------------
// 4. ジェネリックデリゲートの共変・反変
// --------------------------------------------------------------
Console.WriteLine("\n4. ジェネリックデリゲートの共変・反変");
Console.WriteLine(new string('-', 40));

Func<string> getString = () => "hello";
Func<object> getObject = getString;
Console.WriteLine($"共変: getObject() = {getObject()}");

Action<object> actOnObject = o => Console.WriteLine($"  処理: {o}");
Action<string> actOnString = actOnObject;
actOnString("反変のテスト");

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// ヘルパークラス
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
