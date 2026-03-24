// デモ: Boxing と Unboxing

Console.WriteLine("=== Boxing ===");

int i = 123;
object o = i;  // Boxing: int をヒープ上の object に格納

Console.WriteLine($"i = {i}");
Console.WriteLine($"o = {o}");
Console.WriteLine($"o.GetType() = {o.GetType()}");  // System.Int32
Console.WriteLine("Boxing では CLR がヒープに領域を確保し、値型の値をコピーする");

Console.WriteLine();
Console.WriteLine("=== Unboxing ===");

int j = (int)o;  // Unboxing: object から int へ戻す

Console.WriteLine($"j = {j}");
Console.WriteLine("Unboxing では CLR が型確認を行い、値をコピー先の値型ストレージへ取り出す");

Console.WriteLine();
Console.WriteLine("=== 型不一致の Unboxing は例外になる ===");

try
{
    double d = (double)o;  // エラー: o は int を保持している
}
catch (InvalidCastException ex)
{
    Console.WriteLine($"InvalidCastException: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("=== よくある暗黙的 Boxing の落とし穴 ===");

int x = 10;

// 落とし穴 1: string.Format は object 引数を受け取る
string s1 = string.Format("Score: {0}", x);  // x が boxing される
Console.WriteLine($"string.Format の結果: {s1}");

// 現代的な解法: 文字列補間（通常はコンパイラ最適化される）
string s2 = $"Score: {x}";
Console.WriteLine($"文字列補間の結果: {s2}");

Console.WriteLine();
Console.WriteLine("=== ジェネリックで Boxing を回避する ===");

// 非ジェネリックコレクション（boxing が発生）
var arrayList = new System.Collections.ArrayList();
arrayList.Add(1);  // int が object として boxing される
arrayList.Add(2);

// ジェネリックコレクション（boxing なし）
var list = new List<int>();
list.Add(1);  // int をそのまま保持
list.Add(2);

Console.WriteLine("推奨: ArrayList のような非ジェネリックより List<T> を優先して、boxing コストを避ける");
