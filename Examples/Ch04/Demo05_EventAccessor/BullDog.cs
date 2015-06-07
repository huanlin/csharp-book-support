using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo05_EventAccessor
{
    interface IAnimal
    {
        event EventHandler Speaking;
    }

    interface IDog
    {
        event EventHandler Speaking;
    }

    public class BullDog : IAnimal, IDog
    {
        private EventHandler _animalSpeaking;
        private EventHandler _dogSpeaking;

        event EventHandler IAnimal.Speaking
        {
            add { _animalSpeaking += value; }
            remove { _animalSpeaking -= value; }
        }

        event EventHandler IDog.Speaking
        {
            add { _dogSpeaking += value; }
            remove { _dogSpeaking -= value; }
        }
    }
}
