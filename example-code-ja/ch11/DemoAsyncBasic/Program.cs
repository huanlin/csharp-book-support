// デモ: 非同期プログラミングの基本概念

Console.WriteLine("=== 非同期プログラミング基礎 ===\n");

// --------------------------------------------------------------
// 1. 基本の async/await
// --------------------------------------------------------------
Console.WriteLine("1. I/O 操作のシミュレーション（Task.Delay）");
Console.WriteLine(new string('-', 40));

await DownloadFileAsync("file1.txt");

// --------------------------------------------------------------
// 2. CPU バウンド処理（Task.Run）
// --------------------------------------------------------------
Console.WriteLine("\n2. CPU バウンド処理（Task.Run）");
Console.WriteLine(new string('-', 40));

Console.WriteLine($"メインスレッド ID: {Environment.CurrentManagedThreadId}");

// 重い計算をバックグラウンドスレッドへオフロード
await Task.Run(() => LongRunningCalculation());

Console.WriteLine("計算完了、メインスレッドへ復帰");

// --------------------------------------------------------------
// 3. ConfigureAwait(false)
// --------------------------------------------------------------
Console.WriteLine("\n3. ライブラリパターン（ConfigureAwait）");
Console.WriteLine(new string('-', 40));

// ライブラリでは通常、元のコンテキストへ戻る必要がない
await DoLibraryWorkAsync().ConfigureAwait(false);

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// シミュレーション用メソッド
// ============================================================

async Task DownloadFileAsync(string fileName)
{
    Console.WriteLine($"ダウンロード開始: {fileName}...");
    
    // I/O 待機をシミュレーション（スレッドはブロックしない）
    await Task.Delay(1000);
    
    Console.WriteLine($"ダウンロード完了: {fileName}");
}

void LongRunningCalculation()
{
    Console.WriteLine($"計算中...（スレッド ID: {Environment.CurrentManagedThreadId}）");
    
    // CPU 計算をシミュレーション
    double result = 0;
    for (int i = 0; i < 10000000; i++)
    {
        result += Math.Sqrt(i);
    }
}

async Task DoLibraryWorkAsync()
{
    Console.WriteLine("ライブラリ処理開始...");
    await Task.Delay(500).ConfigureAwait(false);
    Console.WriteLine("ライブラリ処理完了（別スレッドの可能性あり）");
}
