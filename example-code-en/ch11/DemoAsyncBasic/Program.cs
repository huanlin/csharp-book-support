// Demo: Basic concepts of Asynchronous Programming

Console.WriteLine("=== Asynchronous Programming Basics ===\n");

// --------------------------------------------------------------
// 1. Basic async/await
// --------------------------------------------------------------
Console.WriteLine("1. Simulating I/O operations (Task.Delay)");
Console.WriteLine(new string('-', 40));

await DownloadFileAsync("file1.txt");

// --------------------------------------------------------------
// 2. CPU-bound operations (Task.Run)
// --------------------------------------------------------------
Console.WriteLine("\n2. CPU-bound operations (Task.Run)");
Console.WriteLine(new string('-', 40));

Console.WriteLine($"Main Thread ID: {Environment.CurrentManagedThreadId}");

// Offload heavy calculation to a background thread
await Task.Run(() => LongRunningCalculation());

Console.WriteLine("Calculation complete, back to main thread");

// --------------------------------------------------------------
// 3. ConfigureAwait(false)
// --------------------------------------------------------------
Console.WriteLine("\n3. Library Pattern (ConfigureAwait)");
Console.WriteLine(new string('-', 40));

// In libraries, we usually don't need to return to the original context
await DoLibraryWorkAsync().ConfigureAwait(false);

Console.WriteLine("\n=== Example End ===");

// ============================================================
// Simulating methods
// ============================================================

async Task DownloadFileAsync(string fileName)
{
    Console.WriteLine($"Starting download {fileName}...");
    
    // Simulating I/O wait (non-blocking)
    await Task.Delay(1000);
    
    Console.WriteLine($"Download complete: {fileName}");
}

void LongRunningCalculation()
{
    Console.WriteLine($"Calculating... (Thread ID: {Environment.CurrentManagedThreadId})");
    
    // Simulating CPU computation
    double result = 0;
    for (int i = 0; i < 10000000; i++)
    {
        result += Math.Sqrt(i);
    }
}

async Task DoLibraryWorkAsync()
{
    Console.WriteLine("Library work starting...");
    await Task.Delay(500).ConfigureAwait(false);
    Console.WriteLine("Library work finished (might be on a different thread)");
}
