// デモ: 標準イベントパターン（EventHandler<T>）

Console.WriteLine("=== 標準イベントパターン ===\n");

var product = new Product("iPhone", 999);

product.PriceChanged += (sender, e) =>
{
    Console.WriteLine($"  価格変更: ${e.OldPrice} -> ${e.NewPrice}");
};

product.Price = 899;
product.Price = 899;  // 変化なし
product.Price = 799;

Console.WriteLine("\n=== 例の終了 ===\n");

// ============================================================
// ヘルパークラス
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
