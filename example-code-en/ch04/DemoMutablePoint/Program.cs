MutablePoint p = new MutablePoint { X = 10, Y = 20 };
p.Move(5, 5);  // p becomes (15, 25)

// But...
MutablePoint[] points = new MutablePoint[1];
points[0] = new MutablePoint { X = 10, Y = 20 };
points[0].Move(5, 5);  // For arrays, this line successfully modifies the element

Console.WriteLine($"{points[0].X} , {points[0].Y}"); // "15 , 25"

// But... an even more confusing scenario
IList<MutablePoint> list = 
    new List<MutablePoint> { new MutablePoint { X = 10, Y = 20 } };
list[0].Move(5, 5);  // For List<T>, a "copy" is modified and then immediately lost

Console.WriteLine($"{list[0].X} , {list[0].Y}"); // "10 , 20"

// ✗ Not recommended: mutable struct
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
