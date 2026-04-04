using System.Runtime.CompilerServices;

Console.WriteLine("=== 智慧參數驗證（CallerArgumentExpression）===\n");

int age = -5;

try
{
    age.ThrowIfNegative();
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine(ex.Message);
}

Console.WriteLine("\n說明：");
Console.WriteLine("呼叫端不必手動傳入 nameof(age)。");
Console.WriteLine("編譯器會自動把原始運算式字串填入例外的參數名稱。");

public static class GuardExtensions
{
    public static void ThrowIfNegative(
        this int value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(paramName, "參數不可為負值");
        }
    }
}
