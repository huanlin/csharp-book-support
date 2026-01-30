// 示範 readonly struct 與 readonly 方法

// 主程式
var m1 = new Money(100, "TWD");
var m2 = new Money(50, "TWD");
var m3 = m1.Add(m2);

Console.WriteLine($"m1: {m1.Amount} {m1.Currency}");
Console.WriteLine($"m2: {m2.Amount} {m2.Currency}");
Console.WriteLine($"m3: {m3.Amount} {m3.Currency}");

var p = new Point { X = 3, Y = 4 };
Console.WriteLine($"Point: ({p.X}, {p.Y}), Distance: {p.GetDistance()}");

p.Reset();
Console.WriteLine($"After Reset: ({p.X}, {p.Y})");

// Money 是一個 readonly struct，建立後無法修改其狀態
readonly struct Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException("幣別不同，無法相加");

        return new Money(Amount + other.Amount, Currency);
    }
}

// Point 示範 readonly 方法
struct Point
{
    public int X;
    public int Y;

    // readonly 方法保證不會修改欄位
    public readonly double GetDistance()
    {
        return Math.Sqrt(X * X + Y * Y);
    }

    // 這個方法可以修改欄位
    public void Reset()
    {
        X = Y = 0;
    }
}
