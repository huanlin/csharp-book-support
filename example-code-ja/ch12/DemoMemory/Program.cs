// デモ: Memory<T> と MemoryPool<T>
using System.Buffers;

Console.WriteLine("=== Memory<T> と非同期処理 ===\n");

// --------------------------------------------------------------
// 1. Memory<T> の利用
// --------------------------------------------------------------
Console.WriteLine("1. Memory<T> の基本操作");
Console.WriteLine(new string('-', 40));

await ProcessDataAsync();

// --------------------------------------------------------------
// 2. MemoryPool<T> の利用
// --------------------------------------------------------------
Console.WriteLine("\n2. MemoryPool<T> からのレンタル");
Console.WriteLine(new string('-', 40));

await ProcessWithPoolAsync();

Console.WriteLine("\n=== 例の終了 ===");


static async Task ProcessDataAsync()
{
    byte[] data = { 1, 2, 3, 4, 5 };
    var processor = new DataProcessor();
    
    Console.WriteLine("データ処理を開始...");
    await processor.ProcessAsync(data); // 暗黙的に Memory<byte> へ変換
    Console.WriteLine("処理完了");
}

static async Task ProcessWithPoolAsync()
{
    // 共有プールからメモリをレンタル
    using (IMemoryOwner<byte> owner = MemoryPool<byte>.Shared.Rent(10))
    {
        Memory<byte> buffer = owner.Memory;
        
        // データを埋める
        for (int i = 0; i < buffer.Length; i++)
        {
            buffer.Span[i] = (byte)(i * 10);
        }
        
        Console.WriteLine($"{buffer.Length} bytes をレンタル");
        
        var processor = new DataProcessor();
        await processor.ProcessAsync(buffer);
        
        // using ブロック終了時に自動返却される
    }
    Console.WriteLine("メモリを返却しました");
}


// --------------------------------------------------------------
// プロセッサのシミュレーション
// --------------------------------------------------------------

public class DataProcessor
{
    // フィールドには Memory<byte> は保持できるが、Span<byte> は保持できない
    // これが Memory<T> の主用途: async を跨ぐ／長期保持する
    private Memory<byte> _buffer; 

    public async Task ProcessAsync(Memory<byte> data)
    {
        _buffer = data;
        
        // 1 回目処理: Span に変換して利用
        // 注: Span は現在の同期ブロック内でのみ有効
        Console.WriteLine($"[Sync] 最初の 3 バイトを処理: {_buffer.Span[0]}, {_buffer.Span[1]}, {_buffer.Span[2]}");

        Console.WriteLine("[Async] I/O 待機中 (100ms)...");
        await Task.Delay(100);
        
        // 2 回目処理: 再度 Span に変換
        // _buffer は Memory<T>（Heap 上）なので await 後も有効
        if (_buffer.Length >= 3)
        {
             _buffer.Span[0] = 255;
             Console.WriteLine($"[Sync] 先頭バイトを変更: {_buffer.Span[0]}");
        }
    }
}
