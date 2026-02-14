// Demo: Null Operators: ??, ??=, ?., ?[]

Console.WriteLine("=== 3.3 Null Operators ===\n");

// Null Coalescing Operator (??)
Console.WriteLine("[Null Coalescing Operator ??]");
int? x = null;
int? y = x ?? 5;  // y's result is 5
Console.WriteLine($"int? x = null; x ?? 5 = {y}");

int? score = null;
int finalScore = score ?? 0;
Console.WriteLine($"score = null; score ?? 0 = {finalScore}");

string? userName = null;
string name = userName ?? "Guest";
Console.WriteLine($"userName = null; userName ?? \"Guest\" = {name}");

// Null Coalescing Assignment Operator (??=)
Console.WriteLine("\n[Null Coalescing Assignment Operator ??=]");
string? fontName = null;
Console.WriteLine($"fontName initial value: {fontName ?? "null"}");
fontName ??= "MS Gothic";
Console.WriteLine($"fontName ??= \"MS Gothic\" After: {fontName}");

fontName ??= "Arial";  // Won't take effect since fontName already has a value
Console.WriteLine($"fontName ??= \"Arial\" After: {fontName} (Unchanged, because it already has a value)");

// Lazy Initialization Example
Console.WriteLine("\n[Lazy Initialization Example]");
var cache = new LazyCache();
Console.WriteLine($"First call to GetData(): {cache.GetData()}");
Console.WriteLine($"Second call to GetData(): {cache.GetData()} (Using cache)");

// Null-conditional Operator (?.)
Console.WriteLine("\n[Null-conditional Operator ?.]");
object? obj = null;
string? converted = obj?.ToString();
Console.WriteLine($"null?.ToString() = {converted ?? "null"}");

obj = 42;
converted = obj?.ToString();
Console.WriteLine($"42?.ToString() = {converted}");

// With Indexer (?[])
Console.WriteLine("\n[Null-conditional Operator with Indexer ?[]]");
string? str = null;
char? firstChar = str?[0];
Console.WriteLine($"null?[0] = {firstChar?.ToString() ?? "null"}");

str = "Hello";
firstChar = str?[0];
Console.WriteLine($"\"Hello\"?[0] = {firstChar}");

// Chain Calling (Short-circuiting behavior)
Console.WriteLine("\n[Chain Calling]");
Customer? customer = new Customer { Name = "Alice", Orders = new List<Order> { new Order { Amount = 100 } } };
int? orderCount = customer?.Orders?.Count;
Console.WriteLine($"customer?.Orders?.Count = {orderCount}");

customer = null;
orderCount = customer?.Orders?.Count;
Console.WriteLine($"(customer = null) customer?.Orders?.Count = {orderCount?.ToString() ?? "null"}");

// Combined Usage
Console.WriteLine("\n[Combined Usage: ?. + ??]");
string ConvertToString(object? obj)
{
    return obj?.ToString() ?? "None";
}
Console.WriteLine($"ConvertToString(null) = {ConvertToString(null)}");
Console.WriteLine($"ConvertToString(123) = {ConvertToString(123)}");

// Multi-level Null Handling
Console.WriteLine("\n[Multi-level Null Handling]");
User? user = new User { Name = "Bob", Profile = null };
string displayName = user?.Profile?.DisplayName ?? user?.Name ?? "Anonymous";
Console.WriteLine($"user?.Profile?.DisplayName ?? user?.Name ?? \"Anonymous\" = {displayName}");

// Helper Classes
class LazyCache
{
    private List<string>? _cache;
    
    public List<string> GetData()
    {
        _cache ??= LoadFromDatabase();
        return _cache;
    }
    
    private List<string> LoadFromDatabase()
    {
        Console.WriteLine("  (Loading from database...)");
        return new List<string> { "Item1", "Item2", "Item3" };
    }
}

class Customer
{
    public string Name { get; set; } = "";
    public List<Order>? Orders { get; set; }
}

class Order
{
    public decimal Amount { get; set; }
}

class User
{
    public string? Name { get; set; }
    public Profile? Profile { get; set; }
}

class Profile
{
    public string? DisplayName { get; set; }
}
