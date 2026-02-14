// Demo: dynamic types and ExpandoObject
using System.Dynamic;

// var vs dynamic Comparison
var s1 = "hello";      // Compile-time type is string
dynamic s2 = "hello";  // Compile-time type is dynamic; runtime type is string

Console.WriteLine($"s1 Compile-time: string, Runtime: {s1.GetType().Name}");
Console.WriteLine($"s2 Compile-time: dynamic, Runtime: {s2.GetType().Name}");

// dynamic mixing example
dynamic x = 2;
var y = x * 10;        // Compile-time type of y is dynamic
var z = (int)x;        // Compile-time type of z is int
Console.WriteLine($"\ny Compile-time: dynamic, Runtime: {y.GetType().Name}");
Console.WriteLine($"z Compile-time: int, Runtime: {z.GetType().Name}");

// Dynamic property addition with ExpandoObject
dynamic obj = new ExpandoObject();
obj.FirstName = "Bruce";
obj.LastName = "Wayne";
obj.Age = 35;

Console.WriteLine($"\nDynamic Object: {obj.FirstName} {obj.LastName}, Age: {obj.Age}");

// Handling dynamic objects with methods
string fullName = GetFullName(obj);
Console.WriteLine($"Full Name: {fullName}");

// Dynamic objects can also be treated as dictionaries
var dict = (IDictionary<string, object>)obj;
Console.WriteLine($"\nDynamic Object Properties:");
foreach (var kvp in dict)
{
    Console.WriteLine($"  {kvp.Key} = {kvp.Value}");
}

static string GetFullName(dynamic obj)
{
    return obj.FirstName + " " + obj.LastName;
}
