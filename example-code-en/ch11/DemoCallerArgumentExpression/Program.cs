using System.Runtime.CompilerServices;

Console.WriteLine("=== Smart parameter validation (CallerArgumentExpression) ===\n");

int age = -5;

try
{
    age.ThrowIfNegative();
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine(ex.Message);
}

Console.WriteLine("\nExplanation:");
Console.WriteLine("The caller does not need to pass nameof(age) manually.");
Console.WriteLine("The compiler fills in the original expression text as the parameter name.");

public static class GuardExtensions
{
    public static void ThrowIfNegative(
        this int value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(paramName, "The argument cannot be negative.");
        }
    }
}
