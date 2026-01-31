// 示範自訂泛型類別

Console.WriteLine("=== 自訂泛型類別 ===\n");

// --------------------------------------------------------------
// 1. 使用自訂泛型串列
// --------------------------------------------------------------
Console.WriteLine("1. 使用自訂泛型串列");
Console.WriteLine(new string('-', 40));

var intList = new MyGenericList<int>(5);
intList.Add(100);
intList.Add(200);
intList.Add(300);

Console.WriteLine($"intList[0] = {intList[0]}");
Console.WriteLine($"intList[1] = {intList[1]}");
Console.WriteLine($"Count = {intList.Count}");

var empList = new MyGenericList<Employee>(5);
empList.Add(new Employee("A001", "王曉明"));
empList.Add(new Employee("A002", "李大同"));

Console.WriteLine($"\nEmployee: {empList[0]}");
Console.WriteLine($"Employee: {empList[1]}");

// --------------------------------------------------------------
// 2. 預設值的使用
// --------------------------------------------------------------
Console.WriteLine("\n2. 預設值的使用（default）");
Console.WriteLine(new string('-', 40));

Console.WriteLine($"GetValueOrDefault(intList, 0) = {GetValueOrDefault(intList, 0)}");
Console.WriteLine($"GetValueOrDefault(intList, 99) = {GetValueOrDefault(intList, 99)}");

var strList = new MyGenericList<string>(3);
strList.Add("Hello");

Console.WriteLine($"GetValueOrDefault(strList, 0) = {GetValueOrDefault(strList, 0)}");
Console.WriteLine($"GetValueOrDefault(strList, 99) = {GetValueOrDefault(strList, 99) ?? "(null)"}");

// --------------------------------------------------------------
// 3. 泛型方法
// --------------------------------------------------------------
Console.WriteLine("\n3. 泛型方法");
Console.WriteLine(new string('-', 40));

var util = new Utility();
util.Print(100);           // 型別推斷為 Print<int>
util.Print("Hello");       // 型別推斷為 Print<string>
util.Print(3.14);          // 型別推斷為 Print<double>

// --------------------------------------------------------------
// 4. 清空陣列（Zap）
// --------------------------------------------------------------
Console.WriteLine("\n4. 清空陣列（Zap）");
Console.WriteLine(new string('-', 40));

int[] numbers = { 1, 2, 3, 4, 5 };
Console.WriteLine($"清空前：{string.Join(", ", numbers)}");
Zap(numbers);  // 全部變成 0
Console.WriteLine($"清空後：{string.Join(", ", numbers)}");

string?[] names = { "Alice", "Bob", "Charlie" };
Console.WriteLine($"\n清空前：{string.Join(", ", names)}");
Zap(names);  // 全部變成 null
Console.WriteLine($"清空後：{string.Join(", ", names.Select(n => n ?? "(null)"))}");

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 輔助方法
// ============================================================

static T? GetValueOrDefault<T>(MyGenericList<T> list, int index)
{
    if (index < 0 || index >= list.Count)
        return default;  // C# 7.1+ 可省略 default(T)

    return list[index];
}

static void Zap<T>(T[] array)
{
    for (int i = 0; i < array.Length; i++)
        array[i] = default!;  // 將每個元素設為預設值
}

// ============================================================
// 輔助類別
// ============================================================

public class MyGenericList<T>
{
    private T[] _elements;
    private int _count;

    public int Count => _count;

    public MyGenericList(int capacity)
    {
        _elements = new T[capacity];  // 在建構式中使用 T
    }

    public void Add(T item)
    {
        if (_count >= _elements.Length)
        {
            // 擴充容量
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
        Console.WriteLine($"Type: {typeof(T).Name}, Value: {obj}");
    }
}
