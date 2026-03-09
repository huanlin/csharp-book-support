// デモ: オブジェクト初期化子の基本

var student = new Student
{
    Name = "James Bond",
    Birthday = new DateTime(1971, 1, 20)
};
Console.WriteLine($"学生: {student.Name}, 誕生日: {student.Birthday:yyyy-MM-dd}");

// コンストラクターと併用
var stud2 = new Student("John Rambo") { Birthday = new DateTime(1991, 6, 17) };
Console.WriteLine($"学生: {stud2.Name}, 誕生日: {stud2.Birthday:yyyy-MM-dd}");

public class Student
{
    public string Name { get; set; } = "";
    public DateTime Birthday { get; set; }

    public Student() { }
    public Student(string name) { Name = name; }
}
