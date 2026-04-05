// デモ: コレクション式

PrintSection("1. コレクションを初期化するとき");
int[] numbers = [1, 2, 3];
List<string> strings = ["A", "B", "C"];
Console.WriteLine($"numbers = [{string.Join(", ", numbers)}]");
Console.WriteLine($"strings = [{string.Join(", ", strings)}]");

PrintSection("2. 複数のコレクションを組み合わせるとき");
int[] section1 = [1, 2];
int[] section2 = [5, 6];
int[] allLevels = [0, .. section1, 3, 4, .. section2, 7];
Console.WriteLine($"allLevels = [{string.Join(", ", allLevels)}]");

PrintSection("3. メソッドにコレクション引数を渡すとき");
PrintTags(["C#", ".NET", "Coding"]);

Console.WriteLine();
Console.WriteLine("PrintNumbers の 3 つの呼び出し方:");
PrintNumbers(1, 2, 3);
PrintNumbers([1, 2, 3]);
PrintNumbers(new List<int> { 1, 2, 3 });

PrintSection("4. 空コレクションが必要なとき");
int[] emptyArray = [];
List<string> tags = [];
ReadOnlySpan<int> values = [];
Console.WriteLine($"emptyArray.Length = {emptyArray.Length}");
Console.WriteLine($"tags.Count = {tags.Count}");
Console.WriteLine($"values.Length = {values.Length}");

PrintSection("5. Span<T> / ReadOnlySpan<T> の例");
ReadOnlySpan<byte> header = [0xDE, 0xAD, 0xBE, 0xEF];
Console.WriteLine($"header = [{string.Join(", ", header.ToArray().Select(b => $"0x{b:X2}"))}]");

PrintSection("6. Dictionary 初期化の制約");
var scores = new Dictionary<string, int>
{
    ["Alice"] = 95,
    ["Bob"] = 87
};
Dictionary<string, int> emptyScores = [];
Console.WriteLine($"scores[\"Alice\"] = {scores["Alice"]}");
Console.WriteLine($"emptyScores.Count = {emptyScores.Count}");

PrintSection("7. var による型推論はできない");
// var inferred = [1, 2, 3];  // ✗ コンパイル エラー: コレクション式には固定の自然型がない
Console.WriteLine("上のコメントを参照してください。コレクション式は var と直接組み合わせられません。");

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
