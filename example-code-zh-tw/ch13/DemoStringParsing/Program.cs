// 示範傳統字串解析 vs. 使用 Span 的零配置字串解析

Console.WriteLine("=== 字串解析比較 (Allocating vs Non-Allocating) ===\n");

string input = "123,456,789";
Console.WriteLine($"輸入字串: \"{input}\"");

// 傳統方式：Substring 會產生新的字串物件 (Allocation)
int val1 = LegacyParser.Parse(input);
Console.WriteLine($"ParseLegacy 結果: {val1}");

// 現代方式：使用 Span，完全零配置
int val2 = ModernParser.Parse(input.AsSpan());
Console.WriteLine($"ParseModern 結果: {val2}");

// 輔助類別

public static class LegacyParser
{
    public static int Parse(string input)
    {
        int commaPos = input.IndexOf(',');
        // Substring 會產生新的字串物件 (Allocation)
        string firstStr = input.Substring(0, commaPos);
        return int.Parse(firstStr);
    }
}

public static class ModernParser
{
    public static int Parse(ReadOnlySpan<char> input)
    {
        int commaPos = input.IndexOf(',');
        // Slice 只是建立一個視窗 (Zero Allocation)
        ReadOnlySpan<char> firstSpan = input.Slice(0, commaPos);
        // int.Parse 支援 ReadOnlySpan<char>
        return int.Parse(firstSpan);
    }
}
