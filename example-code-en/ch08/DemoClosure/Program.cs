// Demo: Closures and Variable Capture

Console.WriteLine("=== Closures and Variable Capture ===\n");

// --------------------------------------------------------------
// 1. Basic External Variable Capture
// --------------------------------------------------------------
Console.WriteLine("1. Basic External Variable Capture");
Console.WriteLine(new string('-', 40));

int threshold = 10;

Func<int, bool> isAboveThreshold = n => n > threshold;

Console.WriteLine($"threshold = {threshold}");
Console.WriteLine($"isAboveThreshold(15) = {isAboveThreshold(15)}");
Console.WriteLine($"isAboveThreshold(5) = {isAboveThreshold(5)}");

// Modifying external variable affects lambda behavior
threshold = 20;
Console.WriteLine($"\nModified threshold = {threshold}");
Console.WriteLine($"isAboveThreshold(15) = {isAboveThreshold(15)}");

// --------------------------------------------------------------
// 2. Extension of Closure Lifetime
// --------------------------------------------------------------
Console.WriteLine("\n2. Extension of Closure Lifetime");
Console.WriteLine(new string('-', 40));

var counter = CreateCounter();
Console.WriteLine($"counter() = {counter()}");
Console.WriteLine($"counter() = {counter()}");
Console.WriteLine($"counter() = {counter()}");
Console.WriteLine("(The 'count' variable lives along with the delegate object)");

static Func<int> CreateCounter()
{
    int count = 0;  // This variable's lifetime will be extended
    return () => ++count;
}

// --------------------------------------------------------------
// 3. Closure Trap in for-loops
// --------------------------------------------------------------
Console.WriteLine("\n3. Closure Trap in for-loops");
Console.WriteLine(new string('-', 40));

// ✗ Incorrect Demonstration
Console.WriteLine("Incorrect Demonstration (All lambdas capture the same 'i'):");
var wrongActions = new List<Action>();
for (int i = 0; i < 3; i++)
{
    wrongActions.Add(() => Console.Write($"{i} "));
}
Console.Write("  Output: ");
foreach (var action in wrongActions)
    action();
Console.WriteLine("(Expected 0, 1, 2)");

// ✓ Correct Demonstration 1: Create a local copy
Console.WriteLine("\nCorrect Demonstration 1 (Create a local copy):");
var correctActions1 = new List<Action>();
for (int i = 0; i < 3; i++)
{
    int copy = i;  // Create a new variable for each iteration
    correctActions1.Add(() => Console.Write($"{copy} "));
}
Console.Write("  Output: ");
foreach (var action in correctActions1)
    action();
Console.WriteLine();

// ✓ Correct Demonstration 2: Use foreach
Console.WriteLine("\nCorrect Demonstration 2 (Use foreach):");
var numbers = new[] { 0, 1, 2 };
var correctActions2 = new List<Action>();
foreach (var num in numbers)
{
    correctActions2.Add(() => Console.Write($"{num} "));
}
Console.Write("  Output: ");
foreach (var action in correctActions2)
    action();
Console.WriteLine();

// --------------------------------------------------------------
// 4. Timing of Variable Capture
// --------------------------------------------------------------
Console.WriteLine("\n4. Timing of Variable Capture");
Console.WriteLine(new string('-', 40));

int factor = 10;
Func<int, int> multiply = n => n * factor;

Console.WriteLine($"factor = {factor}");
factor = 20;
Console.WriteLine($"Modified factor = {factor}");
Console.WriteLine($"multiply(5) = {multiply(5)}");
Console.WriteLine("(The lambda uses the current value of 'factor' at execution time)");

// --------------------------------------------------------------
// 5. Vowel Removal Example
// --------------------------------------------------------------
Console.WriteLine("\n5. Vowel Removal Example");
Console.WriteLine(new string('-', 40));

string testString = "Not what you might expect";
string vowels = "aeiou";

// Recommended approach: foreach loop
IEnumerable<char> query = testString;
foreach (char vowel in vowels)
    query = query.Where(c => c != vowel);

Console.WriteLine($"Original: \"{testString}\"");
Console.WriteLine($"After removing vowels: \"{string.Concat(query)}\"");

// --------------------------------------------------------------
// 6. How Compilers Handle Closures (Concept Explanation)
// --------------------------------------------------------------
Console.WriteLine("\n6. How Compilers Handle Closures");
Console.WriteLine(new string('-', 40));

Console.WriteLine("When a lambda captures external variables, the compiler generates:");
Console.WriteLine("  1. A hidden class (DisplayClass) to store the captured variables.");
Console.WriteLine("  2. The lambda is converted into a method on that class.");
Console.WriteLine("  3. Operations on the original variable become operations on the class field.");

Console.WriteLine("\n=== Example End ===\n");
