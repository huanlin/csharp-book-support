// Demo: various syntaxes and best practices for exception handling

Console.WriteLine("=== Exception Handling Example ===\n");

// --------------------------------------------------------------
// 1. TryParse vs Parse: handling predictable errors
// --------------------------------------------------------------
Console.WriteLine("1. TryParse vs Parse");
Console.WriteLine(new string('-', 40));

string validInput = "123";
string invalidInput = "abc";

// Use TryParse (recommended practice)
if (int.TryParse(validInput, out int value1))
{
    Console.WriteLine($"TryParse success: {value1}");
}

if (!int.TryParse(invalidInput, out int value2))
{
    Console.WriteLine("TryParse failed: Invalid input format");
}

// Use Parse (throws exception)
try
{
    int value3 = int.Parse(invalidInput);
}
catch (FormatException)
{
    Console.WriteLine("Parse thrown FormatException");
}

// --------------------------------------------------------------
// 2. Catch specific exceptions
// --------------------------------------------------------------
Console.WriteLine("\n2. Catch specific exceptions");
Console.WriteLine(new string('-', 40));

try
{
    int[] numbers = { 1, 2, 3 };
    Console.WriteLine(numbers[10]); // Purposefully access index out of range
}
catch (IndexOutOfRangeException ex)
{
    Console.WriteLine($"Captured specific exception: {ex.GetType().Name}");
    Console.WriteLine($"Message: {ex.Message}");
}

// --------------------------------------------------------------
// 3. Exception filters (when)
// --------------------------------------------------------------
Console.WriteLine("\n3. Exception filters");
Console.WriteLine(new string('-', 40));

for (int errorCode = 1; errorCode <= 3; errorCode++)
{
    try
    {
        throw new CustomException(errorCode);
    }
    catch (CustomException ex) when (ex.ErrorCode == 1)
    {
        Console.WriteLine($"Handling error code 1: {ex.Message}");
    }
    catch (CustomException ex) when (ex.ErrorCode == 2)
    {
        Console.WriteLine($"Handling error code 2: {ex.Message}");
    }
    catch (CustomException ex)
    {
        Console.WriteLine($"Handling other error code {ex.ErrorCode}: {ex.Message}");
    }
}

// --------------------------------------------------------------
// 4. Rethrow exceptions correctly
// --------------------------------------------------------------
Console.WriteLine("\n4. Rethrow exceptions");
Console.WriteLine(new string('-', 40));

try
{
    MethodThatRethrows();
}
catch (InvalidOperationException ex)
{
    // Stack trace should show original throw location
    Console.WriteLine($"Captured rethrown exception: {ex.Message}");
    Console.WriteLine($"Stack trace contains OriginalMethod: {ex.StackTrace?.Contains("OriginalMethod") ?? false}");
}

// --------------------------------------------------------------
// 5. Wrap exception and preserve inner exception
// --------------------------------------------------------------
Console.WriteLine("\n5. Wrap exceptions");
Console.WriteLine(new string('-', 40));

try
{
    StringToDate("not-a-date");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Outer exception: {ex.GetType().Name} - {ex.Message}");
    if (ex.InnerException != null)
    {
        Console.WriteLine($"Inner exception: {ex.InnerException.GetType().Name} - {ex.InnerException.Message}");
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
    Console.WriteLine($"Captured ArgumentNullException: {ex.ParamName}");
}

// --------------------------------------------------------------
// 7. finally clause
// --------------------------------------------------------------
Console.WriteLine("\n7. finally clause");
Console.WriteLine(new string('-', 40));

DemoFinally(throwException: false);
DemoFinally(throwException: true);

Console.WriteLine("\n=== Example End ===");

// ============================================================
// Helper methods and class definitions
// ============================================================

static void OriginalMethod()
{
    throw new InvalidOperationException("Original error");
}

static void MethodThatRethrows()
{
    try
    {
        OriginalMethod();
    }
    catch (InvalidOperationException ex)
    {
        // Log error
        Console.WriteLine($"Logging error: {ex.Message}");
        // Use throw; to preserve stack trace
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
        // Preserve original exception in innerException parameter
        throw new ArgumentException($"Invalid argument: {nameof(input)}", ex);
    }
}

static void ProcessData(string data)
{
    ArgumentNullException.ThrowIfNull(data);
    Console.WriteLine($"Processing data: {data}");
}

static void DemoFinally(bool throwException)
{
    try
    {
        Console.WriteLine($"  try block execution (throwException={throwException})");
        if (throwException)
        {
            throw new Exception("Test exception");
        }
    }
    catch (Exception)
    {
        Console.WriteLine("  catch block execution");
    }
    finally
    {
        Console.WriteLine("  finally block execution (always runs)");
    }
}

// Custom exception class
public class CustomException : Exception
{
    public int ErrorCode { get; }

    public CustomException(int errorCode)
        : base($"Error occurred, error code: {errorCode}")
    {
        ErrorCode = errorCode;
    }
}
