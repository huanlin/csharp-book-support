// デモ: ValueTask を使うべき場面（キャッシュシナリオ）

Console.WriteLine("=== ValueTask パフォーマンス最適化 ===\n");

var service = new CachedDataService();

// --------------------------------------------------------------
// 1 回目呼び出し: キャッシュミス
// --------------------------------------------------------------
Console.WriteLine("1. 1 回目呼び出し（キャッシュミス）");

// 状態確認のために Task へ変換する。再利用が必要な場合も、先に Task 化する
Task<int> t1 = service.GetDataAsync(1).AsTask(); 

Console.WriteLine($"Task 完了済み? {t1.IsCompleted}");
int val1 = await t1;
Console.WriteLine($"取得データ: {val1}");


// --------------------------------------------------------------
// 2 回目呼び出し: キャッシュヒット
// --------------------------------------------------------------
Console.WriteLine("\n2. 2 回目呼び出し（キャッシュヒット）");

// キャッシュヒット時は、直接 await するのが最も安全で一般的
int val2 = await service.GetDataAsync(1);
Console.WriteLine($"取得データ: {val2}");

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// キャッシュサービスのシミュレーション
// ============================================================

public class CachedDataService
{
    private Dictionary<int, int> _cache = new();

    public ValueTask<int> GetDataAsync(int id)
    {
        // 1. キャッシュ確認（Hot Path）
        // ヒット時は Task オブジェクトを割り当てず直接結果を返せる
        if (_cache.TryGetValue(id, out int value))
        {
            Console.WriteLine("-> キャッシュヒット! 直接返却");
            return new ValueTask<int>(value);
        }

        // 2. キャッシュミス（Cold Path）
        // 実際の非同期 I/O を実行するため Task が作られる
        Console.WriteLine("-> キャッシュミス、DB から読み込み...");
        return new ValueTask<int>(FetchFromDbAsync(id));
    }

    private async Task<int> FetchFromDbAsync(int id)
    {
        await Task.Delay(500); // I/O をシミュレーション
        int result = id * 100;
        _cache[id] = result;   // キャッシュ更新
        return result;
    }
}
