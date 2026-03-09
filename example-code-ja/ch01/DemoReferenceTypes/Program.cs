// デモ: 参照型（Reference Type）の参照挙動

Console.WriteLine("=== 参照型の参照挙動 ===");

var user1 = new User { Name = "Alice" };
var user2 = user1;   // 参照をコピー（同じオブジェクトを指す）
user2.Name = "Bob"; // user2 を変更すると user1 にも反映される

Console.WriteLine($"user1.Name = {user1.Name}");  // Bob
Console.WriteLine($"user2.Name = {user2.Name}");  // Bob
Console.WriteLine("結論: user1 と user2 は同じオブジェクトを参照しているため、片方の変更がもう片方に影響する");

Console.WriteLine();
Console.WriteLine("=== ReferenceEquals で確認 ===");
Console.WriteLine($"ReferenceEquals(user1, user2) = {ReferenceEquals(user1, user2)}");  // True

Console.WriteLine();
Console.WriteLine("=== 配列も参照型 ===");

int[] arr1 = { 1, 2, 3 };
int[] arr2 = arr1;  // 参照をコピー
arr2[0] = 999;

Console.WriteLine($"arr1[0] = {arr1[0]}");  // 999（影響あり）
Console.WriteLine("結論: 配列は参照型なので、arr2 = arr1 は参照先アドレスのコピーになる");

// シンプルな class 定義
class User
{
    public string Name { get; set; } = "";
}
