// Demo: Why Extension Methods are Needed

Console.WriteLine("=== Why Extension Methods are Needed ===\n");

// --------------------------------------------------------------
// 1. Pain points of traditional static utility classes
// --------------------------------------------------------------
Console.WriteLine("1. Pain points of traditional static utility classes");
Console.WriteLine(new string('-', 40));

string s1 = "abcdefg";

// Traditional static utility class calling style (nested calls)
string s2 = StringHelper.Capitalize(StringHelper.Reverse(s1));

Console.WriteLine($"Original string: {s1}");
Console.WriteLine($"Reverse and Capitalize (Traditional way): {s2}");
Console.WriteLine("Problem: Nested calls lead to execution order being opposite to reading order");

// --------------------------------------------------------------
// 2. Advantages of Extension Methods
// --------------------------------------------------------------
Console.WriteLine("\n2. Advantages of Extension Methods");
Console.WriteLine(new string('-', 40));

// Using extension methods calling style (method chaining)
string s3 = s1.Reverse().Capitalize();

Console.WriteLine($"Reverse and Capitalize (Extension Method): {s3}");
Console.WriteLine("Advantage: Reading order is natural, from left to right, matching cognitive habits");

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

Console.WriteLine($"Processed result: {processed}");
Console.WriteLine("Advantage: Supports method chaining, code is more concise");

// --------------------------------------------------------------
// 4. Extending DateTime
// --------------------------------------------------------------
Console.WriteLine("\n4. Extending DateTime");
Console.WriteLine(new string('-', 40));

DateTime now = DateTime.Now;
string formatted = now.ToString('/');

Console.WriteLine($"Current Date: {now:yyyy-MM-dd}");
Console.WriteLine($"Formatted Result: {formatted}");

Console.WriteLine("\n=== Example End ===");

// ============================================================
// Traditional Static Utility Class
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
// Extension Method Version
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
