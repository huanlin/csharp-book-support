# BenchmarkArrayPool

This project is the micro-benchmark for Chapter 13, Section 13.5, measuring the performance and allocation overhead of `new byte[4096]` compared with `ArrayPool<byte>.Shared.Rent/Return`.

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


| Method              | Mean      | Error    | StdDev   | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|-------------------- |----------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
| NewArray            | 119.47 ns | 2.531 ns | 6.624 ns |  1.00 |    0.07 | 0.3281 |    4120 B |        1.00 |
| ArrayPoolRentReturn |  10.51 ns | 0.048 ns | 0.040 ns |  0.09 |    0.00 |      - |         - |        0.00 |
```

> **Note**: This benchmark measures only the raw rent and return invocation overhead. It does not include buffer zeroing (`clearArray: true`), I/O, or actual data processing. Do not assume this micro-benchmark speedup directly translates into end-to-end workload gains. The primary real-world advantage of `ArrayPool<T>` is eliminating the 4,120-byte heap allocation, significantly lowering GC pressure under high-throughput workloads.

