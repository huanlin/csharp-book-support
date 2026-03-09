// デモ: コレクション初期化子

var students = new List<Student>
{
    new Student { Name = "James Bond", Birthday = new DateTime(1971, 1, 20) },
    new Student { Name = "Michael Tsai", Birthday = new DateTime(1991, 6, 17) }
};

Console.WriteLine($"学生リスト（合計 {students.Count} 名）:");
foreach (var s in students)
{
    Console.WriteLine($"  {s.Name}");
}

public class Student
{
    public string Name { get; set; } = "";
    public DateTime Birthday { get; set; }
}
