// 示範深層複製 (Deep Copy) 的各種方法

Console.WriteLine("=== 深層複製(使用複製建構式) ===");

var team1 = new TeamWithCopyConstructor("Dev", new List<string> { "Alice", "Bob" });

// 使用複製建構式來深層複製
var team2 = new TeamWithCopyConstructor(team1);
team2.Members.Add("Charlie");

Console.WriteLine($"team1.Members.Count = {team1.Members.Count}");  // 2 (不受影響)
Console.WriteLine($"team2.Members.Count = {team2.Members.Count}");  // 3
Console.WriteLine("結論:複製建構式可以明確控制每個成員的複製行為");

Console.WriteLine();
Console.WriteLine("=== 深層複製(使用物件初始化語法) ===");

var team3 = new Team
{
    Name = "Dev",
    Members = new List<string> { "Alice", "Bob" }
};

// 深層複製:手動建立新的 List
var team4 = new Team
{
    Name = team3.Name,
    Members = new List<string>(team3.Members) // 建立新的 List
};
team4.Members.Add("Charlie");

Console.WriteLine($"team3.Members.Count = {team3.Members.Count}");  // 2 (不受影響)
Console.WriteLine($"team4.Members.Count = {team4.Members.Count}");  // 3
Console.WriteLine("結論:深層複製會深入內層複製所有參考型別成員");

// 簡單的類別(用於物件初始化語法示範)
class Team
{
    public string Name { get; set; } = "";
    public List<string> Members { get; set; } = new();
}

// 使用複製建構式實作深層複製
class TeamWithCopyConstructor
{
    public string Name { get; set; }
    public List<string> Members { get; set; }

    // 一般建構式
    public TeamWithCopyConstructor(string name, List<string> members)
    {
        Name = name;
        Members = members;
    }

    // 複製建構式(copy constructor)
    public TeamWithCopyConstructor(TeamWithCopyConstructor original)
    {
        Name = original.Name;  // string 是 immutable,淺層複製即可
        Members = new List<string>(original.Members);  // 深層複製
    }
}
