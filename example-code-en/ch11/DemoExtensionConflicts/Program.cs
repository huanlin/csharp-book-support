// Demo: Conflict Resolution Between Extension Methods
// When multiple extension methods have compatible signatures, C# picks the one with the more specific parameter type.

// Extension methods for string type
public static class StringHelper
{
    public static bool IsCapitalized(this string s)
    {
        Console.WriteLine("→ Calling StringHelper.IsCapitalized (string)");
        return !string.IsNullOrEmpty(s) && char.IsUpper(s[0]);
    }
}

// Extension methods for object type (more generalized)
public static class ObjectHelper
{
    public static bool IsCapitalized(this object obj)
    {
        Console.WriteLine("→ Calling ObjectHelper.IsCapitalized (object)");
        return obj?.ToString() is string str && str.Length > 0 && char.IsUpper(str[0]);
    }
}

// Demo Main Program
public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Extension Method Conflict Resolution Demo ===\n");

        // Scenario 1: string variable calls IsCapitalized
        Console.WriteLine("Scenario 1: string variable calls IsCapitalized");
        string text = "Perth";
        bool result1 = text.IsCapitalized();
        Console.WriteLine($"Result: {result1}");
        Console.WriteLine("Explanation: Because string is more specific than object, StringHelper version is selected\n");

        // Scenario 2: object variable calls IsCapitalized
        Console.WriteLine("Scenario 2: object variable calls IsCapitalized");
        object obj = "Sydney";
        bool result2 = obj.IsCapitalized();
        Console.WriteLine($"Result: {result2}");
        Console.WriteLine("Explanation: Variable type is object, so ObjectHelper version is selected\n");

        // Scenario 3: Explicitly using static syntax to specify which version to call
        Console.WriteLine("Scenario 3: Explicitly using static syntax");
        bool result3 = ObjectHelper.IsCapitalized("Melbourne");
        Console.WriteLine($"Result: {result3}");
        Console.WriteLine("Explanation: Even if passing a string, static syntax can force call ObjectHelper version\n");

        // Precedence rules summary
        Console.WriteLine("=== Precedence Rules ===");
        Console.WriteLine("1. Instance method > Extension method");
        Console.WriteLine("2. Specific type (class/struct) > Interface");
        Console.WriteLine("3. More specific type > More generalized type");
    }
}
