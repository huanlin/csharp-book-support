// デモ: 値型（Value Type）のコピー挙動

Console.WriteLine("=== 値型のコピー挙動 ===");

int a = 10;
int b = a;  // 値 10 を b にコピー
b = 99;     // b を変更しても a には影響しない

Console.WriteLine($"a = {a}");  // 10
Console.WriteLine($"b = {b}");  // 99
Console.WriteLine("結論: b は a のコピーなので、b を変更しても a は変わらない");

Console.WriteLine();
Console.WriteLine("=== struct も値型 ===");

var p1 = new Point(10, 20);
var p2 = p1;  // struct 全体をコピー
p2.X = 999;   // p2 の変更は p1 に影響しない

Console.WriteLine($"p1 = ({p1.X}, {p1.Y})");  // (10, 20)
Console.WriteLine($"p2 = ({p2.X}, {p2.Y})");  // (999, 20)
Console.WriteLine("結論: struct は値型であり、代入時に構造全体がコピーされる");

// シンプルな struct 定義
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
