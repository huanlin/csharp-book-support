// Demo: Immutable Domain Model

var order = new Order(
    OrderId: 1,
    CustomerName: "Alice",
    Items: new List<OrderItem>
    {
        new OrderItem("Keyboard", 1, 2500)
    },
    CreatedAt: DateTime.Now
);

Console.WriteLine("=== Original Order ===");
Console.WriteLine($"Order ID: {order.OrderId}");
Console.WriteLine($"Customer: {order.CustomerName}");
foreach (var item in order.Items)
{
    Console.WriteLine($"  - {item.ProductName} x {item.Quantity} = {item.TotalPrice:C}");
}
Console.WriteLine($"Total Amount: {order.TotalAmount:C}");

// "Modifying" the order (actually creating a new order)
var updatedOrder = order.AddItem(new OrderItem("Mouse", 2, 800));

Console.WriteLine();
Console.WriteLine("=== Updated Order ===");
Console.WriteLine($"Order ID: {updatedOrder.OrderId}");
Console.WriteLine($"Customer: {updatedOrder.CustomerName}");
foreach (var item in updatedOrder.Items)
{
    Console.WriteLine($"  - {item.ProductName} x {item.Quantity} = {item.TotalPrice:C}");
}
Console.WriteLine($"Total Amount: {updatedOrder.TotalAmount:C}");

Console.WriteLine();
Console.WriteLine("=== Original Order Remains Unchanged ===");
Console.WriteLine($"Original Order Amount: {order.TotalAmount:C}");
Console.WriteLine($"Original Order Item Count: {order.Items.Count}");

// Using record to define an immutable order item
public record OrderItem(string ProductName, int Quantity, decimal UnitPrice)
{
    public decimal TotalPrice => Quantity * UnitPrice;
}

// Using record to define an immutable order
public record Order(
    int OrderId,
    string CustomerName,
    IReadOnlyList<OrderItem> Items,
    DateTime CreatedAt)
{
    public decimal TotalAmount => Items.Sum(item => item.TotalPrice);

    // Use with expression to "modify" the order
    public Order AddItem(OrderItem item)
    {
        var newItems = Items.Append(item).ToList();
        return this with { Items = newItems };
    }
}
