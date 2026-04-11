// Demo: Event Subscription Management and Avoiding Memory Leaks

Console.WriteLine("=== Events and Memory Leaks ===\n");

// --------------------------------------------------------------
// 1. Subscribing and Unsubscribing
// --------------------------------------------------------------
Console.WriteLine("1. Subscribing and Unsubscribing");
Console.WriteLine(new string('-', 40));

var publisher = new Publisher();

void Handler1(object? sender, EventArgs e) => Console.WriteLine("  Handler 1");
void Handler2(object? sender, EventArgs e) => Console.WriteLine("  Handler 2");

publisher.SomethingHappened += Handler1;
publisher.SomethingHappened += Handler2;

Console.WriteLine("Both handlers are subscribed:");
publisher.Trigger();

publisher.SomethingHappened -= Handler1;

Console.WriteLine("\nAfter unsubscribing Handler 1:");
publisher.Trigger();

// --------------------------------------------------------------
// 2. Managing Subscriptions with IDisposable
// --------------------------------------------------------------
Console.WriteLine("\n2. Managing Subscriptions with IDisposable");
Console.WriteLine(new string('-', 40));

var service = new DataService();

using (var subscriber = new DataSubscriber(service))
{
    Console.WriteLine("Subscriber within using block:");
    service.RaiseDataChanged();
}

Console.WriteLine("\nSubscriber has been Disposed:");
service.RaiseDataChanged();
Console.WriteLine("(No output because it has been unsubscribed)");

Console.WriteLine("\n=== Example End ===\n");

// ============================================================
// Helper Classes
// ============================================================

public class Publisher
{
    public event EventHandler? SomethingHappened;

    public void Trigger()
    {
        SomethingHappened?.Invoke(this, EventArgs.Empty);
    }
}

// Managing subscriptions with IDisposable
public class DataService
{
    public event EventHandler? DataChanged;

    public void RaiseDataChanged()
    {
        DataChanged?.Invoke(this, EventArgs.Empty);
    }
}

public class DataSubscriber : IDisposable
{
    private readonly DataService _service;

    public DataSubscriber(DataService service)
    {
        _service = service;
        _service.DataChanged += OnDataChanged;
    }

    private void OnDataChanged(object? sender, EventArgs e)
    {
        Console.WriteLine("  DataSubscriber received data change notification");
    }

    public void Dispose()
    {
        _service.DataChanged -= OnDataChanged;
    }
}
