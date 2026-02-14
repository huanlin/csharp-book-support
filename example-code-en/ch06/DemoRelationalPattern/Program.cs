// Demo: Constant Pattern and Relational Pattern

Console.WriteLine("=== Constant Pattern and Relational Pattern Example ===\n");

// --------------------------------------------------------------
// 1. Constant Pattern
// --------------------------------------------------------------
Console.WriteLine("1. Constant Pattern");
Console.WriteLine(new string('-', 40));

object[] values = [0, 1, "hello", true, null!, 3.14, 2m];

foreach (object? value in values)
{
    string description = DescribeConstant(value);
    Console.WriteLine($"{value ?? "null"} -> {description}");
}

// Note: Types must match exactly
Console.WriteLine("\nType Matching Considerations:");
object decimalValue = 2m;  // decimal
Console.WriteLine($"obj is 2  (int)    : {decimalValue is 2}");   // False
Console.WriteLine($"obj is 2m (decimal): {decimalValue is 2m}");  // True

// --------------------------------------------------------------
// 2. Relational Pattern: Grade Levels
// --------------------------------------------------------------
Console.WriteLine("\n2. Relational Pattern: Grade Levels");
Console.WriteLine(new string('-', 40));

int[] scores = [95, 85, 75, 65, 55, 100, 0];

foreach (int score in scores)
{
    string grade = GetGrade(score);
    Console.WriteLine($"Score {score,3} -> Grade {grade}");
}

// --------------------------------------------------------------
// 3. Relational Pattern: BMI Classification
// --------------------------------------------------------------
Console.WriteLine("\n3. Relational Pattern: BMI Classification");
Console.WriteLine(new string('-', 40));

decimal[] bmiValues = [17.5m, 22.0m, 25.5m, 28.5m, 32.0m, 36.0m];

foreach (decimal bmi in bmiValues)
{
    string category = GetBmiCategory(bmi);
    Console.WriteLine($"BMI {bmi,5:F1} -> {category}");
}

// --------------------------------------------------------------
// 4. Relational Pattern: Age Classification
// --------------------------------------------------------------
Console.WriteLine("\n4. Relational Pattern: Age Classification");
Console.WriteLine(new string('-', 40));

int[] ages = [0, 3, 12, 18, 35, 65, 80];

foreach (int age in ages)
{
    string category = GetAgeCategory(age);
    Console.WriteLine($"Age {age,2} -> {category}");
}

// --------------------------------------------------------------
// 5. Relational Pattern: Temperature Description
// --------------------------------------------------------------
Console.WriteLine("\n5. Relational Pattern: Temperature Description");
Console.WriteLine(new string('-', 40));

double[] temperatures = [-10, 0, 15, 25, 35, 40];

foreach (double temp in temperatures)
{
    string description = DescribeTemperature(temp);
    Console.WriteLine($"{temp,3}°C -> {description}");
}

Console.WriteLine("\n=== Example End ===");

// ============================================================
// Pattern Matching Methods
// ============================================================

static string DescribeConstant(object? obj) => obj switch
{
    0 => "Zero",
    1 => "One",
    "hello" => "Greeting",
    true => "True",
    null => "Null",
    _ => "Other"
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
    < 18.5m => "Underweight",
    < 24m => "Normal range",
    < 27m => "Overweight",
    < 30m => "Mildly obese",
    < 35m => "Moderately obese",
    _ => "Severely obese"
};

static string GetAgeCategory(int age) => age switch
{
    < 0 => "Invalid age",
    < 3 => "Infant/Toddler",
    < 13 => "Child",
    < 18 => "Teenager",
    < 65 => "Adult",
    _ => "Senior"
};

static string DescribeTemperature(double temp) => temp switch
{
    < 0 => "Freezing (below zero)",
    < 10 => "Cold",
    < 20 => "Cool",
    < 30 => "Warm",
    < 35 => "Hot",
    _ => "Scorching"
};
