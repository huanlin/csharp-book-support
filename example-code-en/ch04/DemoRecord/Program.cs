// Demo: record type and value equality

// Compare equality differences between class and record
Console.WriteLine("=== record Value Equality ===");
var r1 = new Person("Alice", 30);
var r2 = new Person("Alice", 30);
Console.WriteLine($"r1 == r2: {r1 == r2}");  // True (value equality)
Console.WriteLine($"r1.Equals(r2): {r1.Equals(r2)}");  // True
Console.WriteLine($"ReferenceEquals(r1, r2): {ReferenceEquals(r1, r2)}");  // False (different instances)

Console.WriteLine();
Console.WriteLine("=== class Reference Equality ===");
var c1 = new PersonClass { Name = "Alice", Age = 30 };
var c2 = new PersonClass { Name = "Alice", Age = 30 };
Console.WriteLine($"c1 == c2: {c1 == c2}");  // False (different instances)
Console.WriteLine($"c1.Equals(c2): {c1.Equals(c2)}");  // False

Console.WriteLine();
Console.WriteLine("=== record ToString ===");
Console.WriteLine(r1);  // Person { Name = Alice, Age = 30 }

Console.WriteLine();
Console.WriteLine("=== record struct ===");
var ps1 = new PointStruct(10, 20);
ps1.X = 100;  // record struct is mutable by default
Console.WriteLine($"PointStruct: {ps1}");

var ip1 = new ImmutablePoint(10, 20);
// ip1.X = 100;  // ✗ Compilation error: readonly record struct is immutable
Console.WriteLine($"ImmutablePoint: {ip1}");

// Primary Constructor syntax
public record Person(string Name, int Age);

// record struct (value type record)
public record struct PointStruct(int X, int Y);

// readonly record struct (immutable value type record)
public readonly record struct ImmutablePoint(int X, int Y);

class PersonClass
{
    public string Name { get; set; } = "";
    public int Age { get; set; }
}
