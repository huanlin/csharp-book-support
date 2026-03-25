// デモ: 非同期プログラミングの基本概念

Console.WriteLine("=== 非同期プログラミング基礎 ===\n");

// --------------------------------------------------------------
// 1. 基本の async/await
// --------------------------------------------------------------
Console.WriteLine("1. I/O 操作のシミュレーション（Task.Delay）");
Console.WriteLine(new string('-', 40));
Console.WriteLine($"呼び出し前: {DescribeExecutionEnvironment()}");

await DownloadFileAsync("file1.txt");

// --------------------------------------------------------------
// 2. CPU バウンド処理（Task.Run）
// --------------------------------------------------------------
Console.WriteLine("\n2. CPU バウンド処理（Task.Run）");
Console.WriteLine(new string('-', 40));

Console.WriteLine($"Task.Run 前: {DescribeExecutionEnvironment()}");

// 重い計算を thread pool に投入
await Task.Run(() => LongRunningCalculation());

Console.WriteLine($"Task.Run 後: {DescribeExecutionEnvironment()}");

// --------------------------------------------------------------
// 3. ConfigureAwait(false)
// --------------------------------------------------------------
Console.WriteLine("\n3. ライブラリパターン（ConfigureAwait）");
Console.WriteLine(new string('-', 40));
Console.WriteLine("Console App には既定で SynchronizationContext がないため、ここでの主眼はスレッド切り替えの観察ではなく意味の理解です。");

await DoLibraryWorkAsync();

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// シミュレーション用メソッド
// ============================================================

async Task DownloadFileAsync(string fileName)
{
    Console.WriteLine($"ダウンロード開始: {fileName}...（{DescribeExecutionEnvironment()}）");
    
    // I/O 待機をシミュレーション（スレッドはブロックしない）
    await Task.Delay(1000);
    
    Console.WriteLine($"ダウンロード完了: {fileName}（{DescribeExecutionEnvironment()}）");
}

void LongRunningCalculation()
{
    Console.WriteLine($"計算中...（{DescribeExecutionEnvironment()}）");
    
    // CPU 計算をシミュレーション
    double result = 0;
    for (int i = 0; i < 10000000; i++)
    {
        result += Math.Sqrt(i);
    }
}

async Task DoLibraryWorkAsync()
{
    Console.WriteLine($"ライブラリ処理開始...（{DescribeExecutionEnvironment()}）");
    
    // 汎用ライブラリ内部では通常、元の context へわざわざ戻す必要がない
    await Task.Delay(500).ConfigureAwait(false);
    
    Console.WriteLine($"ライブラリ処理完了（{DescribeExecutionEnvironment()}）");
}

string DescribeExecutionEnvironment()
{
    var syncContext = SynchronizationContext.Current?.GetType().Name ?? "<null>";
    return $"Thread={Environment.CurrentManagedThreadId}, SyncContext={syncContext}";
}
