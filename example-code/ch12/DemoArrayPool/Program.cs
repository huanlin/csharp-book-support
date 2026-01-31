// 示範 ArrayPool<T> 的使用
using System.Buffers;
using System.Text;

Console.WriteLine("=== ArrayPool<T> 高效能陣列共用 ===\n");

// --------------------------------------------------------------
// 1. ArrayPool 的使用模式
// --------------------------------------------------------------
Console.WriteLine("1. Rent 與 Return 模式");
Console.WriteLine(new string('-', 40));

// 模擬讀取串流
byte[] sampleData = Encoding.UTF8.GetBytes("Hello ArrayPool! This is a test string.");
using var stream = new MemoryStream(sampleData);

ProcessStream(stream);


Console.WriteLine("\n=== 範例結束 ===");


static void ProcessStream(Stream stream)
{
    // 1. 租借：長度可能比請求的 16 還大 (依據 Pool 的實作)
    int bufferSize = 16;
    byte[] buffer = ArrayPool<byte>.Shared.Rent(bufferSize);
    
    Console.WriteLine($"請求 {bufferSize} bytes, 實際取得: {buffer.Length} bytes");
    
    try
    {
        // 2. 使用：注意要針對實際讀取的長度操作
        // 因為 buffer 可能由上一個使用者留下髒資料，且實際長度大於需求
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        
        // 轉為 Span 只處裡有效資料
        ReadOnlySpan<byte> span = buffer.AsSpan(0, bytesRead);
        
        string content = Encoding.UTF8.GetString(span);
        Console.WriteLine($"讀取內容: \"{content}\"");
        
        // 模擬更多讀取...
        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
             span = buffer.AsSpan(0, bytesRead);
             content = Encoding.UTF8.GetString(span);
             Console.WriteLine($"讀取內容: \"{content}\"");
        }
    }
    finally
    {
        // 3. 歸還：務必放在 finally 區塊
        // 歸還後絕對不要再使用這個 buffer！
        ArrayPool<byte>.Shared.Return(buffer);
        Console.WriteLine("Buffer 已歸還");
    }
}
