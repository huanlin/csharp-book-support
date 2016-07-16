using System.Linq;
using CommonClasses;

namespace Demo03_Filter
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee[] employees = Employee.GetEmployees();

            // 找出 Sales 部門的員工，並輸出姓名
            foreach(var emp in employees.Where(e => e.Department == "Sales"))
            {
                System.Console.WriteLine(emp.Name);
            }

            System.Console.WriteLine("=== 使用 Distinct() 來移除重複的元素 ===");
            foreach (var department in employees.Select(e => e.Department).Distinct())
            {
                System.Console.WriteLine(department);
            }
        }


    }
}
