// 示範委派與事件的實戰案例

Console.WriteLine("=== 實戰案例 ===\n");

// --------------------------------------------------------------
// 案例 1：可撤銷的操作系統（命令模式）
// --------------------------------------------------------------
Console.WriteLine("案例 1：可撤銷的操作系統");
Console.WriteLine(new string('-', 40));

var manager = new CommandManager();
var counter = 0;

// 增加命令
var incrementCommand = new Command(
    execute: () => { counter++; Console.WriteLine($"  執行：counter = {counter}"); },
    undo: () => { counter--; Console.WriteLine($"  撤銷：counter = {counter}"); }
);

manager.ExecuteCommand(incrementCommand);
manager.ExecuteCommand(incrementCommand);
manager.ExecuteCommand(incrementCommand);

Console.WriteLine("\n撤銷兩次：");
manager.Undo();
manager.Undo();

// --------------------------------------------------------------
// 案例 2：責任鏈驗證器
// --------------------------------------------------------------
Console.WriteLine("\n\n案例 2：責任鏈驗證器");
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

// --------------------------------------------------------------
// 案例 3：事件聚合器
// --------------------------------------------------------------
Console.WriteLine("\n\n案例 3：事件聚合器");
Console.WriteLine(new string('-', 40));

var aggregator = new EventAggregator();

// 模組 A 訂閱訂單事件
aggregator.Subscribe<OrderPlaced>(evt =>
    Console.WriteLine($"  [訂單模組] 訂單 {evt.OrderId} 已建立，金額：${evt.Amount}"));

// 模組 B 訂閱付款事件
aggregator.Subscribe<PaymentReceived>(evt =>
    Console.WriteLine($"  [付款模組] 訂單 {evt.OrderId} 付款完成"));

// 模組 C 訂閱所有訂單事件
aggregator.Subscribe<OrderPlaced>(evt =>
    Console.WriteLine($"  [通知模組] 發送訂單確認郵件給客戶"));

Console.WriteLine("發布 OrderPlaced 事件：");
aggregator.Publish(new OrderPlaced("ORD-001", 1500));

Console.WriteLine("\n發布 PaymentReceived 事件：");
aggregator.Publish(new PaymentReceived("ORD-001"));

// --------------------------------------------------------------
// 案例 4：帶重試的操作執行器
// --------------------------------------------------------------
Console.WriteLine("\n\n案例 4：帶重試的操作執行器");
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

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 輔助類別
// ============================================================

// 命令模式
public class Command
{
    private readonly Action _execute;
    private readonly Action _undo;

    public Command(Action execute, Action undo)
    {
        _execute = execute;
        _undo = undo;
    }

    public void Execute() => _execute();
    public void Undo() => _undo();
}

public class CommandManager
{
    private readonly Stack<Command> _history = new();

    public void ExecuteCommand(Command command)
    {
        command.Execute();
        _history.Push(command);
    }

    public void Undo()
    {
        if (_history.Count > 0)
        {
            var command = _history.Pop();
            command.Undo();
        }
    }
}

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

// 事件聚合器
public record OrderPlaced(string OrderId, decimal Amount);
public record PaymentReceived(string OrderId);

public class EventAggregator
{
    private readonly Dictionary<Type, List<Delegate>> _subscribers = new();

    public void Subscribe<TEvent>(Action<TEvent> handler)
    {
        var eventType = typeof(TEvent);

        if (!_subscribers.ContainsKey(eventType))
            _subscribers[eventType] = new List<Delegate>();

        _subscribers[eventType].Add(handler);
    }

    public void Publish<TEvent>(TEvent eventData)
    {
        var eventType = typeof(TEvent);

        if (_subscribers.TryGetValue(eventType, out var handlers))
        {
            foreach (Action<TEvent> handler in handlers.Cast<Action<TEvent>>())
            {
                handler(eventData);
            }
        }
    }
}

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
