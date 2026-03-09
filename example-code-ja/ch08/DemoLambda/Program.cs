// デモ: ラムダ式の各種構文

Console.WriteLine("=== ラムダ式の例 ===\n");

// --------------------------------------------------------------
// 1. 式ラムダと文ラムダ
// --------------------------------------------------------------
Console.WriteLine("1. 式ラムダと文ラムダ");
Console.WriteLine(new string('-', 40));

Func<int, int> square1 = x => x * x;

Func<int, int> square2 = x =>
{
    return x * x;
};

Console.WriteLine($"式ラムダ: square1(5) = {square1(5)}");
Console.WriteLine($"文ラムダ: square2(5) = {square2(5)}");

// --------------------------------------------------------------
// 2. 型推論と簡略化
// --------------------------------------------------------------
Console.WriteLine("\n2. 型推論と簡略化");
Console.WriteLine(new string('-', 40));

Func<string, bool> predicate1 = (string s) => { return s.Length > 5; };
Func<string, bool> predicate2 = (s) => { return s.Length > 5; };
Func<string, bool> predicate3 = s => { return s.Length > 5; };
Func<string, bool> predicate4 = s => s.Length > 5;

Console.WriteLine($"predicate4(\"Hello\") = {predicate4("Hello")}");
Console.WriteLine($"predicate4(\"Hi\") = {predicate4("Hi")}");

Func<int> getRandom = () => Random.Shared.Next();
Console.WriteLine($"getRandom() = {getRandom()}");

// --------------------------------------------------------------
// 3. ラムダ既定引数（C# 12）
// --------------------------------------------------------------
Console.WriteLine("\n3. ラムダ既定引数（C# 12）");
Console.WriteLine(new string('-', 40));

var greeting = (string name = "World") => $"Hello, {name}!";

Console.WriteLine(greeting("Alice"));
Console.WriteLine(greeting());

var format = (string text, bool uppercase = false, string prefix = "") =>
{
    var result = prefix + text;
    return uppercase ? result.ToUpper() : result;
};

Console.WriteLine(format("hello"));
Console.WriteLine(format("hello", true));
Console.WriteLine(format("hello", false, ">>> "));
Console.WriteLine(format("hello", true, ">>> "));

// --------------------------------------------------------------
// 4. 匿名メソッドからラムダへの変遷
// --------------------------------------------------------------
Console.WriteLine("\n4. 匿名メソッドからラムダへの変遷");
Console.WriteLine(new string('-', 40));

StringPredicate p1 = delegate(string s) { return s.EndsWith("go"); };
StringPredicate p2 = (string s) => { return s.EndsWith("go"); };
StringPredicate p3 = s => s.EndsWith("go");

Console.WriteLine($"p1(\"Mango\") = {p1("Mango")}");
Console.WriteLine($"p2(\"Mango\") = {p2("Mango")}");
Console.WriteLine($"p3(\"Mango\") = {p3("Mango")}");

// --------------------------------------------------------------
// 5. static ラムダ（C# 9）
// --------------------------------------------------------------
Console.WriteLine("\n5. static ラムダ（C# 9）");
Console.WriteLine(new string('-', 40));

Func<int, int> doubler = static n => n * 2;
Console.WriteLine($"static lambda: doubler(5) = {doubler(5)}");

Console.WriteLine("static ラムダは意図しないクロージャーを防げる。");

// --------------------------------------------------------------
// 6. ラムダとローカルメソッドの使い分け
// --------------------------------------------------------------
Console.WriteLine("\n6. ラムダとローカルメソッドの使い分け");
Console.WriteLine(new string('-', 40));

var numbers = new[] { 1, 2, 3, 4, 5 };
var evens = numbers.Where(x => x % 2 == 0);
Console.WriteLine($"偶数（LINQ + ラムダ）: {string.Join(", ", evens)}");

int Factorial(int n)
{
    int FactorialImpl(int x)
    {
        if (x <= 1) return 1;
        return x * FactorialImpl(x - 1);
    }

    return FactorialImpl(n);
}

Console.WriteLine($"5! = {Factorial(5)}");

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// デリゲート宣言
// ============================================================

delegate bool StringPredicate(string s);
