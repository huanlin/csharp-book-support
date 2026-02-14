// Demo: Closure and Loop Variable Capture Traps

Console.WriteLine("=== Closure Capture and Loop Traps Example ===\n");

// --------------------------------------------------------------
// 1. Variable Capture Trap
// --------------------------------------------------------------
Console.WriteLine("1. Variable Capture Trap");
Console.WriteLine(new string('-', 40));

int[] numbers = [1, 2];
int factor = 10;
IEnumerable<int> queryWithCapture = numbers.Select(n => n * factor);

Console.WriteLine($"factor = {factor}");
factor = 20;
Console.WriteLine($"Modified factor = {factor}");
Console.WriteLine($"Query result: {string.Join(", ", queryWithCapture)}");
Console.WriteLine("(Deferred execution uses the modified value: 20)");

// --------------------------------------------------------------
// 2. Loop Capture Trap and Solutions
// --------------------------------------------------------------
Console.WriteLine("\n2. Loop Capture Trap and Solutions");
Console.WriteLine(new string('-', 40));

string testString = "Not what you might expect";
string vowels = "aeiou";

// Solution 1: foreach
IEnumerable<char> query1 = testString;
foreach (char vowel in vowels)
    query1 = query1.Where(c => c != vowel);

Console.WriteLine($"Source: \"{testString}\"");
Console.WriteLine($"foreach removing vowels: \"{string.Concat(query1)}\"");

// Solution 2: Create a copy within for loop
IEnumerable<char> query2 = testString;
for (int i = 0; i < vowels.Length; i++)
{
    char vowel = vowels[i]; // Create a new variable for each iteration
    query2 = query2.Where(c => c != vowel);
}
Console.WriteLine($"for loop (using copy): \"{string.Concat(query2)}\"");

Console.WriteLine("\n=== Example End ===");
