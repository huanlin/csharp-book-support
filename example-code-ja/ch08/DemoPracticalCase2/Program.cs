// ケース 2: 責務連鎖バリデーター

Console.WriteLine("ケース 2: 責務連鎖バリデーター");
Console.WriteLine(new string('-', 40));

var passwordValidator = new ValidationPipeline<string>()
    .AddRule(s => (s.Length >= 8, "パスワードは 8 文字以上必要"))
    .AddRule(s => (s.Any(char.IsUpper), "大文字を 1 文字以上含める必要"))
    .AddRule(s => (s.Any(char.IsLower), "小文字を 1 文字以上含める必要"))
    .AddRule(s => (s.Any(char.IsDigit), "数字を 1 文字以上含める必要"));

Console.WriteLine("'ab123' を検証:");
var (isValid, errors) = passwordValidator.Validate("ab123");
if (!isValid)
{
    foreach (var error in errors)
        Console.WriteLine($"  失敗: {error}");
}

Console.WriteLine("\n'MyP@ssw0rd' を検証:");
(isValid, errors) = passwordValidator.Validate("MyP@ssw0rd");
if (isValid)
{
    Console.WriteLine("  成功: すべてのルールを満たす");
}

Console.ReadKey();

// ============================================================
// ヘルパークラス
// ============================================================

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
