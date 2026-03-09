// デモ: 可変 struct の落とし穴

MutablePoint p = new MutablePoint { X = 10, Y = 20 };
p.Move(5, 5);  // p は (15, 25)

// ただし...
MutablePoint[] points = new MutablePoint[1];
points[0] = new MutablePoint { X = 10, Y = 20 };
points[0].Move(5, 5);  // 配列では要素そのものが更新される

Console.WriteLine($"{points[0].X} , {points[0].Y}"); // "15 , 25"

// さらに紛らわしい例
IList<MutablePoint> list =
    new List<MutablePoint> { new MutablePoint { X = 10, Y = 20 } };
list[0].Move(5, 5);  // List<T> では一時コピーに対して変更され、結果が失われる

Console.WriteLine($"{list[0].X} , {list[0].Y}"); // "10 , 20"

// ✗ 推奨しない: mutable struct
public struct MutablePoint
{
    public int X;
    public int Y;

    public void Move(int dx, int dy)
    {
        X += dx;
        Y += dy;
    }
}
