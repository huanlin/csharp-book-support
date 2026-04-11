// 示範 Span<T> 零複製切片 (Zero-allocation Slicing)

Console.WriteLine("=== 零複製切片 (Zero-allocation Slicing) ===\n");

int[] array = { 1, 2, 3, 4, 5 };
Span<int> span = array.AsSpan();
Span<int> slice = span.Slice(1, 3); // 指向 [2, 3, 4]

Console.WriteLine($"原始陣列[1]: {array[1]}"); // 2

// 修改 Span 會直接影響原陣列
slice[0] = 99;
Console.WriteLine($"修改 slice[0] 為 99 後，原始陣列[1]: {array[1]}"); // 99
