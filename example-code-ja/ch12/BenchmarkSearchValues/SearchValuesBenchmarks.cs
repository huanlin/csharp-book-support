using BenchmarkDotNet.Attributes;
using System.Buffers;

namespace BenchmarkSearchValues;

[MemoryDiagnoser]
public class SearchValuesBenchmarks
{
    private const string Text = "The quick brown fox jumps over the lazy dog";
    private static readonly char[] VowelsArray = "aeiouAEIOU".ToCharArray();
    private static readonly SearchValues<char> VowelsSearchValues = SearchValues.Create("aeiouAEIOU");

    [Benchmark(Baseline = true)]
    public int CountVowelsArray()
    {
        return CountVowels(Text.AsSpan(), VowelsArray);
    }

    [Benchmark]
    public int CountVowelsSearchValues()
    {
        return CountVowels(Text.AsSpan(), VowelsSearchValues);
    }

    private static int CountVowels(ReadOnlySpan<char> text, ReadOnlySpan<char> vowels)
    {
        int count = 0;
        int pos = 0;

        while (pos < text.Length)
        {
            int idx = text[pos..].IndexOfAny(vowels);
            if (idx < 0)
            {
                break;
            }

            count++;
            pos += idx + 1;
        }

        return count;
    }

    private static int CountVowels(ReadOnlySpan<char> text, SearchValues<char> vowels)
    {
        int count = 0;
        int pos = 0;

        while (pos < text.Length)
        {
            int idx = text[pos..].IndexOfAny(vowels);
            if (idx < 0)
            {
                break;
            }

            count++;
            pos += idx + 1;
        }

        return count;
    }
}
