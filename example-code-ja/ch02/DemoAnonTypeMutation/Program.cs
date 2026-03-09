// デモ: 匿名型の非破壊的変更（C# 10+）

var a1 = new { A = 1, B = 2, C = 3 };
var a2 = a1 with { B = 99 };  // a1 をコピーしつつ B だけ 99 に変更

Console.WriteLine($"元オブジェクト: A={a1.A}, B={a1.B}, C={a1.C}");
Console.WriteLine($"変更後: A={a2.A}, B={a2.B}, C={a2.C}");
