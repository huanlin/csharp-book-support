// 示範深層複製 (Deep Copy) 的各種方法

Console.WriteLine("=== 深層複製(使用複製建構式) ===");

var team1 = new TeamWithCopyConstructor("Dev", new List<string> { "Alice", "Bob" });

// 使用複製建構式來深層複製
var team2 = new TeamWithCopyConstructor(team1);
team2.Members.Add("Charlie");

Console.WriteLine($"team1.Members.Count = {team1.Members.Count}");  // 2 (不受影響)
Console.WriteLine($"team2.Members.Count = {team2.Members.Count}");  // 3
Console.WriteLine("結論：複製建構式可以明確控制每個成員的複製行為");

Console.WriteLine();
Console.WriteLine("=== 深層複製(手動建立新的 List 容器) ===");

var team3 = new Team
{
    Name = "Dev",
    Members = new List<string> { "Alice", "Bob" }
};

// 這個範例之所以能隔離，是因為建立了新的 List，避免共用同一個 Members 集合
// 由於元素是不可變的 string，只需複製元素參考即可
var team4 = new Team
{
    Name = team3.Name,
    Members = new List<string>(team3.Members) // 建立新的 List，避免共用 Members 集合
};
team4.Members.Add("Charlie");

Console.WriteLine($"team3.Members.Count = {team3.Members.Count}");  // 2 (不受影響)
Console.WriteLine($"team4.Members.Count = {team4.Members.Count}");  // 3
Console.WriteLine("結論：這裡能隔離，是因為為 Members 手動建立了新的 List 實例，而不是因為物件初始化語法本身");

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
        // string 是 immutable，直接複製參考不會導致可變狀態共享
        Name = original.Name;

        // 建立新的 List，避免與原物件共用同一個 Members 集合
        // 因為元素是不可變的 string，只需複製元素參考，不必另外複製字串物件
        Members = new List<string>(original.Members);
    }
}
