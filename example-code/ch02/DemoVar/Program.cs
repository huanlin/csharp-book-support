// 示範 var 隱含型別變數

// 基本用法：編譯器推斷型別
int i = 10;
var j = 10;  // 編譯器推斷為 int
Console.WriteLine($"i 的型別: {i.GetType().Name}");
Console.WriteLine($"j 的型別: {j.GetType().Name}");

// 隱含型別陣列
var numbers = new[] { 10, 20, 30, 40 };  // 推斷為 int[]
Console.WriteLine($"\nnumbers 的型別: {numbers.GetType().Name}");

foreach (var x in numbers)  // x 推斷為 int
{
    Console.WriteLine($"  元素: {x}");
}

// 型別推斷：尋找最佳共同型別
var mixed = new[] { 1, 10000000000L };  // 推斷為 long[]
Console.WriteLine($"\nmixed 的型別: {mixed.GetType().Name}");

// 複雜泛型使用 var
var dict = new Dictionary<string, int>();  // 避免重複寫型別
dict["apple"] = 3;
dict["banana"] = 5;
Console.WriteLine($"\ndict 的型別: {dict.GetType().Name}");

// LINQ 查詢結果使用 var
var grouped = dict
    .GroupBy(kvp => kvp.Value > 3)
    .Select(g => new { MoreThan3 = g.Key, Count = g.Count() });

Console.WriteLine("\nLINQ 結果:");
foreach (var item in grouped)
{
    Console.WriteLine($"  MoreThan3={item.MoreThan3}, Count={item.Count}");
}
