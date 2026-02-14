// Demo: Covariance and Contravariance

Console.WriteLine("=== Covariance and Contravariance ===\n");

// --------------------------------------------------------------
// 1. Array Covariance (Unsafe)
// --------------------------------------------------------------
Console.WriteLine("1. Array Covariance (Unsafe)");
Console.WriteLine(new string('-', 40));

string[] strings = new string[3] { "A", "B", "C" };
object[] objects = strings;  // Array covariance: Legal

Console.WriteLine($"objects[0] = {objects[0]}");

// Danger: Compiles OK, but throws ArrayTypeMismatchException at runtime
try
{
    objects[0] = DateTime.Now;  // Attempting to put a DateTime into a string[]
}
catch (ArrayTypeMismatchException ex)
{
    Console.WriteLine($"Error: {ex.GetType().Name}");
    Console.WriteLine("Array covariance is unsafe; type errors are only discovered at runtime.");
}

// --------------------------------------------------------------
// 2. Generic Invariance
// --------------------------------------------------------------
Console.WriteLine("\n2. Generic Invariance");
Console.WriteLine(new string('-', 40));

List<string> stringList = new List<string> { "Hello", "World" };
// List<object> objectList = stringList;  // Compile error!

Console.WriteLine("List<string> cannot be assigned to List<object>");
Console.WriteLine("This is because List<T> is invariant.");

// --------------------------------------------------------------
// 3. Covariance of IEnumerable<out T>
// --------------------------------------------------------------
Console.WriteLine("\n3. Covariance of IEnumerable<out T>");
Console.WriteLine(new string('-', 40));

List<string> strList = new List<string> { "A", "B", "C" };
IEnumerable<object> objEnumerable = strList;  // OK! Covariance

Console.WriteLine("List<string> can be assigned to IEnumerable<object>");
foreach (var obj in objEnumerable)
{
    Console.WriteLine($"  {obj}");
}

// --------------------------------------------------------------
// 4. Custom Covariant Interface (out T)
// --------------------------------------------------------------
Console.WriteLine("\n4. Custom Covariant Interface (out T)");
Console.WriteLine(new string('-', 40));

IProducer<string> stringProducer = new StringProducer();
IProducer<object> objectProducer = stringProducer;  // Covariance: Legal

Console.WriteLine($"objectProducer.GetValue() = {objectProducer.GetValue()}");

// --------------------------------------------------------------
// 5. Contravariance of IComparer<in T>
// --------------------------------------------------------------
Console.WriteLine("\n5. Contravariance of IComparer<in T>");
Console.WriteLine(new string('-', 40));

IComparer<object> objectComparer = new ObjectComparer();
IComparer<string> stringComparer = objectComparer;  // Contravariance: Legal

var list = new List<string> { "Cherry", "Apple", "Banana" };
list.Sort(stringComparer);

Console.WriteLine("Using IComparer<object> to sort strings:");
Console.WriteLine($"  {string.Join(", ", list)}");

// --------------------------------------------------------------
// 6. Delegate Covariance and Contravariance
// --------------------------------------------------------------
Console.WriteLine("\n6. Delegate Covariance and Contravariance");
Console.WriteLine(new string('-', 40));

// Func<out TResult> is covariant
Func<string> stringFactory = () => "Hello";
Func<object> objectFactory = stringFactory;  // OK
Console.WriteLine($"objectFactory() = {objectFactory()}");

// Action<in T> is contravariant
Action<object> objectAction = obj => Console.WriteLine($"  Processing: {obj}");
Action<string> stringAction = objectAction;  // OK
stringAction("Hello, World!");

// --------------------------------------------------------------
// 7. Mnemonic
// --------------------------------------------------------------
Console.WriteLine("\n7. Mnemonic");
Console.WriteLine(new string('-', 40));

Console.WriteLine("out = Output = Covariance");
Console.WriteLine("  Subclass -> Base Class (follows the direction of inheritance)");
Console.WriteLine("  Example: IEnumerable<Dog> -> IEnumerable<Animal>");

Console.WriteLine("\nin = Input = Contravariance");
Console.WriteLine("  Base Class -> Subclass (opposite of inheritance direction)");
Console.WriteLine("  Example: IComparer<Animal> -> IComparer<Dog>");

Console.WriteLine("\n=== Example End ===");

// ============================================================
// Helper Interfaces and Classes
// ============================================================

// Covariant Interface (out T)
public interface IProducer<out T>
{
    T GetValue();
    // void SetValue(T value);  // Error! T cannot be in an input position
}

public class StringProducer : IProducer<string>
{
    public string GetValue() => "This is a string";
}

// Contravariant Interface (in T)
public interface IConsumer<in T>
{
    void Process(T value);
    // T GetValue();  // Error! T cannot be in an output position
}

public class ObjectProcessor : IConsumer<object>
{
    public void Process(object value)
    {
        Console.WriteLine($"Processing object: {value}");
    }
}

// Comparer for sorting
public class ObjectComparer : IComparer<object>
{
    public int Compare(object? x, object? y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;
        return string.Compare(x.ToString(), y.ToString());
    }
}
