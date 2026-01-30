// 示範匿名型別

// 基本匿名型別
var emp = new { Name = "Michael", Birthday = new DateTime(1971, 1, 1) };
Console.WriteLine($"Name: {emp.Name}");
Console.WriteLine($"Birthday: {emp.Birthday:yyyy-MM-dd}");
Console.WriteLine($"實際型別: {emp.GetType()}");

// 型別重用：相同屬性個數與名稱會重用型別
var emp1 = new { Name = "Michael", Birthday = new DateTime(1971, 1, 1) };
var emp2 = new { Name = "John", Birthday = new DateTime(1981, 12, 31) };
Console.WriteLine($"\nemp1 和 emp2 是相同型別? {emp1.GetType() == emp2.GetType()}");  // True

// 值相等性
var p1 = new { X = 1, Y = 2 };
var p2 = new { X = 1, Y = 2 };
Console.WriteLine($"\np1 == p2: {p1 == p2}");           // False (不同參考)
Console.WriteLine($"p1.Equals(p2): {p1.Equals(p2)}");  // True  (屬性值相同)

// 投射初始設定式
var employee = new Employee
{
    Name = "Michael",
    Birthday = new DateTime(1971, 1, 1)
};

// 從物件屬性投射
var emp3 = new { employee.Name, employee.Birthday };
Console.WriteLine($"\n投射物件屬性: {emp3.Name}");

// 從區域變數投射
string name = "John";
int age = 20;
var emp4 = new { name, age };
Console.WriteLine($"投射區域變數: {emp4.name}, {emp4.age}");

// 非破壞性修改 (C# 10+)
var a1 = new { A = 1, B = 2, C = 3 };
var a2 = a1 with { B = 99 };  // 複製 a1，但將 B 改為 99
Console.WriteLine($"\n原始: A={a1.A}, B={a1.B}, C={a1.C}");
Console.WriteLine($"修改後: A={a2.A}, B={a2.B}, C={a2.C}");

// 輔助類別
public class Employee
{
    public string Name { get; set; } = "";
    public DateTime Birthday { get; set; }
}
