# BenchmarkArrayPool

此專案為第 13 章 13.5 節之陣列配置與池化微基準測試（micro-benchmark），比較傳統直接以 `new byte[4096]` 配置堆積陣列與使用 `ArrayPool<byte>.Shared.Rent/Return` 租借歸還的耗時與記憶體配置。

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


| Method              | Mean      | Error    | StdDev   | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|-------------------- |----------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
| NewArray            | 119.47 ns | 2.531 ns | 6.624 ns |  1.00 |    0.07 | 0.3281 |    4120 B |        1.00 |
| ArrayPoolRentReturn |  10.51 ns | 0.048 ns | 0.040 ns |  0.09 |    0.00 |      - |         - |        0.00 |
```

> **說明**：此基準測試僅量測單純的 `Rent` 與 `Return` 呼叫開銷，不包含陣列清零（`clearArray: true`）或任何資料處理與 I/O 操作；請勿將此微基準的數倍差距直接解讀為實際應用系統的整體加速倍數。使用 `ArrayPool<T>` 最關鍵的價值在於將 4,120 位元組的堆積配置降至 0，以顯著降低高頻調用下的 GC 回收壓力。

