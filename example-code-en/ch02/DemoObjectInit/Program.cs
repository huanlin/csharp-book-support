// Demo: Basic Object Initializers

var student = new Student
{
    Name = "James Bond",
    Birthday = new DateTime(1971, 1, 20)
};
Console.WriteLine($"Student: {student.Name}, Birthday: {student.Birthday:yyyy-MM-dd}");

// Using with a constructor
var stud2 = new Student("John Rambo") { Birthday = new DateTime(1991, 6, 17) };
Console.WriteLine($"Student: {stud2.Name}, Birthday: {stud2.Birthday:yyyy-MM-dd}");

public class Student
{
    public string Name { get; set; } = "";
    public DateTime Birthday { get; set; }

    public Student() { }
    public Student(string name) { Name = name; }
}
