// デモ: 定数パターンと関係パターン

Console.WriteLine("=== 定数パターンと関係パターンの例 ===\n");

// --------------------------------------------------------------
// 1. 定数パターン
// --------------------------------------------------------------
Console.WriteLine("1. 定数パターン");
Console.WriteLine(new string('-', 40));

object[] values = [0, 1, "hello", true, null!, 3.14, 2m];

foreach (object? value in values)
{
    string description = DescribeConstant(value);
    Console.WriteLine($"{value ?? "null"} -> {description}");
}

// 注意: 型は厳密一致
Console.WriteLine("\n型一致の注意:");
object decimalValue = 2m;  // decimal
Console.WriteLine($"obj is 2  (int)    : {decimalValue is 2}");   // False
Console.WriteLine($"obj is 2m (decimal): {decimalValue is 2m}");  // True

// --------------------------------------------------------------
// 2. 関係パターン: 成績判定
// --------------------------------------------------------------
Console.WriteLine("\n2. 関係パターン: 成績判定");
Console.WriteLine(new string('-', 40));

int[] scores = [95, 85, 75, 65, 55, 100, 0];

foreach (int score in scores)
{
    string grade = GetGrade(score);
    Console.WriteLine($"点数 {score,3} -> 評価 {grade}");
}

// --------------------------------------------------------------
// 3. 関係パターン: BMI 分類
// --------------------------------------------------------------
Console.WriteLine("\n3. 関係パターン: BMI 分類");
Console.WriteLine(new string('-', 40));

decimal[] bmiValues = [17.5m, 22.0m, 25.5m, 28.5m, 32.0m, 36.0m];

foreach (decimal bmi in bmiValues)
{
    string category = GetBmiCategory(bmi);
    Console.WriteLine($"BMI {bmi,5:F1} -> {category}");
}

// --------------------------------------------------------------
// 4. 関係パターン: 年齢分類
// --------------------------------------------------------------
Console.WriteLine("\n4. 関係パターン: 年齢分類");
Console.WriteLine(new string('-', 40));

int[] ages = [0, 3, 12, 18, 35, 65, 80];

foreach (int age in ages)
{
    string category = GetAgeCategory(age);
    Console.WriteLine($"年齢 {age,2} -> {category}");
}

// --------------------------------------------------------------
// 5. 関係パターン: 気温説明
// --------------------------------------------------------------
Console.WriteLine("\n5. 関係パターン: 気温説明");
Console.WriteLine(new string('-', 40));

double[] temperatures = [-10, 0, 15, 25, 35, 40];

foreach (double temp in temperatures)
{
    string description = DescribeTemperature(temp);
    Console.WriteLine($"{temp,3}°C -> {description}");
}

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// パターンマッチメソッド
// ============================================================

static string DescribeConstant(object? obj) => obj switch
{
    0 => "ゼロ",
    1 => "1",
    "hello" => "挨拶",
    true => "真",
    null => "null",
    _ => "その他"
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
    < 18.5m => "低体重",
    < 24m => "普通体重",
    < 27m => "過体重",
    < 30m => "軽度肥満",
    < 35m => "中等度肥満",
    _ => "重度肥満"
};

static string GetAgeCategory(int age) => age switch
{
    < 0 => "無効な年齢",
    < 3 => "乳幼児",
    < 13 => "子ども",
    < 18 => "未成年",
    < 65 => "成人",
    _ => "高齢者"
};

static string DescribeTemperature(double temp) => temp switch
{
    < 0 => "氷点下",
    < 10 => "寒い",
    < 20 => "涼しい",
    < 30 => "暖かい",
    < 35 => "暑い",
    _ => "猛暑"
};
