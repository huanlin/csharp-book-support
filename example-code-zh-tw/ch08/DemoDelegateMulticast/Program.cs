// 示範多點傳送委派（Multicast Delegate）

Console.WriteLine("4. 多點傳送委派");
Console.WriteLine(new string('-', 40));

Action? notifyAll = null;

Action notifyAdmin = () => Console.WriteLine("  通知管理員");
Action writeLog = () => Console.WriteLine("  記錄日誌");
Action sendMail = () => Console.WriteLine("  發送郵件");

// 使用 += 加入方法
notifyAll += notifyAdmin;
notifyAll += writeLog;
notifyAll += sendMail;

Console.WriteLine("執行所有通知：");
notifyAll?.Invoke();

Console.WriteLine("\n移除「發送郵件」後：");
notifyAll -= sendMail;
notifyAll?.Invoke();

Console.ReadKey();
