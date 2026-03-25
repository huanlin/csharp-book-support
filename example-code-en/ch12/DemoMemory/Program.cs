// Demo: Memory<T> and MemoryPool<T>
using System.Buffers;

Console.WriteLine("=== Memory<T> and Asynchronous Operations ===\n");

// --------------------------------------------------------------
// 1. Using Memory<T>
// --------------------------------------------------------------
Console.WriteLine("1. Basic operations of Memory<T>");
Console.WriteLine(new string('-', 40));

await ProcessDataAsync();

// --------------------------------------------------------------
// 2. Using MemoryPool<T>
// --------------------------------------------------------------
Console.WriteLine("\n2. Renting from MemoryPool<T>");
Console.WriteLine(new string('-', 40));

await ProcessWithPoolAsync();

Console.WriteLine("\n=== Example End ===");


static async Task ProcessDataAsync()
{
    byte[] data = { 1, 2, 3, 4, 5 };
    var processor = new DataProcessor();
    
    Console.WriteLine("Starting data processing...");
    await processor.ProcessAsync(data); // Implicitly converted to Memory<byte>
    Console.WriteLine("Processing complete");
}

static async Task ProcessWithPoolAsync()
{
    // Rent memory from the shared pool
    using (IMemoryOwner<byte> owner = MemoryPool<byte>.Shared.Rent(10))
    {
        Memory<byte> buffer = owner.Memory;
        
        // Fill in some data
        for (int i = 0; i < buffer.Length; i++)
        {
            buffer.Span[i] = (byte)(i * 10);
        }
        
        Console.WriteLine($"Rented {buffer.Length} bytes");
        
        var processor = new DataProcessor();
        await processor.ProcessAsync(buffer);
        
        // Memory will be returned automatically when the using block ends
    }
    Console.WriteLine("Memory returned");
}


// --------------------------------------------------------------
// Simulating a processor
// --------------------------------------------------------------

public class DataProcessor
{
    // Fields can store Memory<byte>, but not Span<byte>
    // This is a common use of Memory<T>: across async methods or long-term storage
    private Memory<byte> _buffer; 

    public async Task ProcessAsync(Memory<byte> data)
    {
        _buffer = data;
        
        // First operation: convert to Span
        // Note: Span can only be used within the current synchronous block
        if (_buffer.Length >= 3)
        {
            Console.WriteLine($"[Sync] Processing first three bytes: {_buffer.Span[0]}, {_buffer.Span[1]}, {_buffer.Span[2]}");
        }
        else
        {
            Console.WriteLine($"[Sync] Buffer is shorter than 3 bytes. Current length: {_buffer.Length}");
        }

        Console.WriteLine("[Async] Waiting for I/O (100ms)...");
        await Task.Delay(100);
        
        // Second operation: convert to Span again
        // Because the field stores a Memory<T> view and the underlying storage is still alive,
        // we can obtain Span again after await and keep working with it
        if (_buffer.Length >= 3)
        {
             _buffer.Span[0] = 255;
             Console.WriteLine($"[Sync] Modified first byte: {_buffer.Span[0]}");
        }
    }
}
