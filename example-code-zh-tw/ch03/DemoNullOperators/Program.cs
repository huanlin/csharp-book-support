// 示範 Null 運算子：??、??=、?.、?[]

Console.WriteLine("=== 3.3 Null 運算子 ===\n");

// Null 聯合運算子（??）
Console.WriteLine("【Null 聯合運算子 ??】");
int? x = null;
int? y = x ?? 5;  // y 的結果是 5
Console.WriteLine($"int? x = null; x ?? 5 = {y}");

int? score = null;
int finalScore = score ?? 0;
Console.WriteLine($"score = null; score ?? 0 = {finalScore}");

string? userName = null;
string name = userName ?? "訪客";
Console.WriteLine($"userName = null; userName ?? \"訪客\" = {name}");

// Null 聯合指派運算子（??=）
Console.WriteLine("\n【Null 聯合指派運算子 ??=】");
string? fontName = null;
Console.WriteLine($"fontName 初始值: {fontName ?? "null"}");
fontName ??= "新細明體";
Console.WriteLine($"fontName ??= \"新細明體\" 後: {fontName}");

fontName ??= "Arial";  // 不會生效，因為 fontName 已有值
Console.WriteLine($"fontName ??= \"Arial\" 後: {fontName}（不變，因為已有值）");

// 延遲初始化範例
Console.WriteLine("\n【延遲初始化範例】");
var cache = new LazyCache();
Console.WriteLine($"第一次呼叫 GetData(): {cache.GetData()}");
Console.WriteLine($"第二次呼叫 GetData(): {cache.GetData()}（使用快取）");

// Null 條件運算子（?.）
Console.WriteLine("\n【Null 條件運算子 ?.】");
object? obj = null;
string? converted = obj?.ToString();
Console.WriteLine($"null?.ToString() = {converted ?? "null"}");

obj = 42;
converted = obj?.ToString();
Console.WriteLine($"42?.ToString() = {converted}");

// 搭配索引子（?[]）
Console.WriteLine("\n【Null 條件運算子與索引子 ?[]】");
string? str = null;
char? firstChar = str?[0];
Console.WriteLine($"null?[0] = {firstChar?.ToString() ?? "null"}");

str = "Hello";
firstChar = str?[0];
Console.WriteLine($"\"Hello\"?[0] = {firstChar}");

// 鏈式調用（短路行為）
Console.WriteLine("\n【鏈式調用】");
Customer? customer = new Customer { Name = "Alice", Orders = new List<Order> { new Order { Amount = 100 } } };
int? orderCount = customer?.Orders?.Count;
Console.WriteLine($"customer?.Orders?.Count = {orderCount}");

customer = null;
orderCount = customer?.Orders?.Count;
Console.WriteLine($"(customer = null) customer?.Orders?.Count = {orderCount?.ToString() ?? "null"}");

// 組合運用
Console.WriteLine("\n【組合運用：?. + ??】");
string ConvertToString(object? obj)
{
    return obj?.ToString() ?? "無";
}
Console.WriteLine($"ConvertToString(null) = {ConvertToString(null)}");
Console.WriteLine($"ConvertToString(123) = {ConvertToString(123)}");

// 多層 null 處理
Console.WriteLine("\n【多層 null 處理】");
User? user = new User { Name = "Bob", Profile = null };
string displayName = user?.Profile?.DisplayName ?? user?.Name ?? "匿名";
Console.WriteLine($"user?.Profile?.DisplayName ?? user?.Name ?? \"匿名\" = {displayName}");

// 輔助類別
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
        Console.WriteLine("  (正在從資料庫載入...)");
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
