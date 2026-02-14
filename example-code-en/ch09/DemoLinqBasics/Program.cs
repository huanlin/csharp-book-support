// Demo: Basic LINQ Operators

Console.WriteLine("=== Basic LINQ Operators Example ===\n");

// Prepare test data
var employees = GetEmployees();

Console.WriteLine("Employee Data:");
foreach (var e in employees)
{
    Console.WriteLine($"  {e.Name}, {e.Department}, ${e.Salary:N0}, {e.Age} years old");
}

// --------------------------------------------------------------
// 1. Where: Filtering
// --------------------------------------------------------------
Console.WriteLine("\n1. Where: Filtering");
Console.WriteLine(new string('-', 40));

var salesPeople = employees.Where(e => e.Department == "Sales");
Console.WriteLine("Sales Department Employees:");
foreach (var e in salesPeople)
{
    Console.WriteLine($"  {e.Name}");
}

// --------------------------------------------------------------
// 2. Select: Projection
// --------------------------------------------------------------
Console.WriteLine("\n2. Select: Projection");
Console.WriteLine(new string('-', 40));

var names = employees.Select(e => e.Name);
Console.WriteLine($"All Employee Names: {string.Join(", ", names)}");

var summaries = employees.Select(e => new
{
    e.Name,
    e.Salary,
    Tax = e.Salary * 0.05m
});
Console.WriteLine("\nSalary and Tax Summary:");
foreach (var s in summaries)
{
    Console.WriteLine($"  {s.Name}: Salary ${s.Salary:N0}, Tax ${s.Tax:N0}");
}

// --------------------------------------------------------------
// 3. OrderBy / ThenBy: Sorting
// --------------------------------------------------------------
Console.WriteLine("\n3. OrderBy / ThenBy: Sorting");
Console.WriteLine(new string('-', 40));

var sorted = employees
    .OrderBy(e => e.Department)
    .ThenByDescending(e => e.Salary);

Console.WriteLine("Sorted by Department, then by Salary (descending) within the same department:");
foreach (var e in sorted)
{
    Console.WriteLine($"  {e.Department} - {e.Name}: ${e.Salary:N0}");
}

// --------------------------------------------------------------
// 4. Take / Skip: Pagination
// ------------------------------
Console.WriteLine("\n4. Take / Skip: Pagination");
Console.WriteLine(new string('-', 40));

Console.WriteLine($"Top 3: {string.Join(", ", employees.Take(3).Select(e => e.Name))}");
Console.WriteLine($"Skip 2, then Take 2: {string.Join(", ", employees.Skip(2).Take(2).Select(e => e.Name))}");

// .NET 6+ Range Syntax
Console.WriteLine($"Using Range [1..3]: {string.Join(", ", employees.Take(1..3).Select(e => e.Name))}");

// --------------------------------------------------------------
// 5. Distinct: Removal of Duplicates
// --------------------------------------------------------------
Console.WriteLine("\n5. Distinct: Removal of Duplicates");
Console.WriteLine(new string('-', 40));

var departments = employees.Select(e => e.Department).Distinct();
Console.WriteLine($"All Departments: {string.Join(", ", departments)}");

// --------------------------------------------------------------
// 6. Query Syntax vs. Method Syntax
// --------------------------------------------------------------
Console.WriteLine("\n6. Query Syntax vs. Method Syntax");
Console.WriteLine(new string('-', 40));

// Query Syntax
var queryResult = from e in employees
                  where e.Salary > 60000
                  orderby e.Name
                  select e.Name;

// Method Syntax (Equivalent)
var methodResult = employees
    .Where(e => e.Salary > 60000)
    .OrderBy(e => e.Name)
    .Select(e => e.Name);

Console.WriteLine($"Query Syntax Result: {string.Join(", ", queryResult)}");
Console.WriteLine($"Method Syntax Result: {string.Join(", ", methodResult)}");

Console.WriteLine("\n=== Example End ===");

// ============================================================
// Test Data
// ============================================================

static List<Employee> GetEmployees() =>
[
    new("Alice", "Sales", 55000, 28),
    new("Bob", "IT", 72000, 35),
    new("Carol", "Sales", 62000, 42),
    new("Dave", "IT", 58000, 29),
    new("Eve", "HR", 48000, 31),
    new("Frank", "Sales", 70000, 38)
];

// ============================================================
// Data Model
// ============================================================

public record Employee(string Name, string Department, decimal Salary, int Age);
