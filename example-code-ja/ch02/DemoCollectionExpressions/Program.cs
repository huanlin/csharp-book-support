// デモ: コレクション式

// 1. 基本初期化: 同じ右辺の構文で、異なるターゲット型を作れる
int[] array = [1, 2, 3];
List<int> list = [1, 2, 3];
Span<int> span = [1, 2, 3];

Console.WriteLine("基本初期化:");
Console.WriteLine($"  array = [{string.Join(", ", array)}]");
Console.WriteLine($"  list  = [{string.Join(", ", list)}]");
Console.WriteLine($"  span  = [{string.Join(", ", span.ToArray())}]");

// 2. target-typed: メソッド引数もターゲット型を与えられる
Console.WriteLine($"\nSum([1, 2, 3]) = {Sum([1, 2, 3])}");

// 3. 空コレクション
int[] emptyArray = [];
List<string> names = [];
Console.WriteLine($"\n空コレクション: emptyArray.Length = {emptyArray.Length}, names.Count = {names.Count}");

// 4. spread 構文: 既存のシーケンスを展開する
int[] source = [1, 2, 3];
int[] numbers = [0, .. source, 4];
List<int> copied = [.. source];

Console.WriteLine("\nspread:");
Console.WriteLine($"  source = [{string.Join(", ", source)}]");
Console.WriteLine($"  numbers = [{string.Join(", ", numbers)}]");
Console.WriteLine($"  copied = [{string.Join(", ", copied)}]");

static int Sum(int[] values)
{
    int total = 0;
    foreach (int x in values)
    {
        total += x;
    }
    return total;
}
