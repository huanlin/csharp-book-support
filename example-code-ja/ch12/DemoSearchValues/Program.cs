// デモ: SearchValues<T> (.NET 8+) による繰り返し検索の最適化

using System.Buffers;

Console.WriteLine("=== SearchValues<T> (.NET 8+) ===\n");

string text = "Hello World! This is a test.";
int vowels = VowelCounter.CountVowels(text);
Console.WriteLine($"文字列: \"{text}\"");
Console.WriteLine($"母音の数: {vowels}");

// ヘルパークラス

public static class VowelCounter
{
    // 再利用可能な検索テーブルを作成
    private static readonly SearchValues<char> _vowels = SearchValues.Create("aeiouAEIOU");

    public static int CountVowels(ReadOnlySpan<char> text)
    {
        int count = 0;
        int pos = 0;
        int idx;

        // SearchValues を使って検索を高速化
        while ((idx = text.Slice(pos).IndexOfAny(_vowels)) >= 0)
        {
            count++;
            pos += idx + 1; // 次の文字位置へ進む
        }

        return count;
    }
}
