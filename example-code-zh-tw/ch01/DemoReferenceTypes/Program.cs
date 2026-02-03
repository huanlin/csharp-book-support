// 示範參考型別 (Reference Types) 的參考行為

Console.WriteLine("=== 參考型別的參考行為 ===");

var user1 = new User { Name = "Alice" };
var user2 = user1;   // 複製位址，user1 和 user2 指向同一個物件
user2.Name = "Bob";  // 修改 user2 會影響 user1

Console.WriteLine($"user1.Name = {user1.Name}");  // 輸出 Bob
Console.WriteLine($"user2.Name = {user2.Name}");  // 輸出 Bob
Console.WriteLine($"結論：user1 和 user2 指向同一個物件，所以修改其中一個會影響另一個");

Console.WriteLine();
Console.WriteLine("=== ReferenceEquals 驗證 ===");
Console.WriteLine($"ReferenceEquals(user1, user2) = {ReferenceEquals(user1, user2)}");  // True

Console.WriteLine();
Console.WriteLine("=== 陣列也是參考型別 ===");

int[] arr1 = { 1, 2, 3 };
int[] arr2 = arr1;  // 複製位址
arr2[0] = 999;

Console.WriteLine($"arr1[0] = {arr1[0]}");  // 輸出 999（受影響！）
Console.WriteLine($"結論：陣列是參考型別，arr2 = arr1 只是複製參考位址");

// 定義一個簡單的 class
class User
{
    public string Name { get; set; } = "";
}
