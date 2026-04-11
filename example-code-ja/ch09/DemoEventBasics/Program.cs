// デモ: イベントの基本（デリゲートとの違い）

Console.WriteLine("=== デリゲート vs イベント ===\n");

// --------------------------------------------------------------
// 1. デリゲート公開の問題
// --------------------------------------------------------------
Console.WriteLine("1. なぜイベントが必要か（デリゲート公開の問題）");
Console.WriteLine(new string('-', 40));

var buttonWithDelegate = new ButtonWithDelegate();
buttonWithDelegate.Clicked += () => Console.WriteLine("  購読者 A");
buttonWithDelegate.Clicked += () => Console.WriteLine("  購読者 B");

Console.WriteLine("デリゲート公開だと外部コードが危険な操作をできる:");
Console.WriteLine("  - = で購読者を上書き可能");
Console.WriteLine("  - Invoke() を外部から直接呼べる");

// --------------------------------------------------------------
// 2. event 利用（購読を保護）
// --------------------------------------------------------------
Console.WriteLine("\n2. event 利用（購読を保護）");
Console.WriteLine(new string('-', 40));

var button = new Button();
button.Clicked += () => Console.WriteLine("  購読者 A がクリック通知を受信");
button.Clicked += () => Console.WriteLine("  購読者 B がクリック通知を受信");

Console.WriteLine("ボタンクリックをシミュレート:");
button.SimulateClick();

// button.Clicked = null;   // コンパイルエラー
// button.Clicked.Invoke(); // コンパイルエラー

Console.WriteLine("\n=== 例の終了 ===\n");

// ============================================================
// ヘルパークラス
// ============================================================

public class ButtonWithDelegate
{
    public Action? Clicked;  // public デリゲートフィールド（安全でない）

    public void SimulateClick() => Clicked?.Invoke();
}

public class Button
{
    public event Action? Clicked;

    public void SimulateClick() => Clicked?.Invoke();
}
