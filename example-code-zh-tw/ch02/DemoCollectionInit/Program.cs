// 示範集合初始設定式

var students = new List<Student>
{
    new Student { Name = "洪波", Birthday = new DateTime(1971, 1, 20) },
    new Student { Name = "黃濤", Birthday = new DateTime(1991, 6, 17) }
};

Console.WriteLine($"學生清單 (共 {students.Count} 人):");
foreach (var s in students)
{
    Console.WriteLine($"  {s.Name}");
}

public class Student
{
    public string Name { get; set; } = "";
    public DateTime Birthday { get; set; }
}
