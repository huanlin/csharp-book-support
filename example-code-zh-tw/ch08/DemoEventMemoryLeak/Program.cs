// 示範事件訂閱管理與記憶體洩漏的避免

Console.WriteLine("=== 事件與記憶體洩漏 ===\n");

// --------------------------------------------------------------
// 1. 訂閱與取消訂閱
// --------------------------------------------------------------
Console.WriteLine("1. 訂閱與取消訂閱");
Console.WriteLine(new string('-', 40));

var publisher = new Publisher();

void Handler1(object? sender, EventArgs e) => Console.WriteLine("  處理器 1");
void Handler2(object? sender, EventArgs e) => Console.WriteLine("  處理器 2");

publisher.SomethingHappened += Handler1;
publisher.SomethingHappened += Handler2;

Console.WriteLine("兩個處理器都訂閱：");
publisher.Trigger();

publisher.SomethingHappened -= Handler1;

Console.WriteLine("\n取消訂閱處理器 1 後：");
publisher.Trigger();

// --------------------------------------------------------------
// 2. 使用 IDisposable 管理訂閱
// --------------------------------------------------------------
Console.WriteLine("\n2. 使用 IDisposable 管理訂閱");
Console.WriteLine(new string('-', 40));

var service = new DataService();

using (var subscriber = new DataSubscriber(service))
{
    Console.WriteLine("訂閱者在 using 區塊內：");
    service.RaiseDataChanged();
}

Console.WriteLine("\n訂閱者已 Dispose：");
service.RaiseDataChanged();
Console.WriteLine("（無輸出，因為已取消訂閱）");

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 輔助類別
// ============================================================

public class Publisher
{
    public event EventHandler? SomethingHappened;

    public void Trigger()
    {
        SomethingHappened?.Invoke(this, EventArgs.Empty);
    }
}

// IDisposable 管理訂閱
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
        Console.WriteLine("  DataSubscriber 收到資料變更通知");
    }

    public void Dispose()
    {
        _service.DataChanged -= OnDataChanged;
    }
}
