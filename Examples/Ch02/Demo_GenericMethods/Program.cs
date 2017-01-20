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
            var demo = new GenericMethodDemo();
            demo.Print<int>(100);   // 這裡的 <int> 可以省略不寫。

            var result = demo.Print<int, DateTime>(100);
            Console.WriteLine("結果: " + result);

            // 使用第二個版本
            var demoV2 = new GenericMethodDemoVersion2<int>();
            demo.Print(100);   // 這次省略了 <int>。

            var result2 = demoV2.Print<DateTime>(100);
            Console.WriteLine("結果: " + result);

        }
    }

    class GenericMethodDemo
    {
        public void Print<T>(T obj)
        {
            Console.WriteLine("Hello, " + obj.ToString());
        }

        public TResult Print<T, TResult>(T obj) where TResult : new()
        {
            Console.WriteLine("Hello, " + obj.ToString());
            TResult result = new TResult();
            return result;
        }
    }

    class GenericMethodDemoVersion2<T>
    {
        public void Print(T obj)   // 參數型別 T 已經改為宣告在類別層級，這裡就不用寫 <T>。
        {
            Console.WriteLine("Hello, " + obj.ToString());
        }

        public TResult Print<TResult>(T obj) where TResult : new() 
        {
            Console.WriteLine("Hello, " + obj.ToString());
            var result = new TResult();
            return result;
        }
    }

}
