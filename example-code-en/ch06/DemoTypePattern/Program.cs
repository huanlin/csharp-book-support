// Demo: Type Pattern

Console.WriteLine("=== Type Pattern Example ===\n");

// Prepare test data
object[] shapes = [
    new Circle(5),
    new Rectangle(4, 6),
    new Triangle(3, 8),
    "This is a string",
    42,
    null!
];

// --------------------------------------------------------------
// Traditional Way vs Type Pattern
// --------------------------------------------------------------
Console.WriteLine("Traditional Way vs Type Pattern");
Console.WriteLine(new string('-', 40));

foreach (object? shape in shapes)
{
    // Type pattern: Check and cast in one step
    if (shape is Circle c)
    {
        Console.WriteLine($"Circle, Radius: {c.Radius}, Area: {c.Area:F2}");
    }
    else if (shape is Rectangle r)
    {
        Console.WriteLine($"Rectangle, Width: {r.Width}, Height: {r.Height}, Area: {r.Area:F2}");
    }
    else if (shape is Triangle t)
    {
        Console.WriteLine($"Triangle, Base: {t.Base}, Height: {t.Height}, Area: {t.Area:F2}");
    }
    else if (shape is null)
    {
        Console.WriteLine("Object is null");
    }
    else
    {
        Console.WriteLine($"Other Type: {shape.GetType().Name} = {shape}");
    }
}

Console.WriteLine("\n=== Example End ===");

// ============================================================
// Shape Classes
// ============================================================

public abstract class Shape
{
    public abstract double Area { get; }
}

public class Circle : Shape
{
    public double Radius { get; }
    public Circle(double radius) => Radius = radius;
    public override double Area => Math.PI * Radius * Radius;
}

public class Rectangle : Shape
{
    public double Width { get; }
    public double Height { get; }
    public Rectangle(double width, double height) => (Width, Height) = (width, height);
    public override double Area => Width * Height;
}

public class Triangle : Shape
{
    public double Base { get; }
    public double Height { get; }
    public Triangle(double @base, double height) => (Base, Height) = (@base, height);
    public override double Area => Base * Height / 2;
}
