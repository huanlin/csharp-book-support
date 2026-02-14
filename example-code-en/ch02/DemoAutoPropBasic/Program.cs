// Demo: Basic Auto-Implemented Properties

var emp1 = new EmployeeBasic("E001");
emp1.Name = "James Bond";
Console.WriteLine($"Employee: ID={emp1.ID}, Name={emp1.Name}");

public class EmployeeBasic
{
    public string ID { get; private set; }  // Readable but not writable from outside
    public string Name { get; set; } = "";

    public EmployeeBasic(string id)
    {
        ID = id;
    }
}
