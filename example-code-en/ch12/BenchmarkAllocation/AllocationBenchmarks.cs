using BenchmarkDotNet.Attributes;

namespace BenchmarkAllocation;

[MemoryDiagnoser]
public class AllocationBenchmarks
{
    private const int Size = 128;

    [Benchmark(Baseline = true)]
    public int[] HeapAllocation()
    {
        return new int[Size];
    }

    [Benchmark]
    public void StackAllocation()
    {
        Span<int> buffer = stackalloc int[Size];
        buffer[0] = 1; // Use it
    }

    [Benchmark]
    public void CollectionExpressionSpan()
    {
        Span<int> buffer = [1, 2, 3, 4, 5, 6, 7, 8];
    }
}
