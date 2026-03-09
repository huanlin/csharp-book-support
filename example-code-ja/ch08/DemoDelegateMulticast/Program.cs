// デモ: マルチキャストデリゲート

Console.WriteLine("4. マルチキャストデリゲート");
Console.WriteLine(new string('-', 40));

Action? notifyAll = null;

notifyAll += () => Console.WriteLine("  管理者へ通知");
notifyAll += () => Console.WriteLine("  ログを記録");
notifyAll += () => Console.WriteLine("  メール送信");

Console.WriteLine("すべての通知を実行:");
notifyAll?.Invoke();

Console.WriteLine("\n'ログ記録' を削除した後:");
Console.WriteLine("※ ここでは無名ラムダの新規インスタンスになるため直接削除できない。");
Console.WriteLine("  正しく削除するには、名前付きメソッドまたはデリゲート変数を使う。");

Action logAction = () => Console.WriteLine("  [名前付き] ログを記録");
notifyAll = null;
notifyAll += () => Console.WriteLine("  [名前付き] 管理者へ通知");
notifyAll += logAction;

Console.WriteLine("リセットして名前付きデリゲートを追加:");
notifyAll?.Invoke();

Console.WriteLine("\n名前付きデリゲート削除後:");
notifyAll -= logAction;
notifyAll?.Invoke();

Console.ReadKey();
