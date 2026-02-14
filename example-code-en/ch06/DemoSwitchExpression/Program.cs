// Demo: Switch Expression

Console.WriteLine("=== Switch Expression Example ===\n");

// --------------------------------------------------------------
// 1. Basic Switch Expression
// --------------------------------------------------------------
Console.WriteLine("1. Basic Switch Expression");
Console.WriteLine(new string('-', 40));

foreach (DayOfWeek day in Enum.GetValues<DayOfWeek>())
{
    string dayType = GetDayType(day);
    Console.WriteLine($"{day}: {dayType}");
}

// --------------------------------------------------------------
// 2. Type Pattern in Switch Expression
// --------------------------------------------------------------
Console.WriteLine("\n2. Type Pattern in Switch Expression");
Console.WriteLine(new string('-', 40));

object[] shapes = [
    new Circle(5),
    new Rectangle(4, 6),
    new Triangle(3, 8),
    null!
];

foreach (object? shape in shapes)
{
    string description = DescribeShape(shape);
    Console.WriteLine(description);
}

// --------------------------------------------------------------
// 3. Using when clause to add conditions
// --------------------------------------------------------------
Console.WriteLine("\n3. Using when clause to add conditions");
Console.WriteLine(new string('-', 40));

int[] numbers = [-5, 0, 7, 12, 100];

foreach (int n in numbers)
{
    string description = DescribeNumber(n);
    Console.WriteLine($"{n}: {description}");
}

// --------------------------------------------------------------
// 4. Exhaustiveness Check and Default Branch
// --------------------------------------------------------------
Console.WriteLine("\n4. Exhaustiveness Check");
Console.WriteLine(new string('-', 40));

Shape[] allShapes = [
    new Circle(3),
    new Rectangle(2, 4),
    new Triangle(5, 6)
];

foreach (Shape s in allShapes)
{
    // If Shape is a sealed type hierarchy, the compiler can check exhaustiveness
    string name = DescribeShapeExhaustive(s);
    Console.WriteLine(name);
}

Console.WriteLine("\n=== Example End ===");

// ============================================================
// Switch Expression Methods
// ============================================================

static string GetDayType(DayOfWeek day) => day switch
{
    DayOfWeek.Saturday or DayOfWeek.Sunday => "Weekend",
    _ => "Weekday"
};

static string DescribeShape(object? shape) => shape switch
{
    Circle c => $"Circle, Radius {c.Radius}, Area {c.Area:F2}",
    Rectangle r => $"Rectangle, {r.Width} x {r.Height}, Area {r.Area:F2}",
    Triangle t => $"Triangle, Base {t.Base}, Height {t.Height}, Area {t.Area:F2}",
    null => "No Shape",
    _ => "Unknown Shape"
};

static string DescribeNumber(int n) => n switch
{
    int x when x < 0 => "Negative number",
    0 => "Zero",
    int x when x % 2 == 0 => "Positive even number",
    _ => "Positive odd number"
};

static string DescribeShapeExhaustive(Shape shape) => shape switch
{
    Circle => "Circle",
    Rectangle => "Rectangle",
    Triangle => "Triangle",
    _ => throw new ArgumentException("Unknown shape type")
};

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
