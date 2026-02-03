// 示範匿名處置模式（Anonymous Disposal）

Console.WriteLine("=== 匿名處置模式範例 ===\n");

// --------------------------------------------------------------
// 1. 基本的匿名處置
// --------------------------------------------------------------
Console.WriteLine("1. 基本的匿名處置");
Console.WriteLine(new string('-', 40));

Console.WriteLine("執行某個動作前...");
using (Disposable.Create(() => Console.WriteLine("清理動作：執行完畢")))
{
    Console.WriteLine("  執行中...");
}
Console.WriteLine();

// --------------------------------------------------------------
// 2. 暫停和恢復事件處理
// --------------------------------------------------------------
Console.WriteLine("2. 事件管理器範例");
Console.WriteLine(new string('-', 40));

var eventManager = new EventManager();

Console.WriteLine("正常觸發事件：");
eventManager.RaiseEvent("事件 1");
eventManager.RaiseEvent("事件 2");

Console.WriteLine("\n使用 SuspendEvents 暫停事件：");
using (eventManager.SuspendEvents())
{
    eventManager.RaiseEvent("事件 3（不會觸發）");
    eventManager.RaiseEvent("事件 4（不會觸發）");
}

Console.WriteLine("\n恢復後觸發事件：");
eventManager.RaiseEvent("事件 5");

// --------------------------------------------------------------
// 3. 巢狀暫停
// --------------------------------------------------------------
Console.WriteLine("\n3. 巢狀暫停");
Console.WriteLine(new string('-', 40));

using (eventManager.SuspendEvents())
{
    Console.WriteLine("  第一層暫停");
    using (eventManager.SuspendEvents())
    {
        Console.WriteLine("  第二層暫停");
        eventManager.RaiseEvent("不會觸發");
    }
    Console.WriteLine("  離開第二層，仍在第一層暫停中");
    eventManager.RaiseEvent("仍然不會觸發");
}
Console.WriteLine("  完全離開暫停");
eventManager.RaiseEvent("這個會觸發");

// --------------------------------------------------------------
// 4. 計時器範例
// --------------------------------------------------------------
Console.WriteLine("\n4. 計時器範例");
Console.WriteLine(new string('-', 40));

using (Timer.Start("模擬長時間操作"))
{
    // 模擬一些工作
    Thread.Sleep(100);
}

using (Timer.Start("另一個操作"))
{
    Thread.Sleep(50);
}

// --------------------------------------------------------------
// 5. 臨時狀態切換
// --------------------------------------------------------------
Console.WriteLine("\n5. 臨時狀態切換");
Console.WriteLine(new string('-', 40));

var worker = new StateWorker();
Console.WriteLine($"初始狀態：{worker.State}");

using (worker.TemporarilySetState("處理中"))
{
    Console.WriteLine($"臨時狀態：{worker.State}");
    worker.DoWork();
}

Console.WriteLine($"恢復狀態：{worker.State}");

// --------------------------------------------------------------
// 6. 確保例外時也能執行清理
// --------------------------------------------------------------
Console.WriteLine("\n6. 例外時的清理");
Console.WriteLine(new string('-', 40));

try
{
    using (Disposable.Create(() => Console.WriteLine("  清理動作仍然執行")))
    {
        Console.WriteLine("  準備拋出例外...");
        throw new InvalidOperationException("測試例外");
    }
}
catch (InvalidOperationException)
{
    Console.WriteLine("  例外已被捕捉");
}

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// Disposable 輔助類別
// ============================================================

/// <summary>
/// 通用的匿名 IDisposable 實作
/// 允許透過委派定義清理動作
/// </summary>
public class Disposable : IDisposable
{
    private Action? _onDispose;

    public static Disposable Create(Action onDispose)
        => new Disposable(onDispose);

    public static readonly Disposable Empty = new Disposable(() => { });

    private Disposable(Action onDispose)
        => _onDispose = onDispose;

    public void Dispose()
    {
        _onDispose?.Invoke();
        _onDispose = null;  // 防止多次執行
    }
}

// ============================================================
// 應用範例類別
// ============================================================

/// <summary>
/// 事件管理器，支援暫停和恢復事件處理
/// </summary>
public class EventManager
{
    private int _suspendCount = 0;

    public IDisposable SuspendEvents()
    {
        _suspendCount++;
        Console.WriteLine($"  暫停事件（層級 {_suspendCount}）");
        return Disposable.Create(() =>
        {
            _suspendCount--;
            Console.WriteLine($"  恢復事件（層級 {_suspendCount}）");
        });
    }

    public void RaiseEvent(string eventName)
    {
        if (_suspendCount == 0)
        {
            Console.WriteLine($"  觸發事件：{eventName}");
        }
        else
        {
            // 事件被暫停，不做任何事
        }
    }
}

/// <summary>
/// 簡單的計時器，使用匿名處置記錄執行時間
/// </summary>
public class Timer
{
    public static IDisposable Start(string operationName)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        Console.WriteLine($"  開始：{operationName}");
        
        return Disposable.Create(() =>
        {
            stopwatch.Stop();
            Console.WriteLine($"  結束：{operationName}（耗時 {stopwatch.ElapsedMilliseconds} ms）");
        });
    }
}

/// <summary>
/// 支援臨時狀態切換的工作類別
/// </summary>
public class StateWorker
{
    public string State { get; private set; } = "閒置";

    public IDisposable TemporarilySetState(string temporaryState)
    {
        string originalState = State;
        State = temporaryState;
        
        return Disposable.Create(() =>
        {
            State = originalState;
        });
    }

    public void DoWork()
    {
        Console.WriteLine($"  執行工作（當前狀態：{State}）");
    }
}
