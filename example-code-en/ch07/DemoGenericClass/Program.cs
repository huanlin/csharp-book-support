// Demo: Custom Generic Classes

Console.WriteLine("=== Custom Generic Classes ===\n");

// --------------------------------------------------------------
// 1. Using Custom Generic List
// --------------------------------------------------------------
Console.WriteLine("1. Using Custom Generic List");
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
// 2. Using Default Values
// --------------------------------------------------------------
Console.WriteLine("\n2. Using Default Values (default)");
Console.WriteLine(new string('-', 40));

Console.WriteLine($"GetValueOrDefault(intList, 0) = {GetValueOrDefault(intList, 0)}");
Console.WriteLine($"GetValueOrDefault(intList, 99) = {GetValueOrDefault(intList, 99)}");

var strList = new MyGenericList<string>(3);
strList.Add("Hello");

Console.WriteLine($"GetValueOrDefault(strList, 0) = {GetValueOrDefault(strList, 0)}");
Console.WriteLine($"GetValueOrDefault(strList, 99) = {GetValueOrDefault(strList, 99) ?? "(null)"}");

// --------------------------------------------------------------
// 3. Generic Methods
// --------------------------------------------------------------
Console.WriteLine("\n3. Generic Methods");
Console.WriteLine(new string('-', 40));

var util = new Utility();
util.Print(100);           // Inferred as Print<int>
util.Print("Hello");       // Inferred as Print<string>
util.Print(3.14);          // Inferred as Print<double>

// --------------------------------------------------------------
// 4. Clearing an Array (Zap)
// --------------------------------------------------------------
Console.WriteLine("\n4. Clearing an Array (Zap)");
Console.WriteLine(new string('-', 40));

int[] numbers = { 1, 2, 3, 4, 5 };
Console.WriteLine($"Before Zap: {string.Join(", ", numbers)}");
Zap(numbers);  // All elements become 0
Console.WriteLine($"After Zap: {string.Join(", ", numbers)}");

string?[] names = { "Alice", "Bob", "Charlie" };
Console.WriteLine($"\nBefore Zap: {string.Join(", ", names)}");
Zap(names);  // All elements become null
Console.WriteLine($"After Zap: {string.Join(", ", names.Select(n => n ?? "(null)"))}");

Console.WriteLine("\n=== Example End ===");

// ============================================================
// Helper Methods
// ============================================================

static T? GetValueOrDefault<T>(MyGenericList<T> list, int index)
{
    if (index < 0 || index >= list.Count)
        return default;  // default(T) can be omitted since C# 7.1

    return list[index];
}

static void Zap<T>(T[] array)
{
    for (int i = 0; i < array.Length; i++)
        array[i] = default!;  // Set each element to its default value
}

// ============================================================
// Data Classes
// ============================================================

public class MyGenericList<T>
{
    private T[] _elements;
    private int _count;

    public int Count => _count;

    public MyGenericList(int capacity)
    {
        _elements = new T[capacity];  // Use T in the constructor
    }

    public void Add(T item)
    {
        if (_count >= _elements.Length)
        {
            // Expand capacity
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
