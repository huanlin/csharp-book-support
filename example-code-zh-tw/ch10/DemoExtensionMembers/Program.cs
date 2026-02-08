// 示範 C# 14 擴充成員（Extension Members）
// 需要 C# 14

Console.WriteLine("=== C# 14 擴充成員 ===\n");

// --------------------------------------------------------------
// 1. 擴充屬性
// --------------------------------------------------------------
Console.WriteLine("1. 擴充屬性");
Console.WriteLine(new string('-', 40));

string text = "hello";
Console.WriteLine($"text = \"{text}\"");
Console.WriteLine($"text.IsEmpty = {text.IsEmpty}");

string? empty = "";
Console.WriteLine($"empty = \"\"");
Console.WriteLine($"empty.IsEmpty = {empty.IsEmpty}");

// --------------------------------------------------------------
// 2. DateTime 擴充屬性
// --------------------------------------------------------------
Console.WriteLine("\n2. DateTime 擴充屬性");
Console.WriteLine(new string('-', 40));

var today = DateTime.Today;
Console.WriteLine($"今天：{today:yyyy-MM-dd}");
Console.WriteLine($"IsWeekend = {today.IsWeekend}");
Console.WriteLine($"FirstDayOfMonth = {today.FirstDayOfMonth:yyyy-MM-dd}");
Console.WriteLine($"LastDayOfMonth = {today.LastDayOfMonth:yyyy-MM-dd}");

// 測試週末
var saturday = today.AddDays(-(int)today.DayOfWeek + 6);
Console.WriteLine($"\n週六：{saturday:yyyy-MM-dd}");
Console.WriteLine($"IsWeekend = {saturday.IsWeekend}");

// --------------------------------------------------------------
// 3. 擴充方法（新語法）
// --------------------------------------------------------------
Console.WriteLine("\n3. 擴充方法（新語法）");
Console.WriteLine(new string('-', 40));

string reversed = "Hello".Reverse();
Console.WriteLine($"\"Hello\".Reverse() = \"{reversed}\"");

string iso = DateTime.Now.ToIso8601();
Console.WriteLine($"DateTime.Now.ToIso8601() = \"{iso}\"");

// --------------------------------------------------------------
// 4. 集合擴充
// --------------------------------------------------------------
Console.WriteLine("\n4. 集合擴充");
Console.WriteLine(new string('-', 40));

var numbers = new[] { 1, 2, 3 };
Console.WriteLine($"numbers.IsEmpty = {numbers.IsEmpty}");

var emptyList = new List<int>();
Console.WriteLine($"emptyList.IsEmpty = {emptyList.IsEmpty}");

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// C# 14 擴充成員定義
// ============================================================

public static class StringExtensions
{
    extension(string s)
    {
        // 擴充屬性（避免與 string.IsNullOrEmpty 方法衝突，改名為 IsEmpty）
        public bool IsEmpty => string.IsNullOrEmpty(s);

        // 擴充方法
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
        // 擴充屬性：判斷是否為週末
        public bool IsWeekend => dt.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

        // 擴充屬性：取得該月的第一天
        public DateTime FirstDayOfMonth => new DateTime(dt.Year, dt.Month, 1);

        // 擴充屬性：取得該月的最後一天（注意：必須透過 dt 來呼叫 FirstDayOfMonth）
        public DateTime LastDayOfMonth => dt.FirstDayOfMonth.AddMonths(1).AddDays(-1);

        // 擴充方法：格式化為 ISO 8601
        public string ToIso8601() => dt.ToString("yyyy-MM-ddTHH:mm:ss");
    }
}

public static class EnumerableExtensions
{
    // 擴充實例成員
    extension<TSource>(IEnumerable<TSource> source)
    {
        public bool IsEmpty => !Enumerable.Any(source);
    }
}

