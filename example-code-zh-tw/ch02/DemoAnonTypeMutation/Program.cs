// 示範匿名型別非破壞性修改 (C# 10+)

var a1 = new { A = 1, B = 2, C = 3 };
var a2 = a1 with { B = 99 };  // 複製 a1，但將 B 改為 99

Console.WriteLine($"原始: A={a1.A}, B={a1.B}, C={a1.C}");
Console.WriteLine($"修改後: A={a2.A}, B={a2.B}, C={a2.C}");
