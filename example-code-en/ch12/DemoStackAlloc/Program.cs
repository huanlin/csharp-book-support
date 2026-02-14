// Demo: stackalloc and Collection Expressions
using System.Runtime.InteropServices;

Console.WriteLine("=== stackalloc and Collection Expressions ===\n");

// --------------------------------------------------------------
// 1. Basic usage of stackalloc
// --------------------------------------------------------------
Console.WriteLine("1. stackalloc array allocation");
Console.WriteLine(new string('-', 40));

// Traditional syntax (C# 7.2+)
Span<int> numbers = stackalloc int[] { 1, 2, 3, 4, 5 };
Console.WriteLine($"Numbers[0]: {numbers[0]}");

// Modify
numbers[0] = 99;
Console.WriteLine($"Modified Numbers[0]: {numbers[0]}");

// --------------------------------------------------------------
// 2. Collection Expressions (C# 12)
// --------------------------------------------------------------
Console.WriteLine("\n2. Collection Expressions (C# 12)");
Console.WriteLine(new string('-', 40));

// The compiler automatically optimizes:
// If the target is Span<T>, it tends to use stackalloc (if length is known and small enough)
// Otherwise, it creates a regular array
Span<int> data = [10, 20, 30, 40, 50];

Console.WriteLine($"Data[0]: {data[0]}");

// --------------------------------------------------------------
// 3. Loop Initialization
// --------------------------------------------------------------
Console.WriteLine("\n3. Dynamic Initialization (Loop)");
Console.WriteLine(new string('-', 40));

// Only suitable for small temporary storage
int length = 5;
Span<int> buffer = stackalloc int[length];

for (int i = 0; i < length; i++)
{
    buffer[i] = i * i;
}

Console.WriteLine($"Buffer: [{string.Join(", ", buffer.ToArray())}]");

// --------------------------------------------------------------
// 4. Native Memory Interop (Advanced)
// --------------------------------------------------------------
Console.WriteLine("\n4. Native Memory (Unsafe)");
Console.WriteLine(new string('-', 40));

// Note: To use unsafe blocks (like pointer operations),
// <AllowUnsafeBlocks>true</AllowUnsafeBlocks> must be set in the .csproj
unsafe
{
    // If you need pointers, you can do this, but Span is usually sufficient
    int* ptr = stackalloc int[3];
    ptr[0] = 100;
    ptr[1] = 200;
    ptr[2] = 300;
    
    Console.WriteLine($"Pointer[1]: {ptr[1]}");
}

Console.WriteLine("\n=== Example End ===");
