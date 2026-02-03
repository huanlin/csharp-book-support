// 示範基本自動實作屬性

var emp1 = new EmployeeBasic("E001");
emp1.Name = "張三";
Console.WriteLine($"員工: ID={emp1.ID}, Name={emp1.Name}");

public class EmployeeBasic
{
    public string ID { get; private set; }  // 外界可讀，但不可改變
    public string Name { get; set; } = "";

    public EmployeeBasic(string id)
    {
        ID = id;
    }
}
