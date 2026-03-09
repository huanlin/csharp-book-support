// デモ: クエリ合成戦略（段階的・条件付き）

Console.WriteLine("=== クエリ合成戦略の例 ===\n");

// --------------------------------------------------------------
// 1. 段階的クエリ構築
// --------------------------------------------------------------
Console.WriteLine("1. 段階的クエリ構築");
Console.WriteLine(new string('-', 40));

string[] names = ["Tom", "Dick", "Harry", "Mary", "Jay"];

var filtered = names.Where(n => n.Contains('a'));
var sorted = filtered.OrderBy(n => n);
var query = sorted.Select(n => n.ToUpper());

Console.WriteLine($"元データ: {string.Join(", ", names)}");
Console.WriteLine($"結果: {string.Join(", ", query)}");

// --------------------------------------------------------------
// 2. 条件付きクエリ構築
// --------------------------------------------------------------
Console.WriteLine("\n2. 条件付きクエリ構築");
Console.WriteLine(new string('-', 40));

var products = GetProducts();
Console.WriteLine("全商品:");
foreach (var p in products)
{
    Console.WriteLine($"  {p.Name}: ${p.Price}, 在庫 {p.Stock}");
}

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

Console.WriteLine($"\n条件: 在庫あり={onlyInStock}, 価格>={minPrice}, ソート={sortByPrice}");
Console.WriteLine("結果:");
foreach (var p in productQuery)
{
    Console.WriteLine($"  {p.Name}: ${p.Price}");
}

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// テストデータ
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
// データモデル
// ============================================================

public record Product(string Name, decimal Price, int Stock);
