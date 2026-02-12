using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

Console.WriteLine("=== C# Lambda 新特性範例 ===\n");

// --------------------------------------------------------------
// 1. 為 Lambda 加上 Attribute (C# 10)
// --------------------------------------------------------------
Console.WriteLine("1. 為 Lambda 加上 Attribute (C# 10)");

// 加在 Lambda 運算式本身（針對方法）
var func = [Description("這是一個測試")] () => { Console.WriteLine("Lambda 執行中..."); };

// 加在參數上
var add = ([Description("被加數")] int a, [Description("加數")] int b) => a + b;

// 加在回傳值上
var getMessage = [return: NotNull] () => "Hello";

// 執行看看
func();
Console.WriteLine($"1 + 2 = {add(1, 2)}");
Console.WriteLine($"Message: {getMessage()}");

// 透過 Reflection 查看 Attribute
var methodInfo = func.Method;
var attr = methodInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
Console.WriteLine($"Lambda 的 Attribute: {attr?.Description}");

var paramsInfo = add.Method.GetParameters();
foreach (var p in paramsInfo)
{
    var pAttr = p.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
    Console.WriteLine($"參數 {p.Name} 的 Attribute: {pAttr?.Description}");
}

// --------------------------------------------------------------
// 2. 預設 Lambda 參數 (C# 12)
// --------------------------------------------------------------
Console.WriteLine("\n2. 預設 Lambda 參數 (C# 12)");

var print = (string message = "World") => Console.WriteLine($"Hello, {message}!");

print();          // Hello, World!
print("C# 12");   // Hello, C# 12!

// 結合具名引數
var format = (string text, bool uppercase = false, string prefix = ">> ") =>
{
    var result = prefix + text;
    return uppercase ? result.ToUpper() : result;
};

Console.WriteLine(format("hello"));
Console.WriteLine(format("hello", true));
Console.WriteLine(format("hello", false, "## ")); 

Console.WriteLine("\n=== 範例結束 ===");
