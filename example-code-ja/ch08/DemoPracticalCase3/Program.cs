// ケース 3: Event Aggregator

Console.WriteLine("ケース 3: Event Aggregator");
Console.WriteLine(new string('-', 40));

var aggregator = new EventAggregator();

// モジュール A: 注文イベント購読
aggregator.Subscribe<OrderPlaced>(evt =>
    Console.WriteLine($"  [注文モジュール] 注文 {evt.OrderId} が作成, 金額: ${evt.Amount}"));

// モジュール B: 入金イベント購読
aggregator.Subscribe<PaymentReceived>(evt =>
    Console.WriteLine($"  [決済モジュール] 注文 {evt.OrderId} の入金完了"));

// モジュール C: 注文イベント購読
aggregator.Subscribe<OrderPlaced>(evt =>
    Console.WriteLine("  [通知モジュール] 注文確認メールを送信"));

Console.WriteLine("OrderPlaced イベント発行:");
aggregator.Publish(new OrderPlaced("ORD-001", 1500));

Console.WriteLine("\nPaymentReceived イベント発行:");
aggregator.Publish(new PaymentReceived("ORD-001"));

Console.ReadKey();

// ============================================================
// ヘルパークラス
// ============================================================

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
