// Demo: defensive copy when a readonly method calls a non-readonly method

var point = new Point { X = 3, Y = 4 };
point.PrintInfo();

struct Point
{
    public int X;
    public int Y;

    public readonly void PrintInfo()
    {
        Console.WriteLine($"Point: ({X}, {Y})");
        LogState(); // This line produces CS8656
    }

    private void LogState()
    {
        Console.WriteLine($"Current state: X={X}, Y={Y}");
    }
}
