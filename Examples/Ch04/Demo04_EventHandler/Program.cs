using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo04_EventHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            MySubscriber subscriber = new MySubscriber();
            subscriber.SetText();
        }
    }
}
