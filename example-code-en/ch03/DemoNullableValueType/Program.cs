// Demo: Nullable Value Types

// Basic syntax: T? is equivalent to Nullable<T>
int? nullableInt = null;
int? anotherNullable = 25;

Console.WriteLine("=== 3.2 Nullable Value Types ===\n");

// Assignment and Retrieval
Console.WriteLine("[Assignment and Retrieval]");
Console.WriteLine($"nullableInt = {nullableInt?.ToString() ?? "null"}");
Console.WriteLine($"anotherNullable = {anotherNullable}");

// Checking if a value exists
Console.WriteLine("\n[Checking if a value exists]");
int? score = null;

// Method 1: Comparison operator
if (score != null) 
{ 
    Console.WriteLine($"score (using != null): {score}"); 
}
else 
{
    Console.WriteLine("score is null (checked using != null)");
}

// Method 2: HasValue property (more explicit)
if (score.HasValue) 
{ 
    Console.WriteLine($"score (using HasValue): {score.Value}"); 
}
else 
{
    Console.WriteLine("score has no value (checked using HasValue)");
}

// Interoperability with non-nullable types
Console.WriteLine("\n[Interoperability with non-nullable types]");
int i = 10;
int? j = i;  // ✓ Implicit conversion
Console.WriteLine($"int i = {i} → int? j = {j}");

int? x = 10;
// int y = x;        // ✗ Compile-time error!
int z = (int)x;      // ✓ Explicit conversion, but throws an exception if x is null at runtime
Console.WriteLine($"int? x = {x} → int z = {z} (explicit conversion)");

// Null Propagation Operations
Console.WriteLine("\n[Null Propagation Operations]");
int? a = null;
int? b = 10;
int? c = a + b;  // c is null
int? d = a * b;  // d is also null
Console.WriteLine($"a = null, b = 10");
Console.WriteLine($"a + b = {c?.ToString() ?? "null"} (null propagation)");
Console.WriteLine($"a * b = {d?.ToString() ?? "null"} (null propagation)");

// Mixed Operations
Console.WriteLine("\n[Mixed Operations]");
int? m = 10;
int n = 5;
int? result = m * n;  // result = 50 (type is int?)
Console.WriteLine($"int? m = {m}, int n = {n}");
Console.WriteLine($"m * n = {result} (result type is int?)");

// GetValueOrDefault Method
Console.WriteLine("\n[GetValueOrDefault Method]");
int? maybeAge = null;
int age = maybeAge.GetValueOrDefault(18);  // If null, use default value 18
Console.WriteLine($"maybeAge = null → GetValueOrDefault(18) = {age}");

maybeAge = 30;
age = maybeAge.GetValueOrDefault(18);
Console.WriteLine($"maybeAge = 30 → GetValueOrDefault(18) = {age}");
