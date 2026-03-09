// デモ: 自動実装プロパティの基本

var emp1 = new EmployeeBasic("E001");
emp1.Name = "James Bond";
Console.WriteLine($"社員: ID={emp1.ID}, 名前={emp1.Name}");

public class EmployeeBasic
{
    public string ID { get; private set; }  // 外部からは読み取りのみ
    public string Name { get; set; } = "";

    public EmployeeBasic(string id)
    {
        ID = id;
    }
}
