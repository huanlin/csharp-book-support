# BenchmarkAllocation

This project is the micro-benchmark for Chapter 13, Section 13.6, comparing temporary allocation strategies for 128 integers: heap allocation (`new int[128]`), stack allocation (`stackalloc int[128]`), and collection expression assignment to `Span<int>` (`Span<int> span = [.. source]`).

## How to Run

Run in Release mode from the project directory:

```bash
dotnet run -c Release
```

## Measurement Environment and Benchmark Report

- **OS**: Windows 11 (10.0.26200)
- **CPU**: 12th Gen Intel Core i7-12700H 2.30GHz (14 physical cores, 20 logical threads)
- **.NET SDK**: 10.0.303
- **Runtime**: .NET 10.0.11 (X64 RyuJIT x86-64-v3, Concurrent Workstation GC)
- **BenchmarkDotNet**: v0.15.8

### Results

```
BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.9278/25H2/2025Update/HudsonValley2)
12th Gen Intel Core i7-12700H 2.30GHz, 1 CPU, 20 logical and 14 physical cores
.NET SDK 10.0.303
  [Host]     : .NET 10.0.11 (10.0.11, 10.0.1126.37416), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.11 (10.0.11, 10.0.1126.37416), X64 RyuJIT x86-64-v3


| Method                   | Mean      | Error    | StdDev   | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|------------------------- |----------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
| HeapAllocation           | 120.54 ns | 2.081 ns | 1.947 ns |  1.00 |    0.02 | 0.0427 |     536 B |        1.00 |
| StackAllocation          |  84.63 ns | 0.702 ns | 0.656 ns |  0.70 |    0.01 |      - |         - |        0.00 |
| CollectionExpressionSpan | 120.53 ns | 1.811 ns | 1.694 ns |  1.00 |    0.02 | 0.0427 |     536 B |        1.00 |
```

> **Note**: This benchmark illustrates that assigning a collection expression to `Span<T>` in this scenario still resulted in heap allocation (536 B), rather than zero-allocation stack allocation. When deterministic stack allocation is required, prefer the explicit `stackalloc` syntax.

