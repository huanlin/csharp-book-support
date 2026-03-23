// デモ: 並列実行とタイムアウト制御

Console.WriteLine("=== 並列実行と Task 合成 ===\n");

// --------------------------------------------------------------
// 1. 逐次実行（効率が低い）
// --------------------------------------------------------------
Console.WriteLine("1. 逐次実行（Sequential）");
Console.WriteLine(new string('-', 40));

var swatch = System.Diagnostics.Stopwatch.StartNew();

await DoTaskAsync("Task A", 1000);
await DoTaskAsync("Task B", 1000);

swatch.Stop();
Console.WriteLine($"逐次実行の合計時間: {swatch.ElapsedMilliseconds} ms");


// --------------------------------------------------------------
// 2. 並列実行（Task.WhenAll）
// --------------------------------------------------------------
Console.WriteLine("\n2. 並列実行（Task.WhenAll）");
Console.WriteLine(new string('-', 40));

swatch.Restart();

var t1 = DoTaskAsync("Task C", 1000);
var t2 = DoTaskAsync("Task D", 1000);

await Task.WhenAll(t1, t2);

swatch.Stop();
Console.WriteLine($"並列実行の合計時間: {swatch.ElapsedMilliseconds} ms");


// --------------------------------------------------------------
// 3. タイムアウト制御（Task.WhenAny）
// --------------------------------------------------------------
Console.WriteLine("\n3. タイムアウト制御（Task.WhenAny）");
Console.WriteLine(new string('-', 40));

try
{
    await GetDataWithTimeoutAsync("http://slow-api.com", 2000);
}
catch (TimeoutException ex)
{
    Console.WriteLine($"例外を捕捉: {ex.Message}");
}


// --------------------------------------------------------------
// 4. 1 件ずつ処理（Task.WhenEach） - .NET 9+
// --------------------------------------------------------------
Console.WriteLine("\n4. 1 件ずつ処理（Task.WhenEach） - .NET 9+");
Console.WriteLine(new string('-', 40));

var tasks = new List<Task<int>>();
for (int i = 1; i <= 3; i++)
{
    tasks.Add(ProcessItemAsync(i));
}

// Task.WhenEach を await foreach で処理
await foreach (var task in Task.WhenEach(tasks))
{
    try 
    {
        var result = await task;
        Console.WriteLine($"処理完了: Result={result}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"処理失敗: {ex.Message}");
    }
}

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// シミュレーション用メソッド
// ============================================================

async Task DoTaskAsync(string name, int delay)
{
    Console.WriteLine($"{name} 開始...");
    await Task.Delay(delay);
    Console.WriteLine($"{name} 完了");
}

async Task<string> GetDataWithTimeoutAsync(string url, int timeoutMs)
{
    Console.WriteLine($"{url} をリクエスト（タイムアウト: {timeoutMs}ms）...");

    // 長時間処理をシミュレーション（3 秒）
    Task<string> dataTask = DownloadDataAsync(url);
    Task timeoutTask = Task.Delay(timeoutMs);
    
    // どちらか先に終わるまで待つ
    Task winner = await Task.WhenAny(dataTask, timeoutTask);
    
    if (winner == timeoutTask)
    {
        throw new TimeoutException("操作がタイムアウトしました。");
    }
    
    // ここで結果を取得する。dataTask が失敗していれば、この await で例外が再送出される
    return await dataTask;
}

async Task<string> DownloadDataAsync(string url)
{
    await Task.Delay(3000); // 低速ネットワークをシミュレーション（3 秒）
    return "データ内容";
}

async Task<int> ProcessItemAsync(int id)
{
    var delay = Random.Shared.Next(500, 1500);
    await Task.Delay(delay);
    return id * 10;
}
