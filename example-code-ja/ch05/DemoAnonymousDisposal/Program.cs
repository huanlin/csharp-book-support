// デモ: Anonymous Disposal パターン

Console.WriteLine("=== Anonymous Disposal パターン例 ===\n");

// --------------------------------------------------------------
// 1. 基本的な匿名破棄
// --------------------------------------------------------------
Console.WriteLine("1. 基本的な匿名破棄");
Console.WriteLine(new string('-', 40));

Console.WriteLine("アクション実行前...");
using (Disposable.Create(() => Console.WriteLine("クリーンアップ: 実行完了")))
{
    Console.WriteLine("  実行中...");
}
Console.WriteLine();

// --------------------------------------------------------------
// 2. イベント処理の一時停止と再開
// --------------------------------------------------------------
Console.WriteLine("2. EventManager の例");
Console.WriteLine(new string('-', 40));

var eventManager = new EventManager();

Console.WriteLine("通常のイベント発火:");
eventManager.RaiseEvent("Event 1");
eventManager.RaiseEvent("Event 2");

Console.WriteLine("\nSuspendEvents でイベント停止:");
using (eventManager.SuspendEvents())
{
    eventManager.RaiseEvent("Event 3（発火しない）");
    eventManager.RaiseEvent("Event 4（発火しない）");
}

Console.WriteLine("\n復帰後のイベント発火:");
eventManager.RaiseEvent("Event 5");

// --------------------------------------------------------------
// 3. ネストした停止
// --------------------------------------------------------------
Console.WriteLine("\n3. ネストした停止");
Console.WriteLine(new string('-', 40));

using (eventManager.SuspendEvents())
{
    Console.WriteLine("  1段目の停止");
    using (eventManager.SuspendEvents())
    {
        Console.WriteLine("  2段目の停止");
        eventManager.RaiseEvent("発火しない");
    }
    Console.WriteLine("  2段目を抜けても1段目停止中");
    eventManager.RaiseEvent("まだ発火しない");
}
Console.WriteLine("  完全に停止解除");
eventManager.RaiseEvent("これは発火する");

// --------------------------------------------------------------
// 4. タイマー例
// --------------------------------------------------------------
Console.WriteLine("\n4. タイマー例");
Console.WriteLine(new string('-', 40));

using (Timer.Start("長時間処理をシミュレート"))
{
    Thread.Sleep(100);
}

using (Timer.Start("別の処理"))
{
    Thread.Sleep(50);
}

// --------------------------------------------------------------
// 5. 一時的な状態切り替え
// --------------------------------------------------------------
Console.WriteLine("\n5. 一時状態の切り替え");
Console.WriteLine(new string('-', 40));

var worker = new StateWorker();
Console.WriteLine($"初期状態: {worker.State}");

using (worker.TemporarilySetState("Processing"))
{
    Console.WriteLine($"一時状態: {worker.State}");
    worker.DoWork();
}

Console.WriteLine($"復元後の状態: {worker.State}");

// --------------------------------------------------------------
// 6. 例外時でもクリーンアップ
// --------------------------------------------------------------
Console.WriteLine("\n6. 例外時のクリーンアップ");
Console.WriteLine(new string('-', 40));

try
{
    using (Disposable.Create(() => Console.WriteLine("  例外時でもクリーンアップ実行")))
    {
        Console.WriteLine("  例外を送出します...");
        throw new InvalidOperationException("テスト例外");
    }
}
catch (InvalidOperationException)
{
    Console.WriteLine("  例外を捕捉");
}

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// Disposable ヘルパー
// ============================================================

/// <summary>
/// 匿名 IDisposable の汎用実装
/// デリゲートでクリーンアップ処理を定義できる
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
        _onDispose = null;  // 多重実行防止
    }
}

// ============================================================
// 利用例クラス
// ============================================================

/// <summary>
/// イベント処理の停止/再開をサポートする EventManager
/// </summary>
public class EventManager
{
    private int _suspendCount = 0;

    public IDisposable SuspendEvents()
    {
        _suspendCount++;
        Console.WriteLine($"  イベント停止（レベル {_suspendCount}）");
        return Disposable.Create(() =>
        {
            _suspendCount--;
            Console.WriteLine($"  イベント再開（レベル {_suspendCount}）");
        });
    }

    public void RaiseEvent(string eventName)
    {
        if (_suspendCount == 0)
        {
            Console.WriteLine($"  イベント発火: {eventName}");
        }
        else
        {
            // 停止中は何もしない
        }
    }
}

/// <summary>
/// 匿名破棄を使った簡易タイマー
/// </summary>
public class Timer
{
    public static IDisposable Start(string operationName)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        Console.WriteLine($"  開始: {operationName}");

        return Disposable.Create(() =>
        {
            stopwatch.Stop();
            Console.WriteLine($"  終了: {operationName}（経過 {stopwatch.ElapsedMilliseconds} ms）");
        });
    }
}

/// <summary>
/// 一時的な状態切り替えをサポートするワーカー
/// </summary>
public class StateWorker
{
    public string State { get; private set; } = "Idle";

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
        Console.WriteLine($"  作業中（現在状態: {State}）");
    }
}
