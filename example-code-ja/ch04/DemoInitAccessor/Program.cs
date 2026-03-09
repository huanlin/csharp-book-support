// デモ: init アクセサーと required 修飾子

Console.WriteLine("=== init アクセサー ===");
var person = new Person { Name = "Alice", Age = 30 };
Console.WriteLine($"Name: {person.Name}, Age: {person.Age}");

// 初期化後の変更はコンパイルエラー
// person.Name = "Bob";  // ✗ init プロパティは初期化時のみ設定可能

Console.WriteLine();
Console.WriteLine("=== required 修飾子 ===");
// Name は必須
var employee = new Employee { Name = "Bob" };  // ✓ Age は任意
Console.WriteLine($"Employee: {employee.Name}, Age: {employee.Age}");

// Name 未設定だとコンパイルエラー
// var invalid = new Employee { Age = 30 };  // ✗ Name は required

// init アクセサーを使用
public class Person
{
    public string Name { get; init; } = "";
    public int Age { get; init; }
}

// required 修飾子（C# 11）を使用
public class Employee
{
    public required string Name { get; init; }
    public int Age { get; init; }  // 任意
}
