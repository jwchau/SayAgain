using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class PersonState
    {
        public PersonState()
        {
            mood = 0;
            volatility = 0;
        }

        protected double mood;
        protected double volatility;
        protected double goal;

        protected double getMood()
        {
            return mood;
        }

        protected void setMood(double m)
        {
            this.mood = m;
        }

        protected double getVolatility()
        {
            return volatility;
        }

        protected void setVolatility(double v)
        {

        }

    }
}
