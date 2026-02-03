// 示範實值型別 (Value Types) 的複製行為

Console.WriteLine("=== 實值型別的複製行為 ===");

int a = 10;
int b = a;  // 複製一份 10 給 b
b = 99;     // 修改 b 不會影響 a

Console.WriteLine($"a = {a}");  // 輸出 10
Console.WriteLine($"b = {b}");  // 輸出 99
Console.WriteLine($"結論：修改 b 不會影響 a，因為 b 是 a 的複本");

Console.WriteLine();
Console.WriteLine("=== struct 也是實值型別 ===");

var p1 = new Point(10, 20);
var p2 = p1;  // 複製整個 struct
p2.X = 999;   // 修改 p2 不會影響 p1

Console.WriteLine($"p1 = ({p1.X}, {p1.Y})");  // (10, 20)
Console.WriteLine($"p2 = ({p2.X}, {p2.Y})");  // (999, 20)
Console.WriteLine($"結論：struct 是實值型別，賦值時會複製整個結構");

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
