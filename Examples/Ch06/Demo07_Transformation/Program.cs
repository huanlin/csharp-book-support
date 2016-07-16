using System.Collections.Generic;
using System.Linq;
using CommonClasses;

namespace Demo07_Transformation
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee[] employees = Employee.GetEmployees();

        }

        static List<string> GetEmployeeNames_OldStyle(IEnumerable<Employee> employees)
        {
            var empNames = new List<string>();
            
            foreach (var emp in employees)
            {
                empNames.Add(emp.Name);
            }
            return empNames;
        }

        static List<string> GetEmployeeNames_LinqStyle(IEnumerable<Employee> employees)
        {
            /* 還沒有完全善用 LINQ API 的版本
            var empNames = new List<string>();

            foreach (var name in employees.Select(e => e.Name)) // 經由 Select 的轉換之後，得到一個新的集合，集合中的每個元素都是 string。
            {
                empNames.Add(name);
            }
            return empNames;
            */
    
            return employees.Select(e => e.Name).ToList(); // 完全善用 LINQ API，一行就搞定。
        }
        static Dictionary<string, int> GetEmployeeNameAndSalary(IEnumerable<Employee> employees)
        {
            return employees.ToDictionary(e => e.Name, e => e.Salary); 
        }

    }
}
