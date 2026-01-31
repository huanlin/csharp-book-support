// 示範委派（Delegate）的基本概念

Console.WriteLine("=== 委派基本概念 ===\n");

// --------------------------------------------------------------
// 1. 自訂委派型別
// --------------------------------------------------------------
Console.WriteLine("1. 自訂委派型別");
Console.WriteLine(new string('-', 40));

// 使用委派（Calculate 委派型別定義在檔案底部）
Calculate add = (x, y) => x + y;
Calculate multiply = (x, y) => x * y;

Console.WriteLine($"add(10, 5) = {add(10, 5)}");
Console.WriteLine($"multiply(10, 5) = {multiply(10, 5)}");

// --------------------------------------------------------------
// 2. 使用 Predicate<T> 實現搜尋功能
// --------------------------------------------------------------
Console.WriteLine("\n2. 使用 Predicate<T> 實現搜尋");
Console.WriteLine(new string('-', 40));

var fruits = new StringList();
fruits.Add("Apple");
fruits.Add("Mango");
fruits.Add("Banana");

// 尋找以 "go" 結尾的字串
string? result = fruits.Find(s => s.EndsWith("go"));
Console.WriteLine($"以 'go' 結尾的水果：{result}");

// 尋找包含 'a' 的字串
result = fruits.Find(s => s.Contains('a'));
Console.WriteLine($"包含 'a' 的水果：{result}");

// --------------------------------------------------------------
// 3. 多點傳送委派（Multicast Delegate）
// --------------------------------------------------------------
Console.WriteLine("\n3. 多點傳送委派");
Console.WriteLine(new string('-', 40));

Action? notifyAll = null;

// 使用 += 加入方法
notifyAll += () => Console.WriteLine("  通知管理員");
notifyAll += () => Console.WriteLine("  記錄日誌");
notifyAll += () => Console.WriteLine("  發送郵件");

Console.WriteLine("執行所有通知：");
notifyAll?.Invoke();

// --------------------------------------------------------------
// 4. 委派的型別相容性
// --------------------------------------------------------------
Console.WriteLine("\n4. 委派的型別相容性");
Console.WriteLine(new string('-', 40));

// D1, D2 委派型別定義在檔案底部
static void SayHello() => Console.WriteLine("  Hello!");

D1 d1 = SayHello;
// D2 d2 = d1;  // 編譯錯誤：型別不相容
D2 d2 = new D2(d1);  // 可以：明確建立新委派實例

Console.WriteLine("d1 呼叫：");
d1();
Console.WriteLine("d2 呼叫：");
d2();

// --------------------------------------------------------------
// 5. 參數逆變與回傳協變
// --------------------------------------------------------------
Console.WriteLine("\n5. 參數逆變與回傳協變");
Console.WriteLine(new string('-', 40));

// 參數逆變（使用 StringAction 委派型別，定義在檔案底部）

static void ActOnObject(object o) => Console.WriteLine($"  處理物件：{o}");

StringAction sa = ActOnObject;  // 合法：object 比 string 更寬鬆
sa("hello world");

// 回傳協變（使用 ObjectRetriever 委派型別，定義在檔案底部）

static string RetrieveString() => "這是字串";

ObjectRetriever or = RetrieveString;  // 合法：string 比 object 更具體
Console.WriteLine($"  取得：{or()}");

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 委派型別宣告（必須放在 top-level statements 之後）
// ============================================================

delegate int Calculate(int x, int y);
delegate void D1();
delegate void D2();
delegate void StringAction(string s);
delegate object ObjectRetriever();

// ============================================================
// 輔助類別
// ============================================================

public class StringList
{
    private readonly List<string> _items = new();

    public void Add(string item) => _items.Add(item);

    public string? Find(Predicate<string> match)
    {
        foreach (var item in _items)
        {
            if (match(item))
                return item;
        }
        return null;
    }
}
