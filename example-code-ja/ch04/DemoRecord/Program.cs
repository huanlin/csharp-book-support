// デモ: record と値等価

// class と record の等価比較の違い
Console.WriteLine("=== record の値等価 ===");
var r1 = new Person("Alice", 30);
var r2 = new Person("Alice", 30);
Console.WriteLine($"r1 == r2: {r1 == r2}");  // True（値等価）
Console.WriteLine($"r1.Equals(r2): {r1.Equals(r2)}");  // True
Console.WriteLine($"ReferenceEquals(r1, r2): {ReferenceEquals(r1, r2)}");  // False（別インスタンス）

Console.WriteLine();
Console.WriteLine("=== class の参照等価 ===");
var c1 = new PersonClass { Name = "Alice", Age = 30 };
var c2 = new PersonClass { Name = "Alice", Age = 30 };
Console.WriteLine($"c1 == c2: {c1 == c2}");  // False（別インスタンス）
Console.WriteLine($"c1.Equals(c2): {c1.Equals(c2)}");  // False

Console.WriteLine();
Console.WriteLine("=== record の ToString ===");
Console.WriteLine(r1);  // Person { Name = Alice, Age = 30 }

Console.WriteLine();
Console.WriteLine("=== record struct ===");
var ps1 = new PointStruct(10, 20);
ps1.X = 100;  // record struct は既定で可変
Console.WriteLine($"PointStruct: {ps1}");

var ip1 = new ImmutablePoint(10, 20);
// ip1.X = 100;  // ✗ readonly record struct は不変
Console.WriteLine($"ImmutablePoint: {ip1}");

// Primary Constructor 構文
public record Person(string Name, int Age);

// record struct（値型 record）
public record struct PointStruct(int X, int Y);

// readonly record struct（不変の値型 record）
public readonly record struct ImmutablePoint(int X, int Y);

class PersonClass
{
    public string Name { get; set; } = "";
    public int Age { get; set; }
}
