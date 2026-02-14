using BenchmarkDotNet.Attributes;
using System.Buffers;

namespace BenchmarkArrayPool;

[MemoryDiagnoser]
public class ArrayPoolBenchmarks
{
    private const int ArraySize = 4096;

    [Benchmark(Baseline = true)]
    public byte[] NewArray()
    {
        return new byte[ArraySize];
    }

    [Benchmark]
    public void ArrayPoolRentReturn()
    {
        byte[] buffer = ArrayPool<byte>.Shared.Rent(ArraySize);
        ArrayPool<byte>.Shared.Return(buffer);
    }
}
