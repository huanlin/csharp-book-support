// Demo: Basic concepts of Asynchronous Programming

Console.WriteLine("=== Asynchronous Programming Basics ===\n");

// --------------------------------------------------------------
// 1. Basic async/await
// --------------------------------------------------------------
Console.WriteLine("1. Simulating I/O operations (Task.Delay)");
Console.WriteLine(new string('-', 40));
Console.WriteLine($"Before call: {DescribeExecutionEnvironment()}");

await DownloadFileAsync("file1.txt");

// --------------------------------------------------------------
// 2. CPU-bound operations (Task.Run)
// --------------------------------------------------------------
Console.WriteLine("\n2. CPU-bound operations (Task.Run)");
Console.WriteLine(new string('-', 40));

Console.WriteLine($"Before Task.Run: {DescribeExecutionEnvironment()}");

// Queue heavy computation to the thread pool
await Task.Run(() => LongRunningCalculation());

Console.WriteLine($"After Task.Run: {DescribeExecutionEnvironment()}");

// --------------------------------------------------------------
// 3. ConfigureAwait(false)
// --------------------------------------------------------------
Console.WriteLine("\n3. Library Pattern (ConfigureAwait)");
Console.WriteLine(new string('-', 40));
Console.WriteLine("Console apps have no SynchronizationContext by default, so the point here is semantics, not proving a thread switch.");

await DoLibraryWorkAsync();

Console.WriteLine("\n=== Example End ===");

// ============================================================
// Simulating methods
// ============================================================

async Task DownloadFileAsync(string fileName)
{
    Console.WriteLine($"Starting download {fileName}... ({DescribeExecutionEnvironment()})");
    
    // Simulating I/O wait (non-blocking)
    await Task.Delay(1000);
    
    Console.WriteLine($"Download complete: {fileName} ({DescribeExecutionEnvironment()})");
}

void LongRunningCalculation()
{
    Console.WriteLine($"Calculating... ({DescribeExecutionEnvironment()})");
    
    // Simulating CPU computation
    double result = 0;
    for (int i = 0; i < 10000000; i++)
    {
        result += Math.Sqrt(i);
    }
}

async Task DoLibraryWorkAsync()
{
    Console.WriteLine($"Library work starting... ({DescribeExecutionEnvironment()})");
    
    // In general-purpose library code, we usually don't need to post back to the original context
    await Task.Delay(500).ConfigureAwait(false);
    
    Console.WriteLine($"Library work finished ({DescribeExecutionEnvironment()})");
}

string DescribeExecutionEnvironment()
{
    var syncContext = SynchronizationContext.Current?.GetType().Name ?? "<null>";
    return $"Thread={Environment.CurrentManagedThreadId}, SyncContext={syncContext}";
}
