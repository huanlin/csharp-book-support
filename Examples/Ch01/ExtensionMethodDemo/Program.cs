using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethodDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string s1 = "michael";
            Console.WriteLine(s1.Reverse());
            Console.WriteLine(s1.Reverse().Capitalize());

            Console.WriteLine(StringHelper.Reverse(s1));
        }

    }

    public static class StringHelper
    {
        public static string Reverse(this string s)
        {
            var sb = new StringBuilder();
            for (int i = s.Length - 1; i >= 0; i--)
            {
                sb.Append(s[i]);
            }
            return sb.ToString();
        }

        public static string Capitalize(this string s)
        {
            return Char.ToUpper(s[0]) + s.Substring(1);
        }
    }
}
