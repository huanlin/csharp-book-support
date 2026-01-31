// 示範事件（Events）的用法

Console.WriteLine("=== 事件範例 ===\n");

// --------------------------------------------------------------
// 1. 委派 vs 事件
// --------------------------------------------------------------
Console.WriteLine("1. 為什麼需要事件（委派的問題）");
Console.WriteLine(new string('-', 40));

// 使用委派的問題示範
var buttonWithDelegate = new ButtonWithDelegate();
buttonWithDelegate.Clicked += () => Console.WriteLine("  訂閱者 A");
buttonWithDelegate.Clicked += () => Console.WriteLine("  訂閱者 B");

Console.WriteLine("使用委派時，外部可以做壞事：");
Console.WriteLine("  - 可以用 = 覆蓋所有訂閱者");
Console.WriteLine("  - 可以直接呼叫 Invoke()");

// --------------------------------------------------------------
// 2. 基本事件用法
// --------------------------------------------------------------
Console.WriteLine("\n2. 使用事件（保護訂閱）");
Console.WriteLine(new string('-', 40));

var button = new Button();
button.Clicked += () => Console.WriteLine("  訂閱者 A 收到點擊通知");
button.Clicked += () => Console.WriteLine("  訂閱者 B 收到點擊通知");

Console.WriteLine("模擬點擊按鈕：");
button.SimulateClick();

// button.Clicked = null;  // 編譯錯誤！
// button.Clicked.Invoke();  // 編譯錯誤！

// --------------------------------------------------------------
// 3. 標準事件模式
// --------------------------------------------------------------
Console.WriteLine("\n3. 標準事件模式（EventHandler<T>）");
Console.WriteLine(new string('-', 40));

var product = new Product("iPhone", 999);

product.PriceChanged += (sender, e) =>
{
    Console.WriteLine($"  價格從 ${e.OldPrice} 變更為 ${e.NewPrice}");
};

product.Price = 899;
product.Price = 899;  // 無輸出（價格未變）
product.Price = 799;

// --------------------------------------------------------------
// 4. 訂閱與取消訂閱
// --------------------------------------------------------------
Console.WriteLine("\n4. 訂閱與取消訂閱");
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
// 5. 使用 IDisposable 管理訂閱
// --------------------------------------------------------------
Console.WriteLine("\n5. 使用 IDisposable 管理訂閱");
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

public class ButtonWithDelegate
{
    public Action? Clicked;  // 公開委派欄位（不安全）

    public void SimulateClick() => Clicked?.Invoke();
}

public class Button
{
    public event Action? Clicked;  // 使用 event 關鍵字

    public void SimulateClick() => Clicked?.Invoke();
}

// 標準事件模式
public class PriceChangedEventArgs : EventArgs
{
    public decimal OldPrice { get; init; }
    public decimal NewPrice { get; init; }
}

public class Product
{
    public string Name { get; }
    private decimal _price;

    public event EventHandler<PriceChangedEventArgs>? PriceChanged;

    public Product(string name, decimal price)
    {
        Name = name;
        _price = price;
    }

    public decimal Price
    {
        get => _price;
        set
        {
            if (_price != value)
            {
                var args = new PriceChangedEventArgs
                {
                    OldPrice = _price,
                    NewPrice = value
                };

                _price = value;
                OnPriceChanged(args);
            }
        }
    }

    protected virtual void OnPriceChanged(PriceChangedEventArgs e)
    {
        PriceChanged?.Invoke(this, e);
    }
}

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
