// 示範 record 型別與值相等性

// 比較 class 與 record 的相等性差異
Console.WriteLine("=== record 值相等性 ===");
var r1 = new Person("Alice", 30);
var r2 = new Person("Alice", 30);
Console.WriteLine($"r1 == r2: {r1 == r2}");  // True（值相等）
Console.WriteLine($"r1.Equals(r2): {r1.Equals(r2)}");  // True
Console.WriteLine($"ReferenceEquals(r1, r2): {ReferenceEquals(r1, r2)}");  // False（不同實例）

Console.WriteLine();
Console.WriteLine("=== class 參考相等性 ===");
var c1 = new PersonClass { Name = "Alice", Age = 30 };
var c2 = new PersonClass { Name = "Alice", Age = 30 };
Console.WriteLine($"c1 == c2: {c1 == c2}");  // False（不同的實例）
Console.WriteLine($"c1.Equals(c2): {c1.Equals(c2)}");  // False

Console.WriteLine();
Console.WriteLine("=== record 的 ToString ===");
Console.WriteLine(r1);  // Person { Name = Alice, Age = 30 }

Console.WriteLine();
Console.WriteLine("=== record struct ===");
var ps1 = new PointStruct(10, 20);
ps1.X = 100;  // record struct 預設是可變的
Console.WriteLine($"PointStruct: {ps1}");

var ip1 = new ImmutablePoint(10, 20);
// ip1.X = 100;  // ✗ 編譯錯誤：readonly record struct 是不可變的
Console.WriteLine($"ImmutablePoint: {ip1}");

// 主要建構子語法（Primary Constructor）
public record Person(string Name, int Age);

// record struct（值型別的 record）
public record struct PointStruct(int X, int Y);

// readonly record struct（不可變的值型別 record）
public readonly record struct ImmutablePoint(int X, int Y);

class PersonClass
{
    public string Name { get; set; } = "";
    public int Age { get; set; }
}
