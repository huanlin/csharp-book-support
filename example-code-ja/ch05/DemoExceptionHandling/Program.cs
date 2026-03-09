// デモ: 例外処理の構文とベストプラクティス

Console.WriteLine("=== 例外処理の例 ===\n");

// --------------------------------------------------------------
// 1. TryParse vs Parse（予測可能な失敗の扱い）
// --------------------------------------------------------------
Console.WriteLine("1. TryParse vs Parse");
Console.WriteLine(new string('-', 40));

string validInput = "123";
string invalidInput = "abc";

// TryParse（推奨）
if (int.TryParse(validInput, out int value1))
{
    Console.WriteLine($"TryParse 成功: {value1}");
}

if (!int.TryParse(invalidInput, out int value2))
{
    Console.WriteLine("TryParse 失敗: 入力形式が不正");
}

// Parse（例外を投げる）
try
{
    int value3 = int.Parse(invalidInput);
}
catch (FormatException)
{
    Console.WriteLine("Parse は FormatException を送出");
}

// --------------------------------------------------------------
// 2. 具体的な例外を捕捉
// --------------------------------------------------------------
Console.WriteLine("\n2. 具体的な例外を捕捉");
Console.WriteLine(new string('-', 40));

try
{
    int[] numbers = { 1, 2, 3 };
    Console.WriteLine(numbers[10]); // 範囲外アクセス
}
catch (IndexOutOfRangeException ex)
{
    Console.WriteLine($"具体例外を捕捉: {ex.GetType().Name}");
    Console.WriteLine($"メッセージ: {ex.Message}");
}

// --------------------------------------------------------------
// 3. 例外フィルター（when）
// --------------------------------------------------------------
Console.WriteLine("\n3. 例外フィルター");
Console.WriteLine(new string('-', 40));

for (int errorCode = 1; errorCode <= 3; errorCode++)
{
    try
    {
        throw new CustomException(errorCode);
    }
    catch (CustomException ex) when (ex.ErrorCode == 1)
    {
        Console.WriteLine($"エラーコード 1 を処理: {ex.Message}");
    }
    catch (CustomException ex) when (ex.ErrorCode == 2)
    {
        Console.WriteLine($"エラーコード 2 を処理: {ex.Message}");
    }
    catch (CustomException ex)
    {
        Console.WriteLine($"その他エラーコード {ex.ErrorCode} を処理: {ex.Message}");
    }
}

// --------------------------------------------------------------
// 4. 正しい再スロー
// --------------------------------------------------------------
Console.WriteLine("\n4. 例外の再スロー");
Console.WriteLine(new string('-', 40));

try
{
    MethodThatRethrows();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"再スロー例外を捕捉: {ex.Message}");
    Console.WriteLine($"スタックトレースに OriginalMethod を含む: {ex.StackTrace?.Contains("OriginalMethod") ?? false}");
}

// --------------------------------------------------------------
// 5. 例外ラップと InnerException 保持
// --------------------------------------------------------------
Console.WriteLine("\n5. 例外のラップ");
Console.WriteLine(new string('-', 40));

try
{
    StringToDate("not-a-date");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"外側例外: {ex.GetType().Name} - {ex.Message}");
    if (ex.InnerException != null)
    {
        Console.WriteLine($"内側例外: {ex.InnerException.GetType().Name} - {ex.InnerException.Message}");
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
    Console.WriteLine($"ArgumentNullException を捕捉: {ex.ParamName}");
}

// --------------------------------------------------------------
// 7. finally 句
// --------------------------------------------------------------
Console.WriteLine("\n7. finally 句");
Console.WriteLine(new string('-', 40));

DemoFinally(throwException: false);
DemoFinally(throwException: true);

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// ヘルパーとクラス定義
// ============================================================

static void OriginalMethod()
{
    throw new InvalidOperationException("元のエラー");
}

static void MethodThatRethrows()
{
    try
    {
        OriginalMethod();
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"ログ: {ex.Message}");
        throw; // スタックトレース維持
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
        throw new ArgumentException($"不正な引数: {nameof(input)}", ex);
    }
}

static void ProcessData(string data)
{
    ArgumentNullException.ThrowIfNull(data);
    Console.WriteLine($"データ処理: {data}");
}

static void DemoFinally(bool throwException)
{
    try
    {
        Console.WriteLine($"  try ブロック実行（throwException={throwException}）");
        if (throwException)
        {
            throw new Exception("テスト例外");
        }
    }
    catch (Exception)
    {
        Console.WriteLine("  catch ブロック実行");
    }
    finally
    {
        Console.WriteLine("  finally ブロック実行（常に実行）");
    }
}

// カスタム例外
public class CustomException : Exception
{
    public int ErrorCode { get; }

    public CustomException(int errorCode)
        : base($"エラー発生。コード: {errorCode}")
    {
        ErrorCode = errorCode;
    }
}
