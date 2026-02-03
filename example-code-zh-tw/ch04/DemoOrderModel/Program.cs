// 示範不可變的領域模型

var order = new Order(
    OrderId: 1,
    CustomerName: "Alice",
    Items: new List<OrderItem>
    {
        new OrderItem("Keyboard", 1, 2500)
    },
    CreatedAt: DateTime.Now
);

Console.WriteLine("=== 原始訂單 ===");
Console.WriteLine($"訂單編號: {order.OrderId}");
Console.WriteLine($"客戶: {order.CustomerName}");
foreach (var item in order.Items)
{
    Console.WriteLine($"  - {item.ProductName} x {item.Quantity} = {item.TotalPrice:C}");
}
Console.WriteLine($"總金額: {order.TotalAmount:C}");

// 「修改」訂單（實際上是建立新訂單）
var updatedOrder = order.AddItem(new OrderItem("Mouse", 2, 800));

Console.WriteLine();
Console.WriteLine("=== 更新後訂單 ===");
Console.WriteLine($"訂單編號: {updatedOrder.OrderId}");
Console.WriteLine($"客戶: {updatedOrder.CustomerName}");
foreach (var item in updatedOrder.Items)
{
    Console.WriteLine($"  - {item.ProductName} x {item.Quantity} = {item.TotalPrice:C}");
}
Console.WriteLine($"總金額: {updatedOrder.TotalAmount:C}");

Console.WriteLine();
Console.WriteLine("=== 原始訂單不變 ===");
Console.WriteLine($"原始訂單金額: {order.TotalAmount:C}");
Console.WriteLine($"原始訂單項目數: {order.Items.Count}");

// 使用 record 定義不可變的訂單項目
public record OrderItem(string ProductName, int Quantity, decimal UnitPrice)
{
    public decimal TotalPrice => Quantity * UnitPrice;
}

// 使用 record 定義不可變的訂單
public record Order(
    int OrderId,
    string CustomerName,
    IReadOnlyList<OrderItem> Items,
    DateTime CreatedAt)
{
    public decimal TotalAmount => Items.Sum(item => item.TotalPrice);

    // 使用 with 表達式來「修改」訂單
    public Order AddItem(OrderItem item)
    {
        var newItems = Items.Append(item).ToList();
        return this with { Items = newItems };
    }
}
