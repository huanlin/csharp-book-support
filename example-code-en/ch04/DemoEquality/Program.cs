// Demo: Equality Comparison

Console.WriteLine("=== Class: Manual value equality implementation ===");
var p1 = new Person("Alice", 30);
var p2 = new Person("Alice", 30);
Console.WriteLine($"p1 == p2: {p1 == p2}");  // True
Console.WriteLine($"p1.Equals(p2): {p1.Equals(p2)}");  // True
Console.WriteLine($"ReferenceEquals(p1, p2): {ReferenceEquals(p1, p2)}");  // False

Console.WriteLine();
Console.WriteLine("=== Record: Automatic value equality ===");
var r1 = new PersonRecord("Alice", 30);
var r2 = new PersonRecord("Alice", 30);
Console.WriteLine($"r1 == r2: {r1 == r2}");  // True
Console.WriteLine($"r1.Equals(r2): {r1.Equals(r2)}");  // True

Console.WriteLine();
Console.WriteLine("=== Importance of GetHashCode ===");
var dict = new Dictionary<Person, string>();
dict[p1] = "First";
Console.WriteLine($"dict[p1]: {dict[p1]}");
Console.WriteLine($"dict[p2]: {dict[p2]}");  // Found! Because GetHashCode is consistent Across instances
 Console.WriteLine(); Console.WriteLine("=== HashCode.Combine Demo ==="); Console.WriteLine($"p1.GetHashCode(): {p1.GetHashCode()}"); Console.WriteLine($"p2.GetHashCode(): {p2.GetHashCode()}"); Console.WriteLine($"HashCode same: {p1.GetHashCode() == p2.GetHashCode()}");

// Class with manually implemented value equality
public class Person : IEquatable<Person>
{
    public string Name { get; }
    public int Age { get; }

    public Person(string name, int age) => (Name, Age) = (name, age);

    // 1. Implement IEquatable<T>
    public bool Equals(Person? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name && Age == other.Age;
    }

    // 2. Override Object.Equals
    public override bool Equals(object? obj)
    {
        return Equals(obj as Person);
    }

    // 3. Override GetHashCode (must be consistent with Equals)
    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Age);
    }

    // 4. Override operators
    public static bool operator ==(Person? left, Person? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    public static bool operator !=(Person? left, Person? right) => !(left == right);
}

// record automatically implements value equality
public record PersonRecord(string Name, int Age);
