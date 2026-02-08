// 案例 2：責任鏈驗證器

Console.WriteLine("案例 2：責任鏈驗證器");
Console.WriteLine(new string('-', 40));

var passwordValidator = new ValidationPipeline<string>()
    .AddRule(s => (s.Length >= 8, "密碼長度必須至少 8 個字元"))
    .AddRule(s => (s.Any(char.IsUpper), "密碼必須包含至少一個大寫字母"))
    .AddRule(s => (s.Any(char.IsLower), "密碼必須包含至少一個小寫字母"))
    .AddRule(s => (s.Any(char.IsDigit), "密碼必須包含至少一個數字"));

Console.WriteLine("驗證 'ab123'：");
var (isValid, errors) = passwordValidator.Validate("ab123");
if (!isValid)
{
    foreach (var error in errors)
        Console.WriteLine($"  ✗ {error}");
}

Console.WriteLine("\n驗證 'MyP@ssw0rd'：");
(isValid, errors) = passwordValidator.Validate("MyP@ssw0rd");
if (isValid)
{
    Console.WriteLine("  ✓ 密碼符合所有規則");
}

Console.ReadKey();

// ============================================================
// 輔助類別
// ============================================================

// 驗證管道
public class ValidationPipeline<T>
{
    private readonly List<Func<T, (bool isValid, string? error)>> _validators = new();

    public ValidationPipeline<T> AddRule(Func<T, (bool, string?)> validator)
    {
        _validators.Add(validator);
        return this;
    }

    public (bool isValid, List<string> errors) Validate(T item)
    {
        var errors = new List<string>();

        foreach (var validator in _validators)
        {
            var (isValid, error) = validator(item);
            if (!isValid && error != null)
                errors.Add(error);
        }

        return (errors.Count == 0, errors);
    }
}
