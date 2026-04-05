// 示範集合運算式（Collection Expressions）

PrintSection("1. 初始化集合");
int[] numbers = [1, 2, 3];
List<string> strings = ["A", "B", "C"];
Console.WriteLine($"numbers = [{string.Join(", ", numbers)}]");
Console.WriteLine($"strings = [{string.Join(", ", strings)}]");

PrintSection("2. 合併多個集合");
int[] section1 = [1, 2];
int[] section2 = [5, 6];
int[] allLevels = [0, .. section1, 3, 4, .. section2, 7];
Console.WriteLine($"allLevels = [{string.Join(", ", allLevels)}]");

PrintSection("3. 傳遞參數給方法");
PrintTags(["C#", ".NET", "Coding"]);

Console.WriteLine();
Console.WriteLine("PrintNumbers 的三種呼叫方式:");
PrintNumbers(1, 2, 3);
PrintNumbers([1, 2, 3]);
PrintNumbers(new List<int> { 1, 2, 3 });

PrintSection("4. 需要空集合時");
int[] emptyArray = [];
List<string> tags = [];
ReadOnlySpan<int> values = [];
Console.WriteLine($"emptyArray.Length = {emptyArray.Length}");
Console.WriteLine($"tags.Count = {tags.Count}");
Console.WriteLine($"values.Length = {values.Length}");

PrintSection("5. Span<T> / ReadOnlySpan<T> 範例");
ReadOnlySpan<byte> header = [0xDE, 0xAD, 0xBE, 0xEF];
Console.WriteLine($"header = [{string.Join(", ", header.ToArray().Select(b => $"0x{b:X2}"))}]");

PrintSection("6. Dictionary 初始化的限制");
var scores = new Dictionary<string, int>
{
    ["Alice"] = 95,
    ["Bob"] = 87
};
Dictionary<string, int> emptyScores = [];
Console.WriteLine($"scores[\"Alice\"] = {scores["Alice"]}");
Console.WriteLine($"emptyScores.Count = {emptyScores.Count}");

PrintSection("7. 無法靠 var 推斷型別");
// var inferred = [1, 2, 3];  // ✗ 編譯失敗：collection expression 沒有固定自然型別
Console.WriteLine("請參考上方註解：collection expression 不能直接搭配 var。");

static void PrintSection(string title)
{
    Console.WriteLine();
    Console.WriteLine(title);
    Console.WriteLine(new string('-', title.Length));
}

static void PrintTags(ReadOnlySpan<string> tags)
{
    Console.WriteLine("PrintTags:");
    foreach (var tag in tags)
    {
        Console.WriteLine($"  {tag}");
    }
}

static void PrintNumbers(params IList<int> numbers)
{
    Console.WriteLine($"  [{string.Join(", ", numbers)}]");
}
