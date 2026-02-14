// Demo: Logical Pattern Combinators (and, or, not)

Console.WriteLine("=== Logical Pattern Combinators Example ===\n");

// --------------------------------------------------------------
// 1. and Combinator
// --------------------------------------------------------------
Console.WriteLine("1. and Combinator");
Console.WriteLine(new string('-', 40));

int[] percentValues = [-10, 0, 50, 100, 150];

foreach (int n in percentValues)
{
    bool isValid = IsValidPercent(n);
    Console.WriteLine($"{n,4}: {(isValid ? "Valid Percentage" : "Invalid")}");
}

Console.WriteLine("\nDigit classification:");
int[] numbers = [5, 25, 250, 2500];

foreach (int n in numbers)
{
    string category = DescribeDigits(n);
    Console.WriteLine($"{n,4}: {category}");
}

// --------------------------------------------------------------
// 2. or Combinator
// --------------------------------------------------------------
Console.WriteLine("\n2. or Combinator");
Console.WriteLine(new string('-', 40));

Console.WriteLine("Weekend judgment:");
foreach (DayOfWeek day in Enum.GetValues<DayOfWeek>())
{
    bool isWeekend = IsWeekend(day);
    Console.WriteLine($"{day,-9}: {(isWeekend ? "Weekend" : "Weekday")}");
}

Console.WriteLine("\nVowel judgment:");
string testChars = "aEiOuBcD";
foreach (char c in testChars)
{
    bool isVowel = IsVowel(c);
    Console.WriteLine($"'{c}': {(isVowel ? "Vowel" : "Not a Vowel")}");
}

// --------------------------------------------------------------
// 3. not Combinator
// --------------------------------------------------------------
Console.WriteLine("\n3. not Combinator");
Console.WriteLine(new string('-', 40));

object?[] testObjects = ["Hello", null, 42, "World"];

foreach (object? obj in testObjects)
{
    if (obj is not null)
    {
        Console.WriteLine($"Not null: {obj}");
    }
    else
    {
        Console.WriteLine("Is null");
    }
}

Console.WriteLine("\nType exclusion:");
object[] mixedValues = ["text", 123, 45.6, true];

foreach (object value in mixedValues)
{
    if (value is not string)
    {
        Console.WriteLine($"Not a string: {value} ({value.GetType().Name})");
    }
    else
    {
        Console.WriteLine($"Is a string: \"{value}\"");
    }
}

// --------------------------------------------------------------
// 4. Combined Usage
// --------------------------------------------------------------
Console.WriteLine("\n4. Combined Usage");
Console.WriteLine(new string('-', 40));

Console.WriteLine("Character classification:");
string chars = "aZ5_@";

foreach (char c in chars)
{
    bool isLetterOrDigit = IsLetterOrDigit(c);
    bool isIdentifierStart = IsValidIdentifierStart(c);
    Console.WriteLine($"'{c}': Letter/Digit={isLetterOrDigit}, Identifier Start={isIdentifierStart}");
}

// --------------------------------------------------------------
// 5. Operator Precedence
// --------------------------------------------------------------
Console.WriteLine("\n5. Operator Precedence");
Console.WriteLine(new string('-', 40));

int[] rangeTests = [25, 75, 125, 175];

foreach (int n in rangeTests)
{
    // and has higher precedence than or
    bool inRange = n is >= 0 and <= 50 or >= 100 and <= 150;
    Console.WriteLine($"{n,3}: In range [0-50] or [100-150] = {inRange}");
}

Console.WriteLine("\n=== Example End ===");

// ============================================================
// Pattern Matching Methods
// ============================================================

static bool IsValidPercent(int n) => n is >= 0 and <= 100;

static string DescribeDigits(int n) => n switch
{
    >= 1 and <= 9 => "Single digit",
    >= 10 and <= 99 => "Double digits",
    >= 100 and <= 999 => "Triple digits",
    _ => "Other"
};

static bool IsWeekend(DayOfWeek day) =>
    day is DayOfWeek.Saturday or DayOfWeek.Sunday;

static bool IsVowel(char c) =>
    c is 'a' or 'e' or 'i' or 'o' or 'u'
       or 'A' or 'E' or 'I' or 'O' or 'U';

static bool IsLetterOrDigit(char c) => c is
    (>= 'a' and <= 'z') or
    (>= 'A' and <= 'Z') or
    (>= '0' and <= '9');

static bool IsValidIdentifierStart(char c) => c is
    (>= 'a' and <= 'z') or
    (>= 'A' and <= 'Z') or
    '_';
