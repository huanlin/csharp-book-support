// 示範串列模式（List Pattern）- C# 11+

Console.WriteLine("=== 串列模式範例 ===\n");

// --------------------------------------------------------------
// 1. 基本串列模式
// --------------------------------------------------------------
Console.WriteLine("1. 基本串列模式");
Console.WriteLine(new string('-', 40));

int[] exactMatch = [1, 2, 3, 4, 5];

if (exactMatch is [1, 2, 3, 4, 5])
{
    Console.WriteLine("[1, 2, 3, 4, 5]: 完全匹配");
}

// --------------------------------------------------------------
// 2. 使用底線匹配任意元素
// --------------------------------------------------------------
Console.WriteLine("\n2. 使用底線匹配任意元素");
Console.WriteLine(new string('-', 40));

int[] wildcardTest = [1, 99, 3, 88, 5];

if (wildcardTest is [1, _, 3, _, 5])
{
    Console.WriteLine("[1, _, 3, _, 5]: 第 2 和第 4 個元素可以是任何值");
}

// --------------------------------------------------------------
// 3. 切片模式
// --------------------------------------------------------------
Console.WriteLine("\n3. 切片模式");
Console.WriteLine(new string('-', 40));

int[] sliceTest = [1, 2, 3, 4, 5];

if (sliceTest is [1, .., 5])
{
    Console.WriteLine("[1, .., 5]: 以 1 開頭，以 5 結尾");
}

if (sliceTest is [1, ..])
{
    Console.WriteLine("[1, ..]: 以 1 開頭，後面可以是任何東西");
}

if (sliceTest is [.., 5])
{
    Console.WriteLine("[.., 5]: 以 5 結尾");
}

// --------------------------------------------------------------
// 4. 擷取切片
// --------------------------------------------------------------
Console.WriteLine("\n4. 擷取切片");
Console.WriteLine(new string('-', 40));

int[] numbers = [1, 2, 3, 4, 5];

if (numbers is [var first, .. var middle, var last])
{
    Console.WriteLine($"第一個：{first}");
    Console.WriteLine($"最後一個：{last}");
    Console.WriteLine($"中間：{string.Join(", ", middle)}");
}

// --------------------------------------------------------------
// 5. 實戰範例：命令列參數解析
// --------------------------------------------------------------
Console.WriteLine("\n5. 實戰範例：命令列參數解析");
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
// 6. 巢狀模式
// --------------------------------------------------------------
Console.WriteLine("\n6. 巢狀模式");
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
// 7. 綜合範例
// --------------------------------------------------------------
Console.WriteLine("\n7. 綜合範例");
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

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 模式比對方法
// ============================================================

static string ParseCommand(string[] args) => args switch
{
    ["help"] => "顯示說明",
    ["version"] => "顯示版本",
    ["copy", var src, var dest] => $"複製 {src} 到 {dest}",
    ["move", var src, var dest] => $"移動 {src} 到 {dest}",
    ["delete", var file] => $"刪除 {file}",
    [var cmd, ..] => $"未知命令：{cmd}",
    [] => "請輸入命令"
};

static string DescribeArray(int[] arr) => arr switch
{
    [> 0, > 0, > 0] => "所有元素都是正數",
    [1 or 2, _, < 10] => "第一個是 1 或 2，最後一個小於 10",
    [_, _, _] => "三個元素的陣列",
    _ => "其他"
};

static string CheckPattern(int[] values) => values switch
{
    [] => "空陣列",
    [1, 2, _, 10] => "包含 1, 2, 任一數字, 10",
    [1, 2, .., 10] => "包含 1, 2 開頭，10 結尾",
    [1, 2] => "包含 1 然後 2",
    [int item1, int item2, int item3] =>
        $"包含三個元素：{item1}, {item2}, {item3}",
    [0, _] => "以 0 開頭，後面跟一個數字",
    [0, ..] => "以 0 開頭，後面可以是任意數量",
    [2, .. int[] others] => $"以 2 開頭，後面還有 {others.Length} 個",
    [..] => "任意順序的任意元素"
};
