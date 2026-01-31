// 示範非同步程式設計的基本觀念

Console.WriteLine("=== 非同步程式設計基礎 ===\n");

// --------------------------------------------------------------
// 1. 基本的 async/await
// --------------------------------------------------------------
Console.WriteLine("1. 模擬 I/O 操作 (Task.Delay)");
Console.WriteLine(new string('-', 40));

await DownloadFileAsync("file1.txt");

// --------------------------------------------------------------
// 2. CPU 密集型操作 (Task.Run)
// --------------------------------------------------------------
Console.WriteLine("\n2. CPU 密集型操作 (Task.Run)");
Console.WriteLine(new string('-', 40));

Console.WriteLine($"主執行緒 ID: {Environment.CurrentManagedThreadId}");

// 將繁重運算丟到背景執行緒
await Task.Run(() => LongRunningCalculation());

Console.WriteLine("計算完成，回到主執行緒");

// --------------------------------------------------------------
// 3. ConfigureAwait(false)
// --------------------------------------------------------------
Console.WriteLine("\n3. Library 模式 (ConfigureAwait)");
Console.WriteLine(new string('-', 40));

// 在 Library 中通常不需要回到原本的 Context
await DoLibraryWorkAsync().ConfigureAwait(false);

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 模擬方法
// ============================================================

async Task DownloadFileAsync(string fileName)
{
    Console.WriteLine($"開始下載 {fileName}...");
    
    // 模擬 I/O 等待 (非阻塞)
    await Task.Delay(1000);
    
    Console.WriteLine($"下載完成: {fileName}");
}

void LongRunningCalculation()
{
    Console.WriteLine($"計算中... (執行緒 ID: {Environment.CurrentManagedThreadId})");
    
    // 模擬 CPU 運算
    double result = 0;
    for (int i = 0; i < 10000000; i++)
    {
        result += Math.Sqrt(i);
    }
}

async Task DoLibraryWorkAsync()
{
    Console.WriteLine("Library 工作開始...");
    await Task.Delay(500).ConfigureAwait(false);
    Console.WriteLine("Library 工作結束 (可能在不同執行緒)");
}
