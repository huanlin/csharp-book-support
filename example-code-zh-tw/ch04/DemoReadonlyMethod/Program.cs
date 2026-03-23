// 示範 defensive copy：readonly 方法呼叫非 readonly 方法

var point = new Point { X = 3, Y = 4 };
point.PrintInfo();

struct Point
{
    public int X;
    public int Y;

    public readonly void PrintInfo()
    {
        Console.WriteLine($"Point: ({X}, {Y})");
        LogState(); // 這裡會出現 CS8656
    }

    private void LogState()
    {
        Console.WriteLine($"Current state: X={X}, Y={Y}");
    }
}
