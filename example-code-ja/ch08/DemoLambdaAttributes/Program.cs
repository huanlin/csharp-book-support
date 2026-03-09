using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

Console.WriteLine("=== C# ラムダ新機能の例 ===\n");

// --------------------------------------------------------------
// 1. ラムダへの属性付与（C# 10）
// --------------------------------------------------------------
Console.WriteLine("1. ラムダへの属性付与（C# 10）");

var func = [Description("This is a test")] () => { Console.WriteLine("ラムダ実行中..."); };

var add = ([Description("第1オペランド")] int a, [Description("第2オペランド")] int b) => a + b;

var getMessage = [return: NotNull] () => "Hello";

func();
Console.WriteLine($"1 + 2 = {add(1, 2)}");
Console.WriteLine($"Message: {getMessage()}");

var methodInfo = func.Method;
var attr = methodInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
Console.WriteLine($"ラムダの属性: {attr?.Description}");

var paramsInfo = add.Method.GetParameters();
foreach (var p in paramsInfo)
{
    var pAttr = p.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
    Console.WriteLine($"引数 {p.Name} の属性: {pAttr?.Description}");
}

// --------------------------------------------------------------
// 2. ラムダ既定引数（C# 12）
// --------------------------------------------------------------
Console.WriteLine("\n2. ラムダ既定引数（C# 12）");

var print = (string message = "World") => Console.WriteLine($"Hello, {message}!");

print();
print("C# 12");

var format = (string text, bool uppercase = false, string prefix = ">> ") =>
{
    var result = prefix + text;
    return uppercase ? result.ToUpper() : result;
};

Console.WriteLine(format("hello"));
Console.WriteLine(format("hello", true));
Console.WriteLine(format("hello", false, "## "));

Console.WriteLine("\n=== 例の終了 ===\n");
