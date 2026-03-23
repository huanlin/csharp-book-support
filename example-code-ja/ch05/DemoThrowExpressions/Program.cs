// デモ: throw 式（C# 7+）

Console.WriteLine("=== throw 式の例 ===\n");

// --------------------------------------------------------------
// 1. null 合体演算子で使用
// --------------------------------------------------------------
Console.WriteLine("1. null 合体演算子内の throw");
Console.WriteLine(new string('-', 40));

string? nullableInput = "Hello";
string result1 = nullableInput ?? throw new ArgumentNullException(nameof(nullableInput));
Console.WriteLine($"結果: {result1}");

try
{
    string? nullInput = null;
    string result2 = nullInput ?? throw new ArgumentNullException(nameof(nullInput));
}
catch (ArgumentNullException ex)
{
    Console.WriteLine($"例外を捕捉: {ex.ParamName} が null");
}

// --------------------------------------------------------------
// 2. 三項演算子で使用
// --------------------------------------------------------------
Console.WriteLine("\n2. 三項演算子内の throw");
Console.WriteLine(new string('-', 40));

string ValidateInput(string? input) =>
    input != null ? input.Trim() :
    throw new ArgumentNullException(nameof(input));

Console.WriteLine($"'  test  ' の検証: '{ValidateInput("  test  ")}'");

try
{
    ValidateInput(null);
}
catch (ArgumentNullException)
{
    Console.WriteLine("null 検証: ArgumentNullException を送出");
}

// --------------------------------------------------------------
// 3. 式形式メンバーで使用
// --------------------------------------------------------------
Console.WriteLine("\n3. 式形式メンバー内の throw");
Console.WriteLine(new string('-', 40));

var calculator = new Calculator();
Console.WriteLine($"10 / 2 = {calculator.Divide(10, 2)}");

try
{
    calculator.Divide(10, 0);
}
catch (DivideByZeroException)
{
    Console.WriteLine("10 / 0: DivideByZeroException を送出");
}

try
{
    calculator.NotImplementedMethod();
}
catch (NotImplementedException)
{
    Console.WriteLine("未実装メソッド呼び出し: NotImplementedException を送出");
}

// --------------------------------------------------------------
// 4. インデクサで使用
// --------------------------------------------------------------
Console.WriteLine("\n4. インデクサ内の throw");
Console.WriteLine(new string('-', 40));

var collection = new SafeCollection<string>(["A", "B", "C"]);
Console.WriteLine($"collection[1] = {collection[1]}");

try
{
    var _ = collection[10];
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"collection[10]: {ex.Message}");
}

// --------------------------------------------------------------
// 5. switch 式で使用
// --------------------------------------------------------------
Console.WriteLine("\n5. switch 式内の throw");
Console.WriteLine(new string('-', 40));

string GetColorName(int colorCode) => colorCode switch
{
    1 => "Red",
    2 => "Green",
    3 => "Blue",
    _ => throw new ArgumentOutOfRangeException(nameof(colorCode), $"未知の色コード: {colorCode}")
};

Console.WriteLine($"色コード 1: {GetColorName(1)}");
Console.WriteLine($"色コード 2: {GetColorName(2)}");

try
{
    GetColorName(99);
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"色コード 99: {ex.Message}");
}

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// ヘルパークラス
// ============================================================

public class Calculator
{
    public int Divide(int a, int b) =>
        b != 0 ? a / b : throw new DivideByZeroException("除数は 0 にできません");

    public void NotImplementedMethod() =>
        throw new NotImplementedException("このメソッドは未実装です");
}

public class SafeCollection<T>
{
    private readonly T[] _items;

    public SafeCollection(T[] items) => _items = items;

    public T this[int index] =>
        index >= 0 && index < _items.Length
            ? _items[index]
            : throw new ArgumentOutOfRangeException(
                nameof(index),
                index,
                $"インデックスは 0 から {_items.Length - 1} の範囲である必要があります");
}
