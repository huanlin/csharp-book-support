// Demo: Basic String Interpolation

var name = "Michael";
var age = 50;

// Traditional approach
var s1 = string.Format("Name: {0}, Age: {1}", name, age);
// String interpolation
var s2 = $"Name: {name}, Age: {age}";

Console.WriteLine(s1);
Console.WriteLine(s2);

// Formatting and Alignment
var price = 123.456m;
var dt = DateTime.Now;

Console.WriteLine($"\nFormatting Examples:");
Console.WriteLine($"  Price: {price:C2}");           // Currency format
Console.WriteLine($"  Date: {dt:yyyy-MM-dd}");       // Date format
Console.WriteLine($"  Hex: {255:X4}");               // Hexadecimal

// Alignment
var playerName = "Jordan";
var score = 95;
Console.WriteLine($"\nAlignment Examples:");
Console.WriteLine($"|{playerName,-10}|{score,5}|");  // Left-aligned / Right-aligned

// Constant String Interpolation (C# 10+)
const string Greeting = "Hello";
const string Target = "World";
const string Message = $"{Greeting}, {Target}!";
Console.WriteLine($"\nConstant Interpolation: {Message}");
