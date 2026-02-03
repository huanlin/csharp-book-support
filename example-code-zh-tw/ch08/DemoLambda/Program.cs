// 示範 Lambda 表達式的各種寫法

Console.WriteLine("=== Lambda 表達式範例 ===\n");

// --------------------------------------------------------------
// 1. 運算式 Lambda vs 陳述式 Lambda
// --------------------------------------------------------------
Console.WriteLine("1. 運算式 Lambda vs 陳述式 Lambda");
Console.WriteLine(new string('-', 40));

// 運算式 lambda（單行，無大括號）
Func<int, int> square1 = x => x * x;

// 陳述式 lambda（有大括號和 return）
Func<int, int> square2 = x =>
{
    return x * x;
};

Console.WriteLine($"運算式 lambda: square1(5) = {square1(5)}");
Console.WriteLine($"陳述式 lambda: square2(5) = {square2(5)}");

// --------------------------------------------------------------
// 2. 參數型別推斷與簡化
// --------------------------------------------------------------
Console.WriteLine("\n2. 參數型別推斷與簡化");
Console.WriteLine(new string('-', 40));

// 完整寫法
Func<string, bool> predicate1 = (string s) => { return s.Length > 5; };

// 省略參數型別
Func<string, bool> predicate2 = (s) => { return s.Length > 5; };

// 省略括號（只有一個參數）
Func<string, bool> predicate3 = s => { return s.Length > 5; };

// 省略大括號和 return
Func<string, bool> predicate4 = s => s.Length > 5;

Console.WriteLine($"predicate4(\"Hello\") = {predicate4("Hello")}");
Console.WriteLine($"predicate4(\"Hi\") = {predicate4("Hi")}");

// 無參數時必須保留括號
Func<int> getRandom = () => Random.Shared.Next();
Console.WriteLine($"getRandom() = {getRandom()}");

// --------------------------------------------------------------
// 3. Lambda 預設參數（C# 12）
// --------------------------------------------------------------
Console.WriteLine("\n3. Lambda 預設參數（C# 12）");
Console.WriteLine(new string('-', 40));

// lambda 可以有預設參數值
var greeting = (string name = "World") => $"Hello, {name}!";

Console.WriteLine(greeting("Alice"));  // Hello, Alice!
Console.WriteLine(greeting());         // Hello, World!

// 結合多個預設參數
var format = (string text, bool uppercase = false, string prefix = "") =>
{
    var result = prefix + text;
    return uppercase ? result.ToUpper() : result;
};

Console.WriteLine(format("hello"));             // hello
Console.WriteLine(format("hello", true));       // HELLO
Console.WriteLine(format("hello", false, ">>> ")); // >>> hello
Console.WriteLine(format("hello", true, ">>> "));  // >>> HELLO

// --------------------------------------------------------------
// 4. 從匿名方法到 Lambda 的演變
// --------------------------------------------------------------
Console.WriteLine("\n4. 從匿名方法到 Lambda 的演變");
Console.WriteLine(new string('-', 40));

// StringPredicate 委派型別定義在檔案底部

// C# 2.0：匿名方法
StringPredicate p1 = delegate(string s) { return s.EndsWith("go"); };

// C# 3.0：陳述式 lambda
StringPredicate p2 = (string s) => { return s.EndsWith("go"); };

// C# 3.0：運算式 lambda（最簡形式）
StringPredicate p3 = s => s.EndsWith("go");

Console.WriteLine($"p1(\"Mango\") = {p1("Mango")}");
Console.WriteLine($"p2(\"Mango\") = {p2("Mango")}");
Console.WriteLine($"p3(\"Mango\") = {p3("Mango")}");

// --------------------------------------------------------------
// 5. 靜態 Lambda（C# 9）
// --------------------------------------------------------------
Console.WriteLine("\n5. 靜態 Lambda（C# 9）");
Console.WriteLine(new string('-', 40));

// 靜態 lambda 不能捕獲外部變數
Func<int, int> doubler = static n => n * 2;
Console.WriteLine($"static lambda: doubler(5) = {doubler(5)}");

// 以下會編譯錯誤：
// int factor = 2;
// Func<int, int> multiplier = static n => n * factor;  // 錯誤

Console.WriteLine("靜態 lambda 可避免意外的閉包開銷");

// --------------------------------------------------------------
// 6. Lambda 與區域方法的選擇
// --------------------------------------------------------------
Console.WriteLine("\n6. Lambda vs 區域方法");
Console.WriteLine(new string('-', 40));

// Lambda 適合：傳遞給 LINQ 或作為參數
var numbers = new[] { 1, 2, 3, 4, 5 };
var evens = numbers.Where(x => x % 2 == 0);
Console.WriteLine($"偶數（LINQ + Lambda）：{string.Join(", ", evens)}");

// 區域方法適合：需要遞迴
int Factorial(int n)
{
    // 區域方法支援遞迴
    int FactorialImpl(int x)
    {
        if (x <= 1) return 1;
        return x * FactorialImpl(x - 1);
    }

    return FactorialImpl(n);
}

Console.WriteLine($"5! = {Factorial(5)}");

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 委派型別宣告（必須放在 top-level statements 之後）
// ============================================================

delegate bool StringPredicate(string s);
