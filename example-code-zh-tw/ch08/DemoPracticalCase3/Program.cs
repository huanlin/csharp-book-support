// 案例 3：事件聚合器

Console.WriteLine("案例 3：事件聚合器");
Console.WriteLine(new string('-', 40));

var aggregator = new EventAggregator();

// 模組 A 訂閱訂單事件
aggregator.Subscribe<OrderPlaced>(evt =>
    Console.WriteLine($"  [訂單模組] 訂單 {evt.OrderId} 已建立，金額：${evt.Amount}"));

// 模組 B 訂閱付款事件
aggregator.Subscribe<PaymentReceived>(evt =>
    Console.WriteLine($"  [付款模組] 訂單 {evt.OrderId} 付款完成"));

// 模組 C 訂閱所有訂單事件
aggregator.Subscribe<OrderPlaced>(evt =>
    Console.WriteLine($"  [通知模組] 發送訂單確認郵件給客戶"));

Console.WriteLine("發布 OrderPlaced 事件：");
aggregator.Publish(new OrderPlaced("ORD-001", 1500));

Console.WriteLine("\n發布 PaymentReceived 事件：");
aggregator.Publish(new PaymentReceived("ORD-001"));

Console.ReadKey();

// ============================================================
// 輔助類別
// ============================================================

// 事件聚合器
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
