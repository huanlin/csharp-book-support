// 示範相等性比較（Equality Comparison）

Console.WriteLine("=== class 手動實作值相等 ===");
var p1 = new Person("Alice", 30);
var p2 = new Person("Alice", 30);
Console.WriteLine($"p1 == p2: {p1 == p2}");  // True
Console.WriteLine($"p1.Equals(p2): {p1.Equals(p2)}");  // True
Console.WriteLine($"ReferenceEquals(p1, p2): {ReferenceEquals(p1, p2)}");  // False

Console.WriteLine();
Console.WriteLine("=== record 自動值相等 ===");
var r1 = new PersonRecord("Alice", 30);
var r2 = new PersonRecord("Alice", 30);
Console.WriteLine($"r1 == r2: {r1 == r2}");  // True
Console.WriteLine($"r1.Equals(r2): {r1.Equals(r2)}");  // True

Console.WriteLine();
Console.WriteLine("=== GetHashCode 的重要性 ===");
var dict = new Dictionary<Person, string>();
dict[p1] = "First";
Console.WriteLine($"dict[p1]: {dict[p1]}");
Console.WriteLine($"dict[p2]: {dict[p2]}");  // 找得到！因為 GetHashCode 一致

Console.WriteLine();
Console.WriteLine("=== HashCode.Combine 示範 ===");
Console.WriteLine($"p1.GetHashCode(): {p1.GetHashCode()}");
Console.WriteLine($"p2.GetHashCode(): {p2.GetHashCode()}");
Console.WriteLine($"HashCode 相同: {p1.GetHashCode() == p2.GetHashCode()}");

// 手動實作值相等的 class
public class Person : IEquatable<Person>
{
    public string Name { get; }
    public int Age { get; }

    public Person(string name, int age) => (Name, Age) = (name, age);

    // 1. 實作 IEquatable<T>
    public bool Equals(Person? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name && Age == other.Age;
    }

    // 2. 覆寫 Object.Equals
    public override bool Equals(object? obj)
    {
        return Equals(obj as Person);
    }

    // 3. 覆寫 GetHashCode (必須與 Equals 一致)
    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Age);
    }

    // 4. 覆寫運算子
    public static bool operator ==(Person? left, Person? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    public static bool operator !=(Person? left, Person? right) => !(left == right);
}

// record 自動實作值相等
public record PersonRecord(string Name, int Age);
