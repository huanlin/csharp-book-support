// デモ: Nullable Reference Types（NRT）
#nullable enable
using System.Diagnostics.CodeAnalysis;

Console.WriteLine("=== 3.4 Nullable Reference Types ===\n");

// 基本概念: string と string?
Console.WriteLine("[非 Nullable 参照型 vs Nullable 参照型]");
string nonNullable = "Hello";       // null 不可
string? nullable = null;             // null 可

Console.WriteLine($"string nonNullable = \"{nonNullable}\"（null 不可）");
Console.WriteLine($"string? nullable = {nullable ?? "null"}（null 可）");

// コンパイラのフロー解析
Console.WriteLine("\n[コンパイラのフロー解析]");
void ProcessWithEarlyReturn(string? input)
{
    if (input == null)
    {
        Console.WriteLine("  input は null のため早期 return");
        return;
    }

    // ここでは input が null でないと判定される
    Console.WriteLine($"  input.Length = {input.Length}（警告なし）");
}

ProcessWithEarlyReturn(null);
ProcessWithEarlyReturn("Test");

// is null / is not null パターン
Console.WriteLine("\n[is null / is not null パターン]");
void ProcessWithIsPattern(string? input)
{
    if (input is null)
    {
        Console.WriteLine("  input は null");
        return;
    }

    // ここでは input が null でないと判定される
    Console.WriteLine($"  input = \"{input}\"");
}

ProcessWithIsPattern(null);
ProcessWithIsPattern("Pattern matching test");

// null 許容抑制演算子 (!)
Console.WriteLine("\n[null 許容抑制演算子 !]");
string? maybeNull = GetSomething();
// このケースでは null にならないと分かっている
string definitelyNotNull = maybeNull!;  // コンパイラに「null ではない」と伝える
Console.WriteLine($"maybeNull! = \"{definitelyNotNull}\"");

// API 設計の実例
Console.WriteLine("\n[API 設計の実例]");
var userService = new UserService();

// GetUser: 非 null を返す
try
{
    var user = userService.GetUser(1);
    Console.WriteLine($"GetUser(1): {user.Name}");
}
catch (UserNotFoundException ex)
{
    Console.WriteLine($"GetUser は例外を送出: {ex.Message}");
}

// FindUser: null を返しうる
var foundUser = userService.FindUser("alice@example.com");
Console.WriteLine($"FindUser(\"alice@example.com\"): {foundUser?.Name ?? "見つかりません"}");

var notFoundUser = userService.FindUser("nonexistent@example.com");
Console.WriteLine($"FindUser(\"nonexistent@example.com\"): {notFoundUser?.Name ?? "見つかりません"}");

// TryGetUser パターンと [NotNullWhen] 属性
Console.WriteLine("\n[TryGetUser パターンと NotNullWhen 属性]");
if (userService.TryGetUser(1, out var tryUser))
{
    // [NotNullWhen(true)] により tryUser は非 null と判定される
    Console.WriteLine($"TryGetUser(1) 成功: {tryUser.Name}");
}
else
{
    Console.WriteLine("TryGetUser(1) 失敗");
}

if (userService.TryGetUser(999, out var notFoundTryUser))
{
    Console.WriteLine($"TryGetUser(999) 成功: {notFoundTryUser.Name}");
}
else
{
    Console.WriteLine("TryGetUser(999) 失敗: ユーザーが存在しません");
}

// ヘルパーメソッド
string GetSomething() => "Something";

// User クラス
class User
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Nickname { get; set; }  // null 許容
}

class UserNotFoundException : Exception
{
    public UserNotFoundException(int id) : base($"ユーザー ID が見つかりません: {id}") { }
}

class UserService
{
    private readonly List<User> _users = new()
    {
        new User { Id = 1, Name = "Alice", Nickname = "A" },
        new User { Id = 2, Name = "Bob", Nickname = null }
    };

    // 戻り値は null 不可。見つからない場合は例外
    public User GetUser(int id)
    {
        var user = _users.Find(u => u.Id == id);
        return user ?? throw new UserNotFoundException(id);
    }

    // 戻り値は null 可。見つからない場合は null
    public User? FindUser(string email)
    {
        // 簡易例: 名前からメールアドレスを作って比較
        return _users.Find(u => u.Name.ToLower() + "@example.com" == email.ToLower());
    }

    // TryGet パターン: [NotNullWhen] 属性を利用
    public bool TryGetUser(int id, [NotNullWhen(true)] out User? user)
    {
        user = _users.Find(u => u.Id == id);
        return user != null;
    }
}
