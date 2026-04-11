// ケース 4: リトライ付き実行

Console.WriteLine("ケース 4: リトライ付き実行");
Console.WriteLine(new string('-', 40));

var executor = new RetryExecutor(maxRetries: 3);

int attempt = 0;
executor.Execute(() =>
{
    attempt++;
    Console.WriteLine($"  試行 #{attempt}...");
    if (attempt < 3)
        throw new Exception("失敗をシミュレート");
    Console.WriteLine("  実行成功!");
});

Console.ReadKey();

// ============================================================
// ヘルパークラス
// ============================================================

public class RetryExecutor
{
    private readonly int _maxRetries;

    public RetryExecutor(int maxRetries)
    {
        _maxRetries = maxRetries;
    }

    public void Execute(Action action)
    {
        for (int i = 0; i < _maxRetries; i++)
        {
            try
            {
                action();
                return;
            }
            catch (Exception)
            {
                if (i == _maxRetries - 1)
                    throw;
            }
        }
    }
}
