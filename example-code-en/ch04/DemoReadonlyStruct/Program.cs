// Demo: readonly struct and readonly methods

// Main program
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

var large = new LargeStruct { Field1 = 1, Field2 = 2 };
large.Process();

// Money is a readonly struct, its state cannot be modified after creation
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
            throw new InvalidOperationException("Different currencies cannot be added");

        return new Money(Amount + other.Amount, Currency);
    }
}

// Point demonstrates readonly methods
struct Point
{
    public int X;
    public int Y;

    // readonly method guarantees that fields won't be modified
    public readonly double GetDistance()
    {
        return Math.Sqrt(X * X + Y * Y);
    }

    // This method can modify fields
    public void Reset()
    {
        X = Y = 0;
    }
}

// Demonstrates a defensive copy: a readonly method calls a non-readonly method.
struct LargeStruct
{
    public int Field1;
    public int Field2;

    public readonly void Process()
    {
        Helper(); // This line produces CS8656
    }

    private void Helper()
    {
        Console.WriteLine($"Processing: {Field1}, {Field2}");
    }
}
