// 示範包裝查詢（Wrapped Queries）

Console.WriteLine("=== 包裝查詢範例 ===\n");

string[] names = ["Tom", "Dick", "Harry", "Mary", "Jay"];

// 包裝形式（將內部查詢結果作為外部查詢的輸入）
var wrappedQuery = from n1 in
                   (
                       from n2 in names
                       select n2.Replace("a", "").Replace("e", "")
                                .Replace("i", "").Replace("o", "").Replace("u", "")
                   )
                   where n1.Length > 2
                   orderby n1
                   select n1;

Console.WriteLine($"包裝查詢結果：{string.Join(", ", wrappedQuery)}");

// 等效的串接語法
var fluentQuery = names
    .Select(n => n.Replace("a", "").Replace("e", "")
                  .Replace("i", "").Replace("o", "").Replace("u", ""))
    .Where(n => n.Length > 2)
    .OrderBy(n => n);

Console.WriteLine($"串接語法結果：{string.Join(", ", fluentQuery)}");

Console.WriteLine("\n=== 範例結束 ===");
