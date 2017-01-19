using System;
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

            Console.WriteLine("員工薪資總計：" + total);

            DemoAggregate();
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

        // 使用 Aggregate() 來達成 Sum() 的效果（純粹為了方便理解而寫的範例，不是建議你這樣用）。
        static void DemoAggregate()
        {
            Console.Write("Aggregate() 範例的輸出結果：");

            int[] numbers = { 1, 2, 3, 4, 5 };
            int result = numbers.Aggregate((current, next) => current + next);
            // 以下註解說明了上一行程式碼的累計函式的內部運作過程：
            // 1 + 2 = 3
            // 3 + 3 = 6
            // 6 + 4 = 10
            // 10 + 5 = 15
            Console.WriteLine(result); // 輸出 15。
        }

    }
}
