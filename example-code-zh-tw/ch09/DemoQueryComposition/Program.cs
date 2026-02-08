// 示範查詢組合策略：漸進式與條件式

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
