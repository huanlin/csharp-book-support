// Demo: List Pattern - C# 11+

Console.WriteLine("=== List Pattern Example ===\n");

// --------------------------------------------------------------
// 1. Basic List Pattern
// --------------------------------------------------------------
Console.WriteLine("1. Basic List Pattern");
Console.WriteLine(new string('-', 40));

int[] exactMatch = [1, 2, 3, 4, 5];

if (exactMatch is [1, 2, 3, 4, 5])
{
    Console.WriteLine("[1, 2, 3, 4, 5]: Exact match");
}

// --------------------------------------------------------------
// 2. Using Underscore to Match Any Element
// --------------------------------------------------------------
Console.WriteLine("\n2. Using Underscore to Match Any Element");
Console.WriteLine(new string('-', 40));

int[] wildcardTest = [1, 99, 3, 88, 5];

if (wildcardTest is [1, _, 3, _, 5])
{
    Console.WriteLine("[1, _, 3, _, 5]: The 2nd and 4th elements can be any value");
}

// --------------------------------------------------------------
// 3. Slice Pattern
// --------------------------------------------------------------
Console.WriteLine("\n3. Slice Pattern");
Console.WriteLine(new string('-', 40));

int[] sliceTest = [1, 2, 3, 4, 5];

if (sliceTest is [1, .., 5])
{
    Console.WriteLine("[1, .., 5]: Starts with 1, ends with 5");
}

if (sliceTest is [1, ..])
{
    Console.WriteLine("[1, ..]: Starts with 1, followed by anything");
}

if (sliceTest is [.., 5])
{
    Console.WriteLine("[.., 5]: Ends with 5");
}

// --------------------------------------------------------------
// 4. Capturing Slices
// --------------------------------------------------------------
Console.WriteLine("\n4. Capturing Slices");
Console.WriteLine(new string('-', 40));

int[] numbers = [1, 2, 3, 4, 5];

if (numbers is [var first, .. var middle, var last])
{
    Console.WriteLine($"First: {first}");
    Console.WriteLine($"Last: {last}");
    Console.WriteLine($"Middle: {string.Join(", ", middle)}");
}

// --------------------------------------------------------------
// 5. Practical Example: Command-Line Argument Parsing
// --------------------------------------------------------------
Console.WriteLine("\n5. Practical Example: Command-Line Argument Parsing");
Console.WriteLine(new string('-', 40));

string[][] commandTests =
[
    ["help"],
    ["version"],
    ["copy", "source.txt", "dest.txt"],
    ["move", "old.txt", "new.txt"],
    ["delete", "temp.txt"],
    ["unknown", "arg1", "arg2"],
    []
];

foreach (string[] cmdArgs in commandTests)
{
    string result = ParseCommand(cmdArgs);
    Console.WriteLine($"[{string.Join(", ", cmdArgs)}] -> {result}");
}

// --------------------------------------------------------------
// 6. Nested Patterns
// --------------------------------------------------------------
Console.WriteLine("\n6. Nested Patterns");
Console.WriteLine(new string('-', 40));

int[][] testArrays =
[
    [1, 2, 3],
    [-1, 0, 5],
    [1, 5, 3],
    [2, 4, 8]
];

foreach (int[] arr in testArrays)
{
    string description = DescribeArray(arr);
    Console.WriteLine($"[{string.Join(", ", arr)}]: {description}");
}

// --------------------------------------------------------------
// 7. Comprehensive Example
// --------------------------------------------------------------
Console.WriteLine("\n7. Comprehensive Example");
Console.WriteLine(new string('-', 40));

int[][] patternTests =
[
    [],
    [1, 2],
    [1, 2, 99, 10],
    [1, 2, 3, 4, 5, 6, 7, 8, 9, 10],
    [9, 7, 5],
    [0, 1, 2, 3],
    [2, 4, 6, 8, 10]
];

foreach (int[] values in patternTests)
{
    string pattern = CheckPattern(values);
    Console.WriteLine($"[{string.Join(", ", values)}] -> {pattern}");
}

Console.WriteLine("\n=== Example End ===");

// ============================================================
// Pattern Matching Methods
// ============================================================

static string ParseCommand(string[] args) => args switch
{
    ["help"] => "Show help",
    ["version"] => "Show version",
    ["copy", var src, var dest] => $"Copy {src} to {dest}",
    ["move", var src, var dest] => $"Move {src} to {dest}",
    ["delete", var file] => $"Delete {file}",
    [var cmd, ..] => $"Unknown command: {cmd}",
    [] => "Please enter a command"
};

static string DescribeArray(int[] arr) => arr switch
{
    [> 0, > 0, > 0] => "All elements are positive",
    [1 or 2, _, < 10] => "First is 1 or 2, last is less than 10",
    [_, _, _] => "Array with three elements",
    _ => "Other"
};

static string CheckPattern(int[] values) => values switch
{
    [] => "Empty array",
    [1, 2, _, 10] => "Contains 1, 2, any number, 10",
    [1, 2, .., 10] => "Starts with 1, 2, ends with 10",
    [1, 2] => "Contains 1 then 2",
    [int item1, int item2, int item3] =>
        $"Contains three elements: {item1}, {item2}, {item3}",
    [0, _] => "Starts with 0, followed by one number",
    [0, ..] => "Starts with 0, followed by any amount",
    [2, .. int[] others] => $"Starts with 2, followed by {others.Length} others",
    [..] => "Any elements in any order"
};
