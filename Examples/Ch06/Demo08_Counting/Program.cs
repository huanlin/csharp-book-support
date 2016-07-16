using System.Linq;
using CommonClasses;

namespace Demo08_Counting
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = CountEmployeesWithHighSalary();
            int total = GetEmployeeSalaryTotal();
        }

        static int CountEmployeesWithHighSalary()
        {
            Employee[] employees = Employee.GetEmployees();

            /* 未使用 LINQ 的寫法
            int count = 0;
            foreach (var emp in employees)
            {
                if (emp.Salary >= 60000)
                {
                    count++;
                }
            }
            return count;
            */

            // 使用 LINQ
            return employees.Count(e => e.Salary >= 60000);
        }

        static int GetEmployeeSalaryTotal()
        {
            Employee[] employees = Employee.GetEmployees();

            /* 未使用 LINQ 的寫法
            int salaryTotal = 0;
            foreach (var emp in employees)
            {
                salaryTotal += emp.Salary;
            }
            return salaryTotal;
            */

            // 使用 LINQ
            return employees.Sum(e => e.Salary);
        }

    }
}
