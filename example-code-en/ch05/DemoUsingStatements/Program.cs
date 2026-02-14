// Demo: using statement and using declaration

Console.WriteLine("=== Using Statement Example ===\n");

// Create test file
string testFilePath = "test_using.txt";
File.WriteAllText(testFilePath, "This is test content\nSecond line\nThird line");

try
{
    // --------------------------------------------------------------
    // 1. Traditional using statement (nested block)
    // --------------------------------------------------------------
    Console.WriteLine("1. Traditional using statement");
    Console.WriteLine(new string('-', 40));

    using (var file = File.OpenRead(testFilePath))
    {
        using (var reader = new StreamReader(file))
        {
            string content = reader.ReadToEnd();
            Console.WriteLine($"Reading content:\n{content}");
        } // reader.Dispose() is called here
    } // file.Dispose() is called here

    Console.WriteLine("File has been automatically closed\n");

    // --------------------------------------------------------------
    // 2. using declaration (C# 8+, no nesting required)
    // --------------------------------------------------------------
    Console.WriteLine("2. using declaration (C# 8+)");
    Console.WriteLine(new string('-', 40));

    ReadFileWithUsingDeclaration(testFilePath);

    // --------------------------------------------------------------
    // 3. using declaration with multiple resources
    // --------------------------------------------------------------
    Console.WriteLine("3. Multiple resources with using declaration");
    Console.WriteLine(new string('-', 40));

    CopyFileContent(testFilePath, "test_copy.txt");

    // Verify copy result
    Console.WriteLine($"Copied content:\n{File.ReadAllText("test_copy.txt")}");

    // --------------------------------------------------------------
    // 4. Asynchronous resource disposal (await using)
    // --------------------------------------------------------------
    Console.WriteLine("4. Asynchronous resource disposal");
    Console.WriteLine(new string('-', 40));

    await ReadFileAsync(testFilePath);

    // --------------------------------------------------------------
    // 5. Demonstrate disposal order (LIFO - Last In, First Out)
    // --------------------------------------------------------------
    Console.WriteLine("5. Disposal order demonstration");
    Console.WriteLine(new string('-', 40));

    DemoDisposeOrder();

    // --------------------------------------------------------------
    // 6. Ensure resources are disposed even when an exception occurs
    // --------------------------------------------------------------
    Console.WriteLine("6. Resource disposal during exception");
    Console.WriteLine(new string('-', 40));

    try
    {
        using var resource = new TrackedResource("Test Resource");
        Console.WriteLine("Preparing to throw exception...");
        throw new InvalidOperationException("Intentionally thrown exception");
    }
    catch (InvalidOperationException)
    {
        Console.WriteLine("Exception captured");
    }
    Console.WriteLine("(Note: Even if an exception occurs, the resource is still correctly disposed)\n");
}
finally
{
    // Clean up test files
    File.Delete(testFilePath);
    if (File.Exists("test_copy.txt"))
        File.Delete("test_copy.txt");
}

Console.WriteLine("=== Example End ===");

// ============================================================
// Helper Methods
// ============================================================

static void ReadFileWithUsingDeclaration(string path)
{
    // using declaration: variables are automatically disposed when the method ends
    using var file = File.OpenRead(path);
    using var reader = new StreamReader(file);
    
    string firstLine = reader.ReadLine() ?? "";
    Console.WriteLine($"First line: {firstLine}");
    Console.WriteLine("(At the end of the method, both file and reader will be automatically disposed)\n");
}

static void CopyFileContent(string sourcePath, string destPath)
{
    // Multiple using declarations in the same scope
    using var sourceStream = File.OpenRead(sourcePath);
    using var destStream = File.Create(destPath);
    
    sourceStream.CopyTo(destStream);
    Console.WriteLine($"Copied {sourcePath} to {destPath}\n");
}

static async Task ReadFileAsync(string path)
{
    // await using is used for asynchronous disposal
    await using var stream = new FileStream(
        path, 
        FileMode.Open, 
        FileAccess.Read, 
        FileShare.Read, 
        bufferSize: 4096, 
        useAsync: true);
    
    using var reader = new StreamReader(stream);
    string content = await reader.ReadToEndAsync();
    Console.WriteLine($"Asynchronous read completed, content length: {content.Length} characters\n");
}

static void DemoDisposeOrder()
{
    using var resource1 = new TrackedResource("Resource 1");
    using var resource2 = new TrackedResource("Resource 2");
    using var resource3 = new TrackedResource("Resource 3");
    
    Console.WriteLine("All resources established");
    Console.WriteLine("(When leaving scope, the disposal order is: Resource 3 → Resource 2 → Resource 1)\n");
}

// ============================================================
// Helper Classes
// ============================================================

/// <summary>
/// Resource class that tracks creation and disposal actions
/// </summary>
public class TrackedResource : IDisposable
{
    private readonly string _name;
    private bool _disposed = false;

    public TrackedResource(string name)
    {
        _name = name;
        Console.WriteLine($"  Created: {_name}");
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            Console.WriteLine($"  Disposed: {_name}");
            _disposed = true;
        }
    }
}
