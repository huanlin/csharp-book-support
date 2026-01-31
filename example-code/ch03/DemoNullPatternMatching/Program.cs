// 示範 Pattern Matching 與 Null
#nullable enable

Console.WriteLine("=== 3.5 Pattern Matching 與 Null ===\n");

// is null 與 is not null
Console.WriteLine("【is null 與 is not null】");
User? user1 = null;
User? user2 = new User { Name = "Alice", IsVip = true };

// 傳統寫法
if (user1 == null) Console.WriteLine("user1 == null: true（傳統寫法）");

// 現代寫法（C# 9+）
if (user1 is null) Console.WriteLine("user1 is null: true（現代寫法）");
if (user2 is not null) Console.WriteLine("user2 is not null: true（現代寫法）");

// is not null 比 != null 更安全，因為不會被自訂的 == 運算子覆寫
Console.WriteLine("\n【優點：is null 不受自訂 == 運算子影響】");
var customObj = new CustomEquality();
Console.WriteLine($"customObj == null: {customObj == null}（自訂 == 可能回傳 true）");
Console.WriteLine($"customObj is null: {customObj is null}（is null 永遠正確）");

// Switch Expression 與 Null
Console.WriteLine("\n【Switch Expression 與 Null】");
string GetDisplayName(User? user) => user switch
{
    null => "訪客",
    { IsVip: true } => $"VIP: {user.Name}",
    _ => user.Name
};

Console.WriteLine($"GetDisplayName(null) = {GetDisplayName(null)}");
Console.WriteLine($"GetDisplayName(VIP user) = {GetDisplayName(user2)}");
Console.WriteLine($"GetDisplayName(normal user) = {GetDisplayName(new User { Name = "Bob", IsVip = false })}");

// Property Pattern 與 Null
Console.WriteLine("\n【Property Pattern 與 Null】");
Customer? customer1 = new Customer 
{ 
    Name = "Charlie", 
    Age = 25, 
    Address = new Address { City = "台北" } 
};
Customer? customer2 = new Customer 
{ 
    Name = "Dave", 
    Age = 16, 
    Address = new Address { City = "高雄" } 
};
Customer? customer3 = null;
Customer? customer4 = new Customer 
{ 
    Name = "Eve", 
    Age = 20, 
    Address = null  // Address 為 null
};

void CheckCustomer(Customer? customer, string label)
{
    // Property pattern 自動處理多層 null 檢查
    if (customer is { Address.City: "台北", Age: > 18 })
    {
        Console.WriteLine($"{label}: 符合條件（台北、年齡>18）");
    }
    else
    {
        Console.WriteLine($"{label}: 不符合條件");
    }
}

CheckCustomer(customer1, "customer1 (Charlie, 25, 台北)");
CheckCustomer(customer2, "customer2 (Dave, 16, 高雄)");
CheckCustomer(customer3, "customer3 (null)");
CheckCustomer(customer4, "customer4 (Eve, 20, Address=null)");

// 實用組合範例
Console.WriteLine("\n【實用組合範例】");
string ProcessOrder(Order? order) => order switch
{
    null => "無訂單",
    { Status: OrderStatus.Cancelled } => "訂單已取消",
    { Items.Count: 0 } => "空訂單",
    { TotalAmount: > 10000 } => $"大額訂單：{order.TotalAmount:C}",
    _ => $"一般訂單：{order.TotalAmount:C}"
};

Console.WriteLine($"ProcessOrder(null) = {ProcessOrder(null)}");
Console.WriteLine($"ProcessOrder(已取消) = {ProcessOrder(new Order { Status = OrderStatus.Cancelled })}");
Console.WriteLine($"ProcessOrder(空訂單) = {ProcessOrder(new Order { Status = OrderStatus.Active, Items = new List<string>() })}");
Console.WriteLine($"ProcessOrder(大額) = {ProcessOrder(new Order { Status = OrderStatus.Active, Items = new List<string> { "A" }, TotalAmount = 15000 })}");
Console.WriteLine($"ProcessOrder(一般) = {ProcessOrder(new Order { Status = OrderStatus.Active, Items = new List<string> { "B" }, TotalAmount = 500 })}");

// 輔助類別
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

// 自訂 == 運算子的類別，用於展示 is null 的優勢
class CustomEquality
{
    public static bool operator ==(CustomEquality? a, CustomEquality? b)
    {
        // 故意返回 true，模擬錯誤的實作
        return true;
    }
    
    public static bool operator !=(CustomEquality? a, CustomEquality? b)
    {
        return !(a == b);
    }
    
    public override bool Equals(object? obj) => true;
    public override int GetHashCode() => 0;
}
