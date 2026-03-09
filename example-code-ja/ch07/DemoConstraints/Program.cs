// デモ: ジェネリック制約

Console.WriteLine("=== ジェネリック制約 ===\n");

// --------------------------------------------------------------
// 1. where T : IComparable<T> 制約
// --------------------------------------------------------------
Console.WriteLine("1. where T : IComparable<T> 制約");
Console.WriteLine(new string('-', 40));

var intList = new MyList<int>();
intList.Add(30);
intList.Add(10);
intList.Add(20);

Console.WriteLine($"Compare(0, 1): {intList.Compare(0, 1)}");
Console.WriteLine($"Compare(1, 2): {intList.Compare(1, 2)}");
Console.WriteLine($"Compare(0, 0): {intList.Compare(0, 0)}");

// --------------------------------------------------------------
// 2. where T : class / struct 制約
// --------------------------------------------------------------
Console.WriteLine("\n2. class / struct 制約");
Console.WriteLine(new string('-', 40));

var refContainer = new RefContainer<string>();
refContainer.Value = "Hello";
Console.WriteLine($"RefContainer<string>.Value = {refContainer.Value}");

var valContainer = new ValContainer<int>();
valContainer.Value = 42;
Console.WriteLine($"ValContainer<int>.Value = {valContainer.Value}");

// 次はコンパイルエラー
// var refContainer2 = new RefContainer<int>();
// var valContainer2 = new ValContainer<string>();

// --------------------------------------------------------------
// 3. where T : new() 制約
// --------------------------------------------------------------
Console.WriteLine("\n3. where T : new() 制約");
Console.WriteLine(new string('-', 40));

var factory = new Factory<Product>();
var product = factory.CreateInstance();
Console.WriteLine($"生成インスタンス: {product}");

// --------------------------------------------------------------
// 4. 複合制約
// --------------------------------------------------------------
Console.WriteLine("\n4. 複合制約");
Console.WriteLine(new string('-', 40));

var repo = new Repository<Customer>();
var customer = new Customer { Id = 1, Name = "John Smith" };
repo.Save(customer);

// --------------------------------------------------------------
// 5. 制約組み合わせ例
// --------------------------------------------------------------
Console.WriteLine("\n5. 制約組み合わせ例");
Console.WriteLine(new string('-', 40));

var point1 = new EquatablePoint { X = 1, Y = 2 };
var point2 = new EquatablePoint { X = 1, Y = 2 };

var valEquatable = new ValEquatable<EquatablePoint>();
Console.WriteLine($"point1.Equals(point2) = {valEquatable.AreEqual(point1, point2)}");

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// ヘルパークラス
// ============================================================

public class MyList<T> where T : IComparable<T>
{
    private readonly List<T> _items = new();

    public void Add(T item) => _items.Add(item);

    public int Compare(int index1, int index2)
    {
        return _items[index1].CompareTo(_items[index2]);
    }
}

public class RefContainer<T> where T : class
{
    public T? Value { get; set; }
}

public class ValContainer<T> where T : struct
{
    public T Value { get; set; }
}

public class Factory<T> where T : new()
{
    public T CreateInstance()
    {
        return new T();
    }
}

public class Product
{
    public string Name { get; set; } = "Default Product";
    public override string ToString() => $"Product: {Name}";
}

public interface IEntity
{
    int Id { get; }
}

public class Repository<T> where T : IEntity, new()
{
    public void Save(T entity)
    {
        Console.WriteLine($"エンティティ保存, ID = {entity.Id}");
    }

    public T Create() => new T();
}

public class Customer : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}

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
