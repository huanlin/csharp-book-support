// 示範不可變集合（Immutable Collections）
using System.Collections.Immutable;

Console.WriteLine("=== ImmutableList 基本用法 ===");
var list1 = ImmutableList.Create(1, 2, 3);
var list2 = list1.Add(4);

Console.WriteLine($"list1: {string.Join(", ", list1)}");  // 1, 2, 3 (不變)
Console.WriteLine($"list2: {string.Join(", ", list2)}");  // 1, 2, 3, 4 (新的)

Console.WriteLine();
Console.WriteLine("=== Builder 模式（效能優化）===");
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
Console.WriteLine("=== 效能特性說明 ===");
Console.WriteLine("ImmutableArray: O(1) 讀取, O(N) 修改");
Console.WriteLine("ImmutableList:  O(log N) 讀取, O(log N) 修改");
Console.WriteLine("選擇建議: 讀多寫少用 ImmutableArray, 頻繁修改用 ImmutableList");

Console.WriteLine();
Console.WriteLine("=== ImmutableDictionary 示範 ===");
var dict1 = ImmutableDictionary<string, int>.Empty
    .Add("Alice", 30)
    .Add("Bob", 25);
var dict2 = dict1.SetItem("Alice", 31);

Console.WriteLine($"dict1[Alice]: {dict1["Alice"]}");  // 30
Console.WriteLine($"dict2[Alice]: {dict2["Alice"]}");  // 31
