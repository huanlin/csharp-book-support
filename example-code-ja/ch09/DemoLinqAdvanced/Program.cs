// デモ: 高度な LINQ 演算子（.NET 6 ～ .NET 9 新機能含む）

Console.WriteLine("=== 高度な LINQ 演算子の例 ===\n");

var employees = GetEmployees();
var departments = GetDepartments();

// --------------------------------------------------------------
// 1. SelectMany: ネストコレクション平坦化
// --------------------------------------------------------------
Console.WriteLine("1. SelectMany: ネストコレクション平坦化");
Console.WriteLine(new string('-', 40));

Console.WriteLine("部門データ:");
foreach (var d in departments)
{
    Console.WriteLine($"  {d.Name}: {string.Join(", ", d.Members)}");
}

var allMembers = departments.SelectMany(d => d.Members);
Console.WriteLine($"\n全メンバー（平坦化）: {string.Join(", ", allMembers)}");

// --------------------------------------------------------------
// 2. First / FirstOrDefault / Single
// --------------------------------------------------------------
Console.WriteLine("\n2. First / FirstOrDefault / Single");
Console.WriteLine(new string('-', 40));

var firstSales = employees.First(e => e.Department == "Sales");
Console.WriteLine($"最初の Sales: {firstSales.Name}");

var firstCEO = employees.FirstOrDefault(e => e.Title == "CEO");
Console.WriteLine($"最初の CEO: {firstCEO?.Name ?? "（該当なし）"}");

var bob = employees.Single(e => e.Name == "Bob");
Console.WriteLine($"唯一の Bob: {bob.Name}, {bob.Department}");

// --------------------------------------------------------------
// 3. Any / All
// --------------------------------------------------------------
Console.WriteLine("\n3. Any / All");
Console.WriteLine(new string('-', 40));

var hasHighSalary = employees.Any(e => e.Salary > 70000);
Console.WriteLine($"給与 70k 超はいるか? {(hasHighSalary ? "Yes" : "No")}");

var allAdults = employees.All(e => e.Age >= 18);
Console.WriteLine($"全員成人か? {(allAdults ? "Yes" : "No")}");

// --------------------------------------------------------------
// 4. 集計 & MaxBy / MinBy (.NET 6)
// --------------------------------------------------------------
Console.WriteLine("\n4. 集計: Count / Sum / MaxBy (.NET 6)");
Console.WriteLine(new string('-', 40));

Console.WriteLine($"従業員数: {employees.Count()}");
Console.WriteLine($"給与合計: ${employees.Sum(e => e.Salary):N0}");

var highestPaid = employees.MaxBy(e => e.Salary);
Console.WriteLine($"最高給与（MaxBy）: {highestPaid?.Name} (${highestPaid?.Salary:N0})");

var lowestPaid = employees.MinBy(e => e.Salary);
Console.WriteLine($"最低給与（MinBy）: {lowestPaid?.Name} (${lowestPaid?.Salary:N0})");

if (employees.TryGetNonEnumeratedCount(out int count))
{
    Console.WriteLine($"高速件数取得（TryGetNonEnumeratedCount）: {count}");
}

// --------------------------------------------------------------
// 5. Chunk (.NET 6)
// --------------------------------------------------------------
Console.WriteLine("\n5. Chunk (.NET 6): バッチ処理");
Console.WriteLine(new string('-', 40));

var chunkedEmployees = employees.Chunk(2);
int chunkIndex = 1;
foreach (var batch in chunkedEmployees)
{
    Console.WriteLine($"バッチ {chunkIndex++}（{batch.Length} 人）: {string.Join(", ", batch.Select(e => e.Name))}");
}

// --------------------------------------------------------------
// 6. Distinct / DistinctBy (.NET 6)
// --------------------------------------------------------------
Console.WriteLine("\n6. Distinct / DistinctBy (.NET 6)");
Console.WriteLine(new string('-', 40));

var depts = employees.Select(e => e.Department).Distinct();
Console.WriteLine($"部門一覧（Distinct）: {string.Join(", ", depts)}");

var distinctByDept = employees.DistinctBy(e => e.Department);
Console.WriteLine("部門ごとの代表（DistinctBy）:");
foreach (var emp in distinctByDept)
{
    Console.WriteLine($"  {emp.Department}: {emp.Name}");
}

// --------------------------------------------------------------
// 7. GroupBy / CountBy / AggregateBy
// --------------------------------------------------------------
Console.WriteLine("\n7. Grouping: GroupBy / CountBy / AggregateBy");
Console.WriteLine(new string('-', 40));

Console.WriteLine("[GroupBy]");
var byDept = employees.GroupBy(e => e.Department);
foreach (var group in byDept)
{
    Console.WriteLine($"  {group.Key}（{group.Count()} 人）: {string.Join(", ", group.Select(e => e.Name))}");
}

Console.WriteLine("\n[CountBy (.NET 9)]");
foreach (var (deptKey, empCount) in employees.CountBy(e => e.Department))
{
    Console.WriteLine($"  {deptKey}: {empCount} 人");
}

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
// 8. Index (.NET 9) / Zip
// --------------------------------------------------------------
Console.WriteLine("\n8. Index (.NET 9) / Zip");
Console.WriteLine(new string('-', 40));

Console.WriteLine("[Index]");
foreach (var (index, item) in employees.Take(3).Index())
{
    Console.WriteLine($"  [{index}] {item.Name}");
}

Console.WriteLine("\n[Zip]");
string[] names = ["Alice", "Bob", "Carol"];
int[] scores = [85, 92, 78];

var paired = names.Zip(scores, (n, s) => $"{n}: {s} 点");
Console.WriteLine($"  ペア結果: {string.Join(", ", paired)}");

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// テストデータ
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
