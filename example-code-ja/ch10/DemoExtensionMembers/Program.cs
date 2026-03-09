// デモ: C# 14 拡張メンバー
// C# 14 が必要

Console.WriteLine("=== C# 14 拡張メンバー ===\n");

// --------------------------------------------------------------
// 1. 拡張プロパティ
// --------------------------------------------------------------
Console.WriteLine("1. 拡張プロパティ");
Console.WriteLine(new string('-', 40));

string text = "hello";
Console.WriteLine($"text = \"{text}\"");
Console.WriteLine($"text.IsEmpty = {text.IsEmpty}");

string? empty = "";
Console.WriteLine("empty = \"\"");
Console.WriteLine($"empty.IsEmpty = {empty.IsEmpty}");

// --------------------------------------------------------------
// 2. DateTime 拡張プロパティ
// --------------------------------------------------------------
Console.WriteLine("\n2. DateTime 拡張プロパティ");
Console.WriteLine(new string('-', 40));

var today = DateTime.Today;
Console.WriteLine($"今日: {today:yyyy-MM-dd}");
Console.WriteLine($"IsWeekend = {today.IsWeekend}");
Console.WriteLine($"FirstDayOfMonth = {today.FirstDayOfMonth:yyyy-MM-dd}");
Console.WriteLine($"LastDayOfMonth = {today.LastDayOfMonth:yyyy-MM-dd}");

// 週末の確認
var saturday = today.AddDays(-(int)today.DayOfWeek + 6);
Console.WriteLine($"\n土曜日: {saturday:yyyy-MM-dd}");
Console.WriteLine($"IsWeekend = {saturday.IsWeekend}");

// --------------------------------------------------------------
// 3. 拡張メソッド（新構文）
// --------------------------------------------------------------
Console.WriteLine("\n3. 拡張メソッド（新構文）");
Console.WriteLine(new string('-', 40));

string reversed = "Hello".Reverse();
Console.WriteLine($"\"Hello\".Reverse() = \"{reversed}\"");

string iso = DateTime.Now.ToIso8601();
Console.WriteLine($"DateTime.Now.ToIso8601() = \"{iso}\"");

// --------------------------------------------------------------
// 4. コレクション拡張
// --------------------------------------------------------------
Console.WriteLine("\n4. コレクション拡張");
Console.WriteLine(new string('-', 40));

var numbers = new[] { 1, 2, 3 };
Console.WriteLine($"numbers.IsEmpty = {numbers.IsEmpty}");

var emptyList = new List<int>();
Console.WriteLine($"emptyList.IsEmpty = {emptyList.IsEmpty}");

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// C# 14 拡張メンバー定義
// ============================================================

public static class StringExtensions
{
    extension(string s)
    {
        // 拡張プロパティ（string.IsNullOrEmpty メソッドとの衝突を避けるため IsEmpty に改名）
        public bool IsEmpty => string.IsNullOrEmpty(s);

        // 拡張メソッド
        public string Reverse()
        {
            if (string.IsNullOrEmpty(s)) return s;
            var chars = s.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }
    }
}

public static class DateTimeExtensions
{
    extension(DateTime dt)
    {
        // 拡張プロパティ: 週末かどうかを判定
        public bool IsWeekend => dt.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

        // 拡張プロパティ: 月初日を取得
        public DateTime FirstDayOfMonth => new DateTime(dt.Year, dt.Month, 1);

        // 拡張プロパティ: 月末日を取得（注: dt.FirstDayOfMonth 経由で呼ぶ）
        public DateTime LastDayOfMonth => dt.FirstDayOfMonth.AddMonths(1).AddDays(-1);

        // 拡張メソッド: ISO 8601 形式に変換
        public string ToIso8601() => dt.ToString("yyyy-MM-ddTHH:mm:ss");
    }
}

public static class EnumerableExtensions
{
    // 拡張インスタンスメンバー
    extension<TSource>(IEnumerable<TSource> source)
    {
        public bool IsEmpty => !Enumerable.Any(source);
    }
}
