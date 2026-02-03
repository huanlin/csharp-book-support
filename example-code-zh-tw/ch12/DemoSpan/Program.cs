// 示範 Span<T> 與高效能字串處理
using System.Buffers;

Console.WriteLine("=== Span<T> 高效能記憶體操作 ===\n");

// --------------------------------------------------------------
// 1. 零複製切片 (Zero-allocation Slicing)
// --------------------------------------------------------------
Console.WriteLine("1. 零複製切片 (Slicing)");
Console.WriteLine(new string('-', 40));

int[] array = { 1, 2, 3, 4, 5 };
Span<int> span = array.AsSpan();
Span<int> subSpan = span.Slice(1, 3); // 指向 [2, 3, 4]

Console.WriteLine($"原始陣列[1]: {array[1]}"); // 2

// 修改 Span 會直接影響原陣列
subSpan[0] = 99;
Console.WriteLine($"修改 SubSpan[0] 為 99 後，原始陣列[1]: {array[1]}"); // 99


// --------------------------------------------------------------
// 2. 字串解析效能比較
// --------------------------------------------------------------
Console.WriteLine("\n2. 字串解析比較 (Allocating vs Non-Allocating)");
Console.WriteLine(new string('-', 40));

string input = "123,456,789";
Console.WriteLine($"輸入字串: \"{input}\"");

// 傳統方式
int val1 = LegacyParser.Parse(input);
Console.WriteLine($"ParseLegacy 結果: {val1}");

// Loop 執行以凸顯差異 (示意)
// 在 BenchmarkDotNet 中會看到明顯差異：Legacy 有 Allocation，Modern 為 0
int val2 = ModernParser.Parse(input.AsSpan());
Console.WriteLine($"ParseModern 結果: {val2}");


// --------------------------------------------------------------
// 3. SearchValues<T> (.NET 8+)
// --------------------------------------------------------------
Console.WriteLine("\n3. SearchValues<T> (.NET 8+)");
Console.WriteLine(new string('-', 40));

string text = "Hello World! This is a test.";
int vowels = VowelCounter.CountVowels(text);
Console.WriteLine($"字串: \"{text}\"");
Console.WriteLine($"母音數量: {vowels}");


Console.WriteLine("\n=== 範例結束 ===");

// --------------------------------------------------------------
// 模擬方法
// --------------------------------------------------------------


// --------------------------------------------------------------
// 輔助類別
// --------------------------------------------------------------

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

public static class VowelCounter
{
    // 建立可重複使用的搜尋值
    private static readonly SearchValues<char> _vowels = SearchValues.Create("aeiouAEIOU");

    public static int CountVowels(ReadOnlySpan<char> text)
    {
        int count = 0;
        int pos = 0;
        
        // 使用 SearchValues 優化搜尋
        while ((pos = text.Slice(pos).IndexOfAny(_vowels)) >= 0)
        {
            count++;
            pos++; // 移動到下一個字元
        }
        
        return count;
    }
}

