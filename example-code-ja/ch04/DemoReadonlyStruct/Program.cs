// デモ: readonly struct と readonly メソッド

// メイン処理
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

var large = new LargeStruct { Field1 = 1, Field2 = 2 };
large.Process();

// Money は readonly struct: 生成後に状態変更不可
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

    // readonly メソッド: フィールドを変更しないことを保証
    public readonly double GetDistance()
    {
        return Math.Sqrt(X * X + Y * Y);
    }

    // こちらはフィールド変更可能
    public void Reset()
    {
        X = Y = 0;
    }
}

// defensive copy の例: readonly メソッドから non-readonly メソッドを呼び出す
struct LargeStruct
{
    public int Field1;
    public int Field2;

    public readonly void Process()
    {
        Helper(); // ここで CS8656 が出る
    }

    private void Helper()
    {
        Console.WriteLine($"Processing: {Field1}, {Field2}");
    }
}
