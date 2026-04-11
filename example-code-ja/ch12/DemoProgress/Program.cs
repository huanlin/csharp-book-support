// デモ: 進捗報告（IProgress<T>）

Console.WriteLine("=== 進捗報告の仕組み ===\n");

// Progress<T> を作成し、進捗更新時のコールバックを定義
// 注: UI アプリではこのコールバックは自動的に UI スレッドで実行される
var progress = new Progress<int>(percent => 
{
    Console.Write($"\r現在の進捗: {percent}%   "); // \r で行頭に戻り上書き表示
});

Console.WriteLine("実行開始...");

await ProcessAsync(progress);

Console.WriteLine("\n\n実行完了!");

// ============================================================
// 長時間処理のシミュレーション
// ============================================================

async Task ProcessAsync(IProgress<int> progress)
{
    // CPU 負荷の高い処理をバックグラウンドへオフロード
    await Task.Run(() =>
    {
        for (int i = 0; i <= 100; i += 10)
        {
            // 進捗を報告
            // progress が null の場合はこの行は実行されない（安全）
            progress?.Report(i);
            
            // 処理時間をシミュレーション
            Thread.Sleep(500);
        }
    });
}
