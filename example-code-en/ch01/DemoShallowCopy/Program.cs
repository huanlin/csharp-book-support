using System.Text;

// Demo: Issues with Shallow Copy

Console.WriteLine("=== Issues with Shallow Copy ===");

var team1 = new Team
{
    Name = "Dev",
    Members = new List<string> { "Alice", "Bob" }
};

// Shallow Copy
var team2 = team1.ShallowCopy();
team2.Name = "QA";               // team2 has its own copy of Name
team2.Members.Add("Charlie");    // But Members still points to the same List!

Console.WriteLine($"team1.Name = {team1.Name}");            // Dev
Console.WriteLine($"team2.Name = {team2.Name}");            // QA
Console.WriteLine($"team1.Members.Count = {team1.Members.Count}");  // 3 (Affected!)
Console.WriteLine($"team2.Members.Count = {team2.Members.Count}");  // 3
Console.WriteLine("Conclusion: Shallow copy only copies the object itself; reference type fields still point to the same object");

Console.WriteLine();
Console.WriteLine("=== Array.Clone() is a Shallow Copy ===");

var original = new StringBuilder[]
{
    new StringBuilder("A"),
    new StringBuilder("B")
};
var copy = (StringBuilder[])original.Clone();
copy[0].Append("!");  // Modifying copy affects original!

Console.WriteLine($"original[0] = {original[0]}");  // A! (Affected!)
Console.WriteLine("Conclusion: Array.Clone() is a shallow copy; be careful with reference type elements");

// Provide an explicit ShallowCopy method
class Team
{
    public string Name { get; set; } = "";
    public List<string> Members { get; set; } = new();

    public Team ShallowCopy() => (Team)MemberwiseClone();
}
