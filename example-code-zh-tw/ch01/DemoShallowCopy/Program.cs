using System.Text;

// 示範淺層複製 (Shallow Copy) vs 深層複製 (Deep Copy)

Console.WriteLine("=== 淺層複製的問題 ===");

var team1 = new Team
{
    Name = "Dev",
    Members = new List<string> { "Alice", "Bob" }
};

// 淺層複製
var team2 = (Team)team1.Clone();
team2.Name = "QA";               // team2 有自己的 Name 副本
team2.Members.Add("Charlie");    // 但 Members 仍指向同一個 List！

Console.WriteLine($"team1.Name = {team1.Name}");            // Dev
Console.WriteLine($"team2.Name = {team2.Name}");            // QA
Console.WriteLine($"team1.Members.Count = {team1.Members.Count}");  // 3 (受影響！)
Console.WriteLine($"team2.Members.Count = {team2.Members.Count}");  // 3
Console.WriteLine("結論：淺層複製只複製物件本身，內部的參考型別欄位仍指向同一個物件");

Console.WriteLine();
Console.WriteLine("=== 深層複製 ===");

var team3 = new Team
{
    Name = "Dev",
    Members = new List<string> { "Alice", "Bob" }
};

// 深層複製：手動建立新的 List
var team4 = new Team
{
    Name = team3.Name,
    Members = new List<string>(team3.Members) // 建立新的 List
};
team4.Members.Add("Charlie");

Console.WriteLine($"team3.Members.Count = {team3.Members.Count}");  // 2 (不受影響)
Console.WriteLine($"team4.Members.Count = {team4.Members.Count}");  // 3
Console.WriteLine("結論：深層複製會遞迴複製所有參考型別成員");

Console.WriteLine();
Console.WriteLine("=== 陣列的 Clone() 是淺層複製 ===");

var original = new StringBuilder[]
{
    new StringBuilder("A"),
    new StringBuilder("B")
};
var copy = (StringBuilder[])original.Clone();
copy[0].Append("!");  // 修改 copy 會影響 original！

Console.WriteLine($"original[0] = {original[0]}");  // A! (受影響！)
Console.WriteLine("結論：Array.Clone() 是淺層複製，對參考型別元素要特別注意");

// 實作 ICloneable 的類別（淺層複製）
class Team : ICloneable
{
    public string Name { get; set; } = "";
    public List<string> Members { get; set; } = new();

    public object Clone()
    {
        // MemberwiseClone 執行淺層複製
        return MemberwiseClone();
    }
}
