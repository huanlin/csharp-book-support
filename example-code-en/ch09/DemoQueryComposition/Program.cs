// Demo: Query Composition Strategies: Progressive and Conditional

Console.WriteLine("=== Query Composition Strategies Example ===\n");

// --------------------------------------------------------------
// 1. Progressive Query Construction
// --------------------------------------------------------------
Console.WriteLine("1. Progressive Query Construction");
Console.WriteLine(new string('-', 40));

string[] names = ["Tom", "Dick", "Harry", "Mary", "Jay"];

var filtered = names.Where(n => n.Contains('a'));
var sorted = filtered.OrderBy(n => n);
var query = sorted.Select(n => n.ToUpper());

Console.WriteLine($"Source: {string.Join(", ", names)}");
Console.WriteLine($"Result: {string.Join(", ", query)}");

// --------------------------------------------------------------
// 2. Conditional Query Construction
// --------------------------------------------------------------
Console.WriteLine("\n2. Conditional Query Construction");
Console.WriteLine(new string('-', 40));

var products = GetProducts();
Console.WriteLine("All Products:");
foreach (var p in products)
{
    Console.WriteLine($"  {p.Name}: ${p.Price}, Stock {p.Stock}");
}

// Simulated user filtering conditions
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

Console.WriteLine($"\nFiltering conditions: Stock>0={onlyInStock}, Price>={minPrice}, Sort={sortByPrice}");
Console.WriteLine("Result:");
foreach (var p in productQuery)
{
    Console.WriteLine($"  {p.Name}: ${p.Price}");
}

Console.WriteLine("\n=== Example End ===");

// ============================================================
// Test Data
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
// Data Model
// ============================================================

public record Product(string Name, decimal Price, int Stock);
