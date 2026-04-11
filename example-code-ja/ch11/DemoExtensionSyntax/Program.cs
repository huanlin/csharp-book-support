// デモ: 拡張メソッド構文とベストプラクティス

Console.WriteLine("=== 拡張メソッド構文 ===\n");

// --------------------------------------------------------------
// 1. インターフェイス拡張（IEnumerable<T>）
// --------------------------------------------------------------
Console.WriteLine("1. インターフェイス拡張");
Console.WriteLine(new string('-', 40));

// string は IEnumerable<char> を実装
char firstChar = "Seattle".First();
Console.WriteLine($"\"Seattle\".First() = '{firstChar}'");

// 配列も IEnumerable<T> を実装
int firstNumber = new[] { 1, 2, 3 }.First();
Console.WriteLine($"[1, 2, 3].First() = {firstNumber}");

// List<T> も IEnumerable<T> を実装
var myList = new List<string> { "Apple", "Banana", "Cherry" };
string firstItem = myList.First();
Console.WriteLine($"myList.First() = \"{firstItem}\"");

// --------------------------------------------------------------
// 2. ジェネリック拡張メソッド
// --------------------------------------------------------------
Console.WriteLine("\n2. ジェネリック拡張メソッド");
Console.WriteLine(new string('-', 40));

List<string>? names = null;
Console.WriteLine($"names.IsNullOrEmpty() = {names.IsNullOrEmpty()}");

names = new List<string>();
Console.WriteLine($"空 List.IsNullOrEmpty() = {names.IsNullOrEmpty()}");

names.Add("Alice");
Console.WriteLine($"要素あり List.IsNullOrEmpty() = {names.IsNullOrEmpty()}");

// --------------------------------------------------------------
// 3. Null 安全
// --------------------------------------------------------------
Console.WriteLine("\n3. Null 安全");
Console.WriteLine(new string('-', 40));

string? text = null;
Console.WriteLine($"null.IsNullOrWhiteSpace() = {text.IsNullOrWhiteSpace()}");
Console.WriteLine($"null.Truncate(10) = \"{text.Truncate(10)}\"");

text = "Hello, World!";
Console.WriteLine($"\"Hello, World!\".Truncate(5) = \"{text.Truncate(5)}\"");

// --------------------------------------------------------------
// 4. 優先順位: インスタンスメソッド > 拡張メソッド
// --------------------------------------------------------------
Console.WriteLine("\n4. 優先順位");
Console.WriteLine(new string('-', 40));

var test = new Test();
test.Foo(123);  // 引数が int でもインスタンスメソッドが呼ばれる

// 拡張メソッドを明示呼び出し
Extensions.Foo(test, 123);

// --------------------------------------------------------------
// 5. 拡張メソッド間の衝突
// --------------------------------------------------------------
Console.WriteLine("\n5. 拡張メソッド衝突解決");
Console.WriteLine(new string('-', 40));

bool test1 = "Perth".IsCapitalized();
Console.WriteLine($"\"Perth\".IsCapitalized() = {test1}");
Console.WriteLine("（string は object より具体的なため StringExtensions 側が呼ばれる）");

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// 拡張メソッド定義
// ============================================================

public static class EnumerableExtensions
{
    public static T First<T>(this IEnumerable<T> sequence)
    {
        foreach (T element in sequence)
            return element;
        throw new InvalidOperationException("シーケンスに要素がありません。");
    }

    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? source)
    {
        return source == null || !source.Any();
    }
}

public static class StringExtensions
{
    public static bool IsNullOrWhiteSpace(this string? value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    public static string Truncate(this string? value, int maxLength)
    {
        if (value == null) return string.Empty;
        return value.Length <= maxLength ? value : value.Substring(0, maxLength);
    }

    public static bool IsCapitalized(this string s)
    {
        return !string.IsNullOrEmpty(s) && char.IsUpper(s[0]);
    }
}

public static class ObjectExtensions
{
    public static bool IsCapitalized(this object s)
    {
        return s?.ToString() is string str
            && str.Length > 0
            && char.IsUpper(str[0]);
    }
}

// 優先順位テスト用クラス
public class Test
{
    public void Foo(object x)  // インスタンスメソッド（引数は object）
    {
        Console.WriteLine("呼び出し: インスタンスメソッド Foo(object)");
    }
}

public static class Extensions
{
    public static void Foo(this Test t, int x)  // 拡張メソッド（引数は int）
    {
        Console.WriteLine("呼び出し: 拡張メソッド Foo(int)");
    }
}
