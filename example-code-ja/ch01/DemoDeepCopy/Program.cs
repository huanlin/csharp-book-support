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

// この例で分離できるのは、新しい List を作成し、要素の string が不変だから
var team4 = new Team
{
    Name = team3.Name,
    Members = new List<string>(team3.Members)
};
team4.Members.Add("Charlie");

Console.WriteLine($"team3.Members.Count = {team3.Members.Count}");  // 2（影響なし）
Console.WriteLine($"team4.Members.Count = {team4.Members.Count}");  // 3
Console.WriteLine("結論: この例では、要素が不変な string なので List コンテナーを複製すれば十分");

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
        // string はイミュータブルなので、参照を直接コピーしても可変状態の共有にはならない
        Name = original.Name;

        // 分離を達成するために新しい List コンテナーを作成する
        // 要素は不変な string なので、コンテナーを複製するだけで可変コレクションの共有を回避できる
        Members = new List<string>(original.Members);
    }
}
