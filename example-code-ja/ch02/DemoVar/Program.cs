// デモ: var の型推論

// 基本: コンパイラが型を推論
int i = 10;
var j = 10;  // int と推論される
Console.WriteLine($"i の型: {i.GetType().Name}");
Console.WriteLine($"j の型: {j.GetType().Name}");

// 暗黙型配列
var numbers = new[] { 10, 20, 30, 40 };  // int[] と推論
Console.WriteLine($"\nnumbers の型: {numbers.GetType().Name}");

foreach (var x in numbers)  // x は int と推論
{
    Console.WriteLine($"  要素: {x}");
}

// 最適共通型の推論
var mixed = new[] { 1, 10000000000L };  // long[] と推論
Console.WriteLine($"\nmixed の型: {mixed.GetType().Name}");

// 複雑なジェネリック型での var
var dict = new Dictionary<string, int>();  // 型名の重複を避ける
dict["apple"] = 3;
dict["banana"] = 5;
Console.WriteLine($"\ndict の型: {dict.GetType().Name}");

// LINQ 結果での var
var grouped = dict
    .GroupBy(kvp => kvp.Value > 3)
    .Select(g => new { MoreThan3 = g.Key, Count = g.Count() });

Console.WriteLine("\nLINQ の結果:");
foreach (var item in grouped)
{
    Console.WriteLine($"  MoreThan3={item.MoreThan3}, Count={item.Count}");
}
