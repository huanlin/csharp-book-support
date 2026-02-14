// Demo: init accessor and required modifier

Console.WriteLine("=== init Accessor ===");
var person = new Person { Name = "Alice", Age = 30 };
Console.WriteLine($"Name: {person.Name}, Age: {person.Age}");

// Attempting to modify afterwards results in a compilation error
// person.Name = "Bob";  // ✗ Compilation error: init properties can only be set during initialization

Console.WriteLine();
Console.WriteLine("=== required Modifier ===");
// Must provide Name
var employee = new Employee { Name = "Bob" };  // ✓ Correct, Age is optional
Console.WriteLine($"Employee: {employee.Name}, Age: {employee.Age}");

// Compilation error if Name is not provided
// var invalid = new Employee { Age = 30 };  // ✗ Compilation error: Name is required

// Using init accessor
public class Person
{
    public string Name { get; init; } = "";
    public int Age { get; init; }
}

// Using required modifier (C# 11)
public class Employee
{
    public required string Name { get; init; }
    public int Age { get; init; }  // Optional
}
