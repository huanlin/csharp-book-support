// Demo: Span<T> Zero-allocation Slicing

Console.WriteLine("=== Zero-allocation Slicing ===\n");

int[] array = { 1, 2, 3, 4, 5 };
Span<int> span = array.AsSpan();
Span<int> slice = span.Slice(1, 3); // Points to [2, 3, 4]

Console.WriteLine($"Original array[1]: {array[1]}"); // 2

// Modifying the Span directly affects the original array
slice[0] = 99;
Console.WriteLine($"After modifying slice[0] to 99, original array[1]: {array[1]}"); // 99
