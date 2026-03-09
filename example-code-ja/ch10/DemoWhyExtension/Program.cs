// デモ: なぜ拡張メソッドが必要か

Console.WriteLine("=== なぜ拡張メソッドが必要か ===\n");

// --------------------------------------------------------------
// 1. 従来の static ユーティリティクラスの課題
// --------------------------------------------------------------
Console.WriteLine("1. 従来の static ユーティリティクラスの課題");
Console.WriteLine(new string('-', 40));

string s1 = "abcdefg";

// 従来の static ユーティリティクラス呼び出し（ネスト呼び出し）
string s2 = StringHelper.Capitalize(StringHelper.Reverse(s1));

Console.WriteLine($"元の文字列: {s1}");
Console.WriteLine($"逆順 + 先頭大文字（従来方式）: {s2}");
Console.WriteLine("課題: ネスト呼び出しは実行順が読み順と逆になりやすい");

// --------------------------------------------------------------
// 2. 拡張メソッドの利点
// --------------------------------------------------------------
Console.WriteLine("\n2. 拡張メソッドの利点");
Console.WriteLine(new string('-', 40));

// 拡張メソッド呼び出し（メソッドチェーン）
string s3 = s1.Reverse().Capitalize();

Console.WriteLine($"逆順 + 先頭大文字（拡張メソッド）: {s3}");
Console.WriteLine("利点: 左から右へ自然に読め、思考順序に合う");

// --------------------------------------------------------------
// 3. Fluent API
// --------------------------------------------------------------
Console.WriteLine("\n3. Fluent API");
Console.WriteLine(new string('-', 40));

string processed = "hello world"
    .Capitalize()
    .Reverse()
    .Truncate(8)
    .Reverse();

Console.WriteLine($"処理結果: {processed}");
Console.WriteLine("利点: メソッドチェーンによりコードが簡潔になる");

// --------------------------------------------------------------
// 4. DateTime の拡張
// --------------------------------------------------------------
Console.WriteLine("\n4. DateTime の拡張");
Console.WriteLine(new string('-', 40));

DateTime now = DateTime.Now;
string formatted = now.ToString('/');

Console.WriteLine($"現在日付: {now:yyyy-MM-dd}");
Console.WriteLine($"整形結果: {formatted}");

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// 従来の static ユーティリティクラス
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
// 拡張メソッド版
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
