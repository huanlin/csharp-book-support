// デモ: イベント購読管理とメモリリーク回避

Console.WriteLine("=== イベントとメモリリーク ===\n");

// --------------------------------------------------------------
// 1. 購読と解除
// --------------------------------------------------------------
Console.WriteLine("1. 購読と解除");
Console.WriteLine(new string('-', 40));

var publisher = new Publisher();

void Handler1(object? sender, EventArgs e) => Console.WriteLine("  ハンドラー 1");
void Handler2(object? sender, EventArgs e) => Console.WriteLine("  ハンドラー 2");

publisher.SomethingHappened += Handler1;
publisher.SomethingHappened += Handler2;

Console.WriteLine("両方のハンドラーを購読:");
publisher.Trigger();

publisher.SomethingHappened -= Handler1;

Console.WriteLine("\nハンドラー 1 解除後:");
publisher.Trigger();

// --------------------------------------------------------------
// 2. IDisposable で購読管理
// --------------------------------------------------------------
Console.WriteLine("\n2. IDisposable で購読管理");
Console.WriteLine(new string('-', 40));

var service = new DataService();

using (var subscriber = new DataSubscriber(service))
{
    Console.WriteLine("using ブロック内の購読者:");
    service.RaiseDataChanged();
}

Console.WriteLine("\n購読者を Dispose 後:");
service.RaiseDataChanged();
Console.WriteLine("（解除済みなので出力なし）");

Console.WriteLine("\n=== 例の終了 ===\n");

// ============================================================
// ヘルパークラス
// ============================================================

public class Publisher
{
    public event EventHandler? SomethingHappened;

    public void Trigger()
    {
        SomethingHappened?.Invoke(this, EventArgs.Empty);
    }
}

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
        Console.WriteLine("  DataSubscriber がデータ変更通知を受信");
    }

    public void Dispose()
    {
        _service.DataChanged -= OnDataChanged;
    }
}
