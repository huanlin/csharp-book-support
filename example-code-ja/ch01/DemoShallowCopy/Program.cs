using System.Text;

// デモ: シャローコピーの問題点

Console.WriteLine("=== シャローコピーの問題点 ===");

var team1 = new Team
{
    Name = "Dev",
    Members = new List<string> { "Alice", "Bob" }
};

// シャローコピー
var team2 = team1.ShallowCopy();
team2.Name = "QA";               // Name は独立している
team2.Members.Add("Charlie");    // Members は同じ List を参照している

Console.WriteLine($"team1.Name = {team1.Name}");            // Dev
Console.WriteLine($"team2.Name = {team2.Name}");            // QA
Console.WriteLine($"team1.Members.Count = {team1.Members.Count}");  // 3（影響あり）
Console.WriteLine($"team2.Members.Count = {team2.Members.Count}");  // 3
Console.WriteLine("結論: シャローコピーはオブジェクト本体のみを複製し、参照型フィールドは同じ参照先を共有する");

Console.WriteLine();
Console.WriteLine("=== Array.Clone() はシャローコピー ===");

var original = new StringBuilder[]
{
    new StringBuilder("A"),
    new StringBuilder("B")
};
var copy = (StringBuilder[])original.Clone();
copy[0].Append("!");  // copy の変更が original に反映される

Console.WriteLine($"original[0] = {original[0]}");  // A!（影響あり）
Console.WriteLine("結論: Array.Clone() はシャローコピーなので、参照型要素には注意が必要");

// 明示的な ShallowCopy メソッド
class Team
{
    public string Name { get; set; } = "";
    public List<string> Members { get; set; } = new();

    public Team ShallowCopy() => (Team)MemberwiseClone();
}
