// 示範 ValueTask 的使用時機 (快取場景)

Console.WriteLine("=== ValueTask 效能優化 ===\n");

var service = new CachedDataService();

// --------------------------------------------------------------
// 第一次呼叫: 未命中快取
// --------------------------------------------------------------
Console.WriteLine("1. 第一次呼叫 (Cache Miss)");

// 將 ValueTask 轉為 Task 以便觀察狀態 (實務上通常直接 await)
Task<int> t1 = service.GetDataAsync(1).AsTask(); 

Console.WriteLine($"Task 是否已完成? {t1.IsCompleted}");
int val1 = await t1;
Console.WriteLine($"取得資料: {val1}");


// --------------------------------------------------------------
// 第二次呼叫: 命中快取
// --------------------------------------------------------------
Console.WriteLine("\n2. 第二次呼叫 (Cache Hit)");

ValueTask<int> vt2 = service.GetDataAsync(1);

Console.WriteLine($"ValueTask 是否已完成? {vt2.IsCompleted}");
int val2 = await vt2;
Console.WriteLine($"取得資料: {val2}");

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 模擬快取服務
// ============================================================

public class CachedDataService
{
    private Dictionary<int, int> _cache = new();

    public ValueTask<int> GetDataAsync(int id)
    {
        // 1. 檢查快取 (Hot Path)
        // 如果命中，直接回傳結果，無需配置 Task 物件
        if (_cache.TryGetValue(id, out int value))
        {
            Console.WriteLine("-> 命中快取！直接回傳");
            return new ValueTask<int>(value);
        }

        // 2. 快取未命中 (Cold Path)
        // 執行真正的非同步 I/O，這時會建立 Task
        Console.WriteLine("-> 快取未命中，讀取資料庫...");
        return new ValueTask<int>(FetchFromDbAsync(id));
    }

    private async Task<int> FetchFromDbAsync(int id)
    {
        await Task.Delay(500); // 模擬 I/O
        int result = id * 100;
        _cache[id] = result;   // 更新快取
        return result;
    }
}
