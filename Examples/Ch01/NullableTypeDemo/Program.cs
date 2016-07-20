using System;

namespace NullableTypeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo1();
            Demo2();
        }

        static void Demo1()
        {
            Nullable<int> x = new Nullable<int>(10);
            int? y = 10;
            int? z = null;

            //if (z.Value == 10) { }      // 執行時期出錯!

            if (z == 10) { }    // 執行時期不會出錯

            //int m = y;              // 編譯失敗!
            if (y == 10)
            {
                Console.WriteLine("y == 10");
            }

            bool? flag = null;
            if (flag == false)
            {
                Console.WriteLine("flag is false.");
            }
        }

        static void Demo2()
        {
            Console.WriteLine("--- Nullable 變數運算 --- ");
            int? m = null;
            int? n = 10;
            int? o = m + n;
            int? p = m * n;
            Console.WriteLine("m + n = " + (o == null ? "NULL" : o.ToString()));
            Console.WriteLine("m * n = " + (p == null ? "NULL" : p.ToString()));
        }

    }
}
