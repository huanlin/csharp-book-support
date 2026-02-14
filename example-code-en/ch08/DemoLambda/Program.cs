// Demo: Various Syntax of Lambda Expressions

Console.WriteLine("=== Lambda Expression Examples ===\n");

// --------------------------------------------------------------
// 1. Expression Lambda vs. Statement Lambda
// --------------------------------------------------------------
Console.WriteLine("1. Expression Lambda vs. Statement Lambda");
Console.WriteLine(new string('-', 40));

// Expression lambda (single line, no curly braces)
Func<int, int> square1 = x => x * x;

// Statement lambda (with curly braces and return)
Func<int, int> square2 = x =>
{
    return x * x;
};

Console.WriteLine($"Expression lambda: square1(5) = {square1(5)}");
Console.WriteLine($"Statement lambda: square2(5) = {square2(5)}");

// --------------------------------------------------------------
// 2. Parameter Type Inference and Simplification
// --------------------------------------------------------------
Console.WriteLine("\n2. Parameter Type Inference and Simplification");
Console.WriteLine(new string('-', 40));

// Full syntax
Func<string, bool> predicate1 = (string s) => { return s.Length > 5; };

// Omitting parameter type
Func<string, bool> predicate2 = (s) => { return s.Length > 5; };

// Omitting parentheses (only for a single parameter)
Func<string, bool> predicate3 = s => { return s.Length > 5; };

// Omitting curly braces and return
Func<string, bool> predicate4 = s => s.Length > 5;

Console.WriteLine($"predicate4(\"Hello\") = {predicate4("Hello")}");
Console.WriteLine($"predicate4(\"Hi\") = {predicate4("Hi")}");

// Parentheses must be kept when there are no parameters
Func<int> getRandom = () => Random.Shared.Next();
Console.WriteLine($"getRandom() = {getRandom()}");

// --------------------------------------------------------------
// 3. Lambda Default Parameters (C# 12)
// --------------------------------------------------------------
Console.WriteLine("\n3. Lambda Default Parameters (C# 12)");
Console.WriteLine(new string('-', 40));

// Lambda can have default parameter values
var greeting = (string name = "World") => $"Hello, {name}!";

Console.WriteLine(greeting("Alice"));  // Hello, Alice!
Console.WriteLine(greeting());         // Hello, World!

// Combining multiple default parameters
var format = (string text, bool uppercase = false, string prefix = "") =>
{
    var result = prefix + text;
    return uppercase ? result.ToUpper() : result;
};

Console.WriteLine(format("hello"));             // hello
Console.WriteLine(format("hello", true));       // HELLO
Console.WriteLine(format("hello", false, ">>> ")); // >>> hello
Console.WriteLine(format("hello", true, ">>> "));  // >>> HELLO

// --------------------------------------------------------------
// 4. Evolution from Anonymous Methods to Lambda
// --------------------------------------------------------------
Console.WriteLine("\n4. Evolution from Anonymous Methods to Lambda");
Console.WriteLine(new string('-', 40));

// StringPredicate delegate type is defined at the bottom of the file

// C# 2.0: Anonymous Method
StringPredicate p1 = delegate(string s) { return s.EndsWith("go"); };

// C# 3.0: Statement Lambda
StringPredicate p2 = (string s) => { return s.EndsWith("go"); };

// C# 3.0: Expression Lambda (minimal form)
StringPredicate p3 = s => s.EndsWith("go");

Console.WriteLine($"p1(\"Mango\") = {p1("Mango")}");
Console.WriteLine($"p2(\"Mango\") = {p2("Mango")}");
Console.WriteLine($"p3(\"Mango\") = {p3("Mango")}");

// --------------------------------------------------------------
// 5. Static Lambda (C# 9)
// --------------------------------------------------------------
Console.WriteLine("\n5. Static Lambda (C# 9)");
Console.WriteLine(new string('-', 40));

// Static lambdas cannot capture external variables
Func<int, int> doubler = static n => n * 2;
Console.WriteLine($"static lambda: doubler(5) = {doubler(5)}");

// The following would cause a compile error:
// int factor = 2;
// Func<int, int> multiplier = static n => n * factor;  // Error

Console.WriteLine("Static lambdas avoid unexpected closure overhead.");

// --------------------------------------------------------------
// 6. Choice between Lambda and Local Methods
// --------------------------------------------------------------
Console.WriteLine("\n6. Lambda vs. Local Method");
Console.WriteLine(new string('-', 40));

// Lambda is suitable for: passing to LINQ or as a parameter
var numbers = new[] { 1, 2, 3, 4, 5 };
var evens = numbers.Where(x => x % 2 == 0);
Console.WriteLine($"Even numbers (LINQ + Lambda): {string.Join(", ", evens)}");

// Local method is suitable for: recursion needs
int Factorial(int n)
{
    // Local methods support recursion
    int FactorialImpl(int x)
    {
        if (x <= 1) return 1;
        return x * FactorialImpl(x - 1);
    }

    return FactorialImpl(n);
}

Console.WriteLine($"5! = {Factorial(5)}");

Console.WriteLine("\n=== Example End ===");

// ============================================================
// Delegate Type Declarations (must be after top-level statements)
// ============================================================

delegate bool StringPredicate(string s);
