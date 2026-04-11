// デモ: Span<T> によるゼロアロケーション Slicing

Console.WriteLine("=== ゼロアロケーション Slicing ===\n");

int[] array = { 1, 2, 3, 4, 5 };
Span<int> span = array.AsSpan();
Span<int> slice = span.Slice(1, 3); // [2, 3, 4] を指す

Console.WriteLine($"元の array[1]: {array[1]}"); // 2

// Span を変更すると元配列にも反映される
slice[0] = 99;
Console.WriteLine($"slice[0] を 99 に変更後、元の array[1]: {array[1]}"); // 99
