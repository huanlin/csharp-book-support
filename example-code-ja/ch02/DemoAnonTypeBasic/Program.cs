// デモ: 匿名型の基本

var emp = new { Name = "Michael", Birthday = new DateTime(1971, 1, 1) };
Console.WriteLine($"名前: {emp.Name}");
Console.WriteLine($"誕生日: {emp.Birthday:yyyy-MM-dd}");
Console.WriteLine($"実際の型: {emp.GetType()}");

// 型の再利用
var emp1 = new { Name = "Michael", Birthday = new DateTime(1971, 1, 1) };
var emp2 = new { Name = "John", Birthday = new DateTime(1981, 12, 31) };
Console.WriteLine($"\nemp1 と emp2 は同じ型か? {emp1.GetType() == emp2.GetType()}");  // True

// 等価性
var p1 = new { X = 1, Y = 2 };
var p2 = new { X = 1, Y = 2 };
Console.WriteLine($"\np1 == p2: {p1 == p2}");           // False
Console.WriteLine($"p1.Equals(p2): {p1.Equals(p2)}");  // True

// 射影初期化子
var employee = new Employee
{
    Name = "Michael",
    Birthday = new DateTime(1971, 1, 1)
};

var emp3 = new { employee.Name, employee.Birthday };
Console.WriteLine($"\n射影オブジェクトのプロパティ: {emp3.Name}");

string name = "John";
int age = 20;
var emp4 = new { name, age };
Console.WriteLine($"ローカル変数の射影: {emp4.name}, {emp4.age}");

public class Employee
{
    public string Name { get; set; } = "";
    public DateTime Birthday { get; set; }
}
