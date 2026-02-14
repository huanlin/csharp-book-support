```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.7840/25H2/2025Update/HudsonValley2)
12th Gen Intel Core i7-12700H 2.30GHz, 1 CPU, 20 logical and 14 physical cores
.NET SDK 10.0.103
  [Host]     : .NET 10.0.3 (10.0.3, 10.0.326.7603), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.3 (10.0.3, 10.0.326.7603), X64 RyuJIT x86-64-v3


```
| Method                   | Mean       | Error     | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|------------------------- |-----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| HeapAllocation           | 15.3791 ns | 0.3721 ns | 0.3821 ns |  1.00 |    0.03 | 0.0427 |     536 B |        1.00 |
| StackAllocation          |  3.6129 ns | 0.0278 ns | 0.0247 ns |  0.24 |    0.01 |      - |         - |        0.00 |
| CollectionExpressionSpan |  0.2854 ns | 0.0209 ns | 0.0196 ns |  0.02 |    0.00 |      - |         - |        0.00 |
