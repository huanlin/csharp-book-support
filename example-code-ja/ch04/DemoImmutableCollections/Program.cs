// デモ: Immutable Collections
using System.Collections.Immutable;

Console.WriteLine("=== ImmutableList の基本 ===");
var list1 = ImmutableList.Create(1, 2, 3);
var list2 = list1.Add(4);

Console.WriteLine($"list1: {string.Join(", ", list1)}");  // 1, 2, 3（不変）
Console.WriteLine($"list2: {string.Join(", ", list2)}");  // 1, 2, 3, 4（新規）

Console.WriteLine();
Console.WriteLine("=== Builder パターン（性能最適化） ===");
var builder = ImmutableList.CreateBuilder<int>();
for (int i = 1; i <= 5; i++)
{
    builder.Add(i * 10);
}
var list3 = builder.ToImmutable();
Console.WriteLine($"list3: {string.Join(", ", list3)}");

Console.WriteLine();
Console.WriteLine("=== ImmutableArray と ImmutableList ===");
var array1 = ImmutableArray.Create(1, 2, 3);
var array2 = array1.Add(4);

Console.WriteLine($"array1: {string.Join(", ", array1)}");
Console.WriteLine($"array2: {string.Join(", ", array2)}");

Console.WriteLine();
Console.WriteLine("=== 性能特性 ===");
Console.WriteLine("ImmutableArray: 読み取り O(1), 書き込み O(N)");
Console.WriteLine("ImmutableList:  読み取り O(log N), 書き込み O(log N)");
Console.WriteLine("目安: 読み取り中心なら ImmutableArray、更新が多いなら ImmutableList");

Console.WriteLine();
Console.WriteLine("=== ImmutableDictionary の例 ===");
var dict1 = ImmutableDictionary<string, int>.Empty
    .Add("Alice", 30)
    .Add("Bob", 25);
var dict2 = dict1.SetItem("Alice", 31);

Console.WriteLine($"dict1[Alice]: {dict1["Alice"]}");  // 30
Console.WriteLine($"dict2[Alice]: {dict2["Alice"]}");  // 31
