using System;

namespace CommonClasses
{
    public class Employee
    {
        public string Name;
        public DateTime Birthday;
        public string Department;
        public int Salary;

        public static Employee[] GetEmployees()
        {
            return new Employee[]
            {
                new Employee { Name = "Michael", Department = "RD",    Salary = 59000 },
                new Employee { Name = "Ben",     Department = "RD",    Salary = 73000 },
                new Employee { Name = "Vivid",   Department = "Sales", Salary = 26500 },
                new Employee { Name = "John",    Department = "Sales", Salary = 26500 },
                new Employee { Name = "Sam",     Department = "HR",    Salary = 88000 }
            };
        }
    }


}
