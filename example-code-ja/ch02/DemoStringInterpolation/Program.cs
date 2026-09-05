// デモ: 文字列補間の基本

var name = "Michael";
var age = 50;

// 従来の書き方
var s1 = string.Format("名前: {0}, 年齢: {1}", name, age);
// 文字列補間
var s2 = $"名前: {name}, 年齢: {age}";

Console.WriteLine(s1);
Console.WriteLine(s2);

// 書式指定と桁揃え
var price = 123.456m;
var dt = DateTime.Now;

Console.WriteLine("\n書式指定の例:");
Console.WriteLine($"  価格: {price:C2}");           // 通貨形式
Console.WriteLine($"  日付: {dt:yyyy-MM-dd}");       // 日付形式
Console.WriteLine($"  16進数: {255:X4}");            // 16進数

// 桁揃え
var playerName = "Jordan";
var score = 95;
Console.WriteLine("\n桁揃えの例:");
Console.WriteLine($"|{playerName,-10}|{score,5}|");  // 左寄せ / 右寄せ

// 定数文字列補間（C# 10+）
const string Greeting = "こんにちは";
const string Target = "世界";
const string Message = $"{Greeting}, {Target}!";
Console.WriteLine($"\n定数補間: {Message}");
