// 示範查詢組合策略

Console.WriteLine("=== 查詢組合策略範例 ===\n");

// --------------------------------------------------------------
// 1. 漸進式查詢建構
// --------------------------------------------------------------
Console.WriteLine("1. 漸進式查詢建構");
Console.WriteLine(new string('-', 40));

string[] names = ["Tom", "Dick", "Harry", "Mary", "Jay"];

var filtered = names.Where(n => n.Contains('a'));
var sorted = filtered.OrderBy(n => n);
var query = sorted.Select(n => n.ToUpper());

Console.WriteLine($"來源：{string.Join(", ", names)}");
Console.WriteLine($"結果：{string.Join(", ", query)}");

// --------------------------------------------------------------
// 2. 條件式查詢建構
// --------------------------------------------------------------
Console.WriteLine("\n2. 條件式查詢建構");
Console.WriteLine(new string('-', 40));

var products = GetProducts();
Console.WriteLine("所有產品：");
foreach (var p in products)
{
    Console.WriteLine($"  {p.Name}: ${p.Price}, 庫存 {p.Stock}");
}

// 模擬使用者篩選條件
bool onlyInStock = true;
bool sortByPrice = true;
decimal? minPrice = 20;

var productQuery = products.AsEnumerable();

if (onlyInStock)
    productQuery = productQuery.Where(p => p.Stock > 0);

if (minPrice.HasValue)
    productQuery = productQuery.Where(p => p.Price >= minPrice.Value);

if (sortByPrice)
    productQuery = productQuery.OrderBy(p => p.Price);

Console.WriteLine($"\n篩選條件：庫存>0={onlyInStock}, 價格>={minPrice}, 排序={sortByPrice}");
Console.WriteLine("結果：");
foreach (var p in productQuery)
{
    Console.WriteLine($"  {p.Name}: ${p.Price}");
}

// --------------------------------------------------------------
// 3. 使用 into 關鍵字
// --------------------------------------------------------------
Console.WriteLine("\n3. 使用 into 關鍵字");
Console.WriteLine(new string('-', 40));

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

// --------------------------------------------------------------
// 4. 捕獲變數的陷阱
// --------------------------------------------------------------
Console.WriteLine("\n4. 捕獲變數的陷阱");
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
// 5. for 迴圈的陷阱與解決方法
// --------------------------------------------------------------
Console.WriteLine("\n5. for 迴圈的陷阱與解決方法");
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

// --------------------------------------------------------------
// 6. 包裝查詢 vs 子查詢
// --------------------------------------------------------------
Console.WriteLine("\n6. 包裝查詢");
Console.WriteLine(new string('-', 40));

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

// ============================================================
// 測試資料
// ============================================================

static List<Product> GetProducts() =>
[
    new("Apple", 15, 50),
    new("Banana", 8, 0),
    new("Cherry", 25, 30),
    new("Date", 35, 20),
    new("Elderberry", 45, 0)
];

// ============================================================
// 資料類別
// ============================================================

public record Product(string Name, decimal Price, int Stock);
