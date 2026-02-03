// 示範 throw 表達式（C# 7+）

Console.WriteLine("=== Throw 表達式範例 ===\n");

// --------------------------------------------------------------
// 1. 在 null 合併運算子中使用
// --------------------------------------------------------------
Console.WriteLine("1. null 合併運算子中的 throw");
Console.WriteLine(new string('-', 40));

string? nullableInput = "Hello";
string result1 = nullableInput ?? throw new ArgumentNullException(nameof(nullableInput));
Console.WriteLine($"結果：{result1}");

try
{
    string? nullInput = null;
    string result2 = nullInput ?? throw new ArgumentNullException(nameof(nullInput));
}
catch (ArgumentNullException ex)
{
    Console.WriteLine($"捕捉到例外：{ex.ParamName} 為 null");
}

// --------------------------------------------------------------
// 2. 在三元運算子中使用
// --------------------------------------------------------------
Console.WriteLine("\n2. 三元運算子中的 throw");
Console.WriteLine(new string('-', 40));

string ValidateInput(string? input) =>
    input != null ? input.Trim() :
    throw new ArgumentNullException(nameof(input));

Console.WriteLine($"驗證 '  test  '：'{ValidateInput("  test  ")}'");

try
{
    ValidateInput(null);
}
catch (ArgumentNullException)
{
    Console.WriteLine("驗證 null：拋出 ArgumentNullException");
}

// --------------------------------------------------------------
// 3. 在表達式主體成員中使用
// --------------------------------------------------------------
Console.WriteLine("\n3. 表達式主體成員中的 throw");
Console.WriteLine(new string('-', 40));

var calculator = new Calculator();
Console.WriteLine($"10 / 2 = {calculator.Divide(10, 2)}");

try
{
    calculator.Divide(10, 0);
}
catch (DivideByZeroException)
{
    Console.WriteLine("10 / 0：拋出 DivideByZeroException");
}

try
{
    calculator.NotImplementedMethod();
}
catch (NotImplementedException)
{
    Console.WriteLine("呼叫未實作方法：拋出 NotImplementedException");
}

// --------------------------------------------------------------
// 4. 在索引子中使用
// --------------------------------------------------------------
Console.WriteLine("\n4. 索引子中的 throw");
Console.WriteLine(new string('-', 40));

var collection = new SafeCollection<string>(["A", "B", "C"]);
Console.WriteLine($"collection[1] = {collection[1]}");

try
{
    var _ = collection[10];
}
catch (IndexOutOfRangeException ex)
{
    Console.WriteLine($"collection[10]：{ex.Message}");
}

// --------------------------------------------------------------
// 5. 在 switch 表達式中使用
// --------------------------------------------------------------
Console.WriteLine("\n5. switch 表達式中的 throw");
Console.WriteLine(new string('-', 40));

string GetColorName(int colorCode) => colorCode switch
{
    1 => "紅色",
    2 => "綠色",
    3 => "藍色",
    _ => throw new ArgumentOutOfRangeException(nameof(colorCode), $"未知的顏色代碼：{colorCode}")
};

Console.WriteLine($"顏色代碼 1：{GetColorName(1)}");
Console.WriteLine($"顏色代碼 2：{GetColorName(2)}");

try
{
    GetColorName(99);
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"顏色代碼 99：{ex.Message}");
}

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 輔助類別
// ============================================================

public class Calculator
{
    // 使用 throw 表達式進行參數驗證
    public int Divide(int a, int b) =>
        b != 0 ? a / b : throw new DivideByZeroException("除數不能為零");

    // 表示方法尚未實作
    public void NotImplementedMethod() =>
        throw new NotImplementedException("此方法尚未實作");
}

public class SafeCollection<T>
{
    private readonly T[] _items;

    public SafeCollection(T[] items) => _items = items;

    public T this[int index] =>
        index >= 0 && index < _items.Length
            ? _items[index]
            : throw new IndexOutOfRangeException($"索引 {index} 超出範圍 [0, {_items.Length - 1}]");
}
