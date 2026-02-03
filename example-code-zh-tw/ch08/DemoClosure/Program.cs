// 示範閉包（Closure）與變數捕獲

Console.WriteLine("=== 閉包與變數捕獲 ===\n");

// --------------------------------------------------------------
// 1. 基本的外部變數捕獲
// --------------------------------------------------------------
Console.WriteLine("1. 基本的外部變數捕獲");
Console.WriteLine(new string('-', 40));

int threshold = 10;

Func<int, bool> isAboveThreshold = n => n > threshold;

Console.WriteLine($"threshold = {threshold}");
Console.WriteLine($"isAboveThreshold(15) = {isAboveThreshold(15)}");
Console.WriteLine($"isAboveThreshold(5) = {isAboveThreshold(5)}");

// 修改外部變數會影響 lambda 的行為
threshold = 20;
Console.WriteLine($"\n修改 threshold = {threshold}");
Console.WriteLine($"isAboveThreshold(15) = {isAboveThreshold(15)}");

// --------------------------------------------------------------
// 2. 閉包的生命週期延長
// --------------------------------------------------------------
Console.WriteLine("\n2. 閉包的生命週期延長");
Console.WriteLine(new string('-', 40));

var counter = CreateCounter();
Console.WriteLine($"counter() = {counter()}");
Console.WriteLine($"counter() = {counter()}");
Console.WriteLine($"counter() = {counter()}");
Console.WriteLine("（count 變數隨著委派物件存活）");

static Func<int> CreateCounter()
{
    int count = 0;  // 這個變數的生命週期會延長
    return () => ++count;
}

// --------------------------------------------------------------
// 3. for 迴圈中的閉包陷阱
// --------------------------------------------------------------
Console.WriteLine("\n3. for 迴圈中的閉包陷阱");
Console.WriteLine(new string('-', 40));

// ✗ 錯誤示範
Console.WriteLine("錯誤示範（所有 lambda 捕獲同一個 i）：");
var wrongActions = new List<Action>();
for (int i = 0; i < 3; i++)
{
    wrongActions.Add(() => Console.Write($"{i} "));
}
Console.Write("  輸出：");
foreach (var action in wrongActions)
    action();
Console.WriteLine("（預期 0, 1, 2）");

// ✓ 正確示範 1：建立區域副本
Console.WriteLine("\n正確示範 1（建立區域副本）：");
var correctActions1 = new List<Action>();
for (int i = 0; i < 3; i++)
{
    int copy = i;  // 每次迭代建立新變數
    correctActions1.Add(() => Console.Write($"{copy} "));
}
Console.Write("  輸出：");
foreach (var action in correctActions1)
    action();
Console.WriteLine();

// ✓ 正確示範 2：使用 foreach
Console.WriteLine("\n正確示範 2（使用 foreach）：");
var numbers = new[] { 0, 1, 2 };
var correctActions2 = new List<Action>();
foreach (var num in numbers)
{
    correctActions2.Add(() => Console.Write($"{num} "));
}
Console.Write("  輸出：");
foreach (var action in correctActions2)
    action();
Console.WriteLine();

// --------------------------------------------------------------
// 4. 捕獲變數的時機
// --------------------------------------------------------------
Console.WriteLine("\n4. 捕獲變數的時機");
Console.WriteLine(new string('-', 40));

int factor = 10;
Func<int, int> multiply = n => n * factor;

Console.WriteLine($"factor = {factor}");
factor = 20;
Console.WriteLine($"修改 factor = {factor}");
Console.WriteLine($"multiply(5) = {multiply(5)}");
Console.WriteLine("（lambda 執行時使用的是當下的 factor 值）");

// --------------------------------------------------------------
// 5. 移除母音的範例
// --------------------------------------------------------------
Console.WriteLine("\n5. 移除母音的範例");
Console.WriteLine(new string('-', 40));

string testString = "Not what you might expect";
string vowels = "aeiou";

// 正確寫法：foreach 迴圈
IEnumerable<char> query = testString;
foreach (char vowel in vowels)
    query = query.Where(c => c != vowel);

Console.WriteLine($"原始：\"{testString}\"");
Console.WriteLine($"移除母音後：\"{string.Concat(query)}\"");

// --------------------------------------------------------------
// 6. 編譯器如何處理閉包（概念說明）
// --------------------------------------------------------------
Console.WriteLine("\n6. 編譯器如何處理閉包");
Console.WriteLine(new string('-', 40));

Console.WriteLine("當 lambda 捕獲外部變數時，編譯器會產生：");
Console.WriteLine("  1. 一個隱藏類別（DisplayClass）儲存捕獲的變數");
Console.WriteLine("  2. 將 lambda 轉換為該類別的方法");
Console.WriteLine("  3. 原始變數的操作變成對類別欄位的操作");

Console.WriteLine("\n=== 範例結束 ===");
