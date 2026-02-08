// 示範委派的型別相容性與變異性（Variance）

// 1. 委派的型別相容性
Console.WriteLine("1. 委派的型別相容性");
Console.WriteLine(new string('-', 40));

// D1, D2 委派型別定義在檔案底部
D1 d1 = SayHello;
// D2 d2 = d1;  // 編譯錯誤：型別不相容（雖然簽名相同）
D2 d2 = new D2(d1);  // 可以：明確建立新委派實例（C# 2.0+ 支援此語法）

Console.WriteLine("d1 呼叫：");
d1();
Console.WriteLine("d2 呼叫：");
d2();

// 2. 參數逆變與回傳協變
Console.WriteLine("\n2. 參數逆變與回傳協變");
Console.WriteLine(new string('-', 40));

// 參數逆變（使用 StringAction 委派型別，定義在檔案底部）
// StringAction 預期接收 string，但我們給它一個可以接收 object 的方法（更寬鬆）
// Contravariance: parameter type matches or is a base type

StringAction sa = ActOnObject;  // 合法：object 比 string 更寬鬆
sa("hello world");

// 回傳協變（使用 ObjectRetriever 委派型別，定義在檔案底部）
// ObjectRetriever 預期回傳 object，但我們給它一個回傳 string 的方法（更具體）
// Covariance: return type matches or is a derived type

ObjectRetriever or = RetrieveString;  // 合法：string 比 object 更具體
Console.WriteLine($"  取得：{or()}");

Console.ReadKey();

// ============================================================
// 輔助方法
// ============================================================

static void SayHello() => Console.WriteLine("  Hello!");
static void ActOnObject(object o) => Console.WriteLine($"  處理物件：{o}");
static string RetrieveString() => "這是字串";

// ============================================================
// 委派型別宣告
// ============================================================

delegate void D1();
delegate void D2();
delegate void StringAction(string s);
delegate object ObjectRetriever();
