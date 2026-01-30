MutablePoint p = new MutablePoint { X = 10, Y = 20 };
p.Move(5, 5);  // p 變成 (15, 25)

// 但是...
MutablePoint[] points = new MutablePoint[1];
points[0] = new MutablePoint { X = 10, Y = 20 };
points[0].Move(5, 5);  // 對於陣列，這行會成功修改元素

Console.WriteLine($"{points[0].X} , {points[0].Y}"); // "15 , 25"

// 但是...更令人困惑的情況
IList<MutablePoint> list = 
    new List<MutablePoint> { new MutablePoint { X = 10, Y = 20 } };
list[0].Move(5, 5);  // List<T> 則是修改了「複本」後隨即遺失

Console.WriteLine($"{list[0].X} , {list[0].Y}"); // "10 , 20"

// ✗ 不建議：可變的 struct
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
