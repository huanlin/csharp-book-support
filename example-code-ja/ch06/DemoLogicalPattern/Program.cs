// デモ: 論理パターン結合子（and / or / not）

Console.WriteLine("=== 論理パターン結合子の例 ===\n");

// --------------------------------------------------------------
// 1. and 結合子
// --------------------------------------------------------------
Console.WriteLine("1. and 結合子");
Console.WriteLine(new string('-', 40));

int[] percentValues = [-10, 0, 50, 100, 150];

foreach (int n in percentValues)
{
    bool isValid = IsValidPercent(n);
    Console.WriteLine($"{n,4}: {(isValid ? "有効なパーセント" : "無効")} ");
}

Console.WriteLine("\n桁数分類:");
int[] numbers = [5, 25, 250, 2500];

foreach (int n in numbers)
{
    string category = DescribeDigits(n);
    Console.WriteLine($"{n,4}: {category}");
}

// --------------------------------------------------------------
// 2. or 結合子
// --------------------------------------------------------------
Console.WriteLine("\n2. or 結合子");
Console.WriteLine(new string('-', 40));

Console.WriteLine("週末判定:");
foreach (DayOfWeek day in Enum.GetValues<DayOfWeek>())
{
    bool isWeekend = IsWeekend(day);
    Console.WriteLine($"{day,-9}: {(isWeekend ? "週末" : "平日")}");
}

Console.WriteLine("\n母音判定:");
string testChars = "aEiOuBcD";
foreach (char c in testChars)
{
    bool isVowel = IsVowel(c);
    Console.WriteLine($"'{c}': {(isVowel ? "母音" : "母音ではない")}");
}

// --------------------------------------------------------------
// 3. not 結合子
// --------------------------------------------------------------
Console.WriteLine("\n3. not 結合子");
Console.WriteLine(new string('-', 40));

object?[] testObjects = ["Hello", null, 42, "World"];

foreach (object? obj in testObjects)
{
    if (obj is not null)
    {
        Console.WriteLine($"null ではない: {obj}");
    }
    else
    {
        Console.WriteLine("null です");
    }
}

Console.WriteLine("\n型の除外:");
object[] mixedValues = ["text", 123, 45.6, true];

foreach (object value in mixedValues)
{
    if (value is not string)
    {
        Console.WriteLine($"string ではない: {value} ({value.GetType().Name})");
    }
    else
    {
        Console.WriteLine($"string です: \"{value}\"");
    }
}

// --------------------------------------------------------------
// 4. 複合利用
// --------------------------------------------------------------
Console.WriteLine("\n4. 複合利用");
Console.WriteLine(new string('-', 40));

Console.WriteLine("文字分類:");
string chars = "aZ5_@";

foreach (char c in chars)
{
    bool isLetterOrDigit = IsLetterOrDigit(c);
    bool isIdentifierStart = IsValidIdentifierStart(c);
    Console.WriteLine($"'{c}': 英数字={isLetterOrDigit}, 識別子開始={isIdentifierStart}");
}

// --------------------------------------------------------------
// 5. 演算子優先順位
// --------------------------------------------------------------
Console.WriteLine("\n5. 演算子優先順位");
Console.WriteLine(new string('-', 40));

int[] rangeTests = [25, 75, 125, 175];

foreach (int n in rangeTests)
{
    // and は or より優先
    bool inRange = n is >= 0 and <= 50 or >= 100 and <= 150;
    Console.WriteLine($"{n,3}: [0-50] または [100-150] に含まれる = {inRange}");
}

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// パターンマッチメソッド
// ============================================================

static bool IsValidPercent(int n) => n is >= 0 and <= 100;

static string DescribeDigits(int n) => n switch
{
    >= 1 and <= 9 => "1桁",
    >= 10 and <= 99 => "2桁",
    >= 100 and <= 999 => "3桁",
    _ => "その他"
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
