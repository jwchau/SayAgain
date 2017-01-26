using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class MomState : PersonState
    {
        public MomState(double mood, double volatility)
        {
            this.mood = mood;
            this.volatility = volatility;
        }

        double goal;

        public double GoalMoodDiff()
        {
            return goal - mood;
        }


    }
}
