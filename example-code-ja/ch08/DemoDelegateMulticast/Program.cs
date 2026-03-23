// デモ: マルチキャストデリゲート

Console.WriteLine("4. マルチキャストデリゲート");
Console.WriteLine(new string('-', 40));

Action? notifyAll = null;

Action notifyAdmin = () => Console.WriteLine("  管理者へ通知");
Action writeLog = () => Console.WriteLine("  ログを記録");
Action sendMail = () => Console.WriteLine("  メール送信");

notifyAll += notifyAdmin;
notifyAll += writeLog;
notifyAll += sendMail;

Console.WriteLine("すべての通知を実行:");
notifyAll?.Invoke();

Console.WriteLine("\n'メール送信' を削除した後:");
notifyAll -= sendMail;
notifyAll?.Invoke();

Console.ReadKey();
