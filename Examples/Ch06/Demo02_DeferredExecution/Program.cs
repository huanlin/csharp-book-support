using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo02_DeferredExecution
{
    class Program
    {
        static void Main(string[] args)
        {
            DemoDeferredEvaluation();
            DemoRepeatEvaluation();
        }

        static void DemoDeferredEvaluation()
        {
            Console.WriteLine("========= DemoDeferredEvaluation ===========");

            var numbers = new List<int> { 1, 2, 3 };

            IEnumerable<int> numberQuery = numbers.Select(num => num * 10); // 建立查詢

            numbers.Remove(2);  // 移除元素 2

            foreach (var num in numberQuery)    // 這裡才會執行查詢表示式
            {
                Console.Write(num + " ");   // 輸出 "10 30"
            }
        }

        static void DemoRepeatEvaluation()
        {
            Console.WriteLine("\r\n======= DemoRepeatEvaluation ===========");

            var numbers = new List<int> { 1, 2, 3 };

            IEnumerable<int> numberQuery = numbers.Select(num => num * 10); // 建立查詢

            for (int i = 1; i <= 3; i++)
            {
                Console.WriteLine($"第 {i} 次迴圈, 共 {numberQuery.Count()} 個元素");
                numbers.RemoveAt(0);
            }
        }

    }
}
