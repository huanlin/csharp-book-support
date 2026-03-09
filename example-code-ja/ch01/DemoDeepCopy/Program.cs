// デモ: ディープコピーの実装方法

Console.WriteLine("=== ディープコピー（コピーコンストラクター） ===");

var team1 = new TeamWithCopyConstructor("Dev", new List<string> { "Alice", "Bob" });

// コピーコンストラクターでディープコピー
var team2 = new TeamWithCopyConstructor(team1);
team2.Members.Add("Charlie");

Console.WriteLine($"team1.Members.Count = {team1.Members.Count}");  // 2（影響なし）
Console.WriteLine($"team2.Members.Count = {team2.Members.Count}");  // 3
Console.WriteLine("結論: コピーコンストラクターを使うと、各メンバーのコピー方法を明示的に制御できる");

Console.WriteLine();
Console.WriteLine("=== ディープコピー（オブジェクト初期化子） ===");

var team3 = new Team
{
    Name = "Dev",
    Members = new List<string> { "Alice", "Bob" }
};

// ディープコピー: List を新規生成
var team4 = new Team
{
    Name = team3.Name,
    Members = new List<string>(team3.Members)
};
team4.Members.Add("Charlie");

Console.WriteLine($"team3.Members.Count = {team3.Members.Count}");  // 2（影響なし）
Console.WriteLine($"team4.Members.Count = {team4.Members.Count}");  // 3
Console.WriteLine("結論: ディープコピーでは、ネストした参照型メンバーまで個別に複製する必要がある");

// シンプルな class（初期化子デモ用）
class Team
{
    public string Name { get; set; } = "";
    public List<string> Members { get; set; } = new();
}

// コピーコンストラクターでディープコピーを実装
class TeamWithCopyConstructor
{
    public string Name { get; set; }
    public List<string> Members { get; set; }

    // 通常コンストラクター
    public TeamWithCopyConstructor(string name, List<string> members)
    {
        Name = name;
        Members = members;
    }

    // コピーコンストラクター
    public TeamWithCopyConstructor(TeamWithCopyConstructor original)
    {
        Name = original.Name;  // string はイミュータブルなのでそのままでよい
        Members = new List<string>(original.Members);  // ディープコピー
    }
}
