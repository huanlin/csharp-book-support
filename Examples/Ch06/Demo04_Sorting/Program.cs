using System;
using System.Linq;
using CommonClasses;

namespace Demo04_Sorting
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee[] employees = Employee.GetEmployees();

            // 多欄位排序的錯誤寫法：
            // var query = employees.OrderBy(e => e.Name).OrderBy(e => e.Salary); 

            // 多欄位排序的正確寫法：
            var query = employees.OrderBy(e => e.Salary).ThenBy(e => e.Name);

            foreach ( var emp in query)
            {
                Console.WriteLine($"{emp.Name, -6} : {emp.Salary, 0:C0}");
            }
        }
    }
}
