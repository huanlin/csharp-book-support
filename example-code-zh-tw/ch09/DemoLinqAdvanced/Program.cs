// 示範 LINQ 進階運算子

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

var hasEmptyDept = departments.Any(d => !d.Members.Any());
Console.WriteLine($"有空的部門？{(hasEmptyDept ? "是" : "否")}");

// --------------------------------------------------------------
// 4. Count / Sum / Average / Min / Max
// --------------------------------------------------------------
Console.WriteLine("\n4. 聚合運算：Count / Sum / Average / Min / Max");
Console.WriteLine(new string('-', 40));

Console.WriteLine($"員工總數：{employees.Count()}");
Console.WriteLine($"業務人數：{employees.Count(e => e.Department == "Sales")}");
Console.WriteLine($"薪資總和：${employees.Sum(e => e.Salary):N0}");
Console.WriteLine($"平均年齡：{employees.Average(e => e.Age):F1}");
Console.WriteLine($"最低薪資：${employees.Min(e => e.Salary):N0}");
Console.WriteLine($"最高薪資：${employees.Max(e => e.Salary):N0}");

// --------------------------------------------------------------
// 5. GroupBy：分組
// --------------------------------------------------------------
Console.WriteLine("\n5. GroupBy：分組");
Console.WriteLine(new string('-', 40));

var byDept = employees.GroupBy(e => e.Department);

foreach (var group in byDept)
{
    Console.WriteLine($"{group.Key} 部門：");
    foreach (var e in group)
    {
        Console.WriteLine($"  - {e.Name}: ${e.Salary:N0}");
    }
    Console.WriteLine($"  小計：${group.Sum(e => e.Salary):N0}");
}

// --------------------------------------------------------------
// 6. 集合運算：Union / Intersect / Except
// --------------------------------------------------------------
Console.WriteLine("\n6. 集合運算：Union / Intersect / Except");
Console.WriteLine(new string('-', 40));

var set1 = new[] { "A", "B", "C", "D" };
var set2 = new[] { "C", "D", "E", "F" };

Console.WriteLine($"集合 1：{string.Join(", ", set1)}");
Console.WriteLine($"集合 2：{string.Join(", ", set2)}");
Console.WriteLine($"聯集：{string.Join(", ", set1.Union(set2))}");
Console.WriteLine($"交集：{string.Join(", ", set1.Intersect(set2))}");
Console.WriteLine($"差集 (1-2)：{string.Join(", ", set1.Except(set2))}");

// --------------------------------------------------------------
// 7. Zip：配對
// --------------------------------------------------------------
Console.WriteLine("\n7. Zip：配對");
Console.WriteLine(new string('-', 40));

var names = new[] { "Alice", "Bob", "Carol" };
var scores = new[] { 85, 92, 78 };

var paired = names.Zip(scores, (name, score) => $"{name}: {score}分");
Console.WriteLine($"配對結果：{string.Join(", ", paired)}");

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
    new("Frank", "Sales", 70000, 38, "Representative")
];

static List<Department> GetDepartments() =>
[
    new("Sales", ["Alice", "Carol", "Frank"]),
    new("IT", ["Bob", "Dave"]),
    new("HR", ["Eve"])
];

// ============================================================
// 資料類別
// ============================================================

public record Employee(string Name, string Department, decimal Salary, int Age, string Title);

public record Department(string Name, string[] Members);
