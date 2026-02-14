// Demo: Non-destructive mutation of Anonymous Types (C# 10+)

var a1 = new { A = 1, B = 2, C = 3 };
var a2 = a1 with { B = 99 };  // Copy a1, but change B to 99

Console.WriteLine($"Original: A={a1.A}, B={a1.B}, C={a1.C}");
Console.WriteLine($"Modified: A={a2.A}, B={a2.B}, C={a2.C}");
