// デモ: 標準的な Dispose パターン

Console.WriteLine("=== 標準 Dispose パターン例 ===\n");

// --------------------------------------------------------------
// 1. 基本的な IDisposable の利用
// --------------------------------------------------------------
Console.WriteLine("1. 基本的な IDisposable の利用");
Console.WriteLine(new string('-', 40));

using (var resource = new SimpleResource("Simple Resource"))
{
    resource.DoWork();
} // ここで Dispose

Console.WriteLine();

// --------------------------------------------------------------
// 2. 完全版 Dispose パターン（Finalizer 含む）
// --------------------------------------------------------------
Console.WriteLine("2. 完全版 Dispose パターン");
Console.WriteLine(new string('-', 40));

using (var holder = new ResourceHolder("Full Resource"))
{
    holder.DoWork();
} // ここで Dispose

Console.WriteLine();

// --------------------------------------------------------------
// 3. 継承時の Dispose パターン
// --------------------------------------------------------------
Console.WriteLine("3. 継承時の Dispose パターン");
Console.WriteLine(new string('-', 40));

using (var derived = new DerivedResourceHolder("Derived Resource"))
{
    derived.DoWork();
    derived.DoDerivedWork();
} // ここで Dispose

Console.WriteLine();

// --------------------------------------------------------------
// 4. Dispose 多重呼び出し防止
// --------------------------------------------------------------
Console.WriteLine("4. Dispose 多重呼び出し防止");
Console.WriteLine(new string('-', 40));

var resource2 = new SimpleResource("Disposable Resource");
resource2.Dispose();
resource2.Dispose(); // 2回目は無効
Console.WriteLine("Dispose の重複呼び出しでもエラーにならない\n");

// --------------------------------------------------------------
// 5. sealed クラス向け簡易 Dispose
// --------------------------------------------------------------
Console.WriteLine("5. sealed クラス向け簡易 Dispose");
Console.WriteLine(new string('-', 40));

using (var sealedResource = new SimpleSealedResource("Sealed Resource"))
{
    sealedResource.DoWork();
}

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// クラス定義
// ============================================================

/// <summary>
/// シンプルな IDisposable 実装（管理リソースのみを扱うケース）
/// </summary>
public class SimpleResource : IDisposable
{
    private readonly string _name;
    private bool _disposed = false;

    public SimpleResource(string name)
    {
        _name = name;
        Console.WriteLine($"  リソース生成: {_name}");
    }

    public void DoWork()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        Console.WriteLine($"  {_name} が動作中...");
    }

    public void Dispose()
    {
        if (_disposed) return;

        Console.WriteLine($"  リソース解放: {_name}");
        _disposed = true;
    }
}

/// <summary>
/// Finalizer 対応を含む完全な Dispose パターン
/// アンマネージリソースを直接扱う型向けの完全版
/// </summary>
public class ResourceHolder : IDisposable
{
    private readonly string _name;
    private bool _disposed = false;

    // 管理リソースの擬似例
    private SimpleResource? _managedResource;

    // アンマネージリソースハンドルの擬似例
    private IntPtr _unmanagedHandle = new IntPtr(12345);

    public ResourceHolder(string name)
    {
        _name = name;
        _managedResource = new SimpleResource($"{name} の内部リソース");
        Console.WriteLine($"  ResourceHolder 生成: {_name}");
    }

    public void DoWork()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        Console.WriteLine($"  {_name} が動作中...");
    }

    // 公開 Dispose（オーバーライド不可）
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);  // Finalizer 不要を GC に通知
    }

    // 継承先でオーバーライド可能
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing)
        {
            // 管理リソース解放
            _managedResource?.Dispose();
            _managedResource = null;
            Console.WriteLine($"  {_name}: 管理リソースを解放");
        }

        // アンマネージリソース解放
        if (_unmanagedHandle != IntPtr.Zero)
        {
            if (disposing)
            {
                Console.WriteLine($"  {_name}: アンマネージリソースを解放（Handle: {_unmanagedHandle}）");
            }
            _unmanagedHandle = IntPtr.Zero;
        }

        _disposed = true;
    }

    // Dispose 忘れの保険
    // 実運用コードでは、ここで Console I/O やログ出力などは避ける。
    ~ResourceHolder()
    {
        Dispose(disposing: false);
    }
}

/// <summary>
/// 継承先での Dispose パターン実装例
/// Dispose 後のメンバー利用も防ぐ
/// </summary>
public class DerivedResourceHolder : ResourceHolder
{
    private bool _disposed = false;
    private SimpleResource? _derivedResource;

    public DerivedResourceHolder(string name) : base(name)
    {
        _derivedResource = new SimpleResource($"{name} の派生内部リソース");
    }

    public void DoDerivedWork()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        Console.WriteLine("  派生クラスの処理を実行...");
    }

    protected override void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _derivedResource?.Dispose();
                _derivedResource = null;
                Console.WriteLine("  派生クラス: 独自管理リソースを解放");
            }

            _disposed = true;
        }

        base.Dispose(disposing);
    }
}

/// <summary>
/// sealed クラス向けの簡易 Dispose
/// </summary>
public sealed class SimpleSealedResource : IDisposable
{
    private readonly string _name;
    private bool _disposed = false;

    public SimpleSealedResource(string name)
    {
        _name = name;
        Console.WriteLine($"  sealed リソース生成: {_name}");
    }

    public void DoWork()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        Console.WriteLine($"  {_name} が動作中...");
    }

    public void Dispose()
    {
        if (_disposed) return;

        Console.WriteLine($"  sealed リソース解放: {_name}");
        _disposed = true;
    }
}
