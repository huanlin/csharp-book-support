# BenchmarkAllocation

このプロジェクトは、第 13 章 13.6 節の一時メモリ割り当てに関するマイクロベンチマークです。128 個の整数を処理する際、ヒープ割り当て（`new int[128]`）、スタック割り当て（`stackalloc int[128]`）、およびコレクション式による `Span<int>` への代入（`Span<int> span = [.. source]`）の実行時間とメモリ割り当て動作を比較します。

## 実行方法

プロジェクトのディレクトリで Release モードにて実行します:

```bash
dotnet run -c Release
```

## 測定環境とベンチマーク レポート

- **OS**: Windows 11 (10.0.26200)
- **CPU**: 12th Gen Intel Core i7-12700H 2.30GHz (14 物理コア、20 論理スレッド)
- **.NET SDK**: 10.0.303
- **ランタイム**: .NET 10.0.11 (X64 RyuJIT x86-64-v3, Concurrent Workstation GC)
- **BenchmarkDotNet**: v0.15.8

### 測定結果

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

> **説明**: このベンチマークは、コレクション式を `Span<T>` に代入した際、このシナリオにおいては依然としてヒープ割り当て（536 B）が発生しており、必ずしもゼロ割り当ての `stackalloc` と同一にはならないことを示しています。スタック割り当てを確実に保証したい場合は、明示的な `stackalloc` を優先してください。

