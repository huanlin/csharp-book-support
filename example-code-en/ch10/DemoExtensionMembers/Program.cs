// Demo: C# 14 Extension Members
// Requires C# 14

Console.WriteLine("=== C# 14 Extension Members ===\n");

// --------------------------------------------------------------
// 1. Extension Properties
// --------------------------------------------------------------
Console.WriteLine("1. Extension Properties");
Console.WriteLine(new string('-', 40));

string text = "hello";
Console.WriteLine($"text = \"{text}\"");
Console.WriteLine($"text.IsEmpty = {text.IsEmpty}");

string? empty = "";
Console.WriteLine($"empty = \"\"");
Console.WriteLine($"empty.IsEmpty = {empty.IsEmpty}");

// --------------------------------------------------------------
// 2. DateTime Extension Properties
// --------------------------------------------------------------
Console.WriteLine("\n2. DateTime Extension Properties");
Console.WriteLine(new string('-', 40));

var today = DateTime.Today;
Console.WriteLine($"Today: {today:yyyy-MM-dd}");
Console.WriteLine($"IsWeekend = {today.IsWeekend}");
Console.WriteLine($"FirstDayOfMonth = {today.FirstDayOfMonth:yyyy-MM-dd}");
Console.WriteLine($"LastDayOfMonth = {today.LastDayOfMonth:yyyy-MM-dd}");

// Test Weekend
var saturday = today.AddDays(-(int)today.DayOfWeek + 6);
Console.WriteLine($"\nSaturday: {saturday:yyyy-MM-dd}");
Console.WriteLine($"IsWeekend = {saturday.IsWeekend}");

// --------------------------------------------------------------
// 3. Extension Methods (New Syntax)
// --------------------------------------------------------------
Console.WriteLine("\n3. Extension Methods (New Syntax)");
Console.WriteLine(new string('-', 40));

string reversed = "Hello".Reverse();
Console.WriteLine($"\"Hello\".Reverse() = \"{reversed}\"");

string iso = DateTime.Now.ToIso8601();
Console.WriteLine($"DateTime.Now.ToIso8601() = \"{iso}\"");

// --------------------------------------------------------------
// 4. Collection Extensions
// --------------------------------------------------------------
Console.WriteLine("\n4. Collection Extensions");
Console.WriteLine(new string('-', 40));

var numbers = new[] { 1, 2, 3 };
Console.WriteLine($"numbers.IsEmpty = {numbers.IsEmpty}");

var emptyList = new List<int>();
Console.WriteLine($"emptyList.IsEmpty = {emptyList.IsEmpty}");

Console.WriteLine("\n=== Example End ===");

// ============================================================
// C# 14 Extension Member Definitions
// ============================================================

public static class StringExtensions
{
    extension(string s)
    {
        // Extension Property (Avoid conflict with string.IsNullOrEmpty method, renamed to IsEmpty)
        public bool IsEmpty => string.IsNullOrEmpty(s);

        // Extension Method
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
        // Extension Property: Checks if it's a weekend
        public bool IsWeekend => dt.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

        // Extension Property: Gets the first day of the month
        public DateTime FirstDayOfMonth => new DateTime(dt.Year, dt.Month, 1);

        // Extension Property: Gets the last day of the month (Note: must call via dt.FirstDayOfMonth)
        public DateTime LastDayOfMonth => dt.FirstDayOfMonth.AddMonths(1).AddDays(-1);

        // Extension Method: Formats as ISO 8601
        public string ToIso8601() => dt.ToString("yyyy-MM-ddTHH:mm:ss");
    }
}

public static class EnumerableExtensions
{
    // Extension Instance Members
    extension<TSource>(IEnumerable<TSource> source)
    {
        public bool IsEmpty => !Enumerable.Any(source);
    }
}

