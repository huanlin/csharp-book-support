// デモ: デリゲートの型互換性と分散

// 1. デリゲート型互換性
Console.WriteLine("1. デリゲート型互換性");
Console.WriteLine(new string('-', 40));

D1 d1 = SayHello;
// D2 d2 = d1;  // コンパイルエラー: 型不一致
D2 d2 = new D2(d1);

Console.WriteLine("d1 呼び出し:");
d1();
Console.WriteLine("d2 呼び出し:");
d2();

// 2. 引数反変・戻り値共変
Console.WriteLine("\n2. 引数反変・戻り値共変");
Console.WriteLine(new string('-', 40));

StringAction sa = ActOnObject;
sa("hello world");

ObjectRetriever or = RetrieveString;
Console.WriteLine($"  取得値: {or()}");

Console.ReadKey();

// ============================================================
// ヘルパーメソッド
// ============================================================

static void SayHello() => Console.WriteLine("  Hello!");
static void ActOnObject(object o) => Console.WriteLine($"  オブジェクト処理: {o}");
static string RetrieveString() => "This is a string";

// ============================================================
// デリゲート型宣言
// ============================================================

delegate void D1();
delegate void D2();
delegate void StringAction(string s);
delegate object ObjectRetriever();
