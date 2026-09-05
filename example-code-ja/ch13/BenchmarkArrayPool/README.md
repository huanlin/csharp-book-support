# BenchmarkArrayPool

このプロジェクトは、第 13 章 13.5 節の配列割り当てとプーリングに関するマイクロベンチマークです。`new byte[4096]` による直接的なヒープ配列の割り当てと、`ArrayPool<byte>.Shared.Rent/Return` を使用したレンタル・返却の所要時間およびメモリ割り当て量を比較します。

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


| Method              | Mean      | Error    | StdDev   | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|-------------------- |----------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
| NewArray            | 119.47 ns | 2.531 ns | 6.624 ns |  1.00 |    0.07 | 0.3281 |    4120 B |        1.00 |
| ArrayPoolRentReturn |  10.51 ns | 0.048 ns | 0.040 ns |  0.09 |    0.00 |      - |         - |        0.00 |
```

> **説明**: このベンチマークは単なる `Rent` と `Return` の呼び出しオーバーヘッドのみを測定しており、配列のゼロクリア（`clearArray: true`）や実際のデータ処理、I/O 操作は含まれていません。このマイクロベンチマークの倍率を、実アプリケーション全体の高速化倍率としてそのまま受け取らないように注意してください。`ArrayPool<T>` を利用する最大の価値は、4,120 バイトのヒープ割り当てをゼロに抑え、高頻度な呼び出しにおける GC 負荷を大幅に軽減することにあります。

