using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnonymousTypeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo1();
            Demo2();
        }

        static void Demo1()
        {
            Console.WriteLine("\nDemo1 : 匿名型別基本寫法。");
            var emp = new { Name = "Michael", Birthday = new DateTime(1971, 1, 1) };
            Console.WriteLine("Employee name: " + emp.Name);
            Console.WriteLine("Birthday: " + emp.Birthday.ToString());
            Console.WriteLine("實際的型別是: " + emp.GetType());
        }

        static void Demo2()
        {
            Console.WriteLine("\nDemo2 : 示範 projection initializers 的匿名型別寫法。");

            EmpInfo emp = new EmpInfo()
            {
                Name = "Michael",
                Birthday = new DateTime(1971, 1, 1)
            };

            var emp1 = new { emp.Name, emp.Birthday };

            string name = "John";
            //int age = 20;
            DateTime age = DateTime.Now;

            var emp2 = new { name, age };

            Console.WriteLine("emp1.Name = " + emp1.Name);
            Console.WriteLine("emp2.name = " + emp2.name);

            Console.WriteLine("emp1 的實際型別: " + emp1.GetType());
            Console.WriteLine("emp2 的實際型別: " + emp2.GetType());
        }
    }

    public class EmpInfo
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    }
}
