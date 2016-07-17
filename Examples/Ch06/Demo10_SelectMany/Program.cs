using System;
using System.Collections.Generic;
using System.Linq;
using CommonClasses;

namespace Demo10_SelectMany
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo2();
        }

        static void Demo1()
        {
            Employee[] employees = Employee.GetEmployees();       // 原始的員工集合
            var empGroups = employees.GroupBy(e => e.Department); // 依部門分組
            var flattened = empGroups.SelectMany(g => g);         // 把分組的結果重新組合成一條序列

            foreach (var emp in flattened)
            {
                Console.WriteLine($"{emp.Name}, {emp.Salary,0:C0}");
            }
        }

        static void Demo2()
        {
            var allJapanFinalists = new[]
            {
                new
                {
                    TeamName = "湘北",
                    Players = new string[]
                    {
                        "赤木剛憲", "櫻木花道", "流川楓", "三井壽", "宮城良田", "木暮公延"
                    }
                },
                new
                {
                    TeamName = "海南",
                    Players = new string[]
                    {
                        "牧紳一", "神宗一郎", "清田信長", "高砂一馬", "武藤正", "宮益義范"
                    }
                }
            };



            IEnumerable<string> players = allJapanFinalists.SelectMany(team => team.Players);
            foreach (var player in players)
            {
                Console.WriteLine(player);
            }
        }
    }



}
