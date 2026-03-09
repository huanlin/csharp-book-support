// デモ: ArrayPool<T> の利用
using System.Buffers;
using System.Text;

Console.WriteLine("=== ArrayPool<T> 高性能配列共有 ===\n");

// --------------------------------------------------------------
// 1. ArrayPool 利用パターン
// --------------------------------------------------------------
Console.WriteLine("1. Rent/Return パターン");
Console.WriteLine(new string('-', 40));

// ストリーム読み込みをシミュレーション
byte[] sampleData = Encoding.UTF8.GetBytes("Hello ArrayPool! This is a test string.");
using var stream = new MemoryStream(sampleData);

ProcessStream(stream);


Console.WriteLine("\n=== 例の終了 ===");


static void ProcessStream(Stream stream)
{
    // 1. Rent: 実際に返る長さは要求した 16 より大きい場合がある（Pool 実装次第）
    int bufferSize = 16;
    byte[] buffer = ArrayPool<byte>.Shared.Rent(bufferSize);
    
    Console.WriteLine($"要求サイズ {bufferSize} bytes, 実際に取得: {buffer.Length} bytes");
    
    try
    {
        // 2. Use: 実際に読み取った長さだけを処理する
        // バッファには前回利用時のデータが残っている可能性があり、かつ長さが要求値より大きいこともある
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        
        // Span に変換して有効データのみ処理
        ReadOnlySpan<byte> span = buffer.AsSpan(0, bytesRead);
        
        string content = Encoding.UTF8.GetString(span);
        Console.WriteLine($"読み取り内容: \"{content}\"");
        
        // さらに読み込みを継続
        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
             span = buffer.AsSpan(0, bytesRead);
             content = Encoding.UTF8.GetString(span);
             Console.WriteLine($"読み取り内容: \"{content}\"");
        }
    }
    finally
    {
        // 3. Return: 必ず finally で返却
        // 返却後はそのバッファを使わないこと
        ArrayPool<byte>.Shared.Return(buffer);
        Console.WriteLine("バッファを返却しました");
    }
}
