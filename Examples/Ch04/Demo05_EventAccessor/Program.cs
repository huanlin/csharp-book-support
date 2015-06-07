using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo05_EventAccessor
{
    class Program
    {
        static void Main(string[] args)
        {
            IDog dog = new BullDog();
            dog.Speaking += Dog_Speak;
        }

        static void Dog_Speak(object sender, EventArgs e)
        {
            Console.WriteLine("汪汪!");
        }
    }
}
