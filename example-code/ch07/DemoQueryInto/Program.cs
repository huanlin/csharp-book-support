// 示範使用 into 關鍵字延續查詢

Console.WriteLine("=== 使用 into 關鍵字範例 ===\n");

string[] names = ["Tom", "Dick", "Harry", "Mary", "Jay"];

// 移除母音後篩選長度
var result = from n in names
             select n.Replace("a", "").Replace("e", "")
                     .Replace("i", "").Replace("o", "").Replace("u", "")
             into noVowel
             where noVowel.Length > 2
             orderby noVowel
             select noVowel;

Console.WriteLine($"來源：{string.Join(", ", names)}");
Console.WriteLine($"移除母音後長度 > 2：{string.Join(", ", result)}");

Console.WriteLine("\n=== 範例結束 ===");
