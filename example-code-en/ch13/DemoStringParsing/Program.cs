// Demo: Traditional string parsing vs. Zero-allocation string parsing using Span

Console.WriteLine("=== String Parsing Comparison (Allocating vs Non-Allocating) ===\n");

string input = "123,456,789";
Console.WriteLine($"Input string: \"{input}\"");

// Traditional way: Substring creates a new string object (Allocation)
int val1 = LegacyParser.Parse(input);
Console.WriteLine($"ParseLegacy result: {val1}");

// Modern way: Using Span, completely zero allocation
int val2 = ModernParser.Parse(input.AsSpan());
Console.WriteLine($"ParseModern result: {val2}");

// Helper Classes

public static class LegacyParser
{
    public static int Parse(string input)
    {
        int commaPos = input.IndexOf(',');
        // Substring creates a new string object (Allocation)
        string firstStr = input.Substring(0, commaPos);
        return int.Parse(firstStr);
    }
}

public static class ModernParser
{
    public static int Parse(ReadOnlySpan<char> input)
    {
        int commaPos = input.IndexOf(',');
        // Slice just creates a window (Zero Allocation)
        ReadOnlySpan<char> firstSpan = input.Slice(0, commaPos);
        // int.Parse supports ReadOnlySpan<char>
        return int.Parse(firstSpan);
    }
}
