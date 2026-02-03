// 示範常數模式與關係模式

Console.WriteLine("=== 常數模式與關係模式範例 ===\n");

// --------------------------------------------------------------
// 1. 常數模式
// --------------------------------------------------------------
Console.WriteLine("1. 常數模式");
Console.WriteLine(new string('-', 40));

object[] values = [0, 1, "hello", true, null!, 3.14, 2m];

foreach (object? value in values)
{
    string description = DescribeConstant(value);
    Console.WriteLine($"{value ?? "null"} -> {description}");
}

// 注意型別必須完全匹配
Console.WriteLine("\n型別匹配注意事項：");
object decimalValue = 2m;  // decimal
Console.WriteLine($"obj is 2  (int)    : {decimalValue is 2}");   // False
Console.WriteLine($"obj is 2m (decimal): {decimalValue is 2m}");  // True

// --------------------------------------------------------------
// 2. 關係模式：成績等級
// --------------------------------------------------------------
Console.WriteLine("\n2. 關係模式：成績等級");
Console.WriteLine(new string('-', 40));

int[] scores = [95, 85, 75, 65, 55, 100, 0];

foreach (int score in scores)
{
    string grade = GetGrade(score);
    Console.WriteLine($"分數 {score,3} -> 等級 {grade}");
}

// --------------------------------------------------------------
// 3. 關係模式：BMI 分類
// --------------------------------------------------------------
Console.WriteLine("\n3. 關係模式：BMI 分類");
Console.WriteLine(new string('-', 40));

decimal[] bmiValues = [17.5m, 22.0m, 25.5m, 28.5m, 32.0m, 36.0m];

foreach (decimal bmi in bmiValues)
{
    string category = GetBmiCategory(bmi);
    Console.WriteLine($"BMI {bmi,5:F1} -> {category}");
}

// --------------------------------------------------------------
// 4. 關係模式：年齡分類
// --------------------------------------------------------------
Console.WriteLine("\n4. 關係模式：年齡分類");
Console.WriteLine(new string('-', 40));

int[] ages = [0, 3, 12, 18, 35, 65, 80];

foreach (int age in ages)
{
    string category = GetAgeCategory(age);
    Console.WriteLine($"年齡 {age,2} -> {category}");
}

// --------------------------------------------------------------
// 5. 關係模式：溫度描述
// --------------------------------------------------------------
Console.WriteLine("\n5. 關係模式：溫度描述");
Console.WriteLine(new string('-', 40));

double[] temperatures = [-10, 0, 15, 25, 35, 40];

foreach (double temp in temperatures)
{
    string description = DescribeTemperature(temp);
    Console.WriteLine($"{temp,3}°C -> {description}");
}

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 模式比對方法
// ============================================================

static string DescribeConstant(object? obj) => obj switch
{
    0 => "零",
    1 => "一",
    "hello" => "打招呼",
    true => "真",
    null => "空值",
    _ => "其他"
};

static string GetGrade(int score) => score switch
{
    >= 90 => "A",
    >= 80 => "B",
    >= 70 => "C",
    >= 60 => "D",
    _ => "F"
};

static string GetBmiCategory(decimal bmi) => bmi switch
{
    < 18.5m => "體重過輕",
    < 24m => "正常範圍",
    < 27m => "過重",
    < 30m => "輕度肥胖",
    < 35m => "中度肥胖",
    _ => "重度肥胖"
};

static string GetAgeCategory(int age) => age switch
{
    < 0 => "無效年齡",
    < 3 => "嬰幼兒",
    < 13 => "兒童",
    < 18 => "青少年",
    < 65 => "成年人",
    _ => "銀髮族"
};

static string DescribeTemperature(double temp) => temp switch
{
    < 0 => "極冷（零下）",
    < 10 => "寒冷",
    < 20 => "涼爽",
    < 30 => "溫暖",
    < 35 => "炎熱",
    _ => "酷熱"
};
