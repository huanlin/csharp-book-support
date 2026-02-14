// Demo: Why Generics

using System.Collections;

Console.WriteLine("=== Why Generics ===\n");

// --------------------------------------------------------------
// 1. Problems with ArrayList: Manual Casting
// --------------------------------------------------------------
Console.WriteLine("1. Problems with ArrayList: Manual Casting");
Console.WriteLine(new string('-', 40));

ArrayList intList = new ArrayList();
intList.Add(100);
intList.Add(200);
intList.Add(300);

int i1 = (int)intList[0]!;  // Manual casting is required
int i2 = (int)intList[1]!;

Console.WriteLine($"Extracted elements: {i1}, {i2}");

// Problem: Incorrect casting is not caught at compile time
// DateTime dt = (DateTime)intList[1];  // Compiles OK, but throws InvalidCastException at runtime!

Console.WriteLine("Problem: Manual casting is required for every access, and the compiler doesn't warn about incorrect casting.");

// --------------------------------------------------------------
// 2. Brute Force: Creating Custom Collection Classes
// --------------------------------------------------------------
Console.WriteLine("\n2. Brute Force: Creating Custom Collection Classes");
Console.WriteLine(new string('-', 40));

var myIntList = new IntList();
myIntList.Add(100);
myIntList.Add(200);

int num = myIntList[1];  // No manual casting needed
Console.WriteLine($"IntList extracted element: {num}");

var myStrList = new StringList();
myStrList.Add("John Doe");
myStrList.Add("Jane Smith");

string name = myStrList[1];  // No manual casting needed
Console.WriteLine($"StringList extracted element: {name}");

Console.WriteLine("Problem: A new class is needed for every type, leading to high maintenance costs.");

// --------------------------------------------------------------
// 3. The Beauty of Generics: List<T>
// --------------------------------------------------------------
Console.WriteLine("\n3. The Beauty of Generics: List<T>");
Console.WriteLine(new string('-', 40));

List<int> genericIntList = new List<int>();
genericIntList.Add(100);
genericIntList.Add(200);

int value = genericIntList[1];  // No casting needed, type-safe
Console.WriteLine($"List<int> extracted element: {value}");

List<string> genericStrList = new List<string>();
genericStrList.Add("John Doe");
genericStrList.Add("Jane Smith");

string str = genericStrList[1];  // No casting needed, type-safe
Console.WriteLine($"List<string> extracted element: {str}");

// genericIntList.Add("Error");  // Compile error! Type-safe

Console.WriteLine("Advantages: No manual casting, compile-time type safety, and no redundant classes.");

// --------------------------------------------------------------
// 4. Performance Comparison: Boxing/Unboxing
// --------------------------------------------------------------
Console.WriteLine("\n4. Performance Comparison: Boxing/Unboxing");
Console.WriteLine(new string('-', 40));

// ArrayList causes boxing/unboxing
ArrayList arrayList = new ArrayList();
arrayList.Add(42);        // boxing: int -> object
int val1 = (int)arrayList[0]!;  // unboxing: object -> int

// List<int> does not box
List<int> genericList = new List<int>();
genericList.Add(42);        // No boxing
int val2 = genericList[0];  // No unboxing

Console.WriteLine("ArrayList: Boxes every int added, unboxes on retrieval.");
Console.WriteLine("List<int>: No boxing/unboxing, resulting in better performance.");

Console.WriteLine("\n=== Example End ===");

// ============================================================
// Helper Classes (Result of the brute force approach)
// ============================================================

public class IntList
{
    private ArrayList _numbers = new ArrayList();

    public void Add(int value) => _numbers.Add(value);

    public int this[int index]
    {
        get => (int)_numbers[index]!;
        set => _numbers[index] = value;
    }
}

public class StringList
{
    private ArrayList _strings = new ArrayList();

    public void Add(string value) => _strings.Add(value);

    public string this[int index]
    {
        get => (string)_strings[index]!;
        set => _strings[index] = value;
    }
}
