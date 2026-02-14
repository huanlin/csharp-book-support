// Demo: Standard Event Pattern (EventHandler<T>)

Console.WriteLine("=== Standard Event Pattern ===\n");

var product = new Product("iPhone", 999);

product.PriceChanged += (sender, e) =>
{
    Console.WriteLine($"  Price changed from ${e.OldPrice} to ${e.NewPrice}");
};

product.Price = 899;
product.Price = 899;  // No output (price unchanged)
product.Price = 799;

Console.WriteLine("\n=== Example End ===\n");

// ============================================================
// Helper Classes
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
