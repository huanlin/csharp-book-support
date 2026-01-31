// 示範 using 陳述式和 using 宣告

Console.WriteLine("=== Using 陳述式範例 ===\n");

// 建立測試檔案
string testFilePath = "test_using.txt";
File.WriteAllText(testFilePath, "這是測試內容\n第二行\n第三行");

try
{
    // --------------------------------------------------------------
    // 1. 傳統 using 陳述式（巢狀區塊）
    // --------------------------------------------------------------
    Console.WriteLine("1. 傳統 using 陳述式");
    Console.WriteLine(new string('-', 40));

    using (var file = File.OpenRead(testFilePath))
    {
        using (var reader = new StreamReader(file))
        {
            string content = reader.ReadToEnd();
            Console.WriteLine($"讀取內容：\n{content}");
        } // reader.Dispose() 在此被呼叫
    } // file.Dispose() 在此被呼叫

    Console.WriteLine("檔案已自動關閉\n");

    // --------------------------------------------------------------
    // 2. using 宣告（C# 8+，無需巢狀）
    // --------------------------------------------------------------
    Console.WriteLine("2. using 宣告（C# 8+）");
    Console.WriteLine(new string('-', 40));

    ReadFileWithUsingDeclaration(testFilePath);

    // --------------------------------------------------------------
    // 3. 多個資源的 using 宣告
    // --------------------------------------------------------------
    Console.WriteLine("3. 多個資源的 using 宣告");
    Console.WriteLine(new string('-', 40));

    CopyFileContent(testFilePath, "test_copy.txt");

    // 驗證複製結果
    Console.WriteLine($"複製後的內容：\n{File.ReadAllText("test_copy.txt")}");

    // --------------------------------------------------------------
    // 4. 非同步資源釋放（await using）
    // --------------------------------------------------------------
    Console.WriteLine("4. 非同步資源釋放");
    Console.WriteLine(new string('-', 40));

    await ReadFileAsync(testFilePath);

    // --------------------------------------------------------------
    // 5. 示範釋放順序（後進先出）
    // --------------------------------------------------------------
    Console.WriteLine("5. 示範釋放順序");
    Console.WriteLine(new string('-', 40));

    DemoDisposeOrder();

    // --------------------------------------------------------------
    // 6. 確保例外發生時資源仍被釋放
    // --------------------------------------------------------------
    Console.WriteLine("6. 例外發生時資源釋放");
    Console.WriteLine(new string('-', 40));

    try
    {
        using var resource = new TrackedResource("測試資源");
        Console.WriteLine("準備拋出例外...");
        throw new InvalidOperationException("故意拋出的例外");
    }
    catch (InvalidOperationException)
    {
        Console.WriteLine("例外已被捕捉");
    }
    Console.WriteLine("（注意：即使發生例外，資源仍被正確釋放）\n");
}
finally
{
    // 清理測試檔案
    File.Delete(testFilePath);
    if (File.Exists("test_copy.txt"))
        File.Delete("test_copy.txt");
}

Console.WriteLine("=== 範例結束 ===");

// ============================================================
// 輔助方法
// ============================================================

static void ReadFileWithUsingDeclaration(string path)
{
    // using 宣告：變數在方法結束時自動釋放
    using var file = File.OpenRead(path);
    using var reader = new StreamReader(file);
    
    string firstLine = reader.ReadLine() ?? "";
    Console.WriteLine($"第一行：{firstLine}");
    Console.WriteLine("（方法結束時，file 和 reader 會自動釋放）\n");
}

static void CopyFileContent(string sourcePath, string destPath)
{
    // 多個 using 宣告在同一個作用域
    using var sourceStream = File.OpenRead(sourcePath);
    using var destStream = File.Create(destPath);
    
    sourceStream.CopyTo(destStream);
    Console.WriteLine($"已複製 {sourcePath} 到 {destPath}\n");
}

static async Task ReadFileAsync(string path)
{
    // await using 用於非同步釋放
    await using var stream = new FileStream(
        path, 
        FileMode.Open, 
        FileAccess.Read, 
        FileShare.Read, 
        bufferSize: 4096, 
        useAsync: true);
    
    using var reader = new StreamReader(stream);
    string content = await reader.ReadToEndAsync();
    Console.WriteLine($"非同步讀取完成，內容長度：{content.Length} 字元\n");
}

static void DemoDisposeOrder()
{
    using var resource1 = new TrackedResource("資源 1");
    using var resource2 = new TrackedResource("資源 2");
    using var resource3 = new TrackedResource("資源 3");
    
    Console.WriteLine("所有資源已建立");
    Console.WriteLine("（離開作用域時，釋放順序為：資源 3 → 資源 2 → 資源 1）\n");
}

// ============================================================
// 輔助類別
// ============================================================

/// <summary>
/// 追蹤建立和釋放動作的資源類別
/// </summary>
public class TrackedResource : IDisposable
{
    private readonly string _name;
    private bool _disposed = false;

    public TrackedResource(string name)
    {
        _name = name;
        Console.WriteLine($"  建立：{_name}");
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            Console.WriteLine($"  釋放：{_name}");
            _disposed = true;
        }
    }
}
