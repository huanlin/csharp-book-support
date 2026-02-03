// 示範 stackalloc 與集合運算式
using System.Runtime.InteropServices;

Console.WriteLine("=== stackalloc 與 Collection Expressions ===\n");

// --------------------------------------------------------------
// 1. stackalloc 基本用法
// --------------------------------------------------------------
Console.WriteLine("1. stackalloc 陣列配置");
Console.WriteLine(new string('-', 40));

// 傳統語法 (C# 7.2+)
Span<int> numbers = stackalloc int[] { 1, 2, 3, 4, 5 };
Console.WriteLine($"Numbers[0]: {numbers[0]}");

// 修改
numbers[0] = 99;
Console.WriteLine($"修改後 Numbers[0]: {numbers[0]}");

// --------------------------------------------------------------
// 2. Collection Expressions (C# 12)
// --------------------------------------------------------------
Console.WriteLine("\n2. Collection Expressions (C# 12)");
Console.WriteLine(new string('-', 40));

// 編譯器會自動最佳化：
// 如果目標是 Span<T>，它會傾向使用 stackalloc (如果長度已知且夠小)
// 否則會建立一般陣列
Span<int> data = [10, 20, 30, 40, 50];

Console.WriteLine($"Data[0]: {data[0]}");

// --------------------------------------------------------------
// 3. 迴圈初始化
// --------------------------------------------------------------
Console.WriteLine("\n3. 動態初始化 (Loop)");
Console.WriteLine(new string('-', 40));

// 僅適合小型暫存
int length = 5;
Span<int> buffer = stackalloc int[length];

for (int i = 0; i < length; i++)
{
    buffer[i] = i * i;
}

Console.WriteLine($"Buffer: [{string.Join(", ", buffer.ToArray())}]");

// --------------------------------------------------------------
// 4. 原生記憶體互通 (進階)
// --------------------------------------------------------------
Console.WriteLine("\n4. 原生記憶體 (Unsafe)");
Console.WriteLine(new string('-', 40));

// 注意：若要使用 unsafe 區塊（如指標操作），
// 必須在 .csproj 中設定 <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
unsafe
{
    // 如果需要指標，可以這樣做，但通常 Span 就夠了
    int* ptr = stackalloc int[3];
    ptr[0] = 100;
    ptr[1] = 200;
    ptr[2] = 300;
    
    Console.WriteLine($"Pointer[1]: {ptr[1]}");
}

Console.WriteLine("\n=== 範例結束 ===");
