using System;

Console.WriteLine("=== DemoAsyncStreams ===");

await foreach (var number in GenerateSequenceAsync())
{
    Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} に {number} を受信");
}

Console.WriteLine("完了!");

static async IAsyncEnumerable<int> GenerateSequenceAsync()
{
    for (int i = 0; i < 5; i++)
    {
        await Task.Delay(500); // 非同期処理をシミュレーション
        yield return i;
    }
}
