using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            DemoDictionary();
            DemoSortedDictionary();
            DemoSortedList();
        }

        static void DemoDictionary()
        {
            Console.WriteLine("=========== DemoDictionary ==============");

            var dict = new Dictionary<int, string>();
            dict.Add(3, "Michael");
            dict.Add(1, "Jane");
            dict.Add(2, "Allen");

            foreach (var item in dict)
            {
                Console.WriteLine("{0}-{1}", item.Key, item.Value);
            }
        }

        static void DemoSortedDictionary()
        {
            Console.WriteLine("=========== DemoSortedDictionary ==============");

            var dict = new SortedDictionary<int, string>();
            dict.Add(3, "Michael");
            dict.Add(1, "Jane");
            dict.Add(2, "Allen");

            foreach (var item in dict)
            {
                Console.WriteLine("{0}-{1}", item.Key, item.Value);
            }
        }

        static void DemoSortedList()
        {
            Console.WriteLine("=========== DemoSortedList ==============");

            var dict = new SortedList<string, string>();
            dict.Add("TW", "Taiwan");
            dict.Add("CN", "China");
            dict.Add("US", "United States");

            // 使用索引來取元素時，須分別透過 Keys 和 Values 來取得索引鍵和對應值。
            Console.WriteLine("{0}-{1}", dict.Keys[0], dict.Values[0]);
        }
    }
}
