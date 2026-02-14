using BenchmarkDotNet.Attributes;

namespace BenchmarkParser;

[MemoryDiagnoser]
public class ParserBenchmarks
{
    private const string Input = "123,456,789,101,112,131,415,161,718,192";

    [Benchmark(Baseline = true)]
    public int SubstringParsing()
    {
        int commaPos = Input.IndexOf(',');
        string firstStr = Input.Substring(0, commaPos);
        return int.Parse(firstStr);
    }

    [Benchmark]
    public int SpanParsing()
    {
        ReadOnlySpan<char> input = Input.AsSpan();
        int commaPos = input.IndexOf(',');
        ReadOnlySpan<char> firstSpan = input.Slice(0, commaPos);
        return int.Parse(firstSpan);
    }
}
