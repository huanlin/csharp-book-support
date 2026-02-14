// Demo: Anonymous Disposal pattern

Console.WriteLine("=== Anonymous Disposal Pattern Example ===\n");

// --------------------------------------------------------------
// 1. Basic anonymous disposal
// --------------------------------------------------------------
Console.WriteLine("1. Basic anonymous disposal");
Console.WriteLine(new string('-', 40));

Console.WriteLine("Before executing an action...");
using (Disposable.Create(() => Console.WriteLine("Cleanup action: Execution complete")))
{
    Console.WriteLine("  Executing...");
}
Console.WriteLine();

// --------------------------------------------------------------
// 2. Suspending and resuming event handling
// --------------------------------------------------------------
Console.WriteLine("2. Event Manager Example");
Console.WriteLine(new string('-', 40));

var eventManager = new EventManager();

Console.WriteLine("Normal event trigger:");
eventManager.RaiseEvent("Event 1");
eventManager.RaiseEvent("Event 2");

Console.WriteLine("\nSuspending events using SuspendEvents:");
using (eventManager.SuspendEvents())
{
    eventManager.RaiseEvent("Event 3 (Will not trigger)");
    eventManager.RaiseEvent("Event 4 (Will not trigger)");
}

Console.WriteLine("\nTriggering events after recovery:");
eventManager.RaiseEvent("Event 5");

// --------------------------------------------------------------
// 3. Nested suspension
// --------------------------------------------------------------
Console.WriteLine("\n3. Nested suspension");
Console.WriteLine(new string('-', 40));

using (eventManager.SuspendEvents())
{
    Console.WriteLine("  First layer of suspension");
    using (eventManager.SuspendEvents())
    {
        Console.WriteLine("  Second layer of suspension");
        eventManager.RaiseEvent("Will not trigger");
    }
    Console.WriteLine("  Left second layer, still in first layer suspension");
    eventManager.RaiseEvent("Still will not trigger");
}
Console.WriteLine("  Completely exited suspension");
eventManager.RaiseEvent("This one will trigger");

// --------------------------------------------------------------
// 4. Timer example
// --------------------------------------------------------------
Console.WriteLine("\n4. Timer example");
Console.WriteLine(new string('-', 40));

using (Timer.Start("Simulating long-running operation"))
{
    // Simulate some work
    Thread.Sleep(100);
}

using (Timer.Start("Another operation"))
{
    Thread.Sleep(50);
}

// --------------------------------------------------------------
// 5. Temporary state switch
// --------------------------------------------------------------
Console.WriteLine("\n5. Temporary state switch");
Console.WriteLine(new string('-', 40));

var worker = new StateWorker();
Console.WriteLine($"Initial state: {worker.State}");

using (worker.TemporarilySetState("Processing"))
{
    Console.WriteLine($"Temporary state: {worker.State}");
    worker.DoWork();
}

Console.WriteLine($"Restored state: {worker.State}");

// --------------------------------------------------------------
// 6. Ensure cleanup even during exceptions
// --------------------------------------------------------------
Console.WriteLine("\n6. Cleanup during exceptions");
Console.WriteLine(new string('-', 40));

try
{
    using (Disposable.Create(() => Console.WriteLine("  Cleanup action still executes")))
    {
        Console.WriteLine("  Preparing to throw exception...");
        throw new InvalidOperationException("Test exception");
    }
}
catch (InvalidOperationException)
{
    Console.WriteLine("  Exception captured");
}

Console.WriteLine("\n=== Example End ===");

// ============================================================
// Disposable Helper Class
// ============================================================

/// <summary>
/// Generic anonymous IDisposable implementation
/// Allows defining cleanup actions via delegates
/// </summary>
public class Disposable : IDisposable
{
    private Action? _onDispose;

    public static Disposable Create(Action onDispose)
        => new Disposable(onDispose);

    public static readonly Disposable Empty = new Disposable(() => { });

    private Disposable(Action onDispose)
        => _onDispose = onDispose;

    public void Dispose()
    {
        _onDispose?.Invoke();
        _onDispose = null;  // Prevent multiple executions
    }
}

// ============================================================
// Application Example Classes
// ============================================================

/// <summary>
/// Event manager supporting suspension and resumption of event handling
/// </summary>
public class EventManager
{
    private int _suspendCount = 0;

    public IDisposable SuspendEvents()
    {
        _suspendCount++;
        Console.WriteLine($"  Suspending events (Level {_suspendCount})");
        return Disposable.Create(() =>
        {
            _suspendCount--;
            Console.WriteLine($"  Resuming events (Level {_suspendCount})");
        });
    }

    public void RaiseEvent(string eventName)
    {
        if (_suspendCount == 0)
        {
            Console.WriteLine($"  Triggering event: {eventName}");
        }
        else
        {
            // Events are suspended, do nothing
        }
    }
}

/// <summary>
/// Simple timer using anonymous disposal to record execution time
/// </summary>
public class Timer
{
    public static IDisposable Start(string operationName)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        Console.WriteLine($"  Start: {operationName}");
        
        return Disposable.Create(() =>
        {
            stopwatch.Stop();
            Console.WriteLine($"  End: {operationName} (Elapsed {stopwatch.ElapsedMilliseconds} ms)");
        });
    }
}

/// <summary>
/// Work class supporting temporary state switches
/// </summary>
public class StateWorker
{
    public string State { get; private set; } = "Idle";

    public IDisposable TemporarilySetState(string temporaryState)
    {
        string originalState = State;
        State = temporaryState;
        
        return Disposable.Create(() =>
        {
            State = originalState;
        });
    }

    public void DoWork()
    {
        Console.WriteLine($"  Executing work (Current state: {State})");
    }
}
