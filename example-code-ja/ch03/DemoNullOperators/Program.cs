// デモ: Null 演算子 ??, ??=, ?., ?[]

Console.WriteLine("=== 3.3 Null Operators ===\n");

// null 合体演算子（??）
Console.WriteLine("[null 合体演算子 ??]");
int? x = null;
int y = x ?? 5;
Console.WriteLine($"int? x = null; x ?? 5 = {y}");

int? score = null;
int finalScore = score ?? 0;
Console.WriteLine($"score = null; score ?? 0 = {finalScore}");

string? userName = null;
string name = userName ?? "Guest";
Console.WriteLine($"userName = null; userName ?? \"Guest\" = {name}");

// null 合体代入演算子（??=）
Console.WriteLine("\n[null 合体代入演算子 ??=]");
string? fontName = null;
Console.WriteLine($"fontName 初期値: {fontName ?? "null"}");
fontName ??= "MS Gothic";
Console.WriteLine($"fontName ??= \"MS Gothic\" 後: {fontName}");

fontName ??= "Arial";  // 既に値があるので変化しない
Console.WriteLine($"fontName ??= \"Arial\" 後: {fontName}（既に値があるため変化なし）");

// 遅延初期化の例
Console.WriteLine("\n[遅延初期化の例]");
var cache = new LazyCache();
Console.WriteLine($"GetData() 1回目: {string.Join(", ", cache.GetData())}");
Console.WriteLine($"GetData() 2回目: {string.Join(", ", cache.GetData())}（キャッシュを使用）");

// null 条件演算子（?.）
Console.WriteLine("\n[null 条件演算子 ?.]");
object? obj = null;
string? converted = obj?.ToString();
Console.WriteLine($"null?.ToString() = {converted ?? "null"}");

obj = 42;
converted = obj?.ToString();
Console.WriteLine($"obj?.ToString() = {converted}（この時点で obj = 42）");

// インデクサ付き（?[]）
Console.WriteLine("\n[インデクサ付き null 条件演算子 ?[]]");
string? str = null;
char? firstChar = str?[0];
Console.WriteLine($"null?[0] = {firstChar?.ToString() ?? "null"}");

str = "Hello";
firstChar = str?[0];
Console.WriteLine($"\"Hello\"?[0] = {firstChar}");

Console.WriteLine("空文字列に対して str?[0] を使うと、依然としてインデックス範囲外例外が発生し得ます。");
Console.WriteLine($"FirstCharOrNull(\"\") = {FirstCharOrNull("")?.ToString() ?? "null"}");

// チェーン呼び出し（短絡評価）
Console.WriteLine("\n[チェーン呼び出し]");
Customer? customer = new Customer { Name = "Alice", Orders = new List<Order> { new Order { Amount = 100 } } };
int? orderCount = customer?.Orders?.Count;
Console.WriteLine($"customer?.Orders?.Count = {orderCount}");

customer = null;
orderCount = customer?.Orders?.Count;
Console.WriteLine($"(customer = null) customer?.Orders?.Count = {orderCount?.ToString() ?? "null"}");

// 組み合わせ利用
Console.WriteLine("\n[組み合わせ利用: ?. + ??]");
string ConvertToString(object? obj)
{
    return obj?.ToString() ?? "None";
}
Console.WriteLine($"ConvertToString(null) = {ConvertToString(null)}");
Console.WriteLine($"ConvertToString(123) = {ConvertToString(123)}");

// 多段 null ハンドリング
Console.WriteLine("\n[多段 null ハンドリング]");
User? user = new User { Name = "Bob", Profile = null };
string displayName = user?.Profile?.DisplayName ?? user?.Name ?? "Anonymous";
Console.WriteLine($"user?.Profile?.DisplayName ?? user?.Name ?? \"Anonymous\" = {displayName}");

// null 条件代入演算子（C# 14）
Console.WriteLine("\n[null 条件代入演算子]");
int currentOrderFactoryCalls = 0;
Customer? nullableCustomer = null;
nullableCustomer?.CurrentOrder = GetCurrentOrder();
Console.WriteLine($"customer が null のときの GetCurrentOrder 呼び出し回数 = {currentOrderFactoryCalls}");

nullableCustomer = new Customer { Name = "Carol" };
nullableCustomer?.CurrentOrder = GetCurrentOrder();
Console.WriteLine($"customer が null でないときの GetCurrentOrder 呼び出し回数 = {currentOrderFactoryCalls}");
nullableCustomer?.Points += 100;
if (nullableCustomer is Customer ensuredCustomer)
{
    Console.WriteLine($"nullableCustomer.CurrentOrder?.Amount = {ensuredCustomer.CurrentOrder?.Amount}");
    Console.WriteLine($"nullableCustomer.Points = {ensuredCustomer.Points}");
}

Order GetCurrentOrder()
{
    currentOrderFactoryCalls++;
    Console.WriteLine("  GetCurrentOrder() が呼び出されました");
    return new Order { Amount = 250 };
}

char? FirstCharOrNull(string? value)
{
    return string.IsNullOrEmpty(value) ? null : value[0];
}

// 補助クラス
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
        Console.WriteLine("  （データベースから読み込み中...）");
        return new List<string> { "Item1", "Item2", "Item3" };
    }
}

class Customer
{
    public string Name { get; set; } = "";
    public List<Order>? Orders { get; set; }
    public Order? CurrentOrder { get; set; }
    public int Points { get; set; }
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
