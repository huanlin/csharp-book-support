// デモ: 基本 LINQ 演算子

Console.WriteLine("=== 基本 LINQ 演算子の例 ===\n");

var employees = GetEmployees();

Console.WriteLine("従業員データ:");
foreach (var e in employees)
{
    Console.WriteLine($"  {e.Name}, {e.Department}, ${e.Salary:N0}, {e.Age} 歳");
}

// --------------------------------------------------------------
// 1. Where: フィルタ
// --------------------------------------------------------------
Console.WriteLine("\n1. Where: フィルタ");
Console.WriteLine(new string('-', 40));

var salesPeople = employees.Where(e => e.Department == "Sales");
Console.WriteLine("Sales 部門:");
foreach (var e in salesPeople)
{
    Console.WriteLine($"  {e.Name}");
}

// --------------------------------------------------------------
// 2. Select: 射影
// --------------------------------------------------------------
Console.WriteLine("\n2. Select: 射影");
Console.WriteLine(new string('-', 40));

var names = employees.Select(e => e.Name);
Console.WriteLine($"従業員名一覧: {string.Join(", ", names)}");

var summaries = employees.Select(e => new
{
    e.Name,
    e.Salary,
    Tax = e.Salary * 0.05m
});
Console.WriteLine("\n給与と税額:");
foreach (var s in summaries)
{
    Console.WriteLine($"  {s.Name}: 給与 ${s.Salary:N0}, 税額 ${s.Tax:N0}");
}

// --------------------------------------------------------------
// 3. OrderBy / ThenBy
// --------------------------------------------------------------
Console.WriteLine("\n3. OrderBy / ThenBy: ソート");
Console.WriteLine(new string('-', 40));

var sorted = employees
    .OrderBy(e => e.Department)
    .ThenByDescending(e => e.Salary);

Console.WriteLine("部門順 -> 同部門内給与降順:");
foreach (var e in sorted)
{
    Console.WriteLine($"  {e.Department} - {e.Name}: ${e.Salary:N0}");
}

// --------------------------------------------------------------
// 4. Take / Skip
// --------------------------------------------------------------
Console.WriteLine("\n4. Take / Skip: ページング");
Console.WriteLine(new string('-', 40));

Console.WriteLine($"先頭 3 件: {string.Join(", ", employees.Take(3).Select(e => e.Name))}");
Console.WriteLine($"2 件スキップ後 2 件取得: {string.Join(", ", employees.Skip(2).Take(2).Select(e => e.Name))}");

Console.WriteLine($"Range [1..3]: {string.Join(", ", employees.Take(1..3).Select(e => e.Name))}");

// --------------------------------------------------------------
// 5. Distinct
// --------------------------------------------------------------
Console.WriteLine("\n5. Distinct: 重複除去");
Console.WriteLine(new string('-', 40));

var departments = employees.Select(e => e.Department).Distinct();
Console.WriteLine($"部門一覧: {string.Join(", ", departments)}");

// --------------------------------------------------------------
// 6. Query Syntax vs Method Syntax
// --------------------------------------------------------------
Console.WriteLine("\n6. Query Syntax vs Method Syntax");
Console.WriteLine(new string('-', 40));

var queryResult = from e in employees
                  where e.Salary > 60000
                  orderby e.Name
                  select e.Name;

var methodResult = employees
    .Where(e => e.Salary > 60000)
    .OrderBy(e => e.Name)
    .Select(e => e.Name);

Console.WriteLine($"Query Syntax 結果: {string.Join(", ", queryResult)}");
Console.WriteLine($"Method Syntax 結果: {string.Join(", ", methodResult)}");

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// テストデータ
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
// データモデル
// ============================================================

public record Employee(string Name, string Department, decimal Salary, int Age);
