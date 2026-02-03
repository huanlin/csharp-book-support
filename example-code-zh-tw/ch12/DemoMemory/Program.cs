// 示範 Memory<T> 與 MemoryPool<T>
using System.Buffers;

Console.WriteLine("=== Memory<T> 與非同步操作 ===\n");

// --------------------------------------------------------------
// 1. Memory<T> 的使用
// --------------------------------------------------------------
Console.WriteLine("1. Memory<T> 的基本操作");
Console.WriteLine(new string('-', 40));

await ProcessDataAsync();

// --------------------------------------------------------------
// 2. MemoryPool<T> 的使用
// --------------------------------------------------------------
Console.WriteLine("\n2. MemoryPool<T> 租借");
Console.WriteLine(new string('-', 40));

await ProcessWithPoolAsync();

Console.WriteLine("\n=== 範例結束 ===");


static async Task ProcessDataAsync()
{
    byte[] data = { 1, 2, 3, 4, 5 };
    var processor = new DataProcessor();
    
    Console.WriteLine("開始處理資料...");
    await processor.ProcessAsync(data); // 隱式轉換為 Memory<byte>
    Console.WriteLine("處理完成");
}

static async Task ProcessWithPoolAsync()
{
    // 從共用池租借記憶體
    using (IMemoryOwner<byte> owner = MemoryPool<byte>.Shared.Rent(10))
    {
        Memory<byte> buffer = owner.Memory;
        
        // 填入一些資料
        for (int i = 0; i < buffer.Length; i++)
        {
            buffer.Span[i] = (byte)(i * 10);
        }
        
        Console.WriteLine($"租借了 {buffer.Length} bytes");
        
        var processor = new DataProcessor();
        await processor.ProcessAsync(buffer);
        
        // using 區塊結束時會自動歸還記憶體 (Return)
    }
    Console.WriteLine("記憶體已歸還");
}


// --------------------------------------------------------------
// 模擬處理器
// --------------------------------------------------------------

public class DataProcessor
{
    // field 可以儲存 Memory<byte>，但不能儲存 Span<byte>
    // 這是 Memory<T> 最主要的用途：跨越非同步方法或長期儲存
    private Memory<byte> _buffer; 

    public async Task ProcessAsync(Memory<byte> data)
    {
        _buffer = data;
        
        // 第一次操作：轉為 Span
        // 注意：Span 只能在當前的同步區塊中使用
        Console.WriteLine($"[同步] 處裡前三個 byte: {_buffer.Span[0]}, {_buffer.Span[1]}, {_buffer.Span[2]}");

        Console.WriteLine("[非同步] 等待 I/O (100ms)...");
        await Task.Delay(100);
        
        // 第二次操作：再次轉為 Span
        // 由於 _buffer 是 Memory<T> (儲存在 Heap)，它在 await 之後仍然有效
        if (_buffer.Length >= 3)
        {
             _buffer.Span[0] = 255;
             Console.WriteLine($"[同步] 修改後第一個 byte: {_buffer.Span[0]}");
        }
    }
}
