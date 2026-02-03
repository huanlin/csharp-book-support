// 示範非同步取消 (Cancellation)

Console.WriteLine("=== 非同步取消機制 ===\n");

using var cts = new CancellationTokenSource();

// 設定 3 秒後自動取消
Console.WriteLine("設定 3 秒後自動取消...");
cts.CancelAfter(TimeSpan.FromSeconds(3));

try 
{
    // 傳遞 Token
    await DoWorkAsync(cts.Token);
}
catch (OperationCanceledException)
{
    Console.WriteLine("\n[捕獲例外] 作業已取消 (OperationCanceledException)");
}
catch (Exception ex)
{
    Console.WriteLine($"[錯誤] {ex.Message}");
}

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 模擬長時間工作
// ============================================================

async Task DoWorkAsync(CancellationToken cancellationToken = default)
{
    Console.WriteLine("工作開始...");
    
    for (int i = 1; i <= 10; i++)
    {
        // 1. 檢查是否被取消
        // 這裡會拋出 OperationCanceledException
        cancellationToken.ThrowIfCancellationRequested();

        Console.Write($"進度: {i * 10}% ");

        // 2. 將 Token 傳遞給底層 API (如 Task.Delay)
        // 如果取消，Task.Delay 也會拋出 OperationCanceledException
        await Task.Delay(1000, cancellationToken);
    }
    
    Console.WriteLine("\n工作完成！");
}
