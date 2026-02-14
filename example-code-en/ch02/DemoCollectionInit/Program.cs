// Demo: Collection Initializers

var students = new List<Student>
{
    new Student { Name = "James Bond", Birthday = new DateTime(1971, 1, 20) },
    new Student { Name = "Michael Tsai", Birthday = new DateTime(1991, 6, 17) }
};

Console.WriteLine($"Student List (Total {students.Count} people):");
foreach (var s in students)
{
    Console.WriteLine($"  {s.Name}");
}

public class Student
{
    public string Name { get; set; } = "";
    public DateTime Birthday { get; set; }
}
