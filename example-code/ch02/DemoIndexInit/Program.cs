// 示範索引初始設定式 (C# 6+)

var studentsById = new Dictionary<int, Student>
{
    [101] = new Student { Name = "黃濤" },
    [102] = new Student { Name = "藍波" }
};

Console.WriteLine("學生字典:");
foreach (var kvp in studentsById)
{
    Console.WriteLine($"  ID={kvp.Key}, Name={kvp.Value.Name}");
}

public class Student
{
    public string Name { get; set; } = "";
}
