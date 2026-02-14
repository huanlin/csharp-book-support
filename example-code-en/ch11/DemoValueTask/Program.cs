// Demo: When to use ValueTask (Caching scenarios)

Console.WriteLine("=== ValueTask Performance Optimization ===\n");

var service = new CachedDataService();

// --------------------------------------------------------------
// First call: Cache Miss
// --------------------------------------------------------------
Console.WriteLine("1. First call (Cache Miss)");

// Convert ValueTask to Task to observe state (in practice, usually just await directly)
Task<int> t1 = service.GetDataAsync(1).AsTask(); 

Console.WriteLine($"Task completed? {t1.IsCompleted}");
int val1 = await t1;
Console.WriteLine($"Data retrieved: {val1}");


// --------------------------------------------------------------
// Second call: Cache Hit
// --------------------------------------------------------------
Console.WriteLine("\n2. Second call (Cache Hit)");

ValueTask<int> vt2 = service.GetDataAsync(1);

Console.WriteLine($"ValueTask completed? {vt2.IsCompleted}");
int val2 = await vt2;
Console.WriteLine($"Data retrieved: {val2}");

Console.WriteLine("\n=== Example End ===");

// ============================================================
// Simulating Cache Service
// ============================================================

public class CachedDataService
{
    private Dictionary<int, int> _cache = new();

    public ValueTask<int> GetDataAsync(int id)
    {
        // 1. Check cache (Hot Path)
        // If hit, return result directly without allocating a Task object
        if (_cache.TryGetValue(id, out int value))
        {
            Console.WriteLine("-> Cache hit! Returning directly");
            return new ValueTask<int>(value);
        }

        // 2. Cache miss (Cold Path)
        // Perform real asynchronous I/O, which will create a Task
        Console.WriteLine("-> Cache miss, reading from database...");
        return new ValueTask<int>(FetchFromDbAsync(id));
    }

    private async Task<int> FetchFromDbAsync(int id)
    {
        await Task.Delay(500); // Simulate I/O
        int result = id * 100;
        _cache[id] = result;   // Update cache
        return result;
    }
}
