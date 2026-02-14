// Demo: Copy behavior of Value Types

Console.WriteLine("=== Copy behavior of Value Types ===");

int a = 10;
int b = a;  // Copy the value 10 to b
b = 99;     // Modifying b does not affect a

Console.WriteLine($"a = {a}");  // Output 10
Console.WriteLine($"b = {b}");  // Output 99
Console.WriteLine($"Conclusion: Modifying b does not affect a, because b is a copy of a");

Console.WriteLine();
Console.WriteLine("=== struct is also a Value Type ===");

var p1 = new Point(10, 20);
var p2 = p1;  // Copy the entire struct
p2.X = 999;   // Modifying p2 does not affect p1

Console.WriteLine($"p1 = ({p1.X}, {p1.Y})");  // (10, 20)
Console.WriteLine($"p2 = ({p2.X}, {p2.Y})");  // (999, 20)
Console.WriteLine($"Conclusion: struct is a Value Type; the entire structure is copied during assignment");

// 定義一個簡單的 struct
struct Point
{
    public int X;
    public int Y;

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}
