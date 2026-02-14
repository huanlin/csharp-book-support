// Demo: Nullable Reference Types (NRT)
#nullable enable
using System.Diagnostics.CodeAnalysis;

Console.WriteLine("=== 3.4 Nullable Reference Types ===\n");

// Core Concept: string vs string?
Console.WriteLine("[Non-nullable vs Nullable Reference Type]");
string nonNullable = "Hello";       // Cannot be null
string? nullable = null;            // Can be null

Console.WriteLine($"string nonNullable = \"{nonNullable}\" (cannot be null)");
Console.WriteLine($"string? nullable = {nullable ?? "null"} (can be null)");

// Compiler Flow Analysis
Console.WriteLine("\n[Compiler Flow Analysis]");
void ProcessWithEarlyReturn(string? input)
{
    if (input == null) 
    {
        Console.WriteLine("  input is null, returning early");
        return;
    }
    
    // Compiler knows input is not null here
    Console.WriteLine($"  input.Length = {input.Length} (no warning)");
}

ProcessWithEarlyReturn(null);
ProcessWithEarlyReturn("Test");

// is null / is not null Pattern
Console.WriteLine("\n[is null / is not null Pattern]");
void ProcessWithIsPattern(string? input)
{
    if (input is null)
    {
        Console.WriteLine("  input is null");
        return;
    }
    
    // Compiler knows input is not null here
    Console.WriteLine($"  input = \"{input}\"");
}

ProcessWithIsPattern(null);
ProcessWithIsPattern("Pattern matching test");

// Null-forgiving operator (!)
Console.WriteLine("\n[Null-forgiving Operator !]");
string? maybeNull = GetSomething();
// We know GetSomething won't return null in this case
string definitelyNotNull = maybeNull!;  // Tell the compiler: I guarantee it's not null
Console.WriteLine($"maybeNull! = \"{definitelyNotNull}\"");

// API Design Real-world Example
Console.WriteLine("\n[API Design Real-world Example]");
var userService = new UserService();

// GetUser: Non-nullable return value
try
{
    var user = userService.GetUser(1);
    Console.WriteLine($"GetUser(1): {user.Name}");
}
catch (UserNotFoundException ex)
{
    Console.WriteLine($"GetUser threw an exception: {ex.Message}");
}

// FindUser: Nullable return value
var foundUser = userService.FindUser("alice@example.com");
Console.WriteLine($"FindUser(\"alice@example.com\"): {foundUser?.Name ?? "Not found"}");

var notFoundUser = userService.FindUser("nonexistent@example.com");
Console.WriteLine($"FindUser(\"nonexistent@example.com\"): {notFoundUser?.Name ?? "Not found"}");

// TryGetUser Pattern and [NotNullWhen] Attribute
Console.WriteLine("\n[TryGetUser Pattern and NotNullWhen Attribute]");
if (userService.TryGetUser(1, out var tryUser))
{
    // Compiler knows tryUser is not null here (due to [NotNullWhen(true)])
    Console.WriteLine($"TryGetUser(1) Success: {tryUser.Name}");
}
else
{
    Console.WriteLine("TryGetUser(1) Failed");
}

if (userService.TryGetUser(999, out var notFoundTryUser))
{
    Console.WriteLine($"TryGetUser(999) Success: {notFoundTryUser.Name}");
}
else
{
    Console.WriteLine("TryGetUser(999) Failed: User does not exist");
}

// Helper method
string GetSomething() => "Something";

// User class
class User
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Nickname { get; set; }  // Allowed to be null
}

class UserNotFoundException : Exception
{
    public UserNotFoundException(int id) : base($"Could not find user ID: {id}") { }
}

class UserService
{
    private readonly List<User> _users = new()
    {
        new User { Id = 1, Name = "Alice", Nickname = "A" },
        new User { Id = 2, Name = "Bob", Nickname = null }
    };
    
    // Return value cannot be null; throws exception if not found
    public User GetUser(int id)
    {
        var user = _users.Find(u => u.Id == id);
        return user ?? throw new UserNotFoundException(id);
    }
    
    // Return value can be null; returns null if not found
    public User? FindUser(string email)
    {
        // Simplified example: matching name with email
        return _users.Find(u => u.Name.ToLower() + "@example.com" == email.ToLower());
    }
    
    // TryGet pattern: using [NotNullWhen] attribute
    public bool TryGetUser(int id, [NotNullWhen(true)] out User? user)
    {
        user = _users.Find(u => u.Id == id);
        return user != null;
    }
}
