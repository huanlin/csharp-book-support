# BenchmarkAllocation

此專案為第 13 章 13.6 節之暫存記憶體配置微基準測試（micro-benchmark），比較處理 128 個整數時，堆積配置（`new int[128]`）、堆疊配置（`stackalloc int[128]`）以及集合運算式指派至 `Span<int>`（`Span<int> span = [.. source]`）的耗時與記憶體配置行為。

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


| Method                   | Mean      | Error    | StdDev   | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|------------------------- |----------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
| HeapAllocation           | 120.54 ns | 2.081 ns | 1.947 ns |  1.00 |    0.02 | 0.0427 |     536 B |        1.00 |
| StackAllocation          |  84.63 ns | 0.702 ns | 0.656 ns |  0.70 |    0.01 |      - |         - |        0.00 |
| CollectionExpressionSpan | 120.53 ns | 1.811 ns | 1.694 ns |  1.00 |    0.02 | 0.0427 |     536 B |        1.00 |
```

> **說明**：本測試凸顯集合運算式指派給 `Span<T>` 時，編譯器在此情境下仍進行了堆積配置（536 B），並非必然等同於零配置的 `stackalloc`。因此，若有明確的堆疊配置需求，應優先採用語意直接的 `stackalloc`。

