using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo01_PublisherSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            MySubscriber subscriber = new MySubscriber(); // 1
            subscriber.SetText(); // 3
        }
    }
}
