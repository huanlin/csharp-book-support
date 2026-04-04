// Demo: collection expressions

// 1. Basic initialization: the same right-hand syntax can target different types
int[] array = [1, 2, 3];
List<int> list = [1, 2, 3];
Span<int> span = [1, 2, 3];

Console.WriteLine("Basic initialization:");
Console.WriteLine($"  array = [{string.Join(", ", array)}]");
Console.WriteLine($"  list  = [{string.Join(", ", list)}]");
Console.WriteLine($"  span  = [{string.Join(", ", span.ToArray())}]");

// 2. Target-typed behavior: method arguments can provide the target type too
Console.WriteLine($"\nSum([1, 2, 3]) = {Sum([1, 2, 3])}");

// 3. Empty collections
int[] emptyArray = [];
List<string> names = [];
Console.WriteLine($"\nEmpty collections: emptyArray.Length = {emptyArray.Length}, names.Count = {names.Count}");

// 4. Spread syntax: expand an existing sequence
int[] source = [1, 2, 3];
int[] numbers = [0, .. source, 4];
List<int> copied = [.. source];

Console.WriteLine("\nSpread:");
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
