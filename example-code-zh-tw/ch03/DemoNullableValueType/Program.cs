// 示範可為 Null 的實值型別（Nullable Value Types）

// 基本語法：T? 等同於 Nullable<T>
int? nullableInt = null;
int? anotherNullable = 25;

Console.WriteLine("=== 3.2 可為 Null 的實值型別 ===\n");

// 賦值與取值
Console.WriteLine("【賦值與取值】");
Console.WriteLine($"nullableInt = {nullableInt?.ToString() ?? "null"}");
Console.WriteLine($"anotherNullable = {anotherNullable}");

// 判斷是否有值
Console.WriteLine("\n【判斷是否有值】");
int? score = null;

// 方法 1：比較運算子
if (score != null) 
{ 
    Console.WriteLine($"score (使用 != null): {score}"); 
}
else 
{
    Console.WriteLine("score 是 null（使用 != null 檢查）");
}

// 方法 2：HasValue 屬性（更明確）
if (score.HasValue) 
{ 
    Console.WriteLine($"score (使用 HasValue): {score.Value}"); 
}
else 
{
    Console.WriteLine("score 沒有值（使用 HasValue 檢查）");
}

// 與非 Nullable 型別的互操作
Console.WriteLine("\n【與非 Nullable 型別的互操作】");
int i = 10;
int? j = i;  // ✓ 隱含轉換
Console.WriteLine($"int i = {i} → int? j = {j}");

int? x = 10;
// int y = x;        // ✗ 編譯錯誤！
int z = (int)x;      // ✓ 明確轉換，但若 x 為 null 會在執行時拋出異常
Console.WriteLine($"int? x = {x} → int z = {z}（明確轉換）");

// Null 傳播運算
Console.WriteLine("\n【Null 傳播運算】");
int? a = null;
int? b = 10;
int? c = a + b;  // c 是 null
int? d = a * b;  // d 也是 null
Console.WriteLine($"a = null, b = 10");
Console.WriteLine($"a + b = {c?.ToString() ?? "null"}（null 傳播）");
Console.WriteLine($"a * b = {d?.ToString() ?? "null"}（null 傳播）");

// Nullable 與一般型別混合運算
Console.WriteLine("\n【混合運算】");
int? m = 10;
int n = 5;
int? result = m * n;  // result = 50（型別是 int?）
Console.WriteLine($"int? m = {m}, int n = {n}");
Console.WriteLine($"m * n = {result}（結果型別為 int?）");

// 實用範例：使用 GetValueOrDefault
Console.WriteLine("\n【GetValueOrDefault 方法】");
int? maybeAge = null;
int age = maybeAge.GetValueOrDefault(18);  // 若為 null，使用預設值 18
Console.WriteLine($"maybeAge = null → GetValueOrDefault(18) = {age}");

maybeAge = 30;
age = maybeAge.GetValueOrDefault(18);
Console.WriteLine($"maybeAge = 30 → GetValueOrDefault(18) = {age}");
