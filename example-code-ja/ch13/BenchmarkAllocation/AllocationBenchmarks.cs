using BenchmarkDotNet.Attributes;
using System.Runtime.CompilerServices;

namespace BenchmarkAllocation;

[MemoryDiagnoser]
public class AllocationBenchmarks
{
    private const int Size = 128;
    private static readonly int[] Source = Enumerable.Range(1, Size).ToArray();

    [Benchmark(Baseline = true)]
    public int HeapAllocation()
    {
        int[] buffer = new int[Size];
        Source.AsSpan().CopyTo(buffer);
        return ConsumeArray(buffer);
    }

    [Benchmark]
    public int StackAllocation()
    {
        Span<int> buffer = stackalloc int[Size];
        Source.AsSpan().CopyTo(buffer);
        return ConsumeSpan(buffer);
    }

    [Benchmark]
    public int CollectionExpressionSpan()
    {
        Span<int> buffer = [.. Source];
        return ConsumeSpan(buffer);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static int ConsumeArray(int[] buffer)
    {
        return ConsumeSpan(buffer);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static int ConsumeSpan(ReadOnlySpan<int> buffer)
    {
        int sum = 0;
        for (int i = 0; i < buffer.Length; i++)
        {
            sum += buffer[i];
        }

        return sum;
    }
}

