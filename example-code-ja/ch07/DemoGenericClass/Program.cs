// デモ: カスタムジェネリッククラス

Console.WriteLine("=== カスタムジェネリッククラス ===\n");

// --------------------------------------------------------------
// 1. カスタムジェネリックリスト利用
// --------------------------------------------------------------
Console.WriteLine("1. カスタムジェネリックリスト利用");
Console.WriteLine(new string('-', 40));

var intList = new MyGenericList<int>(5);
intList.Add(100);
intList.Add(200);
intList.Add(300);

Console.WriteLine($"intList[0] = {intList[0]}");
Console.WriteLine($"intList[1] = {intList[1]}");
Console.WriteLine($"Count = {intList.Count}");

var empList = new MyGenericList<Employee>(5);
empList.Add(new Employee("A001", "John Smith"));
empList.Add(new Employee("A002", "Jane Doe"));

Console.WriteLine($"\nEmployee: {empList[0]}");
Console.WriteLine($"Employee: {empList[1]}");

// --------------------------------------------------------------
// 2. 既定値（default）
// --------------------------------------------------------------
Console.WriteLine("\n2. 既定値（default）");
Console.WriteLine(new string('-', 40));

Console.WriteLine($"GetValueOrDefault(intList, 0) = {GetValueOrDefault(intList, 0)}");
Console.WriteLine($"GetValueOrDefault(intList, 99) = {GetValueOrDefault(intList, 99)}");

var strList = new MyGenericList<string>(3);
strList.Add("Hello");

Console.WriteLine($"GetValueOrDefault(strList, 0) = {GetValueOrDefault(strList, 0)}");
Console.WriteLine($"GetValueOrDefault(strList, 99) = {GetValueOrDefault(strList, 99) ?? "(null)"}");

// --------------------------------------------------------------
// 3. ジェネリックメソッド
// --------------------------------------------------------------
Console.WriteLine("\n3. ジェネリックメソッド");
Console.WriteLine(new string('-', 40));

var util = new Utility();
util.Print(100);
util.Print("Hello");
util.Print(3.14);

// --------------------------------------------------------------
// 4. 配列クリア（Zap）
// --------------------------------------------------------------
Console.WriteLine("\n4. 配列クリア（Zap）");
Console.WriteLine(new string('-', 40));

int[] numbers = { 1, 2, 3, 4, 5 };
Console.WriteLine($"Zap 前: {string.Join(", ", numbers)}");
Zap(numbers);
Console.WriteLine($"Zap 後: {string.Join(", ", numbers)}");

string?[] names = { "Alice", "Bob", "Charlie" };
Console.WriteLine($"\nZap 前: {string.Join(", ", names)}");
Zap(names);
Console.WriteLine($"Zap 後: {string.Join(", ", names.Select(n => n ?? "(null)"))}");

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// ヘルパーメソッド
// ============================================================

static T? GetValueOrDefault<T>(MyGenericList<T> list, int index)
{
    if (index < 0 || index >= list.Count)
        return default;

    return list[index];
}

static void Zap<T>(T[] array)
{
    for (int i = 0; i < array.Length; i++)
        array[i] = default!;
}

// ============================================================
// データクラス
// ============================================================

public class MyGenericList<T>
{
    private T[] _elements;
    private int _count;

    public int Count => _count;

    public MyGenericList(int capacity)
    {
        _elements = new T[capacity];
    }

    public void Add(T item)
    {
        if (_count >= _elements.Length)
        {
            var newArray = new T[_elements.Length * 2];
            Array.Copy(_elements, newArray, _elements.Length);
            _elements = newArray;
        }
        _elements[_count++] = item;
    }

    public T this[int index]
    {
        get => _elements[index];
        set => _elements[index] = value;
    }
}

public record Employee(string Id, string Name);

public class Utility
{
    public void Print<T>(T obj)
    {
        Console.WriteLine($"型: {typeof(T).Name}, 値: {obj}");
    }
}
