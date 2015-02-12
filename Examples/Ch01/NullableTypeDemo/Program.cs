using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NullableTypeDemo
{
    class Program
    {
              public static DateTime Now { get; } 

        static void Main(string[] args)
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
    }
}
