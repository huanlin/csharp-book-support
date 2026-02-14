// Demo: Pattern Matching and Null
#nullable enable

Console.WriteLine("=== 3.5 Pattern Matching and Null ===\n");

// is null and is not null
Console.WriteLine("[is null and is not null]");
User? user1 = null;
User? user2 = new User { Name = "Alice", IsVip = true };

// Traditional approach
if (user1 == null) Console.WriteLine("user1 == null: true (traditional approach)");

// Modern approach (C# 9+)
if (user1 is null) Console.WriteLine("user1 is null: true (modern approach)");
if (user2 is not null) Console.WriteLine("user2 is not null: true (modern approach)");

// is not null is safer than != null because it won't be overridden by custom == operators
Console.WriteLine("\n[Advantage: is null is unaffected by custom == operators]");
var customObj = new CustomEquality();
Console.WriteLine($"customObj == null: {customObj == null} (custom == may return true)");
Console.WriteLine($"customObj is null: {customObj is null} (is null is always correct)");

// Switch Expression and Null
Console.WriteLine("\n[Switch Expression and Null]");
string GetDisplayName(User? user) => user switch
{
    null => "Guest",
    { IsVip: true } => $"VIP: {user.Name}",
    _ => user.Name
};

Console.WriteLine($"GetDisplayName(null) = {GetDisplayName(null)}");
Console.WriteLine($"GetDisplayName(VIP user) = {GetDisplayName(user2)}");
Console.WriteLine($"GetDisplayName(normal user) = {GetDisplayName(new User { Name = "Bob", IsVip = false })}");

// Property Pattern and Null
Console.WriteLine("\n[Property Pattern and Null]");
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
    Address = null  // Address is null
};

void CheckCustomer(Customer? customer, string label)
{
    // Property pattern automatically handles multi-level null checks
    if (customer is { Address.City: "Taipei", Age: > 18 })
    {
        Console.WriteLine($"{label}: Match (Taipei, Age > 18)");
    }
    else
    {
        Console.WriteLine($"{label}: No match");
    }
}

CheckCustomer(customer1, "customer1 (Charlie, 25, Taipei)");
CheckCustomer(customer2, "customer2 (Dave, 16, Kaohsiung)");
CheckCustomer(customer3, "customer3 (null)");
CheckCustomer(customer4, "customer4 (Eve, 20, Address=null)");

// Practical Combination Example
Console.WriteLine("\n[Practical Combination Example]");
string ProcessOrder(Order? order) => order switch
{
    null => "No Order",
    { Status: OrderStatus.Cancelled } => "Order Cancelled",
    { Items.Count: 0 } => "Empty Order",
    { TotalAmount: > 10000 } => $"Large Order: {order.TotalAmount:C}",
    _ => $"Normal Order: {order.TotalAmount:C}"
};

Console.WriteLine($"ProcessOrder(null) = {ProcessOrder(null)}");
Console.WriteLine($"ProcessOrder(Cancelled) = {ProcessOrder(new Order { Status = OrderStatus.Cancelled })}");
Console.WriteLine($"ProcessOrder(Empty) = {ProcessOrder(new Order { Status = OrderStatus.Active, Items = new List<string>() })}");
Console.WriteLine($"ProcessOrder(Large) = {ProcessOrder(new Order { Status = OrderStatus.Active, Items = new List<string> { "A" }, TotalAmount = 15000 })}");
Console.WriteLine($"ProcessOrder(Normal) = {ProcessOrder(new Order { Status = OrderStatus.Active, Items = new List<string> { "B" }, TotalAmount = 500 })}");

// Helper Classes
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

// Class with custom == operator to demonstrate the advantage of 'is null'
class CustomEquality
{
    public static bool operator ==(CustomEquality? a, CustomEquality? b)
    {
        // Purposefully return true to simulate a buggy implementation
        return true;
    }
    
    public static bool operator !=(CustomEquality? a, CustomEquality? b)
    {
        return !(a == b);
    }
    
    public override bool Equals(object? obj) => true;
    public override int GetHashCode() => 0;
}
