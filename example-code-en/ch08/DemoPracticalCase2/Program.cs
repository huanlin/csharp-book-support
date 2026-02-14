// Case 2: Chain of Responsibility Validator

Console.WriteLine("Case 2: Chain of Responsibility Validator");
Console.WriteLine(new string('-', 40));

var passwordValidator = new ValidationPipeline<string>()
    .AddRule(s => (s.Length >= 8, "Password must be at least 8 characters long"))
    .AddRule(s => (s.Any(char.IsUpper), "Password must contain at least one uppercase letter"))
    .AddRule(s => (s.Any(char.IsLower), "Password must contain at least one lowercase letter"))
    .AddRule(s => (s.Any(char.IsDigit), "Password must contain at least one digit"));

Console.WriteLine("Validating 'ab123':");
var (isValid, errors) = passwordValidator.Validate("ab123");
if (!isValid)
{
    foreach (var error in errors)
        Console.WriteLine($"  ✗ {error}");
}

Console.WriteLine("\nValidating 'MyP@ssw0rd':");
(isValid, errors) = passwordValidator.Validate("MyP@ssw0rd");
if (isValid)
{
    Console.WriteLine("  ✓ Password meets all rules");
}

Console.ReadKey();

// ============================================================
// Helper Classes
// ============================================================

// Validation Pipeline
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
