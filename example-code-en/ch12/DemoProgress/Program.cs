// Demo: Progress Reporting (IProgress<T>)

Console.WriteLine("=== Progress Reporting Mechanism ===\n");

// Create a Progress<T> object and define the callback for when progress updates
// Note: In UI applications, this callback automatically executes on the UI thread
var progress = new Progress<int>(percent => 
{
    Console.Write($"\rCurrent Progress: {percent}%   "); // \r returns to start of line to overwrite output
});

Console.WriteLine("Starting execution...");

await ProcessAsync(progress);

Console.WriteLine("\n\nExecution completed!");

// ============================================================
// Simulating Long-Running Work
// ============================================================

async Task ProcessAsync(IProgress<int> progress)
{
    // Offload CPU-intensive work to background
    await Task.Run(() =>
    {
        for (int i = 0; i <= 100; i += 10)
        {
            // Report progress
            // If progress is null, this line won't execute (safe)
            progress?.Report(i);
            
            // Simulate work time
            Thread.Sleep(500);
        }
    });
}
