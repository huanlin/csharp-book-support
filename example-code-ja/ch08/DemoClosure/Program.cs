// デモ: クロージャーと変数キャプチャ

Console.WriteLine("=== クロージャーと変数キャプチャ ===\n");

// --------------------------------------------------------------
// 1. 外部変数キャプチャの基本
// --------------------------------------------------------------
Console.WriteLine("1. 外部変数キャプチャの基本");
Console.WriteLine(new string('-', 40));

int threshold = 10;

Func<int, bool> isAboveThreshold = n => n > threshold;

Console.WriteLine($"threshold = {threshold}");
Console.WriteLine($"isAboveThreshold(15) = {isAboveThreshold(15)}");
Console.WriteLine($"isAboveThreshold(5) = {isAboveThreshold(5)}");

threshold = 20;
Console.WriteLine($"\nthreshold を変更 = {threshold}");
Console.WriteLine($"isAboveThreshold(15) = {isAboveThreshold(15)}");

// --------------------------------------------------------------
// 2. クロージャーによる寿命延長
// --------------------------------------------------------------
Console.WriteLine("\n2. クロージャーによる寿命延長");
Console.WriteLine(new string('-', 40));

var counter = CreateCounter();
Console.WriteLine($"counter() = {counter()}");
Console.WriteLine($"counter() = {counter()}");
Console.WriteLine($"counter() = {counter()}");
Console.WriteLine("（count 変数はデリゲートと同じ寿命で保持される）");

static Func<int> CreateCounter()
{
    int count = 0;
    return () => ++count;
}

// --------------------------------------------------------------
// 3. for ループのクロージャートラップ
// --------------------------------------------------------------
Console.WriteLine("\n3. for ループのクロージャートラップ");
Console.WriteLine(new string('-', 40));

Console.WriteLine("誤った例（全ラムダが同じ i を参照）:");
var wrongActions = new List<Action>();
for (int i = 0; i < 3; i++)
{
    wrongActions.Add(() => Console.Write($"{i} "));
}
Console.Write("  出力: ");
foreach (var action in wrongActions)
    action();
Console.WriteLine("（期待: 0 1 2）");

Console.WriteLine("\n正しい例 1（ローカルコピーを作る）:");
var correctActions1 = new List<Action>();
for (int i = 0; i < 3; i++)
{
    int copy = i;
    correctActions1.Add(() => Console.Write($"{copy} "));
}
Console.Write("  出力: ");
foreach (var action in correctActions1)
    action();
Console.WriteLine();

Console.WriteLine("\n正しい例 2（foreach を使う）:");
var numbers = new[] { 0, 1, 2 };
var correctActions2 = new List<Action>();
foreach (var num in numbers)
{
    correctActions2.Add(() => Console.Write($"{num} "));
}
Console.Write("  出力: ");
foreach (var action in correctActions2)
    action();
Console.WriteLine();

// --------------------------------------------------------------
// 4. キャプチャされる値のタイミング
// --------------------------------------------------------------
Console.WriteLine("\n4. キャプチャされる値のタイミング");
Console.WriteLine(new string('-', 40));

int factor = 10;
Func<int, int> multiply = n => n * factor;

Console.WriteLine($"factor = {factor}");
factor = 20;
Console.WriteLine($"factor を変更 = {factor}");
Console.WriteLine($"multiply(5) = {multiply(5)}");
Console.WriteLine("（ラムダは実行時点の factor を参照する）");

// --------------------------------------------------------------
// 5. 母音除去の例
// --------------------------------------------------------------
Console.WriteLine("\n5. 母音除去の例");
Console.WriteLine(new string('-', 40));

string testString = "Not what you might expect";
string vowels = "aeiou";

IEnumerable<char> query = testString;
foreach (char vowel in vowels)
    query = query.Where(c => c != vowel);

Console.WriteLine($"元文字列: \"{testString}\"");
Console.WriteLine($"母音除去後: \"{string.Concat(query)}\"");

// --------------------------------------------------------------
// 6. コンパイラがクロージャーをどう扱うか
// --------------------------------------------------------------
Console.WriteLine("\n6. コンパイラがクロージャーをどう扱うか");
Console.WriteLine(new string('-', 40));

Console.WriteLine("ラムダが外部変数をキャプチャすると、コンパイラは:");
Console.WriteLine("  1. 変数保持用の隠れクラス（DisplayClass）を生成");
Console.WriteLine("  2. ラムダをそのクラスのメソッドへ変換");
Console.WriteLine("  3. 元変数へのアクセスをクラスフィールドアクセスへ置換");

Console.WriteLine("\n=== 例の終了 ===\n");
