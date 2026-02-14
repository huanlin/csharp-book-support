// Demo: Local Methods

Console.WriteLine("=== Local Methods ===\n");

// --------------------------------------------------------------
// 1. Basic Local Method
// --------------------------------------------------------------
Console.WriteLine("1. Basic Local Method");
Console.WriteLine(new string('-', 40));

Demo();

// --------------------------------------------------------------
// 2. Local Method Accessing Outer Variables (Closure)
// --------------------------------------------------------------
Console.WriteLine("\n2. Local Method Accessing Outer Variables");
Console.WriteLine(new string('-', 40));

DemoWithClosure();

// --------------------------------------------------------------
// 3. Recursive Local Method
// --------------------------------------------------------------
Console.WriteLine("\n3. Recursive Local Method");
Console.WriteLine(new string('-', 40));

DemoRecursive();

// --------------------------------------------------------------
// 4. Iterator Local Method
// --------------------------------------------------------------
Console.WriteLine("\n4. Iterator Local Method");
Console.WriteLine(new string('-', 40));

foreach (var num in GetEvenNumbers(1, 10))
{
    Console.Write($"{num} ");
}
Console.WriteLine();

Console.WriteLine("\n=== Example End ===");

// ============================================================
// Demo Methods
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
    int multiplier = 10;  // Outer variable

    Console.WriteLine(Multiply(5));   // 50
    Console.WriteLine(Multiply(3));   // 30

    multiplier = 100;  // Modify outer variable
    Console.WriteLine(Multiply(5));   // 500

    int Multiply(int n)
    {
        return n * multiplier;  // Accessing outer variable directly
    }
}

void DemoRecursive()
{
    Console.WriteLine($"5! = {Factorial(5)}");   // 120
    Console.WriteLine($"10! = {Factorial(10)}"); // 3628800

    int Factorial(int n)
    {
        if (n <= 1) return 1;
        return n * Factorial(n - 1);  // Recursive call
    }
}

IEnumerable<int> GetEvenNumbers(int start, int end)
{
    ValidateRange(start, end);

    return GetEvenNumbersCore(start, end);

    void ValidateRange(int s, int e)
    {
        if (s > e)
            throw new ArgumentException("start cannot be greater than end");
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
