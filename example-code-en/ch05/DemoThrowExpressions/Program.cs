// Demo: throw expressions (C# 7+)

Console.WriteLine("=== Throw Expression Example ===\n");

// --------------------------------------------------------------
// 1. Used in null-coalescing operator
// --------------------------------------------------------------
Console.WriteLine("1. throw in null-coalescing operator");
Console.WriteLine(new string('-', 40));

string? nullableInput = "Hello";
string result1 = nullableInput ?? throw new ArgumentNullException(nameof(nullableInput));
Console.WriteLine($"Result: {result1}");

try
{
    string? nullInput = null;
    string result2 = nullInput ?? throw new ArgumentNullException(nameof(nullInput));
}
catch (ArgumentNullException ex)
{
    Console.WriteLine($"Exception captured: {ex.ParamName} is null");
}

// --------------------------------------------------------------
// 2. Used in ternary operator
// --------------------------------------------------------------
Console.WriteLine("\n2. throw in ternary operator");
Console.WriteLine(new string('-', 40));

string ValidateInput(string? input) =>
    input != null ? input.Trim() :
    throw new ArgumentNullException(nameof(input));

Console.WriteLine($"Validating '  test  ': '{ValidateInput("  test  ")}'");

try
{
    ValidateInput(null);
}
catch (ArgumentNullException)
{
    Console.WriteLine("Validating null: Thrown ArgumentNullException");
}

// --------------------------------------------------------------
// 3. Used in expression-bodied members
// --------------------------------------------------------------
Console.WriteLine("\n3. throw in expression-bodied members");
Console.WriteLine(new string('-', 40));

var calculator = new Calculator();
Console.WriteLine($"10 / 2 = {calculator.Divide(10, 2)}");

try
{
    calculator.Divide(10, 0);
}
catch (DivideByZeroException)
{
    Console.WriteLine("10 / 0: Thrown DivideByZeroException");
}

try
{
    calculator.NotImplementedMethod();
}
catch (NotImplementedException)
{
    Console.WriteLine("Calling unimplemented method: Thrown NotImplementedException");
}

// --------------------------------------------------------------
// 4. Used in indexers
// --------------------------------------------------------------
Console.WriteLine("\n4. throw in indexers");
Console.WriteLine(new string('-', 40));

var collection = new SafeCollection<string>(["A", "B", "C"]);
Console.WriteLine($"collection[1] = {collection[1]}");

try
{
    var _ = collection[10];
}
catch (IndexOutOfRangeException ex)
{
    Console.WriteLine($"collection[10]: {ex.Message}");
}

// --------------------------------------------------------------
// 5. Used in switch expressions
// --------------------------------------------------------------
Console.WriteLine("\n5. throw in switch expressions");
Console.WriteLine(new string('-', 40));

string GetColorName(int colorCode) => colorCode switch
{
    1 => "Red",
    2 => "Green",
    3 => "Blue",
    _ => throw new ArgumentOutOfRangeException(nameof(colorCode), $"Unknown color code: {colorCode}")
};

Console.WriteLine($"Color code 1: {GetColorName(1)}");
Console.WriteLine($"Color code 2: {GetColorName(2)}");

try
{
    GetColorName(99);
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"Color code 99: {ex.Message}");
}

Console.WriteLine("\n=== Example End ===");

// ============================================================
// Helper Classes
// ============================================================

public class Calculator
{
    // Use throw expression for parameter validation
    public int Divide(int a, int b) =>
        b != 0 ? a / b : throw new DivideByZeroException("Divisor cannot be zero");

    // Indicate method not yet implemented
    public void NotImplementedMethod() =>
        throw new NotImplementedException("This method is not yet implemented");
}

public class SafeCollection<T>
{
    private readonly T[] _items;

    public SafeCollection(T[] items) => _items = items;

    public T this[int index] =>
        index >= 0 && index < _items.Length
            ? _items[index]
            : throw new IndexOutOfRangeException($"Index {index} out of range [0, {_items.Length - 1}]");
}
