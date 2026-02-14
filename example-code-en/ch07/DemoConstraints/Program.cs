// Demo: Generic Constraints

Console.WriteLine("=== Generic Constraints ===\n");

// --------------------------------------------------------------
// 1. where T : IComparable<T> Constraint
// --------------------------------------------------------------
Console.WriteLine("1. where T : IComparable<T> Constraint");
Console.WriteLine(new string('-', 40));

var intList = new MyList<int>();
intList.Add(30);
intList.Add(10);
intList.Add(20);

Console.WriteLine($"Compare(0, 1): {intList.Compare(0, 1)}");  // 30 vs 10 -> positive
Console.WriteLine($"Compare(1, 2): {intList.Compare(1, 2)}");  // 10 vs 20 -> negative
Console.WriteLine($"Compare(0, 0): {intList.Compare(0, 0)}");  // 30 vs 30 -> 0

// --------------------------------------------------------------
// 2. where T : class and where T : struct Constraints
// --------------------------------------------------------------
Console.WriteLine("\n2. class and struct Constraints");
Console.WriteLine(new string('-', 40));

var refContainer = new RefContainer<string>();
refContainer.Value = "Hello";
Console.WriteLine($"RefContainer<string>.Value = {refContainer.Value}");

var valContainer = new ValContainer<int>();
valContainer.Value = 42;
Console.WriteLine($"ValContainer<int>.Value = {valContainer.Value}");

// The following will cause compile errors:
// var refContainer2 = new RefContainer<int>();  // int is a struct, doesn't satisfy class constraint
// var valContainer2 = new ValContainer<string>();  // string is a class, doesn't satisfy struct constraint

// --------------------------------------------------------------
// 3. where T : new() Constraint
// --------------------------------------------------------------
Console.WriteLine("\n3. where T : new() Constraint");
Console.WriteLine(new string('-', 40));

var factory = new Factory<Product>();
var product = factory.CreateInstance();
Console.WriteLine($"Created Instance: {product}");

// --------------------------------------------------------------
// 4. Multiple Constraints
// --------------------------------------------------------------
Console.WriteLine("\n4. Multiple Constraints");
Console.WriteLine(new string('-', 40));

var repo = new Repository<Customer>();
var customer = new Customer { Id = 1, Name = "John Smith" };
repo.Save(customer);

// --------------------------------------------------------------
// 5. Combination of Constraints Example
// --------------------------------------------------------------
Console.WriteLine("\n5. Combination of Constraints Example");
Console.WriteLine(new string('-', 40));

// struct + IEquatable<T>
var point1 = new EquatablePoint { X = 1, Y = 2 };
var point2 = new EquatablePoint { X = 1, Y = 2 };

var valEquatable = new ValEquatable<EquatablePoint>();
Console.WriteLine($"point1.Equals(point2) = {valEquatable.AreEqual(point1, point2)}");

Console.WriteLine("\n=== Example End ===");

// ============================================================
// Helper Classes
// ============================================================

// where T : IComparable<T>
public class MyList<T> where T : IComparable<T>
{
    private readonly List<T> _items = new();

    public void Add(T item) => _items.Add(item);

    public int Compare(int index1, int index2)
    {
        return _items[index1].CompareTo(_items[index2]);
    }
}

// where T : class
public class RefContainer<T> where T : class
{
    public T? Value { get; set; }
}

// where T : struct
public class ValContainer<T> where T : struct
{
    public T Value { get; set; }
}

// where T : new()
public class Factory<T> where T : new()
{
    public T CreateInstance()
    {
        return new T();  // Requires new() constraint
    }
}

public class Product
{
    public string Name { get; set; } = "Default Product";
    public override string ToString() => $"Product: {Name}";
}

// Multiple constraints: IEntity + new()
public interface IEntity
{
    int Id { get; }
}

public class Repository<T> where T : IEntity, new()
{
    public void Save(T entity)
    {
        Console.WriteLine($"Saving entity, ID = {entity.Id}");
    }

    public T Create() => new T();
}

public class Customer : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}

// struct + IEquatable<T>
public class ValEquatable<T> where T : struct, IEquatable<T>
{
    public bool AreEqual(T a, T b) => a.Equals(b);
}

public struct EquatablePoint : IEquatable<EquatablePoint>
{
    public int X { get; set; }
    public int Y { get; set; }

    public bool Equals(EquatablePoint other) => X == other.X && Y == other.Y;
}
