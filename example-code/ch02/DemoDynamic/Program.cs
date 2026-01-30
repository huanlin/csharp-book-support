// 示範 dynamic 動態型別與 ExpandoObject
using System.Dynamic;

// var vs dynamic 對比
var s1 = "hello";      // 編譯時期型別為 string
dynamic s2 = "hello";  // 編譯時期型別為 dynamic，執行時期為 string

Console.WriteLine($"s1 編譯時期型別: string, 執行時期型別: {s1.GetType().Name}");
Console.WriteLine($"s2 編譯時期型別: dynamic, 執行時期型別: {s2.GetType().Name}");

// dynamic 混用範例
dynamic x = 2;
var y = x * 10;        // y 的編譯時期型別為 dynamic
var z = (int)x;        // z 的編譯時期型別為 int
Console.WriteLine($"\ny 的編譯時期型別: dynamic, 執行時期型別: {y.GetType().Name}");
Console.WriteLine($"z 的編譯時期型別: int, 執行時期型別: {z.GetType().Name}");

// ExpandoObject 動態添加屬性
dynamic obj = new ExpandoObject();
obj.FirstName = "Bruce";
obj.LastName = "Wayne";
obj.Age = 35;

Console.WriteLine($"\n動態物件: {obj.FirstName} {obj.LastName}, 年齡: {obj.Age}");

// 使用方法處理動態物件
string fullName = GetFullName(obj);
Console.WriteLine($"完整姓名: {fullName}");

// 動態物件也可以當作字典操作
var dict = (IDictionary<string, object>)obj;
Console.WriteLine($"\n動態物件的屬性:");
foreach (var kvp in dict)
{
    Console.WriteLine($"  {kvp.Key} = {kvp.Value}");
}

static string GetFullName(dynamic obj)
{
    return obj.FirstName + " " + obj.LastName;
}
