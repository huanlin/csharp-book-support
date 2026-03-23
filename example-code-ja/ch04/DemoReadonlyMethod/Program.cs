// デモ: readonly メソッドが non-readonly メソッドを呼び出すときの defensive copy

var point = new Point { X = 3, Y = 4 };
point.PrintInfo();

struct Point
{
    public int X;
    public int Y;

    public readonly void PrintInfo()
    {
        Console.WriteLine($"Point: ({X}, {Y})");
        LogState(); // この行で CS8656 が出る
    }

    private void LogState()
    {
        Console.WriteLine($"Current state: X={X}, Y={Y}");
    }
}
