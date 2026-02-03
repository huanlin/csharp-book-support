// 示範泛型約束（Constraints）

Console.WriteLine("=== 泛型約束 ===\n");

// --------------------------------------------------------------
// 1. where T : IComparable<T> 約束
// --------------------------------------------------------------
Console.WriteLine("1. where T : IComparable<T> 約束");
Console.WriteLine(new string('-', 40));

var intList = new MyList<int>();
intList.Add(30);
intList.Add(10);
intList.Add(20);

Console.WriteLine($"Compare(0, 1): {intList.Compare(0, 1)}");  // 30 vs 10 → 正數
Console.WriteLine($"Compare(1, 2): {intList.Compare(1, 2)}");  // 10 vs 20 → 負數
Console.WriteLine($"Compare(0, 0): {intList.Compare(0, 0)}");  // 30 vs 30 → 0

// --------------------------------------------------------------
// 2. where T : class 和 where T : struct 約束
// --------------------------------------------------------------
Console.WriteLine("\n2. class 和 struct 約束");
Console.WriteLine(new string('-', 40));

var refContainer = new RefContainer<string>();
refContainer.Value = "Hello";
Console.WriteLine($"RefContainer<string>.Value = {refContainer.Value}");

var valContainer = new ValContainer<int>();
valContainer.Value = 42;
Console.WriteLine($"ValContainer<int>.Value = {valContainer.Value}");

// 以下會編譯錯誤：
// var refContainer2 = new RefContainer<int>();  // int 是 struct，不符合 class 約束
// var valContainer2 = new ValContainer<string>();  // string 是 class，不符合 struct 約束

// --------------------------------------------------------------
// 3. where T : new() 約束
// --------------------------------------------------------------
Console.WriteLine("\n3. where T : new() 約束");
Console.WriteLine(new string('-', 40));

var factory = new Factory<Product>();
var product = factory.CreateInstance();
Console.WriteLine($"建立的產品：{product}");

// --------------------------------------------------------------
// 4. 多重約束
// --------------------------------------------------------------
Console.WriteLine("\n4. 多重約束");
Console.WriteLine(new string('-', 40));

var repo = new Repository<Customer>();
var customer = new Customer { Id = 1, Name = "王曉明" };
repo.Save(customer);

// --------------------------------------------------------------
// 5. 約束組合範例
// --------------------------------------------------------------
Console.WriteLine("\n5. 約束組合範例");
Console.WriteLine(new string('-', 40));

// struct + IEquatable<T>
var point1 = new EquatablePoint { X = 1, Y = 2 };
var point2 = new EquatablePoint { X = 1, Y = 2 };

var valEquatable = new ValEquatable<EquatablePoint>();
Console.WriteLine($"point1.Equals(point2) = {valEquatable.AreEqual(point1, point2)}");

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 輔助類別
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
        return new T();  // 需要 new() 約束
    }
}

public class Product
{
    public string Name { get; set; } = "預設產品";
    public override string ToString() => $"Product: {Name}";
}

// 多重約束：IEntity + new()
public interface IEntity
{
    int Id { get; }
}

public class Repository<T> where T : IEntity, new()
{
    public void Save(T entity)
    {
        Console.WriteLine($"儲存實體，ID = {entity.Id}");
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
