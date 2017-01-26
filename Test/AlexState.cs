using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class AlexState : PersonState
    {
        public AlexState(double mood, double volatility)
        {
            this.mood = mood;
            this.volatility = volatility;
        }

        double talkedTo = 0;

        public void SetTalked(char f, double amount)
        {
            if (f == 'i')
            {
                talkedTo += amount;
            } else if (f == 'd')
            {
                talkedTo -= amount;
            }
        }

        public bool CheckTalked()
        {
            if (talkedTo == 10000000.0)
            {
                return true;
            }
            return false;
        }

        public void DecreaseMood()
        {

        }

    }
}
