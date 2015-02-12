using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace CallerMemberNameAttrDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var customer = new Customer();
            string s = customer.FullName;
            customer.Test();
        }
    }

    class Customer
    {
        private string _fullName;

        public string FullName
        {
            get
            {
                TraceHelper.ShowCallerMember(this);
                return _fullName;
            }
        }

        public void Test()
        {
            TraceHelper.ShowCallerMember(this, "Test");
        }
    }

    static class TraceHelper
    {
        public static void ShowCallerMember(object obj, [CallerMemberName] string memberName = "")
        {
            Console.WriteLine("Caller member is: {0}.{1}.", obj.GetType().Name, memberName);
        }
    }
}
