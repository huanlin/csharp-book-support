// 示範型別模式（Type Pattern）

Console.WriteLine("=== 型別模式範例 ===\n");

// 準備測試資料
object[] shapes = [
    new Circle(5),
    new Rectangle(4, 6),
    new Triangle(3, 8),
    "這是字串",
    42,
    null!
];

// --------------------------------------------------------------
// 1. 傳統寫法 vs 型別模式
// --------------------------------------------------------------
Console.WriteLine("1. 傳統寫法 vs 型別模式");
Console.WriteLine(new string('-', 40));

foreach (object? shape in shapes)
{
    // 型別模式：檢查與轉型一次完成
    if (shape is Circle c)
    {
        Console.WriteLine($"圓形，半徑：{c.Radius}，面積：{c.Area:F2}");
    }
    else if (shape is Rectangle r)
    {
        Console.WriteLine($"矩形，寬：{r.Width}，高：{r.Height}，面積：{r.Area:F2}");
    }
    else if (shape is Triangle t)
    {
        Console.WriteLine($"三角形，底：{t.Base}，高：{t.Height}，面積：{t.Area:F2}");
    }
    else if (shape is null)
    {
        Console.WriteLine("物件是 null");
    }
    else
    {
        Console.WriteLine($"其他型別：{shape.GetType().Name} = {shape}");
    }
}

// --------------------------------------------------------------
// 2. 處理 null 的行為
// --------------------------------------------------------------
Console.WriteLine("\n2. 處理 null 的行為");
Console.WriteLine(new string('-', 40));

object? obj = null;

// 型別模式不會匹配 null
if (obj is string s)
{
    Console.WriteLine($"是字串：{s}");
}
else if (obj is null)
{
    Console.WriteLine("obj 是 null（使用 is null 匹配）");
}

// --------------------------------------------------------------
// 3. 搭配 nullable reference types
// --------------------------------------------------------------
Console.WriteLine("\n3. 搭配 nullable reference types");
Console.WriteLine(new string('-', 40));

ProcessName("Alice");
ProcessName(null);
ProcessName("   ");

// --------------------------------------------------------------
// 4. is not 模式
// --------------------------------------------------------------
Console.WriteLine("\n4. is not 模式");
Console.WriteLine(new string('-', 40));

object value = "Hello";

if (value is not null)
{
    Console.WriteLine($"value 不是 null：{value}");
}

if (value is not int)
{
    Console.WriteLine($"value 不是 int，實際型別：{value.GetType().Name}");
}

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 輔助方法
// ============================================================

static void ProcessName(string? name)
{
    if (name is string validName && !string.IsNullOrWhiteSpace(validName))
    {
        // validName 是 string（非 null），編譯器知道這點
        Console.WriteLine($"名字：{validName.Trim()}，長度：{validName.Trim().Length}");
    }
    else
    {
        Console.WriteLine("沒有提供有效的名字");
    }
}

// ============================================================
// 形狀類別
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
