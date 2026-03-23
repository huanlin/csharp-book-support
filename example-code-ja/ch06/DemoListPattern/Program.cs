// デモ: リスト パターン（C# 11+）

Console.WriteLine("=== リスト パターンの例 ===\n");

// --------------------------------------------------------------
// 1. 基本的なリスト パターン
// --------------------------------------------------------------
Console.WriteLine("1. 基本的なリスト パターン");
Console.WriteLine(new string('-', 40));

int[] exactMatch = [1, 2, 3, 4, 5];

if (exactMatch is [1, 2, 3, 4, 5])
{
    Console.WriteLine("[1, 2, 3, 4, 5]: 完全一致");
}

// --------------------------------------------------------------
// 2. _ を使って任意要素にマッチ
// --------------------------------------------------------------
Console.WriteLine("\n2. _ を使って任意要素にマッチ");
Console.WriteLine(new string('-', 40));

int[] wildcardTest = [1, 99, 3, 88, 5];

if (wildcardTest is [1, _, 3, _, 5])
{
    Console.WriteLine("[1, _, 3, _, 5]: 2番目と4番目は任意の値");
}

// --------------------------------------------------------------
// 3. スライスパターン
// --------------------------------------------------------------
Console.WriteLine("\n3. スライスパターン");
Console.WriteLine(new string('-', 40));

int[] sliceTest = [1, 2, 3, 4, 5];

if (sliceTest is [1, .., 5])
{
    Console.WriteLine("[1, .., 5]: 先頭が1で末尾が5");
}

if (sliceTest is [1, ..])
{
    Console.WriteLine("[1, ..]: 先頭が1で後続は任意");
}

if (sliceTest is [.., 5])
{
    Console.WriteLine("[.., 5]: 末尾が5");
}

// --------------------------------------------------------------
// 4. スライスのキャプチャ
// --------------------------------------------------------------
Console.WriteLine("\n4. スライスのキャプチャ");
Console.WriteLine(new string('-', 40));

int[] numbers = [1, 2, 3, 4, 5];

if (numbers is [var first, .. var middle, var last])
{
    Console.WriteLine($"先頭: {first}");
    Console.WriteLine($"末尾: {last}");
    Console.WriteLine($"中間: {string.Join(", ", middle)}");
}

// --------------------------------------------------------------
// 5. 実践例: コマンドライン引数解析
// --------------------------------------------------------------
Console.WriteLine("\n5. 実践例: コマンドライン引数解析");
Console.WriteLine(new string('-', 40));

string[][] commandTests =
[
    ["help"],
    ["version"],
    ["copy", "source.txt", "dest.txt"],
    ["move", "old.txt", "new.txt"],
    ["delete", "temp.txt"],
    ["unknown", "arg1", "arg2"],
    []
];

foreach (string[] cmdArgs in commandTests)
{
    string result = ParseCommand(cmdArgs);
    Console.WriteLine($"[{string.Join(", ", cmdArgs)}] -> {result}");
}

// --------------------------------------------------------------
// 6. ネストしたパターン
// --------------------------------------------------------------
Console.WriteLine("\n6. ネストしたパターン");
Console.WriteLine(new string('-', 40));

int[][] testArrays =
[
    [1, 2, 3],
    [-1, 0, 5],
    [1, 5, 3],
    [2, 4, 8]
];

foreach (int[] arr in testArrays)
{
    string description = DescribeArray(arr);
    Console.WriteLine($"[{string.Join(", ", arr)}]: {description}");
}

// --------------------------------------------------------------
// 7. 総合例
// --------------------------------------------------------------
Console.WriteLine("\n7. 総合例");
Console.WriteLine(new string('-', 40));

int[][] patternTests =
[
    [],
    [1, 2],
    [1, 2, 99, 10],
    [1, 2, 3, 4, 5, 6, 7, 8, 9, 10],
    [9, 7, 5],
    [0, 1, 2, 3],
    [2, 4, 6, 8, 10]
];

foreach (int[] values in patternTests)
{
    string pattern = CheckPattern(values);
    Console.WriteLine($"[{string.Join(", ", values)}] -> {pattern}");
}

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// パターンマッチメソッド
// ============================================================

static string ParseCommand(string[] args) => args switch
{
    ["help"] => "ヘルプ表示",
    ["version"] => "バージョン表示",
    ["copy", var src, var dest] => $"{src} を {dest} にコピー",
    ["move", var src, var dest] => $"{src} を {dest} に移動",
    ["delete", var file] => $"{file} を削除",
    [var cmd, ..] => $"不明コマンド: {cmd}",
    [] => "コマンドを入力してください"
};

static string DescribeArray(int[] arr) => arr switch
{
    [> 0, > 0, > 0] => "すべて正の値",
    [1 or 2, _, < 10] => "先頭が1または2で、末尾が10未満",
    [_, _, _] => "3要素の配列",
    _ => "その他"
};

static string CheckPattern(int[] values) => values switch
{
    [] => "空配列",
    [1, 2, _, 10] => "1, 2, 任意, 10 を含む",
    [1, 2, .., 10] => "1, 2 で始まり 10 で終わる",
    [1, 2] => "1 の次が 2",
    [int item1, int item2, int item3] =>
        $"3要素: {item1}, {item2}, {item3}",
    [0, _] => "0 で始まり、続いて1要素",
    [0, ..] => "0 で始まり、後続は任意",
    [2, .. int[] others] => $"2 で始まり、後続 {others.Length} 要素",
    [..] => "任意の内容、任意の長さ"
};
