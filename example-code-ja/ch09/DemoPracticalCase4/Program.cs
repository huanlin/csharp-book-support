// ケース 4: リトライ付き実行
// 本例はデリゲートによって実行フローと実際の処理を分離する方法を示すのみで、失敗は呼び出し側がシミュレートしています。
// 実際の利用前には、再試行可能なエラーと操作の冪等性を確認し、待機・タイムアウト・キャンセル機構を設計する必要があります。
// ここでは上記ポリシーを実装しておらず、catch (Exception) は模擬失敗の捕捉のみを目的としています。
// 詳細は本書第 9 章および https://learn.microsoft.com/azure/architecture/patterns/retry を参照してください。

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
