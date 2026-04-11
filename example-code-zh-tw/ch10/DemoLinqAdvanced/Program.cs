// 示範 LINQ 進階運算子 (含 .NET 6 ~ .NET 9 新特性)

Console.WriteLine("=== LINQ 進階運算子範例 ===\n");

// 準備測試資料
var employees = GetEmployees();
var departments = GetDepartments();

// --------------------------------------------------------------
// 1. SelectMany：攤平巢狀集合
// --------------------------------------------------------------
Console.WriteLine("1. SelectMany：攤平巢狀集合");
Console.WriteLine(new string('-', 40));

Console.WriteLine("部門資料：");
foreach (var d in departments)
{
    Console.WriteLine($"  {d.Name}: {string.Join(", ", d.Members)}");
}

var allMembers = departments.SelectMany(d => d.Members);
Console.WriteLine($"\n所有成員（攤平）：{string.Join(", ", allMembers)}");

// --------------------------------------------------------------
// 2. First / FirstOrDefault / Single
// --------------------------------------------------------------
Console.WriteLine("\n2. First / FirstOrDefault / Single");
Console.WriteLine(new string('-', 40));

var firstSales = employees.First(e => e.Department == "Sales");
Console.WriteLine($"第一個業務：{firstSales.Name}");

var firstCEO = employees.FirstOrDefault(e => e.Title == "CEO");
Console.WriteLine($"第一個 CEO：{firstCEO?.Name ?? "(不存在)"}");

var bob = employees.Single(e => e.Name == "Bob");
Console.WriteLine($"唯一的 Bob：{bob.Name}, {bob.Department}");

// --------------------------------------------------------------
// 3. Any / All：條件判斷
// --------------------------------------------------------------
Console.WriteLine("\n3. Any / All：條件判斷");
Console.WriteLine(new string('-', 40));

var hasHighSalary = employees.Any(e => e.Salary > 70000);
Console.WriteLine($"有人年薪超過 7 萬？{(hasHighSalary ? "是" : "否")}");

var allAdults = employees.All(e => e.Age >= 18);
Console.WriteLine($"所有員工都已成年？{(allAdults ? "是" : "否")}");

// --------------------------------------------------------------
// 4. 聚合運算 & MaxBy / MinBy (.NET 6)
// --------------------------------------------------------------
Console.WriteLine("\n4. 聚合運算：Count / Sum / MaxBy (.NET 6)");
Console.WriteLine(new string('-', 40));

Console.WriteLine($"員工總數：{employees.Count()}");
Console.WriteLine($"薪資總和：${employees.Sum(e => e.Salary):N0}");

// .NET 6: MaxBy / MinBy
var highestPaid = employees.MaxBy(e => e.Salary);
Console.WriteLine($"最高薪員工 (MaxBy)：{highestPaid?.Name} (${highestPaid?.Salary:N0})");

var lowestPaid = employees.MinBy(e => e.Salary);
Console.WriteLine($"最低薪員工 (MinBy)：{lowestPaid?.Name} (${lowestPaid?.Salary:N0})");

// .NET 6: TryGetNonEnumeratedCount
if (employees.TryGetNonEnumeratedCount(out int count))
{
    Console.WriteLine($"快速取得數量 (TryGetNonEnumeratedCount)：{count}");
}

// --------------------------------------------------------------
// 5. Chunk (.NET 6)
// --------------------------------------------------------------
Console.WriteLine("\n5. Chunk (.NET 6)：分批處理");
Console.WriteLine(new string('-', 40));

var chunkedEmployees = employees.Chunk(2); // 每 2 人一組
int chunkIndex = 1;
foreach (var batch in chunkedEmployees)
{
    Console.WriteLine($"第 {chunkIndex++} 批 ({batch.Length} 人)：{string.Join(", ", batch.Select(e => e.Name))}");
}

// --------------------------------------------------------------
// 6. Distinct & DistinctBy (.NET 6)
// --------------------------------------------------------------
Console.WriteLine("\n6. Distinct & DistinctBy (.NET 6)");
Console.WriteLine(new string('-', 40));

var depts = employees.Select(e => e.Department).Distinct();
Console.WriteLine($"不重複的部門 (Distinct)：{string.Join(", ", depts)}");

// .NET 6: DistinctBy
var distinctByDept = employees.DistinctBy(e => e.Department);
Console.WriteLine("各部門代表 (DistinctBy)：");
foreach (var emp in distinctByDept)
{
    Console.WriteLine($"  {emp.Department}: {emp.Name}");
}

// --------------------------------------------------------------
// 7. GroupBy & CountBy / AggregateBy (.NET 9)
// --------------------------------------------------------------
Console.WriteLine("\n7. 分組：GroupBy / CountBy / AggregateBy");
Console.WriteLine(new string('-', 40));

Console.WriteLine("[GroupBy]");
var byDept = employees.GroupBy(e => e.Department);
foreach (var group in byDept)
{
    Console.WriteLine($"  {group.Key} ({group.Count()} 人): {string.Join(", ", group.Select(e => e.Name))}");
}

// .NET 9: CountBy
Console.WriteLine("\n[CountBy (.NET 9)]");
foreach (var (deptKey, empCount) in employees.CountBy(e => e.Department))
{
    Console.WriteLine($"  {deptKey}: {empCount} 人");
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

var paired = names.Zip(scores, (n, s) => $"{n}: {s}分");
Console.WriteLine($"  配對結果：{string.Join(", ", paired)}");


Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 測試資料
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
