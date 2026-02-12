// 示範委派 vs 事件，以及事件的基本用法

Console.WriteLine("=== 委派 vs 事件 ===\n");

// --------------------------------------------------------------
// 1. 委派的問題
// --------------------------------------------------------------
Console.WriteLine("1. 為什麼需要事件（委派的問題）");
Console.WriteLine(new string('-', 40));

// 使用委派的問題示範
var buttonWithDelegate = new ButtonWithDelegate();
buttonWithDelegate.Clicked += () => Console.WriteLine("  訂閱者 A");
buttonWithDelegate.Clicked += () => Console.WriteLine("  訂閱者 B");

Console.WriteLine("使用委派時，外部可以做壞事：");
Console.WriteLine("  - 可以用 = 覆蓋所有訂閱者");
Console.WriteLine("  - 可以直接呼叫 Invoke()");

// --------------------------------------------------------------
// 2. 使用事件（保護訂閱）
// --------------------------------------------------------------
Console.WriteLine("\n2. 使用事件（保護訂閱）");
Console.WriteLine(new string('-', 40));

var button = new Button();
button.Clicked += () => Console.WriteLine("  訂閱者 A 收到點擊通知");
button.Clicked += () => Console.WriteLine("  訂閱者 B 收到點擊通知");

Console.WriteLine("模擬點擊按鈕：");
button.SimulateClick();

// button.Clicked = null;  // 編譯錯誤！
// button.Clicked.Invoke();  // 編譯錯誤！

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 輔助類別
// ============================================================

public class ButtonWithDelegate
{
    public Action? Clicked;  // 公開委派欄位（不安全）

    public void SimulateClick() => Clicked?.Invoke();
}

public class Button
{
    public event Action? Clicked;  // 使用 event 關鍵字

    public void SimulateClick() => Clicked?.Invoke();
}
