// デモ: 共変性と反変性

Console.WriteLine("=== 共変性と反変性 ===\n");

// --------------------------------------------------------------
// 1. 配列共変（安全ではない）
// --------------------------------------------------------------
Console.WriteLine("1. 配列共変（安全ではない）");
Console.WriteLine(new string('-', 40));

string[] strings = new string[3] { "A", "B", "C" };
object[] objects = strings;

Console.WriteLine($"objects[0] = {objects[0]}");

try
{
    objects[0] = DateTime.Now;
}
catch (ArrayTypeMismatchException ex)
{
    Console.WriteLine($"エラー: {ex.GetType().Name}");
    Console.WriteLine("配列共変は安全ではなく、型エラーは実行時に発覚する。");
}

// --------------------------------------------------------------
// 2. ジェネリックの不変性
// --------------------------------------------------------------
Console.WriteLine("\n2. ジェネリックの不変性");
Console.WriteLine(new string('-', 40));

List<string> stringList = new List<string> { "Hello", "World" };
// List<object> objectList = stringList;  // コンパイルエラー

Console.WriteLine("List<string> は List<object> へ代入できない");
Console.WriteLine("List<T> は不変（invariant）だから。");

// --------------------------------------------------------------
// 3. IEnumerable<out T> の共変
// --------------------------------------------------------------
Console.WriteLine("\n3. IEnumerable<out T> の共変");
Console.WriteLine(new string('-', 40));

List<string> strList = new List<string> { "A", "B", "C" };
IEnumerable<object> objEnumerable = strList;

Console.WriteLine("List<string> は IEnumerable<object> に代入可能");
foreach (var obj in objEnumerable)
{
    Console.WriteLine($"  {obj}");
}

// --------------------------------------------------------------
// 4. カスタム共変インターフェイス（out T）
// --------------------------------------------------------------
Console.WriteLine("\n4. カスタム共変インターフェイス（out T）");
Console.WriteLine(new string('-', 40));

IProducer<string> stringProducer = new StringProducer();
IProducer<object> objectProducer = stringProducer;

Console.WriteLine($"objectProducer.GetValue() = {objectProducer.GetValue()}");

// --------------------------------------------------------------
// 5. IComparer<in T> の反変
// --------------------------------------------------------------
Console.WriteLine("\n5. IComparer<in T> の反変");
Console.WriteLine(new string('-', 40));

IComparer<object> objectComparer = new ObjectComparer();
IComparer<string> stringComparer = objectComparer;

var list = new List<string> { "Cherry", "Apple", "Banana" };
list.Sort(stringComparer);

Console.WriteLine("IComparer<object> で string をソート:");
Console.WriteLine($"  {string.Join(", ", list)}");

// --------------------------------------------------------------
// 6. デリゲートの共変/反変
// --------------------------------------------------------------
Console.WriteLine("\n6. デリゲートの共変/反変");
Console.WriteLine(new string('-', 40));

Func<string> stringFactory = () => "Hello";
Func<object> objectFactory = stringFactory;
Console.WriteLine($"objectFactory() = {objectFactory()}");

Action<object> objectAction = obj => Console.WriteLine($"  処理対象: {obj}");
Action<string> stringAction = objectAction;
stringAction("Hello, World!");

// --------------------------------------------------------------
// 7. 覚え方
// --------------------------------------------------------------
Console.WriteLine("\n7. 覚え方");
Console.WriteLine(new string('-', 40));

Console.WriteLine("out = Output = 共変");
Console.WriteLine("  派生型 -> 基底型（継承方向と同じ）");
Console.WriteLine("  例: IEnumerable<Dog> -> IEnumerable<Animal>");

Console.WriteLine("\nin = Input = 反変");
Console.WriteLine("  基底型 -> 派生型（継承方向と逆）");
Console.WriteLine("  例: IComparer<Animal> -> IComparer<Dog>");

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// ヘルパー
// ============================================================

public interface IProducer<out T>
{
    T GetValue();
}

public class StringProducer : IProducer<string>
{
    public string GetValue() => "This is a string";
}

public interface IConsumer<in T>
{
    void Process(T value);
}

public class ObjectProcessor : IConsumer<object>
{
    public void Process(object value)
    {
        Console.WriteLine($"オブジェクト処理: {value}");
    }
}

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
