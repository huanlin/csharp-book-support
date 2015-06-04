using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumeratorDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            DemoNonGeneric();
            DemoGeneric();
            DemoForEach();
        }

        private static void DemoNonGeneric()
        {
            Console.WriteLine("===== 非泛型版本 =====");

            string[] fruits = { "蘋果", "香蕉", "鳳梨" };

            IEnumerator enumrt = fruits.GetEnumerator();    // 從集合物件取得列舉器

            while (enumrt.MoveNext())   // 透過列舉器來巡訪集合中的每一個元素
            {
                string s = (string)enumrt.Current; // 取得目前的元素（取出後須手動轉型）
                Console.WriteLine(s);
            }
        }

        static void DemoGeneric()
        {
            Console.WriteLine("===== 泛型版本 =====");

            string[] fruits = { "蘋果", "香蕉", "鳳梨" };
            
            IEnumerator<string> enumrt = ((IEnumerable<string>)fruits).GetEnumerator();    // 從集合物件取得列舉器

            while (enumrt.MoveNext())   // 透過列舉器來巡訪集合中的每一個元素
            {
                string s = enumrt.Current; // 取得目前的元素（不需要手動轉型）
                Console.WriteLine(s);
            }
        }

        static void DemoForEach()
        {
            Console.WriteLine("===== foreach =====");

            string[] fruits = { "蘋果", "香蕉", "鳳梨" };

            // 使用 foreach
            foreach (var item in fruits)
            {
                Console.WriteLine(item);
            }
        }
    }
}
