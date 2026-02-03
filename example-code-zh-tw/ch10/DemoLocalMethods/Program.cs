// 示範區域方法（Local Methods）

Console.WriteLine("=== 區域方法 ===\n");

// --------------------------------------------------------------
// 1. 基本區域方法
// --------------------------------------------------------------
Console.WriteLine("1. 基本區域方法");
Console.WriteLine(new string('-', 40));

Demo();

// --------------------------------------------------------------
// 2. 區域方法存取外層變數
// --------------------------------------------------------------
Console.WriteLine("\n2. 區域方法存取外層變數");
Console.WriteLine(new string('-', 40));

DemoWithClosure();

// --------------------------------------------------------------
// 3. 遞迴區域方法
// --------------------------------------------------------------
Console.WriteLine("\n3. 遞迴區域方法");
Console.WriteLine(new string('-', 40));

DemoRecursive();

// --------------------------------------------------------------
// 4. 迭代器區域方法
// --------------------------------------------------------------
Console.WriteLine("\n4. 迭代器區域方法");
Console.WriteLine(new string('-', 40));

foreach (var num in GetEvenNumbers(1, 10))
{
    Console.Write($"{num} ");
}
Console.WriteLine();

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 示範方法
// ============================================================

void Demo()
{
    Console.WriteLine(Add(1, 1));   // 2
    Console.WriteLine(Add(3, 4));   // 7
    Console.WriteLine(Add(9, 9));   // 18

    int Add(int m, int n)
    {
        return m + n;
    }
}

void DemoWithClosure()
{
    int multiplier = 10;  // 外層變數

    Console.WriteLine(Multiply(5));   // 50
    Console.WriteLine(Multiply(3));   // 30

    multiplier = 100;  // 修改外層變數
    Console.WriteLine(Multiply(5));   // 500

    int Multiply(int n)
    {
        return n * multiplier;  // 直接存取外層變數
    }
}

void DemoRecursive()
{
    Console.WriteLine($"5! = {Factorial(5)}");   // 120
    Console.WriteLine($"10! = {Factorial(10)}"); // 3628800

    int Factorial(int n)
    {
        if (n <= 1) return 1;
        return n * Factorial(n - 1);  // 遞迴呼叫
    }
}

IEnumerable<int> GetEvenNumbers(int start, int end)
{
    ValidateRange(start, end);

    return GetEvenNumbersCore(start, end);

    void ValidateRange(int s, int e)
    {
        if (s > e)
            throw new ArgumentException("start 不可大於 end");
    }

    IEnumerable<int> GetEvenNumbersCore(int s, int e)
    {
        for (int i = s; i <= e; i++)
        {
            if (i % 2 == 0)
                yield return i;
        }
    }
}
