// デモ: Wrapped Query

Console.WriteLine("=== Wrapped Query の例 ===\n");

string[] names = ["Tom", "Dick", "Harry", "Mary", "Jay"];

var wrappedQuery = from n1 in
                   (
                       from n2 in names
                       select n2.Replace("a", "").Replace("e", "")
                                .Replace("i", "").Replace("o", "").Replace("u", "")
                   )
                   where n1.Length > 2
                   orderby n1
                   select n1;

Console.WriteLine($"Wrapped Query 結果: {string.Join(", ", wrappedQuery)}");

var fluentQuery = names
    .Select(n => n.Replace("a", "").Replace("e", "")
                  .Replace("i", "").Replace("o", "").Replace("u", ""))
    .Where(n => n.Length > 2)
    .OrderBy(n => n);

Console.WriteLine($"Fluent 構文結果: {string.Join(", ", fluentQuery)}");

Console.WriteLine("\n=== 例の終了 ===");
