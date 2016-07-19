using System;

namespace Demo_ObjectInitializer
{
    class Program
    {
        static void Main(string[] args)
        {
            Student student1 = new Student { Name = "王大同", Birthday = new DateTime(1971, 1, 20) };
            Student student2 = new Student("李得標") { Birthday = new DateTime(1991, 6, 17) };

            Console.WriteLine(student1.Name);
            Console.WriteLine(student2.Name);
        }
    }

    public class Student
    {
        public string Name;
        public DateTime Birthday;

        public Student() { }        // 預設建構子

        public Student(string name) // 另一個建構子
        {
            Name = name;
        }
    }
}
