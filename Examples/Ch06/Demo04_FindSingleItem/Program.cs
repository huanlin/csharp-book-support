using System.Linq;
using CommonClasses;

namespace Demo04_FindSingleItem
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee[] employees = Employee.GetEmployees();
            var emp = employees.FirstOrDefault(e => e.Department == "Sales");
            System.Console.WriteLine(emp.Name); // 輸出 "Vivid"

            emp = employees.FirstOrDefault(e => e.Department == "Management");
            System.Console.WriteLine(emp.Name); // 會拋出異常：System.NullReferenceException 

            // 請把上一行程式碼註解掉，以便觀察底下程式碼的執行狀況。
            emp = employees.First(e => e.Department == "Management"); // 這行就會報錯：序列未包含符合的項目。
            System.Console.WriteLine(emp.Name); 

        }
    }
}
