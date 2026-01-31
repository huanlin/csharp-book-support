// 示範可為 Null 的參考型別（Nullable Reference Types, NRT）
#nullable enable
using System.Diagnostics.CodeAnalysis;

Console.WriteLine("=== 3.4 可為 Null 的參考型別 ===\n");

// 核心概念：string vs string?
Console.WriteLine("【Non-nullable vs Nullable Reference Type】");
string nonNullable = "Hello";       // 不可為 null
string? nullable = null;            // 可為 null

Console.WriteLine($"string nonNullable = \"{nonNullable}\" (不可為 null)");
Console.WriteLine($"string? nullable = {nullable ?? "null"} (可為 null)");

// 編譯器流程分析
Console.WriteLine("\n【編譯器流程分析】");
void ProcessWithEarlyReturn(string? input)
{
    if (input == null) 
    {
        Console.WriteLine("  input 是 null，提早返回");
        return;
    }
    
    // 編譯器知道這裡 input 不是 null
    Console.WriteLine($"  input.Length = {input.Length}（無警告）");
}

ProcessWithEarlyReturn(null);
ProcessWithEarlyReturn("Test");

// is null / is not null 模式
Console.WriteLine("\n【is null / is not null 模式】");
void ProcessWithIsPattern(string? input)
{
    if (input is null)
    {
        Console.WriteLine("  input is null");
        return;
    }
    
    // 編譯器知道這裡 input 不是 null
    Console.WriteLine($"  input = \"{input}\"");
}

ProcessWithIsPattern(null);
ProcessWithIsPattern("Pattern matching test");

// Null-forgiving operator (!)
Console.WriteLine("\n【Null-forgiving Operator !】");
string? maybeNull = GetSomething();
// 我們知道 GetSomething 在這個情況下不會返回 null
string definitelyNotNull = maybeNull!;  // 告訴編譯器：我保證不是 null
Console.WriteLine($"maybeNull! = \"{definitelyNotNull}\"");

// API 設計實戰範例
Console.WriteLine("\n【API 設計實戰範例】");
var userService = new UserService();

// GetUser: 不可為 null 的回傳值
try
{
    var user = userService.GetUser(1);
    Console.WriteLine($"GetUser(1): {user.Name}");
}
catch (UserNotFoundException ex)
{
    Console.WriteLine($"GetUser 拋出異常: {ex.Message}");
}

// FindUser: 可為 null 的回傳值
var foundUser = userService.FindUser("alice@example.com");
Console.WriteLine($"FindUser(\"alice@example.com\"): {foundUser?.Name ?? "找不到"}");

var notFoundUser = userService.FindUser("nonexistent@example.com");
Console.WriteLine($"FindUser(\"nonexistent@example.com\"): {notFoundUser?.Name ?? "找不到"}");

// TryGetUser 模式與 [NotNullWhen] 屬性
Console.WriteLine("\n【TryGetUser 模式與 NotNullWhen 屬性】");
if (userService.TryGetUser(1, out var tryUser))
{
    // 編譯器知道這裡 tryUser 不是 null（因為 [NotNullWhen(true)]）
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
    Console.WriteLine("TryGetUser(999) 失敗: 使用者不存在");
}

// 輔助方法
string GetSomething() => "Something";

// 使用者類別
class User
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Nickname { get; set; }  // 允許為 null
}

class UserNotFoundException : Exception
{
    public UserNotFoundException(int id) : base($"找不到使用者 ID: {id}") { }
}

class UserService
{
    private readonly List<User> _users = new()
    {
        new User { Id = 1, Name = "Alice", Nickname = "A" },
        new User { Id = 2, Name = "Bob", Nickname = null }
    };
    
    // 返回值不可為 null：找不到就拋出異常
    public User GetUser(int id)
    {
        var user = _users.Find(u => u.Id == id);
        return user ?? throw new UserNotFoundException(id);
    }
    
    // 返回值可為 null：找不到就返回 null
    public User? FindUser(string email)
    {
        // 簡化範例：用 email 比對 name
        return _users.Find(u => u.Name.ToLower() + "@example.com" == email.ToLower());
    }
    
    // TryGet 模式：使用 [NotNullWhen] 屬性
    public bool TryGetUser(int id, [NotNullWhen(true)] out User? user)
    {
        user = _users.Find(u => u.Id == id);
        return user != null;
    }
}
