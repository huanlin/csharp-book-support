// 示範擴充方法語法與最佳實踐

Console.WriteLine("=== 擴充方法語法 ===\n");

// --------------------------------------------------------------
// 1. 擴充介面（IEnumerable<T>）
// --------------------------------------------------------------
Console.WriteLine("1. 擴充介面");
Console.WriteLine(new string('-', 40));

// string 實作了 IEnumerable<char>
char firstChar = "Seattle".First();
Console.WriteLine($"\"Seattle\".First() = '{firstChar}'");

// 陣列也實作了 IEnumerable<T>
int firstNumber = new[] { 1, 2, 3 }.First();
Console.WriteLine($"[1, 2, 3].First() = {firstNumber}");

// List<T> 也實作了 IEnumerable<T>
var myList = new List<string> { "Apple", "Banana", "Cherry" };
string firstItem = myList.First();
Console.WriteLine($"myList.First() = \"{firstItem}\"");

// --------------------------------------------------------------
// 2. 泛型擴充方法
// --------------------------------------------------------------
Console.WriteLine("\n2. 泛型擴充方法");
Console.WriteLine(new string('-', 40));

List<string>? names = null;
Console.WriteLine($"names.IsNullOrEmpty() = {names.IsNullOrEmpty()}");

names = new List<string>();
Console.WriteLine($"空 List.IsNullOrEmpty() = {names.IsNullOrEmpty()}");

names.Add("Alice");
Console.WriteLine($"有元素的 List.IsNullOrEmpty() = {names.IsNullOrEmpty()}");

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
// 4. 優先順序：實例方法 > 擴充方法
// --------------------------------------------------------------
Console.WriteLine("\n4. 優先順序");
Console.WriteLine(new string('-', 40));

var test = new Test();
test.Foo(123);  // 呼叫實例方法，即使參數是 int

// 明確呼叫擴充方法
Extensions.Foo(test, 123);

// --------------------------------------------------------------
// 5. 擴充方法之間的衝突
// --------------------------------------------------------------
Console.WriteLine("\n5. 擴充方法衝突解決");
Console.WriteLine(new string('-', 40));

bool test1 = "Perth".IsCapitalized();
Console.WriteLine($"\"Perth\".IsCapitalized() = {test1}");
Console.WriteLine("（呼叫 StringExtensions 版本，因為 string 比 object 更具體）");

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 擴充方法定義
// ============================================================

public static class EnumerableExtensions
{
    public static T First<T>(this IEnumerable<T> sequence)
    {
        foreach (T element in sequence)
            return element;
        throw new InvalidOperationException("序列中沒有元素！");
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
        return s?.ToString() is string str && char.IsUpper(str[0]);
    }
}

// 優先順序測試類別
public class Test
{
    public void Foo(object x)  // 實例方法，參數是 object
    {
        Console.WriteLine("呼叫：實例方法 Foo(object)");
    }
}

public static class Extensions
{
    public static void Foo(this Test t, int x)  // 擴充方法，參數是 int
    {
        Console.WriteLine("呼叫：擴充方法 Foo(int)");
    }
}
