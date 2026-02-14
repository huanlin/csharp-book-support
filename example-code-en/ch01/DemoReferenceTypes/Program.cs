// Demo: Reference behavior of Reference Types

Console.WriteLine("=== Reference behavior of Reference Types ===");

var user1 = new User { Name = "Alice" };
var user2 = user1;   // Copy the reference; user1 and user2 point to the same object
user2.Name = "Bob";  // Modifying user2 affects user1

Console.WriteLine($"user1.Name = {user1.Name}");  // Output Bob
Console.WriteLine($"user2.Name = {user2.Name}");  // Output Bob
Console.WriteLine($"Conclusion: user1 and user2 point to the same object, so modifying one affects the other");

Console.WriteLine();
Console.WriteLine("=== Verifying with ReferenceEquals ===");
Console.WriteLine($"ReferenceEquals(user1, user2) = {ReferenceEquals(user1, user2)}");  // True

Console.WriteLine();
Console.WriteLine("=== Arrays are also Reference Types ===");

int[] arr1 = { 1, 2, 3 };
int[] arr2 = arr1;  // Copy the reference
arr2[0] = 999;

Console.WriteLine($"arr1[0] = {arr1[0]}");  // Output 999 (Affected!)
Console.WriteLine($"Conclusion: Arrays are Reference Types; arr2 = arr1 only copies the reference address");

// 定義一個簡單的 class
class User
{
    public string Name { get; set; } = "";
}
