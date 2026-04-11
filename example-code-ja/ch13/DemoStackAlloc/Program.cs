// デモ: stackalloc とコレクション式
using System.Runtime.InteropServices;

Console.WriteLine("=== stackalloc とコレクション式 ===\n");

// --------------------------------------------------------------
// 1. stackalloc の基本利用
// --------------------------------------------------------------
Console.WriteLine("1. stackalloc 配列割り当て");
Console.WriteLine(new string('-', 40));

// 従来構文（C# 7.2+）
Span<int> numbers = stackalloc int[] { 1, 2, 3, 4, 5 };
Console.WriteLine($"数値[0]: {numbers[0]}");

// 値を変更
numbers[0] = 99;
Console.WriteLine($"変更後 数値[0]: {numbers[0]}");

// --------------------------------------------------------------
// 2. コレクション式（C# 12）
// --------------------------------------------------------------
Console.WriteLine("\n2. コレクション式（C# 12）");
Console.WriteLine(new string('-', 40));

// この書き方は簡潔だが、実際にどう lower されるかはコンパイラ次第
// 明示的な stackalloc と同等だという言語保証として受け取らないこと
Span<int> data = [10, 20, 30, 40, 50];

Console.WriteLine($"データ[0]: {data[0]}");

// --------------------------------------------------------------
// 3. ループ初期化
// --------------------------------------------------------------
Console.WriteLine("\n3. 動的初期化（ループ）");
Console.WriteLine(new string('-', 40));

// 小さな一時領域に適している
int length = 5;
Span<int> buffer = stackalloc int[length];

for (int i = 0; i < length; i++)
{
    buffer[i] = i * i;
}

Console.WriteLine($"バッファ: [{string.Join(", ", buffer.ToArray())}]");

// --------------------------------------------------------------
// 4. ネイティブメモリ連携（上級）
// --------------------------------------------------------------
Console.WriteLine("\n4. ネイティブメモリ（Unsafe）");
Console.WriteLine(new string('-', 40));

// 注: unsafe ブロック（ポインター操作など）を使うには
// .csproj に <AllowUnsafeBlocks>true</AllowUnsafeBlocks> が必要
unsafe
{
    // ポインターが必要ならこう書けるが、通常は Span で十分
    int* ptr = stackalloc int[3];
    ptr[0] = 100;
    ptr[1] = 200;
    ptr[2] = 300;
    
    Console.WriteLine($"ポインター[1]: {ptr[1]}");
}

Console.WriteLine("\n=== 例の終了 ===");

