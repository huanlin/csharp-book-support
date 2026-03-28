// デモ: 等価比較

Console.WriteLine("=== class: 手動で値等価を実装 ===");
var p1 = new Person("Alice", 30);
var p2 = new Person("Alice", 30);
Console.WriteLine($"p1 == p2: {p1 == p2}");  // True
Console.WriteLine($"p1.Equals(p2): {p1.Equals(p2)}");  // True
Console.WriteLine($"ReferenceEquals(p1, p2): {ReferenceEquals(p1, p2)}");  // False

Console.WriteLine();
Console.WriteLine("=== record: 自動の値等価 ===");
var r1 = new PersonRecord("Alice", 30);
var r2 = new PersonRecord("Alice", 30);
Console.WriteLine($"r1 == r2: {r1 == r2}");  // True
Console.WriteLine($"r1.Equals(r2): {r1.Equals(r2)}");  // True

Console.WriteLine();
Console.WriteLine("=== GetHashCode の重要性 ===");
var dict = new Dictionary<Person, string>();
dict[p1] = "First";
Console.WriteLine($"dict[p1]: {dict[p1]}");
Console.WriteLine($"dict[p2]: {dict[p2]}");  // ヒット: Equals と GetHashCode の両方が一貫しているため

Console.WriteLine();
Console.WriteLine("=== HashCode.Combine の確認 ===");
Console.WriteLine($"p1.GetHashCode(): {p1.GetHashCode()}");
Console.WriteLine($"p2.GetHashCode(): {p2.GetHashCode()}");
Console.WriteLine($"ハッシュ値が同一: {p1.GetHashCode() == p2.GetHashCode()}");

// class で値等価を手動実装
public class Person : IEquatable<Person>
{
    public string Name { get; }
    public int Age { get; }

    public Person(string name, int age) => (Name, Age) = (name, age);

    // 1. IEquatable<T> 実装
    public bool Equals(Person? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name && Age == other.Age;
    }

    // 2. Object.Equals をオーバーライド
    public override bool Equals(object? obj)
    {
        return Equals(obj as Person);
    }

    // 3. GetHashCode をオーバーライド（Equals と整合する必要あり）
    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Age);
    }

    // 4. 演算子オーバーライド
    public static bool operator ==(Person? left, Person? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    public static bool operator !=(Person? left, Person? right) => !(left == right);
}

// record は値等価を自動実装
public record PersonRecord(string Name, int Age);
