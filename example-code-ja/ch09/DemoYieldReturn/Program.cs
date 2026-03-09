// デモ: 基本的な yield return

Console.WriteLine("=== 1. 基本的な yield return ===");
Console.WriteLine(new string('-', 40));

Console.WriteLine("GetNumbers() を呼び出し...");
var numbers = GetNumbers(); // ここではまだ出力されない
Console.WriteLine("foreach を開始...\n");

foreach (var n in numbers)
{
    Console.WriteLine($"受信 {n}");
}

Console.WriteLine("\n=== 例の終了 ===");

// --------------------------------------------------------------
// イテレーターメソッド
// --------------------------------------------------------------

static IEnumerable<int> GetNumbers()
{
    Console.WriteLine("  1 を yield");
    yield return 1;

    Console.WriteLine("  2 を yield");
    yield return 2;

    Console.WriteLine("  3 を yield");
    yield return 3;
}
