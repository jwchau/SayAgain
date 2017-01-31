using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class DadState : PersonState

    {
        public DadState(double mood, double volatility)
        {
            this.mood = mood;
            this.volatility = volatility;
        }

        public double GoalMoodDiff()
        {
            return goal - mood;
        }
    }
}
