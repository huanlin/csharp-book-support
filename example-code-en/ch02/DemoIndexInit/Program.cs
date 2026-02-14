// Demo: Index Initializers (C# 6+)

var studentsById = new Dictionary<int, Student>
{
    [101] = new Student { Name = "James Bond" },
    [102] = new Student { Name = "John Rambo" }
};

Console.WriteLine("Student Dictionary:");
foreach (var kvp in studentsById)
{
    Console.WriteLine($"  ID={kvp.Key}, Name={kvp.Value.Name}");
}

public class Student
{
    public string Name { get; set; } = "";
}
