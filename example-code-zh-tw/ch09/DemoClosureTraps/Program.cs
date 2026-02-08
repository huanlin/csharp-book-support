// 示範閉包（Closure）與迴圈變數捕獲的陷阱

Console.WriteLine("=== 閉包捕獲與迴圈陷阱範例 ===\n");

// --------------------------------------------------------------
// 1. 捕獲變數的陷阱
// --------------------------------------------------------------
Console.WriteLine("1. 捕獲變數的陷阱");
Console.WriteLine(new string('-', 40));

int[] numbers = [1, 2];
int factor = 10;
IEnumerable<int> queryWithCapture = numbers.Select(n => n * factor);

Console.WriteLine($"factor = {factor}");
factor = 20;
Console.WriteLine($"修改 factor = {factor}");
Console.WriteLine($"查詢結果：{string.Join(", ", queryWithCapture)}");
Console.WriteLine("（延遲執行時使用的是修改後的值 20）");

// --------------------------------------------------------------
// 2. for 迴圈的陷阱與解決方法
// --------------------------------------------------------------
Console.WriteLine("\n2. for 迴圈的陷阱與解決方法");
Console.WriteLine(new string('-', 40));

string testString = "Not what you might expect";
string vowels = "aeiou";

// 正確寫法 1：foreach
IEnumerable<char> query1 = testString;
foreach (char vowel in vowels)
    query1 = query1.Where(c => c != vowel);

Console.WriteLine($"來源：\"{testString}\"");
Console.WriteLine($"foreach 移除母音：\"{string.Concat(query1)}\"");

// 正確寫法 2：for 迴圈內建立副本
IEnumerable<char> query2 = testString;
for (int i = 0; i < vowels.Length; i++)
{
    char vowel = vowels[i]; // 每次迭代建立新變數
    query2 = query2.Where(c => c != vowel);
}
Console.WriteLine($"for（建立副本）：\"{string.Concat(query2)}\"");

Console.WriteLine("\n=== 範例結束 ===");
