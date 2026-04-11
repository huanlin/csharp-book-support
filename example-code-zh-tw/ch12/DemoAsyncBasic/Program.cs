// 示範非同步程式設計的基本觀念

Console.WriteLine("=== 非同步程式設計基礎 ===\n");

// --------------------------------------------------------------
// 1. 基本的 async/await
// --------------------------------------------------------------
Console.WriteLine("1. 模擬 I/O 操作 (Task.Delay)");
Console.WriteLine(new string('-', 40));
Console.WriteLine($"呼叫前: {DescribeExecutionEnvironment()}");

await DownloadFileAsync("file1.txt");

// --------------------------------------------------------------
// 2. CPU 密集型操作 (Task.Run)
// --------------------------------------------------------------
Console.WriteLine("\n2. CPU 密集型操作 (Task.Run)");
Console.WriteLine(new string('-', 40));

Console.WriteLine($"Task.Run 前: {DescribeExecutionEnvironment()}");

// 將繁重運算排入 thread pool 執行
await Task.Run(() => LongRunningCalculation());

Console.WriteLine($"Task.Run 後: {DescribeExecutionEnvironment()}");

// --------------------------------------------------------------
// 3. ConfigureAwait(false)
// --------------------------------------------------------------
Console.WriteLine("\n3. Library 模式 (ConfigureAwait)");
Console.WriteLine(new string('-', 40));
Console.WriteLine("Console App 預設沒有 SynchronizationContext，這裡重點是語意，不是觀察執行緒一定改變。");

await DoLibraryWorkAsync();

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 模擬方法
// ============================================================

async Task DownloadFileAsync(string fileName)
{
    Console.WriteLine($"開始下載 {fileName}... ({DescribeExecutionEnvironment()})");
    
    // 模擬 I/O 等待 (非阻塞)
    await Task.Delay(1000);
    
    Console.WriteLine($"下載完成: {fileName} ({DescribeExecutionEnvironment()})");
}

void LongRunningCalculation()
{
    Console.WriteLine($"計算中... ({DescribeExecutionEnvironment()})");
    
    // 模擬 CPU 運算
    double result = 0;
    for (int i = 0; i < 10000000; i++)
    {
        result += Math.Sqrt(i);
    }
}

async Task DoLibraryWorkAsync()
{
    Console.WriteLine($"Library 工作開始... ({DescribeExecutionEnvironment()})");
    
    // 在通用 Library 內部通常不需要特地排回原本的 Context
    await Task.Delay(500).ConfigureAwait(false);
    
    Console.WriteLine($"Library 工作結束 ({DescribeExecutionEnvironment()})");
}

string DescribeExecutionEnvironment()
{
    var syncContext = SynchronizationContext.Current?.GetType().Name ?? "<null>";
    return $"Thread={Environment.CurrentManagedThreadId}, SyncContext={syncContext}";
}
