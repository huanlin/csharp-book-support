// デモ: Nullable Value Types

// 基本構文: T? は Nullable<T> と同等
int? nullableInt = null;
int? anotherNullable = 25;

Console.WriteLine("=== 3.2 Nullable Value Types ===\n");

// 代入と取得
Console.WriteLine("[代入と取得]");
Console.WriteLine($"nullableInt = {nullableInt?.ToString() ?? "null"}");
Console.WriteLine($"anotherNullable = {anotherNullable}");

// 値があるかどうかの判定
Console.WriteLine("\n[値があるかどうかの判定]");
int? score = null;

// 方法1: 比較演算子
if (score != null)
{
    Console.WriteLine($"score（!= null）: {score}");
}
else
{
    Console.WriteLine("score は null（!= null で判定）");
}

// 方法2: HasValue プロパティ（より明示的）
if (score.HasValue)
{
    Console.WriteLine($"score（HasValue）: {score.Value}");
}
else
{
    Console.WriteLine("score は値を持たない（HasValue で判定）");
}

// 非 Nullable 型との相互運用
Console.WriteLine("\n[非 Nullable 型との相互運用]");
int i = 10;
int? j = i;  // ✓ 暗黙変換
Console.WriteLine($"int i = {i} → int? j = {j}");

int? x = 10;
// int y = x;        // ✗ コンパイルエラー
int z = (int)x;      // ✓ 明示変換（x が null だと実行時例外）
Console.WriteLine($"int? x = {x} → int z = {z}（明示変換）");

// null 伝播演算
Console.WriteLine("\n[null 伝播演算]");
int? a = null;
int? b = 10;
int? c = a + b;  // c は null
int? d = a * b;  // d も null
Console.WriteLine("a = null, b = 10");
Console.WriteLine($"a + b = {c?.ToString() ?? "null"}（null 伝播）");
Console.WriteLine($"a * b = {d?.ToString() ?? "null"}（null 伝播）");

// 混合演算
Console.WriteLine("\n[混合演算]");
int? m = 10;
int n = 5;
int? result = m * n;  // result = 50（型は int?）
Console.WriteLine($"int? m = {m}, int n = {n}");
Console.WriteLine($"m * n = {result}（result 型は int?）");

// GetValueOrDefault メソッド
Console.WriteLine("\n[GetValueOrDefault メソッド]");
int? maybeAge = null;
int age = maybeAge.GetValueOrDefault(18);  // null なら既定値 18
Console.WriteLine($"maybeAge = null → GetValueOrDefault(18) = {age}");

maybeAge = 30;
age = maybeAge.GetValueOrDefault(18);
Console.WriteLine($"maybeAge = 30 → GetValueOrDefault(18) = {age}");
