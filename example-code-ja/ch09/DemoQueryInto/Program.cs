// デモ: into キーワードによる継続クエリ

Console.WriteLine("=== 'into' キーワードの例 ===\n");

string[] names = ["Tom", "Dick", "Harry", "Mary", "Jay"];

var result = from n in names
             select n.Replace("a", "").Replace("e", "")
                     .Replace("i", "").Replace("o", "").Replace("u", "")
             into noVowel
             where noVowel.Length > 2
             orderby noVowel
             select noVowel;

Console.WriteLine($"元データ: {string.Join(", ", names)}");
Console.WriteLine($"母音除去後に長さ > 2: {string.Join(", ", result)}");

Console.WriteLine("\n=== 例の終了 ===");
