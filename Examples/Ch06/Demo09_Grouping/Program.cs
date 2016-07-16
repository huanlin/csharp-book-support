using System;
using System.Linq;
using CommonClasses;

namespace Demo09_Grouping
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("<<< GroupBySingleKey >>>");
            GroupBySingleKey();
            Console.WriteLine("<<< GroupByMultipleKey >>>");
            GroupByMultipleKey();
        }

        static void GroupBySingleKey()
        {
            Employee[] employees = Employee.GetEmployees();

            var groups = employees.GroupBy(e => e.Department); // 依部門分組

            foreach (var group in groups)
            {
                Console.WriteLine($"部門: {group.Key}");
                foreach (var emp in group)
                {
                    Console.WriteLine(emp.Name);
                }
                Console.WriteLine(string.Concat(Enumerable.Repeat("-", 20)));
            }
        }

        static void GroupByMultipleKey()
        {
            Employee[] employees = Employee.GetEmployees();

            var groups = employees.GroupBy(e => new { e.City, e.Department }); // 依「城市+部門」分組

            foreach (var group in groups)
            {
                Console.WriteLine($"群組: {group.Key}");
                foreach (var emp in group)
                {
                    Console.WriteLine(emp.Name);
                }
                Console.WriteLine(string.Concat(Enumerable.Repeat("-", 20)));
            }
        }

    }
}
