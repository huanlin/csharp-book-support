using System;

Console.WriteLine("=== DemoAsyncStreams ===");

await foreach (var number in GenerateSequenceAsync())
{
    Console.WriteLine($"Received {number} at {DateTime.Now:HH:mm:ss.fff}");
}

Console.WriteLine("Done!");

static async IAsyncEnumerable<int> GenerateSequenceAsync()
{
    for (int i = 0; i < 5; i++)
    {
        await Task.Delay(500); // Simulate asynchronous work
        yield return i;
    }
}
