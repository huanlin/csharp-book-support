// 示範標準 Dispose 模式

Console.WriteLine("=== 標準 Dispose 模式範例 ===\n");

// --------------------------------------------------------------
// 1. 基本的 IDisposable 使用
// --------------------------------------------------------------
Console.WriteLine("1. 基本的 IDisposable 使用");
Console.WriteLine(new string('-', 40));

using (var resource = new SimpleResource("簡單資源"))
{
    resource.DoWork();
} // Dispose 在此被呼叫

Console.WriteLine();

// --------------------------------------------------------------
// 2. 完整的 Dispose 模式（含 Finalizer）
// --------------------------------------------------------------
Console.WriteLine("2. 完整的 Dispose 模式");
Console.WriteLine(new string('-', 40));

using (var holder = new ResourceHolder("完整資源"))
{
    holder.DoWork();
} // Dispose 在此被呼叫

Console.WriteLine();

// --------------------------------------------------------------
// 3. 繼承的 Dispose 模式
// --------------------------------------------------------------
Console.WriteLine("3. 繼承的 Dispose 模式");
Console.WriteLine(new string('-', 40));

using (var derived = new DerivedResourceHolder("衍生資源"))
{
    derived.DoWork();
    derived.DoDerivedWork();
} // Dispose 在此被呼叫

Console.WriteLine();

// --------------------------------------------------------------
// 4. 防止重複 Dispose
// --------------------------------------------------------------
Console.WriteLine("4. 防止重複 Dispose");
Console.WriteLine(new string('-', 40));

var resource2 = new SimpleResource("可重複釋放的資源");
resource2.Dispose();
resource2.Dispose(); // 第二次呼叫不會有任何效果
Console.WriteLine("重複呼叫 Dispose 不會發生錯誤\n");

// --------------------------------------------------------------
// 5. 密封類別的簡化 Dispose 模式
// --------------------------------------------------------------
Console.WriteLine("5. 密封類別的簡化 Dispose");
Console.WriteLine(new string('-', 40));

using (var sealedResource = new SimpleSealedResource("密封資源"))
{
    sealedResource.DoWork();
}

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 類別定義
// ============================================================

/// <summary>
/// 簡單的 IDisposable 實作（適用於只包裝受控資源的情況）
/// </summary>
public class SimpleResource : IDisposable
{
    private readonly string _name;
    private bool _disposed = false;

    public SimpleResource(string name)
    {
        _name = name;
        Console.WriteLine($"  建立資源：{_name}");
    }

    public void DoWork()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        Console.WriteLine($"  {_name} 正在工作...");
    }

    public void Dispose()
    {
        if (_disposed) return;
        
        Console.WriteLine($"  釋放資源：{_name}");
        _disposed = true;
    }
}

/// <summary>
/// 完整的 Dispose 模式（含 Finalizer 支援）
/// 適用於直接包裝非受控資源或作為基底類別的情況
/// </summary>
public class ResourceHolder : IDisposable
{
    private readonly string _name;
    private bool _disposed = false;
    
    // 模擬受控資源
    private SimpleResource? _managedResource;
    
    // 模擬非受控資源控制代碼
    private IntPtr _unmanagedHandle = new IntPtr(12345);

    public ResourceHolder(string name)
    {
        _name = name;
        _managedResource = new SimpleResource($"{name} 的內部資源");
        Console.WriteLine($"  建立 ResourceHolder：{_name}");
    }

    public void DoWork()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        Console.WriteLine($"  {_name} 正在工作...");
    }

    // 公開的 Dispose 方法（不可覆寫）
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);  // 告訴 GC 不需要執行終結器
    }

    // 受保護的虛擬 Dispose 方法（可供子類別覆寫）
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing)
        {
            // 釋放受控資源（可以安全地引用其他物件）
            _managedResource?.Dispose();
            _managedResource = null;
            Console.WriteLine($"  {_name}：釋放受控資源");
        }

        // 釋放非受控資源（無論 disposing 為何都要執行）
        if (_unmanagedHandle != IntPtr.Zero)
        {
            Console.WriteLine($"  {_name}：釋放非受控資源（Handle: {_unmanagedHandle}）");
            _unmanagedHandle = IntPtr.Zero;
        }

        _disposed = true;
    }

    // 終結器（只在忘記呼叫 Dispose 時作為備援）
    ~ResourceHolder()
    {
        Console.WriteLine($"  {_name}：終結器被呼叫（這表示忘記呼叫 Dispose！）");
        Dispose(disposing: false);
    }
}

/// <summary>
/// 繼承自 ResourceHolder 的衍生類別
/// 展示如何正確覆寫 Dispose 模式
/// </summary>
public class DerivedResourceHolder : ResourceHolder
{
    private bool _disposed = false;
    private SimpleResource? _derivedResource;

    public DerivedResourceHolder(string name) : base(name)
    {
        _derivedResource = new SimpleResource($"{name} 的衍生內部資源");
    }

    public void DoDerivedWork()
    {
        Console.WriteLine("  執行衍生類別的工作...");
    }

    protected override void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // 釋放衍生類別的受控資源
                _derivedResource?.Dispose();
                _derivedResource = null;
                Console.WriteLine("  衍生類別：釋放自己的受控資源");
            }

            _disposed = true;
        }

        // 呼叫基底類別的 Dispose
        base.Dispose(disposing);
    }
}

/// <summary>
/// 密封類別的簡化 Dispose 模式
/// 因為不會被繼承，所以不需要 protected virtual Dispose(bool)
/// </summary>
public sealed class SimpleSealedResource : IDisposable
{
    private readonly string _name;
    private bool _disposed = false;

    public SimpleSealedResource(string name)
    {
        _name = name;
        Console.WriteLine($"  建立密封資源：{_name}");
    }

    public void DoWork()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        Console.WriteLine($"  {_name} 正在工作...");
    }

    public void Dispose()
    {
        if (_disposed) return;

        Console.WriteLine($"  釋放密封資源：{_name}");
        _disposed = true;
    }
}
