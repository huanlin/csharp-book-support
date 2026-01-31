// 示範基本物件初始設定式

var student = new Student
{
    Name = "洪波",
    Birthday = new DateTime(1971, 1, 20)
};
Console.WriteLine($"學生: {student.Name}, 生日: {student.Birthday:yyyy-MM-dd}");

// 搭配建構子使用
var stud2 = new Student("黃濤") { Birthday = new DateTime(1991, 6, 17) };
Console.WriteLine($"學生: {stud2.Name}, 生日: {stud2.Birthday:yyyy-MM-dd}");

public class Student
{
    public string Name { get; set; } = "";
    public DateTime Birthday { get; set; }

    public Student() { }
    public Student(string name) { Name = name; }
}
