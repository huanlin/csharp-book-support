// デモ: クロージャーとループ変数キャプチャの落とし穴

Console.WriteLine("=== クロージャーキャプチャとループの落とし穴 ===\n");

// --------------------------------------------------------------
// 1. 変数キャプチャの落とし穴
// --------------------------------------------------------------
Console.WriteLine("1. 変数キャプチャの落とし穴");
Console.WriteLine(new string('-', 40));

int[] numbers = [1, 2];
int factor = 10;
IEnumerable<int> queryWithCapture = numbers.Select(n => n * factor);

Console.WriteLine($"factor = {factor}");
factor = 20;
Console.WriteLine($"factor を変更 = {factor}");
Console.WriteLine($"クエリ結果: {string.Join(", ", queryWithCapture)}");
Console.WriteLine("（遅延実行のため変更後の 20 が使われる）");

// --------------------------------------------------------------
// 2. ループキャプチャの落とし穴と対策
// --------------------------------------------------------------
Console.WriteLine("\n2. ループキャプチャの落とし穴と対策");
Console.WriteLine(new string('-', 40));

string testString = "Not what you might expect";
string vowels = "aeiou";

Console.WriteLine($"元文字列: \"{testString}\"");

// 誤った書き方: for ループが i をそのまま捕捉する
IEnumerable<char> brokenQuery = testString;
for (int i = 0; i < vowels.Length; i++)
    brokenQuery = brokenQuery.Where(c => c != vowels[i]);

try
{
    Console.WriteLine($"for の誤った版: \"{string.Concat(brokenQuery)}\"");
}
catch (IndexOutOfRangeException ex)
{
    Console.WriteLine($"for の誤った版は {ex.GetType().Name} を送出");
}

// 対策1: foreach
IEnumerable<char> query1 = testString;
foreach (char vowel in vowels)
    query1 = query1.Where(c => c != vowel);

Console.WriteLine($"foreach で母音除去: \"{string.Concat(query1)}\"");

// 対策2: for 内でローカルコピーを作る
IEnumerable<char> query2 = testString;
for (int i = 0; i < vowels.Length; i++)
{
    char vowel = vowels[i];
    query2 = query2.Where(c => c != vowel);
}
Console.WriteLine($"for + コピーで母音除去: \"{string.Concat(query2)}\"");

Console.WriteLine("\n=== 例の終了 ===");
