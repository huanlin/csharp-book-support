// 示範 init 存取子與 required 修飾詞

Console.WriteLine("=== init 存取子 ===");
var person = new Person { Name = "Alice", Age = 30 };
Console.WriteLine($"Name: {person.Name}, Age: {person.Age}");

// 嘗試之後修改會編譯錯誤
// person.Name = "Bob";  // ✗ 編譯錯誤：init 屬性只能在初始化時設定

Console.WriteLine();
Console.WriteLine("=== required 修飾詞 ===");
// 必須提供 Name
var employee = new Employee { Name = "Bob" };  // ✓ 正確，Age 是非必要的
Console.WriteLine($"Employee: {employee.Name}, Age: {employee.Age}");

// 不提供 Name 會編譯錯誤
// var invalid = new Employee { Age = 30 };  // ✗ 編譯錯誤：Name 是必要的

// 使用 init 存取子
public class Person
{
    public string Name { get; init; } = "";
    public int Age { get; init; }
}

// 使用 required 修飾詞（C# 11）
public class Employee
{
    public required string Name { get; init; }
    public int Age { get; init; }  // 非必要
}
