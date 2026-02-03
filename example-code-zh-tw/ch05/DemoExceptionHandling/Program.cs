// 示範例外處理的各種語法與最佳實踐

Console.WriteLine("=== 例外處理範例 ===\n");

// --------------------------------------------------------------
// 1. TryParse vs Parse：處理可預期的錯誤
// --------------------------------------------------------------
Console.WriteLine("1. TryParse vs Parse");
Console.WriteLine(new string('-', 40));

string validInput = "123";
string invalidInput = "abc";

// 使用 TryParse（建議做法）
if (int.TryParse(validInput, out int value1))
{
    Console.WriteLine($"TryParse 成功：{value1}");
}

if (!int.TryParse(invalidInput, out int value2))
{
    Console.WriteLine("TryParse 失敗：輸入格式不正確");
}

// 使用 Parse（會拋出例外）
try
{
    int value3 = int.Parse(invalidInput);
}
catch (FormatException)
{
    Console.WriteLine("Parse 拋出 FormatException");
}

// --------------------------------------------------------------
// 2. 捕捉特定例外
// --------------------------------------------------------------
Console.WriteLine("\n2. 捕捉特定例外");
Console.WriteLine(new string('-', 40));

try
{
    int[] numbers = { 1, 2, 3 };
    Console.WriteLine(numbers[10]); // 故意存取超出範圍的索引
}
catch (IndexOutOfRangeException ex)
{
    Console.WriteLine($"捕捉到特定例外：{ex.GetType().Name}");
    Console.WriteLine($"訊息：{ex.Message}");
}

// --------------------------------------------------------------
// 3. 例外篩選器（when）
// --------------------------------------------------------------
Console.WriteLine("\n3. 例外篩選器");
Console.WriteLine(new string('-', 40));

for (int errorCode = 1; errorCode <= 3; errorCode++)
{
    try
    {
        throw new CustomException(errorCode);
    }
    catch (CustomException ex) when (ex.ErrorCode == 1)
    {
        Console.WriteLine($"處理錯誤碼 1：{ex.Message}");
    }
    catch (CustomException ex) when (ex.ErrorCode == 2)
    {
        Console.WriteLine($"處理錯誤碼 2：{ex.Message}");
    }
    catch (CustomException ex)
    {
        Console.WriteLine($"處理其他錯誤碼 {ex.ErrorCode}：{ex.Message}");
    }
}

// --------------------------------------------------------------
// 4. 正確地重新拋出例外
// --------------------------------------------------------------
Console.WriteLine("\n4. 重新拋出例外");
Console.WriteLine(new string('-', 40));

try
{
    MethodThatRethrows();
}
catch (InvalidOperationException ex)
{
    // 堆疊追蹤應該顯示原始的拋出位置
    Console.WriteLine($"捕捉到重新拋出的例外：{ex.Message}");
    Console.WriteLine($"堆疊追蹤包含 OriginalMethod: {ex.StackTrace?.Contains("OriginalMethod") ?? false}");
}

// --------------------------------------------------------------
// 5. 包裝例外並保留內部例外
// --------------------------------------------------------------
Console.WriteLine("\n5. 包裝例外");
Console.WriteLine(new string('-', 40));

try
{
    StringToDate("not-a-date");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"外部例外：{ex.GetType().Name} - {ex.Message}");
    if (ex.InnerException != null)
    {
        Console.WriteLine($"內部例外：{ex.InnerException.GetType().Name} - {ex.InnerException.Message}");
    }
}

// --------------------------------------------------------------
// 6. ArgumentNullException.ThrowIfNull (.NET 6+)
// --------------------------------------------------------------
Console.WriteLine("\n6. ArgumentNullException.ThrowIfNull");
Console.WriteLine(new string('-', 40));

try
{
    ProcessData(null!);
}
catch (ArgumentNullException ex)
{
    Console.WriteLine($"捕捉到 ArgumentNullException：{ex.ParamName}");
}

// --------------------------------------------------------------
// 7. finally 子句
// --------------------------------------------------------------
Console.WriteLine("\n7. finally 子句");
Console.WriteLine(new string('-', 40));

DemoFinally(throwException: false);
DemoFinally(throwException: true);

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 輔助方法和類別定義
// ============================================================

static void OriginalMethod()
{
    throw new InvalidOperationException("原始錯誤");
}

static void MethodThatRethrows()
{
    try
    {
        OriginalMethod();
    }
    catch (InvalidOperationException ex)
    {
        // 記錄錯誤
        Console.WriteLine($"記錄錯誤：{ex.Message}");
        // 使用 throw; 保留堆疊追蹤
        throw;
    }
}

static DateTime StringToDate(string input)
{
    try
    {
        return Convert.ToDateTime(input);
    }
    catch (FormatException ex)
    {
        // 將原始例外保存在 innerException 參數
        throw new ArgumentException($"無效的引數：{nameof(input)}", ex);
    }
}

static void ProcessData(string data)
{
    ArgumentNullException.ThrowIfNull(data);
    Console.WriteLine($"處理資料：{data}");
}

static void DemoFinally(bool throwException)
{
    try
    {
        Console.WriteLine($"  try 區塊執行 (throwException={throwException})");
        if (throwException)
        {
            throw new Exception("測試例外");
        }
    }
    catch (Exception)
    {
        Console.WriteLine("  catch 區塊執行");
    }
    finally
    {
        Console.WriteLine("  finally 區塊執行（無論如何都會執行）");
    }
}

// 自訂例外類別
public class CustomException : Exception
{
    public int ErrorCode { get; }

    public CustomException(int errorCode)
        : base($"發生錯誤，錯誤碼：{errorCode}")
    {
        ErrorCode = errorCode;
    }
}
