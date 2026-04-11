// 示範 LINQ 基礎運算子

Console.WriteLine("=== LINQ 基礎運算子範例 ===\n");

// 準備測試資料
var employees = GetEmployees();

Console.WriteLine("員工資料：");
foreach (var e in employees)
{
    Console.WriteLine($"  {e.Name}, {e.Department}, ${e.Salary:N0}, {e.Age}歲");
}

// --------------------------------------------------------------
// 1. Where：篩選
// --------------------------------------------------------------
Console.WriteLine("\n1. Where：篩選");
Console.WriteLine(new string('-', 40));

var salesPeople = employees.Where(e => e.Department == "Sales");
Console.WriteLine("業務部門員工：");
foreach (var e in salesPeople)
{
    Console.WriteLine($"  {e.Name}");
}

// --------------------------------------------------------------
// 2. Select：投影
// --------------------------------------------------------------
Console.WriteLine("\n2. Select：投影");
Console.WriteLine(new string('-', 40));

var names = employees.Select(e => e.Name);
Console.WriteLine($"所有員工姓名：{string.Join(", ", names)}");

var summaries = employees.Select(e => new
{
    e.Name,
    e.Salary,
    Tax = e.Salary * 0.05m
});
Console.WriteLine("\n薪資與稅額摘要：");
foreach (var s in summaries)
{
    Console.WriteLine($"  {s.Name}: 薪資 ${s.Salary:N0}, 稅 ${s.Tax:N0}");
}

// --------------------------------------------------------------
// 3. OrderBy / ThenBy：排序
// --------------------------------------------------------------
Console.WriteLine("\n3. OrderBy / ThenBy：排序");
Console.WriteLine(new string('-', 40));

var sorted = employees
    .OrderBy(e => e.Department)
    .ThenByDescending(e => e.Salary);

Console.WriteLine("依部門排序，同部門依薪資由高到低：");
foreach (var e in sorted)
{
    Console.WriteLine($"  {e.Department} - {e.Name}: ${e.Salary:N0}");
}

// --------------------------------------------------------------
// 4. Take / Skip：分頁
// --------------------------------------------------------------
Console.WriteLine("\n4. Take / Skip：分頁");
Console.WriteLine(new string('-', 40));

Console.WriteLine($"前 3 名：{string.Join(", ", employees.Take(3).Select(e => e.Name))}");
Console.WriteLine($"跳過 2 名後取 2 名：{string.Join(", ", employees.Skip(2).Take(2).Select(e => e.Name))}");

// .NET 6+ Range 語法
Console.WriteLine($"使用 Range 取 [1..3]：{string.Join(", ", employees.Take(1..3).Select(e => e.Name))}");

// --------------------------------------------------------------
// 5. Distinct：去重
// --------------------------------------------------------------
Console.WriteLine("\n5. Distinct：去重");
Console.WriteLine(new string('-', 40));

var departments = employees.Select(e => e.Department).Distinct();
Console.WriteLine($"所有部門：{string.Join(", ", departments)}");

// --------------------------------------------------------------
// 6. 查詢語法 vs 方法語法
// --------------------------------------------------------------
Console.WriteLine("\n6. 查詢語法 vs 方法語法");
Console.WriteLine(new string('-', 40));

// 查詢語法
var queryResult = from e in employees
                  where e.Salary > 60000
                  orderby e.Name
                  select e.Name;

// 方法語法（等效）
var methodResult = employees
    .Where(e => e.Salary > 60000)
    .OrderBy(e => e.Name)
    .Select(e => e.Name);

Console.WriteLine($"查詢語法結果：{string.Join(", ", queryResult)}");
Console.WriteLine($"方法語法結果：{string.Join(", ", methodResult)}");

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 測試資料
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
// 資料類別
// ============================================================

public record Employee(string Name, string Department, decimal Salary, int Age);
