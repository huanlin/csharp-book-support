// Demo: Using ArrayPool<T>
using System.Buffers;
using System.Text;

Console.WriteLine("=== ArrayPool<T> High-performance Array Sharing ===\n");

// --------------------------------------------------------------
// 1. ArrayPool Usage Patterns
// --------------------------------------------------------------
Console.WriteLine("1. Rent and Return Pattern");
Console.WriteLine(new string('-', 40));

// Simulating reading from a stream
byte[] sampleData = Encoding.UTF8.GetBytes("Hello ArrayPool! This is a test string.");
using var stream = new MemoryStream(sampleData);

ProcessStream(stream);


Console.WriteLine("\n=== Example End ===");


static void ProcessStream(Stream stream)
{
    // 1. Rent: The length might be larger than requesting 16 (depending on Pool implementation)
    int bufferSize = 16;
    byte[] buffer = ArrayPool<byte>.Shared.Rent(bufferSize);
    
    Console.WriteLine($"Requested {bufferSize} bytes, actually got: {buffer.Length} bytes");
    
    try
    {
        // 2. Use: Note to operate on the actual read length
        // Because the buffer might contain dirty data from previous users, and actual length > requested
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        
        // Convert to Span to process only valid data
        ReadOnlySpan<byte> span = buffer.AsSpan(0, bytesRead);
        
        string content = Encoding.UTF8.GetString(span);
        Console.WriteLine($"Read content: \"{content}\"");
        
        // More simulations...
        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
             span = buffer.AsSpan(0, bytesRead);
             content = Encoding.UTF8.GetString(span);
             Console.WriteLine($"Read content: \"{content}\"");
        }
    }
    finally
    {
        // 3. Return: Always place in a finally block
        // Never use the buffer after returning it!
        ArrayPool<byte>.Shared.Return(buffer);
        Console.WriteLine("Buffer returned");
    }
}
