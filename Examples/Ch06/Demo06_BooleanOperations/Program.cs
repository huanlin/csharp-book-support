using System;
using System.Linq;
using CommonClasses;

namespace Demo06_BooleanOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee[] employees = Employee.GetEmployees();

            bool isJohnHere = employees.Any(e => e.Name == "John"); // 傳回 true
            bool isSalaryLeagal = employees.All(e => e.Salary > 20000); // 傳回 true

            Console.WriteLine(isJohnHere);
            Console.WriteLine(isSalaryLeagal);
        }
    }
}
