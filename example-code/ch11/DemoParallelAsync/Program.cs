// 示範平行執行與逾時處理

Console.WriteLine("=== 平行執行與 Task 組合 ===\n");

// --------------------------------------------------------------
// 1. 循序執行 (效率較差)
// --------------------------------------------------------------
Console.WriteLine("1. 循序執行 (Sequential)");
Console.WriteLine(new string('-', 40));

var swatch = System.Diagnostics.Stopwatch.StartNew();

await DoTaskAsync("Task A", 1000);
await DoTaskAsync("Task B", 1000);

swatch.Stop();
Console.WriteLine($"循序執行總耗時: {swatch.ElapsedMilliseconds} ms");


// --------------------------------------------------------------
// 2. 平行執行 (Task.WhenAll)
// --------------------------------------------------------------
Console.WriteLine("\n2. 平行執行 (Task.WhenAll)");
Console.WriteLine(new string('-', 40));

swatch.Restart();

var t1 = DoTaskAsync("Task C", 1000);
var t2 = DoTaskAsync("Task D", 1000);

await Task.WhenAll(t1, t2);

swatch.Stop();
Console.WriteLine($"平行執行總耗時: {swatch.ElapsedMilliseconds} ms");


// --------------------------------------------------------------
// 3. 逾時處理 (Task.WhenAny)
// --------------------------------------------------------------
Console.WriteLine("\n3. 逾時處理 (Task.WhenAny)");
Console.WriteLine(new string('-', 40));

try
{
    await GetDataWithTimeoutAsync("http://slow-api.com", 2000);
}
catch (TimeoutException ex)
{
    Console.WriteLine($"捕獲例外: {ex.Message}");
}

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 模擬方法
// ============================================================

async Task DoTaskAsync(string name, int delay)
{
    Console.WriteLine($"{name} 開始...");
    await Task.Delay(delay);
    Console.WriteLine($"{name} 完成");
}

async Task<string> GetDataWithTimeoutAsync(string url, int timeoutMs)
{
    Console.WriteLine($"請求 {url} (逾時限制: {timeoutMs}ms)...");

    // 模擬一個會跑很久的任務 (3秒)
    Task<string> dataTask = DownloadDataAsync(url);
    Task timeoutTask = Task.Delay(timeoutMs);
    
    // 等待任一任務完成
    Task winner = await Task.WhenAny(dataTask, timeoutTask);
    
    if (winner == timeoutTask)
    {
        throw new TimeoutException("作業逾時！");
    }
    
    // 確保這裡不會拋出例外 (雖然 dataTask 已完成，但可能是失敗的)
    return await dataTask;
}

async Task<string> DownloadDataAsync(string url)
{
    await Task.Delay(3000); // 模擬慢速網路 (3秒)
    return "Data content";
}
