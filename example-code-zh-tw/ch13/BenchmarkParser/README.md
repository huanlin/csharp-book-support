# BenchmarkParser

此專案為第 13 章 13.1 節之字串解析效能微基準測試（micro-benchmark），比較傳統 `string.Substring` 搭配 `int.Parse`（造成堆積記憶體配置）與 `ReadOnlySpan<char>` 切片直接解析（零配置）的效能與記憶體開銷。

## 執行方式

請在專案目錄下以 Release 模式執行：

```bash
dotnet run -c Release
```

## 量測環境與原始報告

- **作業系統**：Windows 11 (10.0.26200)
- **處理器**：12th Gen Intel Core i7-12700H 2.30GHz (14 核心、20 執行緒)
- **.NET SDK**：10.0.303
- **執行階段**：.NET 10.0.11 (X64 RyuJIT x86-64-v3, Concurrent Workstation GC)
- **BenchmarkDotNet**：v0.15.8

### 量測結果

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

> **說明**：書稿表格中的 `ParseLegacy` 與 `ParseModern` 即對應此處的 `SubstringParsing` 與 `SpanParsing`。在不同硬體環境上執行時，時間絕對值會有所增減，請以「SpanParsing 完全消除記憶體配置（0 B vs 32 B）」為主要觀察指標。

