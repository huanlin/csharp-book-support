// 示範 SearchValues<T> (.NET 8+) 優化重複搜尋

using System.Buffers;

Console.WriteLine("=== SearchValues<T> (.NET 8+) ===\n");

string text = "Hello World! This is a test.";
int vowels = VowelCounter.CountVowels(text);
Console.WriteLine($"字串: \"{text}\"");
Console.WriteLine($"母音數量: {vowels}");

// 輔助類別

public static class VowelCounter
{
    // 建立可重複使用的搜尋值
    private static readonly SearchValues<char> _vowels = SearchValues.Create("aeiouAEIOU");

    public static int CountVowels(ReadOnlySpan<char> text)
    {
        int count = 0;
        int pos = 0;
        int idx;

        // 使用 SearchValues 優化搜尋
        while ((idx = text.Slice(pos).IndexOfAny(_vowels)) >= 0)
        {
            count++;
            pos += idx + 1; // 移動到下一個字元
        }

        return count;
    }
}
