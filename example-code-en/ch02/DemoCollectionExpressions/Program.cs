// Demo: collection expressions

PrintSection("1. Initializing collections");
int[] numbers = [1, 2, 3];
List<string> strings = ["A", "B", "C"];
Console.WriteLine($"numbers = [{string.Join(", ", numbers)}]");
Console.WriteLine($"strings = [{string.Join(", ", strings)}]");

PrintSection("2. Combining multiple collections");
int[] section1 = [1, 2];
int[] section2 = [5, 6];
int[] allLevels = [0, .. section1, 3, 4, .. section2, 7];
Console.WriteLine($"allLevels = [{string.Join(", ", allLevels)}]");

PrintSection("3. Passing collection arguments");
PrintTags(["C#", ".NET", "Coding"]);

Console.WriteLine();
Console.WriteLine("Three ways to call PrintNumbers:");
PrintNumbers(1, 2, 3);
PrintNumbers([1, 2, 3]);
PrintNumbers(new List<int> { 1, 2, 3 });

PrintSection("4. Creating empty collections");
int[] emptyArray = [];
List<string> tags = [];
ReadOnlySpan<int> values = [];
Console.WriteLine($"emptyArray.Length = {emptyArray.Length}");
Console.WriteLine($"tags.Count = {tags.Count}");
Console.WriteLine($"values.Length = {values.Length}");

PrintSection("5. Span<T> / ReadOnlySpan<T> example");
ReadOnlySpan<byte> header = [0xDE, 0xAD, 0xBE, 0xEF];
Console.WriteLine($"header = [{string.Join(", ", header.ToArray().Select(b => $"0x{b:X2}"))}]");

PrintSection("6. Limitations with dictionary initialization");
var scores = new Dictionary<string, int>
{
    ["Alice"] = 95,
    ["Bob"] = 87
};
Dictionary<string, int> emptyScores = [];
Console.WriteLine($"scores[\"Alice\"] = {scores["Alice"]}");
Console.WriteLine($"emptyScores.Count = {emptyScores.Count}");

PrintSection("7. No type inference with var");
// var inferred = [1, 2, 3];  // ✗ Won't compile: collection expressions do not have a fixed natural type
Console.WriteLine("See the comment above: a collection expression cannot be used directly with var.");

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
