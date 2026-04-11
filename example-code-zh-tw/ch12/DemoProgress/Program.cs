// 示範進度報告 (IProgress<T>)

Console.WriteLine("=== 進度報告機制 ===\n");

// 建立 Progress<T> 物件，並定義當進度更新時要執行的 Callback
// 注意：在 UI 程式中，這個 Callback 會自動在 UI 執行緒上執行
var progress = new Progress<int>(percent => 
{
    Console.Write($"\r目前進度: {percent}%   "); // \r 回到行首覆蓋輸出
});

Console.WriteLine("開始執行...");

await ProcessAsync(progress);

Console.WriteLine("\n\n執行完成！");

// ============================================================
// 模擬長時間工作
// ============================================================

async Task ProcessAsync(IProgress<int> progress)
{
    // 模擬 CPU 密集工作丟到背景
    await Task.Run(() =>
    {
        for (int i = 0; i <= 100; i += 10)
        {
            // 報告進度
            // 如果 progress 為 null，這行就不會執行 (安全)
            progress?.Report(i);
            
            // 模擬工作耗時
            Thread.Sleep(500);
        }
    });
}
