// デモ: ローカル関数

Console.WriteLine("=== ローカル関数 ===\n");

// --------------------------------------------------------------
// 1. 基本的なローカル関数
// --------------------------------------------------------------
Console.WriteLine("1. 基本的なローカル関数");
Console.WriteLine(new string('-', 40));

Demo();

// --------------------------------------------------------------
// 2. 外側変数へアクセスするローカル関数（クロージャ）
// --------------------------------------------------------------
Console.WriteLine("\n2. 外側変数へアクセスするローカル関数");
Console.WriteLine(new string('-', 40));

DemoWithClosure();

// --------------------------------------------------------------
// 3. 再帰ローカル関数
// --------------------------------------------------------------
Console.WriteLine("\n3. 再帰ローカル関数");
Console.WriteLine(new string('-', 40));

DemoRecursive();

// --------------------------------------------------------------
// 4. イテレータローカル関数
// --------------------------------------------------------------
Console.WriteLine("\n4. イテレータローカル関数");
Console.WriteLine(new string('-', 40));

foreach (var num in GetEvenNumbers(1, 10))
{
    Console.Write($"{num} ");
}
Console.WriteLine();

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// デモ用メソッド
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
    int multiplier = 10;  // 外側変数

    Console.WriteLine(Multiply(5));   // 50
    Console.WriteLine(Multiply(3));   // 30

    multiplier = 100;  // 外側変数を変更
    Console.WriteLine(Multiply(5));   // 500

    int Multiply(int n)
    {
        return n * multiplier;  // 外側変数へ直接アクセス
    }
}

void DemoRecursive()
{
    Console.WriteLine($"5! = {Factorial(5)}");   // 120
    Console.WriteLine($"10! = {Factorial(10)}"); // 3628800

    int Factorial(int n)
    {
        if (n <= 1) return 1;
        return n * Factorial(n - 1);  // 再帰呼び出し
    }
}

IEnumerable<int> GetEvenNumbers(int start, int end)
{
    ValidateRange(start, end);

    return GetEvenNumbersCore(start, end);

    void ValidateRange(int s, int e)
    {
        if (s > e)
            throw new ArgumentException("start は end 以下である必要があります。");
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
