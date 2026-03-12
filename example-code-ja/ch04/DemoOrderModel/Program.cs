// デモ: イミュータブルなドメインモデル
using System.Collections.Immutable;

var order = new Order(
    OrderId: 1,
    CustomerName: "Alice",
    Items: ImmutableList.Create(
        new OrderItem("Keyboard", 1, 2500)),
    CreatedAt: DateTime.Now
);

Console.WriteLine("=== 元の注文 ===");
Console.WriteLine($"注文 ID: {order.OrderId}");
Console.WriteLine($"顧客: {order.CustomerName}");
foreach (var item in order.Items)
{
    Console.WriteLine($"  - {item.ProductName} x {item.Quantity} = {item.TotalPrice:C}");
}
Console.WriteLine($"合計金額: {order.TotalAmount:C}");

// 注文を「変更」する（実際は新しい注文を生成）
var updatedOrder = order.AddItem(new OrderItem("Mouse", 2, 800));

Console.WriteLine();
Console.WriteLine("=== 更新後の注文 ===");
Console.WriteLine($"注文 ID: {updatedOrder.OrderId}");
Console.WriteLine($"顧客: {updatedOrder.CustomerName}");
foreach (var item in updatedOrder.Items)
{
    Console.WriteLine($"  - {item.ProductName} x {item.Quantity} = {item.TotalPrice:C}");
}
Console.WriteLine($"合計金額: {updatedOrder.TotalAmount:C}");

Console.WriteLine();
Console.WriteLine("=== 元の注文は変更されない ===");
Console.WriteLine($"元注文の金額: {order.TotalAmount:C}");
Console.WriteLine($"元注文の件数: {order.Items.Count}");

// record でイミュータブルな注文明細を定義
public record OrderItem(string ProductName, int Quantity, decimal UnitPrice)
{
    public decimal TotalPrice => Quantity * UnitPrice;
}

// record でイミュータブルな注文を定義
public record Order(
    int OrderId,
    string CustomerName,
    ImmutableList<OrderItem> Items,
    DateTime CreatedAt)
{
    public decimal TotalAmount => Items.Sum(item => item.TotalPrice);

    // with 式で注文を「変更」
    public Order AddItem(OrderItem item)
    {
        return this with { Items = Items.Add(item) };
    }
}
