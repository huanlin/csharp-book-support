using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

Console.WriteLine("=== C# Lambda New Features Example ===\n");

// --------------------------------------------------------------
// 1. Adding Attributes to Lambda (C# 10)
// --------------------------------------------------------------
Console.WriteLine("1. Adding Attributes to Lambda (C# 10)");

// Adding to the lambda expression itself (targeting the method)
var func = [Description("This is a test")] () => { Console.WriteLine("Lambda executing..."); };

// Adding to parameters
var add = ([Description("The first operand")] int a, [Description("The second operand")] int b) => a + b;

// Adding to the return value
var getMessage = [return: NotNull] () => "Hello";

// Execute
func();
Console.WriteLine($"1 + 2 = {add(1, 2)}");
Console.WriteLine($"Message: {getMessage()}");

// Inspect Attributes via Reflection
var methodInfo = func.Method;
var attr = methodInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
Console.WriteLine($"Lambda's Attribute: {attr?.Description}");

var paramsInfo = add.Method.GetParameters();
foreach (var p in paramsInfo)
{
    var pAttr = p.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
    Console.WriteLine($"Attribute for parameter {p.Name}: {pAttr?.Description}");
}

// --------------------------------------------------------------
// 2. Default Lambda Parameters (C# 12)
// --------------------------------------------------------------
Console.WriteLine("\n2. Default Lambda Parameters (C# 12)");

var print = (string message = "World") => Console.WriteLine($"Hello, {message}!");

print();          // Hello, World!
print("C# 12");   // Hello, C# 12!

// Combining with named arguments
var format = (string text, bool uppercase = false, string prefix = ">> ") =>
{
    var result = prefix + text;
    return uppercase ? result.ToUpper() : result;
};

Console.WriteLine(format("hello"));
Console.WriteLine(format("hello", true));
Console.WriteLine(format("hello", false, "## ")); 

Console.WriteLine("\n=== Example End ===\n");
