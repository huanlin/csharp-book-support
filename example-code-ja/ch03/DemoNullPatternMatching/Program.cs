// デモ: パターンマッチングと null
#nullable enable

Console.WriteLine("=== 3.5 Pattern Matching and Null ===\n");

// is null / is not null
Console.WriteLine("[is null と is not null]");
User? user1 = null;
User? user2 = new User { Name = "Alice", IsVip = true };

// 従来の書き方
if (user1 == null) Console.WriteLine("user1 == null: true（従来書式）");

// モダンな書き方
if (user1 is null) Console.WriteLine("user1 is null: true（モダン書式）");
if (user2 is not null) Console.WriteLine("user2 is not null: true（モダン書式）");

// is null はカスタム == 演算子の影響を受けない
Console.WriteLine("\n[利点: is null はカスタム == の影響を受けない]");
var customObj = new CustomEquality();
Console.WriteLine($"customObj == null: {customObj == null}（カスタム == は true を返しうる）");
Console.WriteLine($"customObj is null: {customObj is null}（is null は常に正しい）");

// Switch 式と null
Console.WriteLine("\n[Switch 式と null]");
string GetDisplayName(User? user) => user switch
{
    null => "Guest",
    { IsVip: true } => $"VIP: {user.Name}",
    _ => user.Name
};

Console.WriteLine($"GetDisplayName(null) = {GetDisplayName(null)}");
Console.WriteLine($"GetDisplayName(VIP user) = {GetDisplayName(user2)}");
Console.WriteLine($"GetDisplayName(normal user) = {GetDisplayName(new User { Name = "Bob", IsVip = false })}");

// プロパティパターンと null
Console.WriteLine("\n[プロパティパターンと null]");
Customer? customer1 = new Customer
{
    Name = "Charlie",
    Age = 25,
    Address = new Address { City = "Taipei" }
};
Customer? customer2 = new Customer
{
    Name = "Dave",
    Age = 16,
    Address = new Address { City = "Kaohsiung" }
};
Customer? customer3 = null;
Customer? customer4 = new Customer
{
    Name = "Eve",
    Age = 20,
    Address = null
};

void CheckCustomer(Customer? customer, string label)
{
    // プロパティパターンは多段 null チェックを自動で処理
    if (customer is { Address.City: "Taipei", Age: > 18 })
    {
        Console.WriteLine($"{label}: 一致（Taipei, Age > 18）");
    }
    else
    {
        Console.WriteLine($"{label}: 不一致");
    }
}

CheckCustomer(customer1, "customer1 (Charlie, 25, Taipei)");
CheckCustomer(customer2, "customer2 (Dave, 16, Kaohsiung)");
CheckCustomer(customer3, "customer3 (null)");
CheckCustomer(customer4, "customer4 (Eve, 20, Address=null)");

// 実践的な組み合わせ例
Console.WriteLine("\n[実践的な組み合わせ例]");
string ProcessOrder(Order? order) => order switch
{
    null => "注文なし",
    { Status: OrderStatus.Cancelled } => "注文キャンセル",
    { Items.Count: 0 } => "空の注文",
    { TotalAmount: > 10000 } => $"高額注文: {order.TotalAmount:C}",
    _ => $"通常注文: {order.TotalAmount:C}"
};

Console.WriteLine($"ProcessOrder(null) = {ProcessOrder(null)}");
Console.WriteLine($"ProcessOrder(Cancelled) = {ProcessOrder(new Order { Status = OrderStatus.Cancelled })}");
Console.WriteLine($"ProcessOrder(Empty) = {ProcessOrder(new Order { Status = OrderStatus.Active, Items = new List<string>() })}");
Console.WriteLine($"ProcessOrder(Large) = {ProcessOrder(new Order { Status = OrderStatus.Active, Items = new List<string> { "A" }, TotalAmount = 15000 })}");
Console.WriteLine($"ProcessOrder(Normal) = {ProcessOrder(new Order { Status = OrderStatus.Active, Items = new List<string> { "B" }, TotalAmount = 500 })}");

// 補助クラス
class User
{
    public string Name { get; set; } = "";
    public bool IsVip { get; set; }
}

class Customer
{
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public Address? Address { get; set; }
}

class Address
{
    public string City { get; set; } = "";
}

class Order
{
    public OrderStatus Status { get; set; }
    public List<string> Items { get; set; } = new();
    public decimal TotalAmount { get; set; }
}

enum OrderStatus
{
    Active,
    Cancelled
}

// is null の利点説明用: カスタム == を持つクラス
class CustomEquality
{
    public static bool operator ==(CustomEquality? a, CustomEquality? b)
    {
        // わざと true を返す（不正実装の想定）
        return true;
    }

    public static bool operator !=(CustomEquality? a, CustomEquality? b)
    {
        return !(a == b);
    }

    public override bool Equals(object? obj) => true;
    public override int GetHashCode() => 0;
}
