namespace CommonClasses
{
    public class Employee
    {
        public int ID;
        public string Name;
        public string City;
        public string Department;
        public int Salary;

        public static Employee[] GetEmployees()
        {
            return new Employee[]
            {
                new Employee { ID=1, Name="Mike",  City="台北", Department="RD",    Salary=59000 },
                new Employee { ID=2, Name="Ben",   City="台北", Department="RD",    Salary=73000 },
                new Employee { ID=3, Name="Vivid", City="紐約", Department="Sales", Salary=26500 },
                new Employee { ID=4, Name="John",  City="台北", Department="Sales", Salary=26500 },
                new Employee { ID=5, Name="Sam",   City="紐約", Department="HR",    Salary=88000 },
                new Employee { ID=6, Name="Ken",   City="紐約", Department="IT",    Salary=66000 }
            };
        }
    }


}
