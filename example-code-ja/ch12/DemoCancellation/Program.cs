// デモ: 非同期キャンセル

Console.WriteLine("=== 非同期キャンセル機構 ===\n");

using var cts = new CancellationTokenSource();

// 3 秒後に自動キャンセル
Console.WriteLine("3 秒後に自動キャンセルを設定...");
cts.CancelAfter(TimeSpan.FromSeconds(3));

try 
{
    // Token を渡す
    await DoWorkAsync(cts.Token);
}
catch (OperationCanceledException)
{
    Console.WriteLine("\n[捕捉] 操作がキャンセルされました（OperationCanceledException）");
}
catch (Exception ex)
{
    Console.WriteLine($"[エラー] {ex.Message}");
}

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// 長時間処理のシミュレーション
// ============================================================

async Task DoWorkAsync(CancellationToken cancellationToken = default)
{
    Console.WriteLine("処理開始...");
    
    for (int i = 1; i <= 10; i++)
    {
        // 1. キャンセル要求を確認
        // 要求済みなら OperationCanceledException を送出
        cancellationToken.ThrowIfCancellationRequested();

        Console.Write($"進捗: {i * 10}% ");

        // 2. 下位 API（Task.Delay など）にも Token を渡す
        // キャンセルされた場合、Task.Delay も OperationCanceledException を送出
        await Task.Delay(1000, cancellationToken);
    }
    
    Console.WriteLine("\n処理完了!");
}
