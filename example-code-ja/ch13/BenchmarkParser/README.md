# BenchmarkParser

このプロジェクトは、第 13 章 13.1 節の文字列パース性能に関するマイクロベンチマークです。従来の `string.Substring` と `int.Parse` によるヒープ割り当てを伴う手法と、`ReadOnlySpan<char>` のスライスによるゼロ割り当て手法の実行速度とメモリ割り当て量を比較します。

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


| Method           | Mean     | Error    | StdDev   | Median   | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|----------------- |---------:|---------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
| SubstringParsing | 18.22 ns | 1.224 ns | 3.610 ns | 20.11 ns |  1.06 |    0.36 | 0.0025 |      32 B |        1.00 |
| SpanParsing      | 12.93 ns | 0.190 ns | 0.178 ns | 13.01 ns |  0.75 |    0.20 |      - |         - |        0.00 |
```

> **説明**: 本書の表にある `ParseLegacy` と `ParseModern` は、ここでの `SubstringParsing` と `SpanParsing` に対応しています。ハードウェア環境によって時間の絶対値は変動しますが、主な着眼点は「SpanParsing がメモリ割り当てを完全に排除している（0 B vs 32 B）」点にあります。

