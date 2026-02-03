// 示範邏輯模式組合子（and, or, not）

Console.WriteLine("=== 邏輯模式組合子範例 ===\n");

// --------------------------------------------------------------
// 1. and 組合子
// --------------------------------------------------------------
Console.WriteLine("1. and 組合子");
Console.WriteLine(new string('-', 40));

int[] percentValues = [-10, 0, 50, 100, 150];

foreach (int n in percentValues)
{
    bool isValid = IsValidPercent(n);
    Console.WriteLine($"{n,4}: {(isValid ? "有效百分比" : "無效")}");
}

Console.WriteLine("\n數字位數分類：");
int[] numbers = [5, 25, 250, 2500];

foreach (int n in numbers)
{
    string category = DescribeDigits(n);
    Console.WriteLine($"{n,4}: {category}");
}

// --------------------------------------------------------------
// 2. or 組合子
// --------------------------------------------------------------
Console.WriteLine("\n2. or 組合子");
Console.WriteLine(new string('-', 40));

Console.WriteLine("週末判斷：");
foreach (DayOfWeek day in Enum.GetValues<DayOfWeek>())
{
    bool isWeekend = IsWeekend(day);
    Console.WriteLine($"{day,-9}: {(isWeekend ? "週末" : "工作日")}");
}

Console.WriteLine("\n母音判斷：");
string testChars = "aEiOuBcD";
foreach (char c in testChars)
{
    bool isVowel = IsVowel(c);
    Console.WriteLine($"'{c}': {(isVowel ? "母音" : "非母音")}");
}

// --------------------------------------------------------------
// 3. not 組合子
// --------------------------------------------------------------
Console.WriteLine("\n3. not 組合子");
Console.WriteLine(new string('-', 40));

object?[] testObjects = ["Hello", null, 42, "World"];

foreach (object? obj in testObjects)
{
    if (obj is not null)
    {
        Console.WriteLine($"不是 null: {obj}");
    }
    else
    {
        Console.WriteLine("是 null");
    }
}

Console.WriteLine("\n型別排除：");
object[] mixedValues = ["text", 123, 45.6, true];

foreach (object value in mixedValues)
{
    if (value is not string)
    {
        Console.WriteLine($"不是字串: {value} ({value.GetType().Name})");
    }
    else
    {
        Console.WriteLine($"是字串: \"{value}\"");
    }
}

// --------------------------------------------------------------
// 4. 組合使用
// --------------------------------------------------------------
Console.WriteLine("\n4. 組合使用");
Console.WriteLine(new string('-', 40));

Console.WriteLine("字元分類：");
string chars = "aZ5_@";

foreach (char c in chars)
{
    bool isLetterOrDigit = IsLetterOrDigit(c);
    bool isIdentifierStart = IsValidIdentifierStart(c);
    Console.WriteLine($"'{c}': 字母/數字={isLetterOrDigit}, 識別符開頭={isIdentifierStart}");
}

// --------------------------------------------------------------
// 5. 運算子優先順序
// --------------------------------------------------------------
Console.WriteLine("\n5. 運算子優先順序");
Console.WriteLine(new string('-', 40));

int[] rangeTests = [25, 75, 125, 175];

foreach (int n in rangeTests)
{
    // and 的優先順序高於 or
    bool inRange = n is >= 0 and <= 50 or >= 100 and <= 150;
    Console.WriteLine($"{n,3}: 在範圍 [0-50] 或 [100-150] = {inRange}");
}

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 模式比對方法
// ============================================================

static bool IsValidPercent(int n) => n is >= 0 and <= 100;

static string DescribeDigits(int n) => n switch
{
    >= 1 and <= 9 => "個位數",
    >= 10 and <= 99 => "兩位數",
    >= 100 and <= 999 => "三位數",
    _ => "其他"
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
