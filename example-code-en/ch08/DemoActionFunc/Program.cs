// Demo: Action<T> and Func<T, TResult>

Console.WriteLine("=== Action and Func Examples ===\n");

// --------------------------------------------------------------
// 1. Action: Delegate with no return value
// --------------------------------------------------------------
Console.WriteLine("1. Action: Delegate with no return value");
Console.WriteLine(new string('-', 40));

// No parameters
Action greet = () => Console.WriteLine("Hello!");
greet();

// One parameter
Action<string> sayHello = name => Console.WriteLine($"Hello, {name}!");
sayHello("Alice");

// Two parameters
Action<string, int> repeat = (text, count) =>
{
    for (int i = 0; i < count; i++)
        Console.WriteLine($"  {text}");
};
repeat("Hi", 3);

// --------------------------------------------------------------
// 2. Func: Delegate with a return value
// --------------------------------------------------------------
Console.WriteLine("\n2. Func: Delegate with a return value");
Console.WriteLine(new string('-', 40));

// No parameters, returns int
Func<int> getRandomNumber = () => Random.Shared.Next(1, 100);
Console.WriteLine($"Random Number: {getRandomNumber()}");

// One parameter, returns bool
Func<string, bool> isEmpty = s => string.IsNullOrEmpty(s);
Console.WriteLine($"Empty String Check: isEmpty(\"\") = {isEmpty("")}");

// Two parameters, returns int
Func<int, int, int> add = (x, y) => x + y;
Console.WriteLine($"add(10, 20) = {add(10, 20)}");

// Three parameters, returns string
Func<string, string, string, string> joinWithSeparator =
    (s1, s2, separator) => $"{s1}{separator}{s2}";
Console.WriteLine($"joinWithSeparator: {joinWithSeparator("Hello", "World", " - ")}");

// --------------------------------------------------------------
// 3. Configuration-Driven Behavior
// --------------------------------------------------------------
Console.WriteLine("\n3. Configuration-Driven Behavior");
Console.WriteLine(new string('-', 40));

var items = new[] { "apple", "BANANA", "Cherry", "date" };
Console.WriteLine($"Test Data: {string.Join(", ", items)}");

// Configuration 1: Process uppercase strings
var processor1 = new DataProcessor(
    shouldProcess: s => s.Any(char.IsUpper),
    onProcessed: s => Console.WriteLine($"  Processing uppercase item: {s}")
);
Console.WriteLine("\nProcessing items containing uppercase letters:");
processor1.Process(items);

// Configuration 2: Process strings with length greater than 5
var processor2 = new DataProcessor(
    shouldProcess: s => s.Length > 5,
    onProcessed: s => Console.WriteLine($"  Processing long item: {s}")
);
Console.WriteLine("\nProcessing items with length > 5:");
processor2.Process(items);

// --------------------------------------------------------------
// 4. Covariance and Contravariance in Generic Delegates
// --------------------------------------------------------------
Console.WriteLine("\n4. Covariance and Contravariance in Generic Delegates");
Console.WriteLine(new string('-', 40));

// Covariance Example (Func's TResult is out)
Func<string> getString = () => "hello";
Func<object> getObject = getString;  // Legal: string can be converted to object
Console.WriteLine($"Covariance: getObject() = {getObject()}");

// Contravariance Example (Action's T is in)
Action<object> actOnObject = o => Console.WriteLine($"  Processing: {o}");
Action<string> actOnString = actOnObject;  // Legal: A more general processor can be used
actOnString("Testing Contravariance");

Console.WriteLine("\n=== Example End ===");

// ============================================================
// Helper Classes
// ============================================================

public class DataProcessor
{
    private readonly Func<string, bool> _shouldProcess;
    private readonly Action<string> _onProcessed;

    public DataProcessor(Func<string, bool> shouldProcess, Action<string> onProcessed)
    {
        _shouldProcess = shouldProcess;
        _onProcessed = onProcessed;
    }

    public void Process(IEnumerable<string> items)
    {
        foreach (var item in items)
        {
            if (_shouldProcess(item))
            {
                _onProcessed(item);
            }
        }
    }
}
