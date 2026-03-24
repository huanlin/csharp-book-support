// Demo: Methods for Deep Copy

Console.WriteLine("=== Deep Copy (Using Copy Constructor) ===");

var team1 = new TeamWithCopyConstructor("Dev", new List<string> { "Alice", "Bob" });

// Use a copy constructor for deep copy
var team2 = new TeamWithCopyConstructor(team1);
team2.Members.Add("Charlie");

Console.WriteLine($"team1.Members.Count = {team1.Members.Count}");  // 2 (Not affected)
Console.WriteLine($"team2.Members.Count = {team2.Members.Count}");  // 3
Console.WriteLine("Conclusion: Copy constructors allow precise control over the copy behavior of each member");

Console.WriteLine();
Console.WriteLine("=== Deep Copy (Using Object Initializer Syntax) ===");

var team3 = new Team
{
    Name = "Dev",
    Members = new List<string> { "Alice", "Bob" }
};

// This achieves isolation here because we create a new List and the elements are immutable strings
var team4 = new Team
{
    Name = team3.Name,
    Members = new List<string>(team3.Members) // Create a new List
};
team4.Members.Add("Charlie");

Console.WriteLine($"team3.Members.Count = {team3.Members.Count}");  // 2 (Not affected)
Console.WriteLine($"team4.Members.Count = {team4.Members.Count}");  // 3
Console.WriteLine("Conclusion: In this example, copying the List container is enough because the elements are immutable strings");

// Simple class (used for object initializer syntax demo)
class Team
{
    public string Name { get; set; } = "";
    public List<string> Members { get; set; } = new();
}

// Implement deep copy using a copy constructor
class TeamWithCopyConstructor
{
    public string Name { get; set; }
    public List<string> Members { get; set; }

    // Standard constructor
    public TeamWithCopyConstructor(string name, List<string> members)
    {
        Name = name;
        Members = members;
    }

    // Copy constructor
    public TeamWithCopyConstructor(TeamWithCopyConstructor original)
    {
        // string is immutable, copying the reference does not lead to shared mutable state
        Name = original.Name;

        // Create a new List container to achieve isolation (new List<string>(original.Members)).
        // Since the elements are immutable strings, copying the container prevents sharing a mutable collection.
        Members = new List<string>(original.Members);
    }
}
