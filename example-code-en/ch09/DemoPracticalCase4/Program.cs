// Case 4: Operation Executor with Retries
// This example only demonstrates how delegates decouple execution flow from the actual operation; failure is simulated by the caller.
// Before production use, verify retryable errors and operation idempotency, and design delays, timeouts, and cancellation.
// Those strategies are not implemented here; catch (Exception) is used solely to catch simulated failures.
// See Chapter 9 of the book and https://learn.microsoft.com/azure/architecture/patterns/retry

Console.WriteLine("Case 4: Operation Executor with Retries");
Console.WriteLine(new string('-', 40));

var executor = new RetryExecutor(maxRetries: 3);

int attempt = 0;
executor.Execute(() =>
{
    attempt++;
    Console.WriteLine($"  Attempt #{attempt}...");
    if (attempt < 3)
        throw new Exception("Simulated failure");
    Console.WriteLine("  Operation succeeded!");
});

Console.ReadKey();

// ============================================================
// Helper Classes
// ============================================================

// Retry Executor
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
