using System.Runtime.CompilerServices;

Console.WriteLine("=== スマートなパラメーター検証（CallerArgumentExpression）===\n");

int age = -5;

try
{
    age.ThrowIfNegative();
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine(ex.Message);
}

Console.WriteLine("\n説明:");
Console.WriteLine("呼び出し側で nameof(age) を手動で渡す必要はありません。");
Console.WriteLine("コンパイラが元の式文字列を自動的に引数名として埋め込みます。");

public static class GuardExtensions
{
    public static void ThrowIfNegative(
        this int value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(paramName, "引数は負の値にできません。");
        }
    }
}
