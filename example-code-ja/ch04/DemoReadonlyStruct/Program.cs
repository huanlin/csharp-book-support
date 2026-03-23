// デモ: readonly struct と readonly メソッド

var m1 = new Money(100, "TWD");
var m2 = new Money(50, "TWD");
var m3 = m1.Add(m2);

Console.WriteLine($"m1: {m1.Amount} {m1.Currency}");
Console.WriteLine($"m2: {m2.Amount} {m2.Currency}");
Console.WriteLine($"m3: {m3.Amount} {m3.Currency}");

var p = new Point { X = 3, Y = 4 };
Console.WriteLine($"Point: ({p.X}, {p.Y}), Distance: {p.GetDistance()}");

p.Reset();
Console.WriteLine($"Reset 後: ({p.X}, {p.Y})");

// Money は readonly struct であり、生成後は状態を変更できない
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
            throw new InvalidOperationException("異なる通貨は加算できません");

        return new Money(Amount + other.Amount, Currency);
    }
}

// Point は readonly メソッドの例
struct Point
{
    public int X;
    public int Y;

    // readonly メソッドはフィールドを変更しないことを保証する
    public readonly double GetDistance()
    {
        return Math.Sqrt(X * X + Y * Y);
    }

    // このメソッドはフィールドを変更できる
    public void Reset()
    {
        X = Y = 0;
    }
}
