// 示範為什麼需要擴充方法

Console.WriteLine("=== 為什麼需要擴充方法 ===\n");

// --------------------------------------------------------------
// 1. 傳統靜態工具類別的痛點
// --------------------------------------------------------------
Console.WriteLine("1. 傳統靜態工具類別的痛點");
Console.WriteLine(new string('-', 40));

string s1 = "abcdefg";

// 傳統靜態工具類別的呼叫方式（巢狀呼叫）
string s2 = StringHelper.Capitalize(StringHelper.Reverse(s1));

Console.WriteLine($"原始字串：{s1}");
Console.WriteLine($"反轉並首字母大寫（傳統方式）：{s2}");
Console.WriteLine("問題：巢狀呼叫導致執行順序與閱讀順序相反");

// --------------------------------------------------------------
// 2. 擴充方法的優勢
// --------------------------------------------------------------
Console.WriteLine("\n2. 擴充方法的優勢");
Console.WriteLine(new string('-', 40));

// 使用擴充方法的呼叫方式（方法鏈）
string s3 = s1.Reverse().Capitalize();

Console.WriteLine($"反轉並首字母大寫（擴充方法）：{s3}");
Console.WriteLine("優點：閱讀順序自然，從左到右，符合思維習慣");

// --------------------------------------------------------------
// 3. 流暢介面（Fluent API）
// --------------------------------------------------------------
Console.WriteLine("\n3. 流暢介面（Fluent API）");
Console.WriteLine(new string('-', 40));

string processed = "hello world"
    .Capitalize()
    .Reverse()
    .Truncate(8)
    .Reverse();

Console.WriteLine($"處理結果：{processed}");
Console.WriteLine("優點：支援方法鏈（method chaining），程式碼更簡潔");

// --------------------------------------------------------------
// 4. 擴充 DateTime
// --------------------------------------------------------------
Console.WriteLine("\n4. 擴充 DateTime");
Console.WriteLine(new string('-', 40));

DateTime now = DateTime.Now;
string formatted = now.ToString('/');

Console.WriteLine($"當前日期：{now:yyyy-MM-dd}");
Console.WriteLine($"格式化後：{formatted}");

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 傳統靜態工具類別
// ============================================================
public static class StringHelper
{
    public static string Reverse(string s)
    {
        if (string.IsNullOrEmpty(s)) return s;

        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    public static string Capitalize(string s)
    {
        if (string.IsNullOrEmpty(s)) return s;
        return char.ToUpper(s[0]) + s.Substring(1);
    }
}

// ============================================================
// 擴充方法版本
// ============================================================
public static class StringExtensions
{
    public static string Reverse(this string s)
    {
        if (string.IsNullOrEmpty(s)) return s;

        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    public static string Capitalize(this string s)
    {
        if (string.IsNullOrEmpty(s)) return s;
        return char.ToUpper(s[0]) + s.Substring(1);
    }

    public static string Truncate(this string? value, int maxLength)
    {
        if (value == null) return string.Empty;
        return value.Length <= maxLength ? value : value.Substring(0, maxLength);
    }
}

public static class DateTimeExtensions
{
    public static string ToString(this DateTime aDate, char separator)
    {
        return $"{aDate.Year}{separator}{aDate.Month:D2}{separator}{aDate.Day:D2}";
    }
}
