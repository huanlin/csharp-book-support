// Case 3: Event Aggregator

Console.WriteLine("Case 3: Event Aggregator");
Console.WriteLine(new string('-', 40));

var aggregator = new EventAggregator();

// Module A subscribes to order events
aggregator.Subscribe<OrderPlaced>(evt =>
    Console.WriteLine($"  [Order Module] Order {evt.OrderId} created, Amount: ${evt.Amount}"));

// Module B subscribes to payment events
aggregator.Subscribe<PaymentReceived>(evt =>
    Console.WriteLine($"  [Payment Module] Order {evt.OrderId} payment completed"));

// Module C subscribes to all order events
aggregator.Subscribe<OrderPlaced>(evt =>
    Console.WriteLine($"  [Notification Module] Sending order confirmation email to customer"));

Console.WriteLine("Publishing OrderPlaced event:");
aggregator.Publish(new OrderPlaced("ORD-001", 1500));

Console.WriteLine("\nPublishing PaymentReceived event:");
aggregator.Publish(new PaymentReceived("ORD-001"));

Console.ReadKey();

// ============================================================
// Helper Classes
// ============================================================

// Event Aggregator
public record OrderPlaced(string OrderId, decimal Amount);
public record PaymentReceived(string OrderId);

public class EventAggregator
{
    private readonly Dictionary<Type, List<Delegate>> _subscribers = new();

    public void Subscribe<TEvent>(Action<TEvent> handler)
    {
        var eventType = typeof(TEvent);

        if (!_subscribers.ContainsKey(eventType))
            _subscribers[eventType] = new List<Delegate>();

        _subscribers[eventType].Add(handler);
    }

    public void Publish<TEvent>(TEvent eventData)
    {
        var eventType = typeof(TEvent);

        if (_subscribers.TryGetValue(eventType, out var handlers))
        {
            foreach (Action<TEvent> handler in handlers.Cast<Action<TEvent>>())
            {
                handler(eventData);
            }
        }
    }
}
