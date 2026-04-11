// 示範標準事件模式（EventHandler<T>）

Console.WriteLine("=== 標準事件模式 ===\n");

var product = new Product("iPhone", 999);

product.PriceChanged += (sender, e) =>
{
    Console.WriteLine($"  價格從 ${e.OldPrice} 變更為 ${e.NewPrice}");
};

product.Price = 899;
product.Price = 899;  // 無輸出（價格未變）
product.Price = 799;

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 輔助類別
// ============================================================

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
