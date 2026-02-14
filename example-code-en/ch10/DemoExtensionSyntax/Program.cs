// Demo: Extension Method Syntax and Best Practices

Console.WriteLine("=== Extension Method Syntax ===\n");

// --------------------------------------------------------------
// 1. Extending Interfaces (IEnumerable<T>)
// --------------------------------------------------------------
Console.WriteLine("1. Extending Interfaces");
Console.WriteLine(new string('-', 40));

// string implements IEnumerable<char>
char firstChar = "Seattle".First();
Console.WriteLine($"\"Seattle\".First() = '{firstChar}'");

// Array also implements IEnumerable<T>
int firstNumber = new[] { 1, 2, 3 }.First();
Console.WriteLine($"[1, 2, 3].First() = {firstNumber}");

// List<T> also implements IEnumerable<T>
var myList = new List<string> { "Apple", "Banana", "Cherry" };
string firstItem = myList.First();
Console.WriteLine($"myList.First() = \"{firstItem}\"");

// --------------------------------------------------------------
// 2. Generic Extension Methods
// --------------------------------------------------------------
Console.WriteLine("\n2. Generic Extension Methods");
Console.WriteLine(new string('-', 40));

List<string>? names = null;
Console.WriteLine($"names.IsNullOrEmpty() = {names.IsNullOrEmpty()}");

names = new List<string>();
Console.WriteLine($"Empty List.IsNullOrEmpty() = {names.IsNullOrEmpty()}");

names.Add("Alice");
Console.WriteLine($"List with elements.IsNullOrEmpty() = {names.IsNullOrEmpty()}");

// --------------------------------------------------------------
// 3. Null Safety
// --------------------------------------------------------------
Console.WriteLine("\n3. Null Safety");
Console.WriteLine(new string('-', 40));

string? text = null;
Console.WriteLine($"null.IsNullOrWhiteSpace() = {text.IsNullOrWhiteSpace()}");
Console.WriteLine($"null.Truncate(10) = \"{text.Truncate(10)}\"");

text = "Hello, World!";
Console.WriteLine($"\"Hello, World!\".Truncate(5) = \"{text.Truncate(5)}\"");

// --------------------------------------------------------------
// 4. Precedence: Instance Methods > Extension Methods
// --------------------------------------------------------------
Console.WriteLine("\n4. Precedence");
Console.WriteLine(new string('-', 40));

var test = new Test();
test.Foo(123);  // Calls instance method, even though the argument is an int

// Explicitly call extension method
Extensions.Foo(test, 123);

// --------------------------------------------------------------
// 5. Conflicts Between Extension Methods
// --------------------------------------------------------------
Console.WriteLine("\n5. Extension Method Conflict Resolution");
Console.WriteLine(new string('-', 40));

bool test1 = "Perth".IsCapitalized();
Console.WriteLine($"\"Perth\".IsCapitalized() = {test1}");
Console.WriteLine("(Calling StringExtensions version because string is more specific than object)");

Console.WriteLine("\n=== Example End ===");

// ============================================================
// Extension Method Definitions
// ============================================================

public static class EnumerableExtensions
{
    public static T First<T>(this IEnumerable<T> sequence)
    {
        foreach (T element in sequence)
            return element;
        throw new InvalidOperationException("No items in sequence!");
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

// Precedence test class
public class Test
{
    public void Foo(object x)  // Instance method, argument is object
    {
        Console.WriteLine("Call: Instance method Foo(object)");
    }
}

public static class Extensions
{
    public static void Foo(this Test t, int x)  // Extension method, argument is int
    {
        Console.WriteLine("Call: Extension method Foo(int)");
    }
}
