// Demo: Asynchronous Cancellation

Console.WriteLine("=== Asynchronous Cancellation Mechanism ===\n");

using var cts = new CancellationTokenSource();

// Set up automatic cancellation after 3 seconds
Console.WriteLine("Setting automatic cancellation after 3 seconds...");
cts.CancelAfter(TimeSpan.FromSeconds(3));

try 
{
    // Pass the Token
    await DoWorkAsync(cts.Token);
}
catch (OperationCanceledException)
{
    Console.WriteLine("\n[Exception Caught] Operation cancelled (OperationCanceledException)");
}
catch (Exception ex)
{
    Console.WriteLine($"[Error] {ex.Message}");
}

Console.WriteLine("\n=== Example End ===");

// ============================================================
// Simulating Long-Running Work
// ============================================================

async Task DoWorkAsync(CancellationToken cancellationToken = default)
{
    Console.WriteLine("Work starting...");
    
    for (int i = 1; i <= 10; i++)
    {
        // 1. Check if cancellation was requested
        // This will throw OperationCanceledException
        cancellationToken.ThrowIfCancellationRequested();

        Console.Write($"Progress: {i * 10}% ");

        // 2. Pass the Token to underlying APIs (like Task.Delay)
        // If cancelled, Task.Delay will also throw OperationCanceledException
        await Task.Delay(1000, cancellationToken);
    }
    
    Console.WriteLine("\nWork completed!");
}
