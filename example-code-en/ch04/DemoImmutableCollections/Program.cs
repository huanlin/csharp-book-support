// Demo: Immutable Collections
using System.Collections.Immutable;

Console.WriteLine("=== ImmutableList Basic Usage ===");
var list1 = ImmutableList.Create(1, 2, 3);
var list2 = list1.Add(4);

Console.WriteLine($"list1: {string.Join(", ", list1)}");  // 1, 2, 3 (unchanged)
Console.WriteLine($"list2: {string.Join(", ", list2)}");  // 1, 2, 3, 4 (new)

Console.WriteLine();
Console.WriteLine("=== Builder Pattern (Performance Optimization) ===");
var builder = ImmutableList.CreateBuilder<int>();
for (int i = 1; i <= 5; i++)
{
    builder.Add(i * 10);
}
var list3 = builder.ToImmutable();
Console.WriteLine($"list3: {string.Join(", ", list3)}");

Console.WriteLine();
Console.WriteLine("=== ImmutableArray vs ImmutableList ===");
var array1 = ImmutableArray.Create(1, 2, 3);
var array2 = array1.Add(4);

Console.WriteLine($"array1: {string.Join(", ", array1)}");
Console.WriteLine($"array2: {string.Join(", ", array2)}");

Console.WriteLine();
Console.WriteLine("=== Performance Characteristics ===");
Console.WriteLine("ImmutableArray: O(1) Read, O(N) Write");
Console.WriteLine("ImmutableList:  O(log N) Read, O(log N) Write");
Console.WriteLine("Selection Hint: Use ImmutableArray for frequent reads, ImmutableList for frequent updates.");

Console.WriteLine();
Console.WriteLine("=== ImmutableDictionary Demo ===");
var dict1 = ImmutableDictionary<string, int>.Empty
    .Add("Alice", 30)
    .Add("Bob", 25);
var dict2 = dict1.SetItem("Alice", 31);

Console.WriteLine($"dict1[Alice]: {dict1["Alice"]}");  // 30
Console.WriteLine($"dict2[Alice]: {dict2["Alice"]}");  // 31
