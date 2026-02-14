// Demo: Basic Anonymous Types

var emp = new { Name = "Michael", Birthday = new DateTime(1971, 1, 1) };
Console.WriteLine($"Name: {emp.Name}");
Console.WriteLine($"Birthday: {emp.Birthday:yyyy-MM-dd}");
Console.WriteLine($"Actual Type: {emp.GetType()}");

// Type Reuse
var emp1 = new { Name = "Michael", Birthday = new DateTime(1971, 1, 1) };
var emp2 = new { Name = "John", Birthday = new DateTime(1981, 12, 31) };
Console.WriteLine($"\nAre emp1 and emp2 the same type? {emp1.GetType() == emp2.GetType()}");  // True

// Equality
var p1 = new { X = 1, Y = 2 };
var p2 = new { X = 1, Y = 2 };
Console.WriteLine($"\np1 == p2: {p1 == p2}");           // False
Console.WriteLine($"p1.Equals(p2): {p1.Equals(p2)}");  // True

// Projection Initializers
var employee = new Employee
{
    Name = "Michael",
    Birthday = new DateTime(1971, 1, 1)
};

var emp3 = new { employee.Name, employee.Birthday };
Console.WriteLine($"\nProjected object property: {emp3.Name}");

string name = "John";
int age = 20;
var emp4 = new { name, age };
Console.WriteLine($"Projected local variables: {emp4.name}, {emp4.age}");

public class Employee
{
    public string Name { get; set; } = "";
    public DateTime Birthday { get; set; }
}
