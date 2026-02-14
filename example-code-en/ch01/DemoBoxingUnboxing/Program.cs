// Demo: Boxing and Unboxing

Console.WriteLine("=== Boxing ===");

int i = 123;
object o = i;  // Boxing: Encapsulating an int into an object on the Heap

Console.WriteLine($"i = {i}");
Console.WriteLine($"o = {o}");
Console.WriteLine($"o.GetType() = {o.GetType()}");  // System.Int32
Console.WriteLine("During boxing, the CLR allocates memory on the heap and copies the value type's value into it");

Console.WriteLine();
Console.WriteLine("=== Unboxing ===");

int j = (int)o;  // Unboxing: Casting the object back to int

Console.WriteLine($"j = {j}");
Console.WriteLine("During unboxing, the CLR checks the type and copies the value from the heap back to the stack");

Console.WriteLine();
Console.WriteLine("=== Unboxing with Mismatched Types Throws an Exception ===");

try
{
    double d = (double)o;  // Error! o contains an int, not a double
}
catch (InvalidCastException ex)
{
    Console.WriteLine($"InvalidCastException: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("=== Common Implicit Boxing Pitfalls ===");

int x = 10;

// Pitfall 1: string.Format accepts object parameters
string s1 = string.Format("Score: {0}", x);  // x is boxed!
Console.WriteLine($"string.Format result: {s1}");

// Modern solution: String interpolation (usually optimized by the compiler)
string s2 = $"Score: {x}";
Console.WriteLine($"String interpolation result: {s2}");

Console.WriteLine();
Console.WriteLine("=== Using Generics to Avoid Boxing ===");

// Non-generic collection (will box)
var arrayList = new System.Collections.ArrayList();
arrayList.Add(1);  // int is boxed into an object
arrayList.Add(2);

// Generic collection (no boxing)
var list = new List<int>();
list.Add(1);  // Stores int directly, no boxing required
list.Add(2);

Console.WriteLine("Recommendation: Prefer generic collections (List<T>) to avoid the overhead of non-generic ones like ArrayList");
