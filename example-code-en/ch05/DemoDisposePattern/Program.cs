// Demo: Standard Dispose pattern

Console.WriteLine("=== Standard Dispose Pattern Example ===\n");

// --------------------------------------------------------------
// 1. Basic IDisposable usage
// --------------------------------------------------------------
Console.WriteLine("1. Basic IDisposable usage");
Console.WriteLine(new string('-', 40));

using (var resource = new SimpleResource("Simple Resource"))
{
    resource.DoWork();
} // Dispose is called here

Console.WriteLine();

// --------------------------------------------------------------
// 2. Full Dispose pattern (including Finalizer)
// --------------------------------------------------------------
Console.WriteLine("2. Full Dispose pattern");
Console.WriteLine(new string('-', 40));

using (var holder = new ResourceHolder("Full Resource"))
{
    holder.DoWork();
} // Dispose is called here

Console.WriteLine();

// --------------------------------------------------------------
// 3. Inherited Dispose pattern
// --------------------------------------------------------------
Console.WriteLine("3. Inherited Dispose pattern");
Console.WriteLine(new string('-', 40));

using (var derived = new DerivedResourceHolder("Derived Resource"))
{
    derived.DoWork();
    derived.DoDerivedWork();
} // Dispose is called here

Console.WriteLine();

// --------------------------------------------------------------
// 4. Prevent duplicate Dispose
// --------------------------------------------------------------
Console.WriteLine("4. Prevent duplicate Dispose");
Console.WriteLine(new string('-', 40));

var resource2 = new SimpleResource("Disposable Resource");
resource2.Dispose();
resource2.Dispose(); // Second call has no effect
Console.WriteLine("Duplicate call to Dispose does not cause an error\n");

// --------------------------------------------------------------
// 5. Simplified Dispose pattern for sealed classes
// --------------------------------------------------------------
Console.WriteLine("5. Simplified Dispose for sealed classes");
Console.WriteLine(new string('-', 40));

using (var sealedResource = new SimpleSealedResource("Sealed Resource"))
{
    sealedResource.DoWork();
}

Console.WriteLine("\n=== Example End ===");

// ============================================================
// Class Definitions
// ============================================================

/// <summary>
/// Simple IDisposable implementation (suitable for cases only wrapping managed resources)
/// </summary>
public class SimpleResource : IDisposable
{
    private readonly string _name;
    private bool _disposed = false;

    public SimpleResource(string name)
    {
        _name = name;
        Console.WriteLine($"  Created resource: {_name}");
    }

    public void DoWork()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        Console.WriteLine($"  {_name} is working...");
    }

    public void Dispose()
    {
        if (_disposed) return;
        
        Console.WriteLine($"  Released resource: {_name}");
        _disposed = true;
    }
}

/// <summary>
/// Full Dispose pattern (including Finalizer support)
/// Suitable for directly wrapping unmanaged resources or as a base class
/// </summary>
public class ResourceHolder : IDisposable
{
    private readonly string _name;
    private bool _disposed = false;
    
    // Simulating a managed resource
    private SimpleResource? _managedResource;
    
    // Simulating an unmanaged resource handle
    private IntPtr _unmanagedHandle = new IntPtr(12345);

    public ResourceHolder(string name)
    {
        _name = name;
        _managedResource = new SimpleResource($"Inner resource of {name}");
        Console.WriteLine($"  Created ResourceHolder: {_name}");
    }

    public void DoWork()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        Console.WriteLine($"  {_name} is working...");
    }

    // Public Dispose method (not overridable)
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);  // Tell GC finalizer execution is not needed
    }

    // Protected virtual Dispose method (can be overridden by subclasses)
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing)
        {
            // Dispose managed resources (safe to reference other objects)
            _managedResource?.Dispose();
            _managedResource = null;
            Console.WriteLine($"  {_name}: Released managed resources");
        }

        // Release unmanaged resources (must execute regardless of disposing value)
        if (_unmanagedHandle != IntPtr.Zero)
        {
            Console.WriteLine($"  {_name}: Released unmanaged resources (Handle: {_unmanagedHandle})");
            _unmanagedHandle = IntPtr.Zero;
        }

        _disposed = true;
    }

    // Finalizer (backup in case Dispose call is forgotten)
    ~ResourceHolder()
    {
        Console.WriteLine($"  {_name}: Finalizer called (this means Dispose was forgotten!)");
        Dispose(disposing: false);
    }
}

/// <summary>
/// Derived class inheriting from ResourceHolder
/// Demonstrates how to correctly override the Dispose pattern
/// </summary>
public class DerivedResourceHolder : ResourceHolder
{
    private bool _disposed = false;
    private SimpleResource? _derivedResource;

    public DerivedResourceHolder(string name) : base(name)
    {
        _derivedResource = new SimpleResource($"Derived inner resource of {name}");
    }

    public void DoDerivedWork()
    {
        Console.WriteLine("  Executing derived class work...");
    }

    protected override void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Dispose managed resources of the derived class
                _derivedResource?.Dispose();
                _derivedResource = null;
                Console.WriteLine("  Derived class: Released its own managed resources");
            }

            _disposed = true;
        }

        // Call the base class Dispose
        base.Dispose(disposing);
    }
}

/// <summary>
/// Simplified Dispose pattern for sealed classes
/// Since it won't be inherited, protected virtual Dispose(bool) is not needed
/// </summary>
public sealed class SimpleSealedResource : IDisposable
{
    private readonly string _name;
    private bool _disposed = false;

    public SimpleSealedResource(string name)
    {
        _name = name;
        Console.WriteLine($"  Created sealed resource: {_name}");
    }

    public void DoWork()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        Console.WriteLine($"  {_name} is working...");
    }

    public void Dispose()
    {
        if (_disposed) return;

        Console.WriteLine($"  Released sealed resource: {_name}");
        _disposed = true;
    }
}
