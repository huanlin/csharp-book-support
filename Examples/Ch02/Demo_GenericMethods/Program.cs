using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_GenericMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            var gmd = new GenericMethodDemo();
            gmd.Print<string>("Jacky");  // 這裡的 <string> 可以省略不寫。
            gmd.Print<int>(100);   // 這裡的 <int> 可以省略不寫。

            var result = gmd.Print<string, DateTime>("Jacky");
            Console.WriteLine("回傳值: " + result);

            // 使用第二個版本
            Console.WriteLine("=== 第二個版本 ===");
            var gmdV2 = new GenericMethodDemoVersion2<int>();
            // gmdV2.Print("Jacky");   // 無法通過編譯! 因為型別 T 已經在建立物件時指定為 int。
            gmdV2.Print(100);   

            var result2 = gmdV2.Print<DateTime>(100);
            Console.WriteLine("回傳值: " + result);
        }
    }

    class GenericMethodDemo
    {
        public void Print<T>(T obj)
        {
            Console.WriteLine("Print<T>() 的參數值：" + obj.ToString());
        }

        public TResult Print<T, TResult>(T obj) where TResult : new()
        {
            Console.WriteLine("Print<T, TResult>() 的參數值：" + obj.ToString());
            TResult result = new TResult();
            return result;
        }
    }

    class GenericMethodDemoVersion2<T>
    {
        public void Print(T obj)   // 參數型別 T 已經改為宣告在類別層級，這裡就不用寫 <T>。
        {
            Console.WriteLine("Print() 的參數值：" + obj.ToString());
        }

        public TResult Print<TResult>(T obj) where TResult : new() 
        {
            Console.WriteLine("Print<TResult>() 的參數值：" + obj.ToString());
            var result = new TResult();
            return result;
        }
    }

}
