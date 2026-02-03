// 示範 Boxing（裝箱）與 Unboxing（拆箱）

Console.WriteLine("=== Boxing（裝箱）===");

int i = 123;
object o = i;  // Boxing：將 int 封裝進 Heap 上的 object

Console.WriteLine($"i = {i}");
Console.WriteLine($"o = {o}");
Console.WriteLine($"o.GetType() = {o.GetType()}");  // System.Int32
Console.WriteLine("Boxing 時，CLR 會在 heap 上配置記憶體，複製 value type 的值進去");

Console.WriteLine();
Console.WriteLine("=== Unboxing（拆箱）===");

int j = (int)o;  // Unboxing：將 object 轉回 int

Console.WriteLine($"j = {j}");
Console.WriteLine("Unboxing 時，CLR 會檢查型別並將值從 heap 複製回 stack");

Console.WriteLine();
Console.WriteLine("=== Unboxing 型別不符會拋出例外 ===");

try
{
    double d = (double)o;  // 錯誤！o 裡面是 int，不是 double
}
catch (InvalidCastException ex)
{
    Console.WriteLine($"InvalidCastException: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("=== 常見的隱性 Boxing 陷阱 ===");

int x = 10;

// 陷阱 1：string.Format 接受 object 參數
string s1 = string.Format("Score: {0}", x);  // x 被 boxing!
Console.WriteLine($"string.Format 結果: {s1}");

// 現代解法：字串插補（編譯器通常會優化）
string s2 = $"Score: {x}";
Console.WriteLine($"字串插補結果: {s2}");

Console.WriteLine();
Console.WriteLine("=== 使用泛型避免 Boxing ===");

// 非泛型集合（會 boxing）
var arrayList = new System.Collections.ArrayList();
arrayList.Add(1);  // int 被 boxing 成 object
arrayList.Add(2);

// 泛型集合（不會 boxing）
var list = new List<int>();
list.Add(1);  // 直接儲存 int，不需 boxing
list.Add(2);

Console.WriteLine("建議：優先使用泛型集合 List<T>，避免非泛型的 ArrayList");
