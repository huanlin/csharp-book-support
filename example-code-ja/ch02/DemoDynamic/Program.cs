// デモ: dynamic と ExpandoObject
using System.Dynamic;

// var と dynamic の比較
var s1 = "hello";      // コンパイル時型は string
dynamic s2 = "hello";  // コンパイル時型は dynamic、実行時型は string

Console.WriteLine($"s1 コンパイル時: string, 実行時: {s1.GetType().Name}");
Console.WriteLine($"s2 コンパイル時: dynamic, 実行時: {s2.GetType().Name}");

// dynamic 混在の例
dynamic x = 2;
var y = x * 10;        // y のコンパイル時型は dynamic
var z = (int)x;        // z のコンパイル時型は int
Console.WriteLine($"\ny コンパイル時: dynamic, 実行時: {y.GetType().Name}");
Console.WriteLine($"z コンパイル時: int, 実行時: {z.GetType().Name}");

// ExpandoObject で動的にプロパティ追加
dynamic obj = new ExpandoObject();
obj.FirstName = "Bruce";
obj.LastName = "Wayne";
obj.Age = 35;

Console.WriteLine($"\n動的オブジェクト: {obj.FirstName} {obj.LastName}, 年齢: {obj.Age}");

// メソッドで dynamic を扱う
string fullName = GetFullName(obj);
Console.WriteLine($"フルネーム: {fullName}");

// dynamic は辞書としても扱える
var dict = (IDictionary<string, object>)obj;
Console.WriteLine("\n動的オブジェクトのプロパティ:");
foreach (var kvp in dict)
{
    Console.WriteLine($"  {kvp.Key} = {kvp.Value}");
}

static string GetFullName(dynamic obj)
{
    return obj.FirstName + " " + obj.LastName;
}
