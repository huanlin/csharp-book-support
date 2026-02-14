// Demo: Advanced LINQ Operators (including new features from .NET 6 to .NET 9)

Console.WriteLine("=== Advanced LINQ Operators Example ===\n");

// Prepare test data
var employees = GetEmployees();
var departments = GetDepartments();

// --------------------------------------------------------------
// 1. SelectMany: Flattening Nested Collections
// --------------------------------------------------------------
Console.WriteLine("1. SelectMany: Flattening Nested Collections");
Console.WriteLine(new string('-', 40));

Console.WriteLine("Department Data:");
foreach (var d in departments)
{
    Console.WriteLine($"  {d.Name}: {string.Join(", ", d.Members)}");
}

var allMembers = departments.SelectMany(d => d.Members);
Console.WriteLine($"\nAll Members (flattened): {string.Join(", ", allMembers)}");

// --------------------------------------------------------------
// 2. First / FirstOrDefault / Single
// --------------------------------------------------------------
Console.WriteLine("\n2. First / FirstOrDefault / Single");
Console.WriteLine(new string('-', 40));

var firstSales = employees.First(e => e.Department == "Sales");
Console.WriteLine($"First Sales person: {firstSales.Name}");

var firstCEO = employees.FirstOrDefault(e => e.Title == "CEO");
Console.WriteLine($"First CEO: {firstCEO?.Name ?? "(Does not exist)"}");

var bob = employees.Single(e => e.Name == "Bob");
Console.WriteLine($"The only Bob: {bob.Name}, {bob.Department}");

// --------------------------------------------------------------
// 3. Any / All: Boolean Operations
// --------------------------------------------------------------
Console.WriteLine("\n3. Any / All: Boolean Operations");
Console.WriteLine(new string('-', 40));

var hasHighSalary = employees.Any(e => e.Salary > 70000);
Console.WriteLine($"Is there anyone with a salary over 70k? {(hasHighSalary ? "Yes" : "No")}");

var allAdults = employees.All(e => e.Age >= 18);
Console.WriteLine($"Are all employees adults? {(allAdults ? "Yes" : "No")}");

// --------------------------------------------------------------
// 4. Aggregation & MaxBy / MinBy (.NET 6)
// --------------------------------------------------------------
Console.WriteLine("\n4. Aggregation: Count / Sum / MaxBy (.NET 6)");
Console.WriteLine(new string('-', 40));

Console.WriteLine($"Total Employees: {employees.Count()}");
Console.WriteLine($"Sum of Salaries: ${employees.Sum(e => e.Salary):N0}");

// .NET 6: MaxBy / MinBy
var highestPaid = employees.MaxBy(e => e.Salary);
Console.WriteLine($"Highest Paid Employee (MaxBy): {highestPaid?.Name} (${highestPaid?.Salary:N0})");

var lowestPaid = employees.MinBy(e => e.Salary);
Console.WriteLine($"Lowest Paid Employee (MinBy): {lowestPaid?.Name} (${lowestPaid?.Salary:N0})");

// .NET 6: TryGetNonEnumeratedCount
if (employees.TryGetNonEnumeratedCount(out int count))
{
    Console.WriteLine($"Fast count retrieval (TryGetNonEnumeratedCount): {count}");
}

// --------------------------------------------------------------
// 5. Chunk (.NET 6): Batch Processing
// --------------------------------------------------------------
Console.WriteLine("\n5. Chunk (.NET 6): Batch Processing");
Console.WriteLine(new string('-', 40));

var chunkedEmployees = employees.Chunk(2); // Groups of 2
int chunkIndex = 1;
foreach (var batch in chunkedEmployees)
{
    Console.WriteLine($"Batch {chunkIndex++} ({batch.Length} people): {string.Join(", ", batch.Select(e => e.Name))}");
}

// --------------------------------------------------------------
// 6. Distinct & DistinctBy (.NET 6)
// --------------------------------------------------------------
Console.WriteLine("\n6. Distinct & DistinctBy (.NET 6)");
Console.WriteLine(new string('-', 40));

var depts = employees.Select(e => e.Department).Distinct();
Console.WriteLine($"Unique Departments (Distinct): {string.Join(", ", depts)}");

// .NET 6: DistinctBy
var distinctByDept = employees.DistinctBy(e => e.Department);
Console.WriteLine("Representative for each department (DistinctBy):");
foreach (var emp in distinctByDept)
{
    Console.WriteLine($"  {emp.Department}: {emp.Name}");
}

// --------------------------------------------------------------
// 7. GroupBy & CountBy / AggregateBy (.NET 9)
// --------------------------------------------------------------
Console.WriteLine("\n7. Grouping: GroupBy / CountBy / AggregateBy");
Console.WriteLine(new string('-', 40));

Console.WriteLine("[GroupBy]");
var byDept = employees.GroupBy(e => e.Department);
foreach (var group in byDept)
{
    Console.WriteLine($"  {group.Key} ({group.Count()} people): {string.Join(", ", group.Select(e => e.Name))}");
}

// .NET 9: CountBy
Console.WriteLine("\n[CountBy (.NET 9)]");
foreach (var (deptKey, empCount) in employees.CountBy(e => e.Department))
{
    Console.WriteLine($"  {deptKey}: {empCount} people");
}

// .NET 9: AggregateBy
Console.WriteLine("\n[AggregateBy (.NET 9)]");
var totalSalaryByDept = employees.AggregateBy(
    keySelector: e => e.Department,
    seed: 0m,
    func: (currentTotal, emp) => currentTotal + emp.Salary
);

foreach (var (deptKey, totalSal) in totalSalaryByDept)
{
    Console.WriteLine($"  {deptKey}: ${totalSal:N0}");
}

// --------------------------------------------------------------
// 8. Index (.NET 9) & Zip
// --------------------------------------------------------------
Console.WriteLine("\n8. Index (.NET 9) & Zip");
Console.WriteLine(new string('-', 40));

// .NET 9: Index
Console.WriteLine("[Index]");
foreach (var (index, item) in employees.Take(3).Index())
{
    Console.WriteLine($"  [{index}] {item.Name}");
}

// Zip
Console.WriteLine("\n[Zip]");
string[] names = ["Alice", "Bob", "Carol"];
int[] scores = [85, 92, 78];

var paired = names.Zip(scores, (n, s) => $"{n}: {s} points");
Console.WriteLine($"  Paired results: {string.Join(", ", paired)}");


Console.WriteLine("\n=== Example End ===");

// ============================================================
// Test Data
// ============================================================

static List<Employee> GetEmployees() =>
[
    new("Alice", "Sales", 55000, 28, "Representative"),
    new("Bob", "IT", 72000, 35, "Developer"),
    new("Carol", "Sales", 62000, 42, "Manager"),
    new("Dave", "IT", 58000, 29, "Developer"),
    new("Eve", "HR", 48000, 31, "Specialist"),
    new("Frank", "Sales", 70000, 38, "Representative"),
    new("Grace", "Marketing", 60000, 33, "Specialist")
];

static List<Department> GetDepartments() =>
[
    new("Sales", ["Alice", "Carol", "Frank"]),
    new("IT", ["Bob", "Dave"]),
    new("HR", ["Eve"]),
    new("Marketing", ["Grace"])
];

public record Employee(string Name, string Department, decimal Salary, int Age, string Title);

public record Department(string Name, string[] Members);
