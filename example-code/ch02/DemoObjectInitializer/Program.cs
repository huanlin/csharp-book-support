// 示範物件初始設定式、集合初始設定式、索引初始設定式

// 物件初始設定式
var student = new Student
{
    Name = "洪波",
    Birthday = new DateTime(1971, 1, 20)
};
Console.WriteLine($"學生: {student.Name}, 生日: {student.Birthday:yyyy-MM-dd}");

// 搭配建構子使用
var stud2 = new Student("黃濤") { Birthday = new DateTime(1991, 6, 17) };
Console.WriteLine($"學生: {stud2.Name}, 生日: {stud2.Birthday:yyyy-MM-dd}");

// 集合初始設定式
var students = new List<Student>
{
    new Student { Name = "洪波", Birthday = new DateTime(1971, 1, 20) },
    new Student { Name = "黃濤", Birthday = new DateTime(1991, 6, 17) }
};

Console.WriteLine($"\n學生清單 (共 {students.Count} 人):");
foreach (var s in students)
{
    Console.WriteLine($"  {s.Name}");
}

// 索引初始設定式 (C# 6+)
var studentsById = new Dictionary<int, Student>
{
    [101] = new Student { Name = "黃濤" },
    [102] = new Student { Name = "藍波" }
};

Console.WriteLine("\n學生字典:");
foreach (var kvp in studentsById)
{
    Console.WriteLine($"  ID={kvp.Key}, Name={kvp.Value.Name}");
}

// 類別定義
public class Student
{
    public string Name { get; set; } = "";
    public DateTime Birthday { get; set; }

    public Student() { }
    public Student(string name) { Name = name; }
}
