// Demo: C# 14 extension Block Syntax
// Define extension properties and extension methods

// C# 14 uses the extension keyword to define extension member blocks
public static class StringExtensions
{
    // Extension member block (instance members)
    extension(string s)
    {
        // Extension Property (Avoid naming conflict with string.IsNullOrEmpty method)
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

// Demo Main Program
public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== C# 14 extension Block Syntax Demo ===\n");

        // Scenario 1: Using extension properties
        Console.WriteLine("Scenario 1: Extension Properties (no parentheses needed)");
        string text = "hello";
        bool isEmpty = text.IsEmpty;
        Console.WriteLine($"  \"hello\".IsEmpty = {isEmpty}");
        
        string emptyText = "";
        Console.WriteLine($"  \"\".IsEmpty = {emptyText.IsEmpty}\n");

        // Scenario 2: Using extension methods
        Console.WriteLine("Scenario 2: Extension Methods (requires parentheses)");
        string reversed = text.Reverse();
        Console.WriteLine($"  \"hello\".Reverse() = \"{reversed}\"\n");

        // Scenario 3: Method Chaining
        Console.WriteLine("Scenario 3: Method Chaining");
        string result = "world".Reverse();
        Console.WriteLine($"  \"world\".Reverse() = \"{result}\"");

        // Explanation
        Console.WriteLine("\n=== Explanation ===");
        Console.WriteLine("- IsEmpty is an extension \"property\", no parentheses needed when calling");
        Console.WriteLine("- Reverse() is an extension \"method\", requires parentheses");
        Console.WriteLine("- This is the advantage of C# 14: Previously, extension properties could not be defined");
    }
}
