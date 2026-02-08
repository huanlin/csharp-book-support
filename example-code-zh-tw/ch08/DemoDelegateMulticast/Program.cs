// 示範多點傳送委派（Multicast Delegate）

Console.WriteLine("4. 多點傳送委派");
Console.WriteLine(new string('-', 40));

Action? notifyAll = null;

// 使用 += 加入方法
notifyAll += () => Console.WriteLine("  通知管理員");
notifyAll += () => Console.WriteLine("  記錄日誌");
notifyAll += () => Console.WriteLine("  發送郵件");

Console.WriteLine("執行所有通知：");
notifyAll?.Invoke();

// 移除其中一個方法
Console.WriteLine("\n移除「記錄日誌」後：");
// 注意：這裡使用 lambda 無法直接移除，因為是匿名方法的新實例。
// 上面的範例主要是展示 += 的語法。
// 若要正確移除，通常需要具名方法或儲存委派實例。

// 正確移除的示範：
Action logAction = () => Console.WriteLine("  [具名] 記錄日誌");
notifyAll = null;
notifyAll += () => Console.WriteLine("  [具名] 通知管理員");
notifyAll += logAction;

Console.WriteLine("重設並加入具名委派...");
notifyAll?.Invoke();

Console.WriteLine("\n移除具名委派後：");
notifyAll -= logAction;
notifyAll?.Invoke();

Console.ReadKey();
