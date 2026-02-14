// Demo: Type Compatibility and Variance of Delegates

// 1. Delegate Type Compatibility
Console.WriteLine("1. Delegate Type Compatibility");
Console.WriteLine(new string('-', 40));

// D1 and D2 delegate types are defined at the bottom of the file
D1 d1 = SayHello;
// D2 d2 = d1;  // Compile error: Type mismatch (even though the signatures are identical)
D2 d2 = new D2(d1);  // Possible: Explicitly create a new delegate instance (C# 2.0+ supports this syntax)

Console.WriteLine("d1 Call:");
d1();
Console.WriteLine("d2 Call:");
d2();

// 2. Parameter Contravariance and Return Covariance
Console.WriteLine("\n2. Parameter Contravariance and Return Covariance");
Console.WriteLine(new string('-', 40));

// Parameter Contravariance (using StringAction delegate type, defined at the bottom)
// StringAction expects to receive a string, but we provide a method that can handle an object (more general)
// Contravariance: parameter type matches or is a base type

StringAction sa = ActOnObject;  // Legal: object is more general than string
sa("hello world");

// Return Covariance (using ObjectRetriever delegate type, defined at the bottom)
// ObjectRetriever expects to return an object, but we provide a method returning a string (more specific)
// Covariance: return type matches or is a derived type

ObjectRetriever or = RetrieveString;  // Legal: string is more specific than object
Console.WriteLine($"  Retrieved: {or()}");

Console.ReadKey();

// ============================================================
// Helper Methods
// ============================================================

static void SayHello() => Console.WriteLine("  Hello!");
static void ActOnObject(object o) => Console.WriteLine($"  Processing object: {o}");
static string RetrieveString() => "This is a string";

// ============================================================
// Delegate Type Declarations
// ============================================================

delegate void D1();
delegate void D2();
delegate void StringAction(string s);
delegate object ObjectRetriever();
