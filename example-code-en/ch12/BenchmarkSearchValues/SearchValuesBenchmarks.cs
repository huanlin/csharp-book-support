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
    public int IndexOfAnyArray()
    {
        return Text.AsSpan().IndexOfAny(VowelsArray);
    }

    [Benchmark]
    public int IndexOfAnySearchValues()
    {
        return Text.AsSpan().IndexOfAny(VowelsSearchValues);
    }
}
