// デモ: using 文と using 宣言

Console.WriteLine("=== Using Statement の例 ===\n");

// テストファイル作成
string testFilePath = "test_using.txt";
File.WriteAllText(testFilePath, "This is test content\nSecond line\nThird line");

try
{
    // --------------------------------------------------------------
    // 1. 従来の using 文（ネストブロック）
    // --------------------------------------------------------------
    Console.WriteLine("1. 従来の using 文");
    Console.WriteLine(new string('-', 40));

    using (var file = File.OpenRead(testFilePath))
    {
        using (var reader = new StreamReader(file))
        {
            string content = reader.ReadToEnd();
            Console.WriteLine($"読み取り内容:\n{content}");
        } // reader.Dispose()
    } // file.Dispose()

    Console.WriteLine("ファイルは自動的にクローズされました\n");

    // --------------------------------------------------------------
    // 2. using 宣言（C# 8+、ネスト不要）
    // --------------------------------------------------------------
    Console.WriteLine("2. using 宣言（C# 8+）");
    Console.WriteLine(new string('-', 40));

    ReadFileWithUsingDeclaration(testFilePath);

    // --------------------------------------------------------------
    // 3. using 宣言で複数リソース
    // --------------------------------------------------------------
    Console.WriteLine("3. using 宣言で複数リソース");
    Console.WriteLine(new string('-', 40));

    CopyFileContent(testFilePath, "test_copy.txt");

    Console.WriteLine($"コピー内容:\n{File.ReadAllText("test_copy.txt")}");

    // --------------------------------------------------------------
    // 4. 非同期リソース解放（await using）
    // --------------------------------------------------------------
    Console.WriteLine("4. 非同期リソース解放");
    Console.WriteLine(new string('-', 40));

    await WriteFileWithAsyncDispose("test_async.txt");

    // 書き込み結果を確認
    Console.WriteLine($"書き込み後の内容:\n{File.ReadAllText("test_async.txt")}");

    // --------------------------------------------------------------
    // 5. 解放順序（LIFO）
    // --------------------------------------------------------------
    Console.WriteLine("5. 解放順序の確認");
    Console.WriteLine(new string('-', 40));

    DemoDisposeOrder();

    // --------------------------------------------------------------
    // 6. 例外時でも確実に解放
    // --------------------------------------------------------------
    Console.WriteLine("6. 例外時のリソース解放");
    Console.WriteLine(new string('-', 40));

    try
    {
        using var resource = new TrackedResource("Test Resource");
        Console.WriteLine("例外を送出します...");
        throw new InvalidOperationException("意図的に送出した例外");
    }
    catch (InvalidOperationException)
    {
        Console.WriteLine("例外を捕捉");
    }
    Console.WriteLine("（例外が発生してもリソースは正しく解放される）\n");
}
finally
{
    File.Delete(testFilePath);
    if (File.Exists("test_copy.txt"))
        File.Delete("test_copy.txt");
    if (File.Exists("test_async.txt"))
        File.Delete("test_async.txt");
}

Console.WriteLine("=== 例の終了 ===");

// ============================================================
// ヘルパーメソッド
// ============================================================

static void ReadFileWithUsingDeclaration(string path)
{
    // using 宣言: メソッド終了時に自動解放
    using var file = File.OpenRead(path);
    using var reader = new StreamReader(file);

    string firstLine = reader.ReadLine() ?? "";
    Console.WriteLine($"1行目: {firstLine}");
    Console.WriteLine("（メソッド終了時に file と reader が自動解放される）\n");
}

static void CopyFileContent(string sourcePath, string destPath)
{
    using var sourceStream = File.OpenRead(sourcePath);
    using var destStream = File.Create(destPath);

    sourceStream.CopyTo(destStream);
    Console.WriteLine($"{sourcePath} を {destPath} へコピー\n");
}

static async Task WriteFileWithAsyncDispose(string path)
{
    byte[] data = System.Text.Encoding.UTF8.GetBytes(
        "このテキストは FileStream を通じて非同期に書き込まれます。\n");

    // IAsyncDisposable を実装する FileStream に直接 await using を適用
    await using var stream = new FileStream(
        path,
        FileMode.Create,
        FileAccess.Write,
        FileShare.None,
        bufferSize: 4096,
        useAsync: true);

    await stream.WriteAsync(data);
    Console.WriteLine($"非同期書き込み完了。{data.Length} バイトを書き込みました");
    Console.WriteLine("（メソッドを抜けると FileStream.DisposeAsync が呼ばれ、flush/close が非同期に行われる）\n");
}

static void DemoDisposeOrder()
{
    using var resource1 = new TrackedResource("Resource 1");
    using var resource2 = new TrackedResource("Resource 2");
    using var resource3 = new TrackedResource("Resource 3");

    Console.WriteLine("全リソースを確立");
    Console.WriteLine("（スコープ離脱時の解放順: Resource 3 → Resource 2 → Resource 1）\n");
}

// ============================================================
// ヘルパークラス
// ============================================================

/// <summary>
/// 生成/解放を表示する追跡用リソース
/// </summary>
public class TrackedResource : IDisposable
{
    private readonly string _name;
    private bool _disposed = false;

    public TrackedResource(string name)
    {
        _name = name;
        Console.WriteLine($"  生成: {_name}");
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            Console.WriteLine($"  解放: {_name}");
            _disposed = true;
        }
    }
}
