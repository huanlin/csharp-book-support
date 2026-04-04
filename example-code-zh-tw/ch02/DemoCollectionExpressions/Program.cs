// 示範集合運算式（Collection Expressions）

// 1. 基本初始化：相同的右邊寫法，可對應不同目標型別
int[] array = [1, 2, 3];
List<int> list = [1, 2, 3];
Span<int> span = [1, 2, 3];

Console.WriteLine("基本初始化:");
Console.WriteLine($"  array = [{string.Join(", ", array)}]");
Console.WriteLine($"  list  = [{string.Join(", ", list)}]");
Console.WriteLine($"  span  = [{string.Join(", ", span.ToArray())}]");

// 2. target-typed：方法引數也能提供目標型別
Console.WriteLine($"\nSum([1, 2, 3]) = {Sum([1, 2, 3])}");

// 3. 空集合
int[] emptyArray = [];
List<string> names = [];
Console.WriteLine($"\n空集合: emptyArray.Length = {emptyArray.Length}, names.Count = {names.Count}");

// 4. spread 語法：展開既有序列
int[] source = [1, 2, 3];
int[] numbers = [0, .. source, 4];
List<int> copied = [.. source];

Console.WriteLine($"\nspread:");
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
