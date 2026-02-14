// Demo: Parallel Execution and Timeout Handling

Console.WriteLine("=== Parallel Execution and Task Composition ===\n");

// --------------------------------------------------------------
// 1. Sequential Execution (Less efficient)
// --------------------------------------------------------------
Console.WriteLine("1. Sequential Execution (Sequential)");
Console.WriteLine(new string('-', 40));

var swatch = System.Diagnostics.Stopwatch.StartNew();

await DoTaskAsync("Task A", 1000);
await DoTaskAsync("Task B", 1000);

swatch.Stop();
Console.WriteLine($"Sequential execution total time: {swatch.ElapsedMilliseconds} ms");


// --------------------------------------------------------------
// 2. Parallel Execution (Task.WhenAll)
// --------------------------------------------------------------
Console.WriteLine("\n2. Parallel Execution (Task.WhenAll)");
Console.WriteLine(new string('-', 40));

swatch.Restart();

var t1 = DoTaskAsync("Task C", 1000);
var t2 = DoTaskAsync("Task D", 1000);

await Task.WhenAll(t1, t2);

swatch.Stop();
Console.WriteLine($"Parallel execution total time: {swatch.ElapsedMilliseconds} ms");


// --------------------------------------------------------------
// 3. Timeout Handling (Task.WhenAny)
// --------------------------------------------------------------
Console.WriteLine("\n3. Timeout Handling (Task.WhenAny)");
Console.WriteLine(new string('-', 40));

try
{
    await GetDataWithTimeoutAsync("http://slow-api.com", 2000);
}
catch (TimeoutException ex)
{
    Console.WriteLine($"Caught exception: {ex.Message}");
}


// --------------------------------------------------------------
// 4. Processing One-by-One (Task.WhenEach) - .NET 9+
// --------------------------------------------------------------
Console.WriteLine("\n4. Processing One-by-One (Task.WhenEach) - .NET 9+");
Console.WriteLine(new string('-', 40));

var tasks = new List<Task<int>>();
for (int i = 1; i <= 3; i++)
{
    tasks.Add(ProcessItemAsync(i));
}

// Use await foreach with Task.WhenEach
await foreach (var task in Task.WhenEach(tasks))
{
    try 
    {
        var result = await task;
        Console.WriteLine($"Processing complete: Result={result}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Processing failed: {ex.Message}");
    }
}

Console.WriteLine("\n=== Example End ===");

// ============================================================
// Simulating Methods
// ============================================================

async Task DoTaskAsync(string name, int delay)
{
    Console.WriteLine($"{name} starting...");
    await Task.Delay(delay);
    Console.WriteLine($"{name} complete");
}

async Task<string> GetDataWithTimeoutAsync(string url, int timeoutMs)
{
    Console.WriteLine($"Requesting {url} (Timeout limit: {timeoutMs}ms)...");

    // Simulating a long-running task (3 seconds)
    Task<string> dataTask = DownloadDataAsync(url);
    Task timeoutTask = Task.Delay(timeoutMs);
    
    // Wait for any task to complete
    Task winner = await Task.WhenAny(dataTask, timeoutTask);
    
    if (winner == timeoutTask)
    {
        throw new TimeoutException("Operation timed out!");
    }
    
    // Ensure exception is not thrown here (although dataTask is complete, it might have failed)
    return await dataTask;
}

async Task<string> DownloadDataAsync(string url)
{
    await Task.Delay(3000); // Simulating slow network (3 seconds)
    return "Data content";
}

async Task<int> ProcessItemAsync(int id)
{
    var delay = Random.Shared.Next(500, 1500);
    await Task.Delay(delay);
    return id * 10;
}
