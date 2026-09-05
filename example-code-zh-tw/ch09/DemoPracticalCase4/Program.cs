// 案例 4：帶重試的操作執行器
// 本例只示範委派如何分開執行流程與實際操作，失敗由呼叫端模擬。
// 實際使用前須確認可重試的錯誤與操作的冪等性，並設計等待、逾時及取消機制。
// 此處未實作上述策略；catch (Exception) 僅用於攔截模擬失敗。
// 詳見本書第 9 章，以及 https://learn.microsoft.com/azure/architecture/patterns/retry

Console.WriteLine("案例 4：帶重試的操作執行器");
Console.WriteLine(new string('-', 40));

var executor = new RetryExecutor(maxRetries: 3);

int attempt = 0;
executor.Execute(() =>
{
    attempt++;
    Console.WriteLine($"  嘗試第 {attempt} 次...");
    if (attempt < 3)
        throw new Exception("模擬失敗");
    Console.WriteLine("  操作成功！");
});

Console.ReadKey();

// ============================================================
// 輔助類別
// ============================================================

// 重試執行器
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
