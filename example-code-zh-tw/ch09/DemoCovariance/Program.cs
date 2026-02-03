// 示範共變性（Covariance）與反變性（Contravariance）

Console.WriteLine("=== 共變性與反變性 ===\n");

// --------------------------------------------------------------
// 1. 陣列的共變性（不安全）
// --------------------------------------------------------------
Console.WriteLine("1. 陣列的共變性（不安全）");
Console.WriteLine(new string('-', 40));

string[] strings = new string[3] { "A", "B", "C" };
object[] objects = strings;  // 陣列共變性：合法

Console.WriteLine($"objects[0] = {objects[0]}");

// 危險：編譯 OK，但執行時會拋出 ArrayTypeMismatchException
try
{
    objects[0] = DateTime.Now;  // 嘗試將 DateTime 放入 string[]
}
catch (ArrayTypeMismatchException ex)
{
    Console.WriteLine($"錯誤：{ex.GetType().Name}");
    Console.WriteLine("陣列共變性不安全，執行時才會發現型別錯誤");
}

// --------------------------------------------------------------
// 2. 泛型的不變性
// --------------------------------------------------------------
Console.WriteLine("\n2. 泛型的不變性");
Console.WriteLine(new string('-', 40));

List<string> stringList = new List<string> { "Hello", "World" };
// List<object> objectList = stringList;  // 編譯錯誤！

Console.WriteLine("List<string> 不能指派給 List<object>");
Console.WriteLine("這是因為 List<T> 是不變的（Invariant）");

// --------------------------------------------------------------
// 3. IEnumerable<out T> 的共變性
// --------------------------------------------------------------
Console.WriteLine("\n3. IEnumerable<out T> 的共變性");
Console.WriteLine(new string('-', 40));

List<string> strList = new List<string> { "A", "B", "C" };
IEnumerable<object> objEnumerable = strList;  // OK！共變性

Console.WriteLine("List<string> 可以指派給 IEnumerable<object>");
foreach (var obj in objEnumerable)
{
    Console.WriteLine($"  {obj}");
}

// --------------------------------------------------------------
// 4. 自訂共變介面
// --------------------------------------------------------------
Console.WriteLine("\n4. 自訂共變介面（out T）");
Console.WriteLine(new string('-', 40));

IProducer<string> stringProducer = new StringProducer();
IProducer<object> objectProducer = stringProducer;  // 共變性：合法

Console.WriteLine($"objectProducer.GetValue() = {objectProducer.GetValue()}");

// --------------------------------------------------------------
// 5. IComparer<in T> 的反變性
// --------------------------------------------------------------
Console.WriteLine("\n5. IComparer<in T> 的反變性");
Console.WriteLine(new string('-', 40));

IComparer<object> objectComparer = new ObjectComparer();
IComparer<string> stringComparer = objectComparer;  // 反變性：合法

var list = new List<string> { "Cherry", "Apple", "Banana" };
list.Sort(stringComparer);

Console.WriteLine("使用 IComparer<object> 來排序 string：");
Console.WriteLine($"  {string.Join(", ", list)}");

// --------------------------------------------------------------
// 6. 委派的共變性與反變性
// --------------------------------------------------------------
Console.WriteLine("\n6. 委派的共變性與反變性");
Console.WriteLine(new string('-', 40));

// Func<out TResult> 是共變的
Func<string> stringFactory = () => "Hello";
Func<object> objectFactory = stringFactory;  // OK
Console.WriteLine($"objectFactory() = {objectFactory()}");

// Action<in T> 是反變的
Action<object> objectAction = obj => Console.WriteLine($"  處理：{obj}");
Action<string> stringAction = objectAction;  // OK
stringAction("Hello, World!");

// --------------------------------------------------------------
// 7. 記憶口訣
// --------------------------------------------------------------
Console.WriteLine("\n7. 記憶口訣");
Console.WriteLine(new string('-', 40));

Console.WriteLine("out = 輸出 = 共變（Covariance）");
Console.WriteLine("  子類別 → 父類別（跟著繼承方向走）");
Console.WriteLine("  例：IEnumerable<Dog> → IEnumerable<Animal>");

Console.WriteLine("\nin = 輸入 = 反變（Contravariance）");
Console.WriteLine("  父類別 → 子類別（與繼承方向相反）");
Console.WriteLine("  例：IComparer<Animal> → IComparer<Dog>");

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 輔助介面與類別
// ============================================================

// 共變介面（out T）
public interface IProducer<out T>
{
    T GetValue();
    // void SetValue(T value);  // 錯誤！T 不能在輸入位置
}

public class StringProducer : IProducer<string>
{
    public string GetValue() => "這是字串";
}

// 反變介面（in T）
public interface IConsumer<in T>
{
    void Process(T value);
    // T GetValue();  // 錯誤！T 不能在輸出位置
}

public class ObjectProcessor : IConsumer<object>
{
    public void Process(object value)
    {
        Console.WriteLine($"處理物件：{value}");
    }
}

// 用於排序的比較器
public class ObjectComparer : IComparer<object>
{
    public int Compare(object? x, object? y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;
        return string.Compare(x.ToString(), y.ToString());
    }
}
