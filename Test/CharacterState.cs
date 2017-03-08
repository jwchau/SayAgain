using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class CharacterState {
        //fields
        private double mood;
        private double volatility;
        private double goal;
        double talkedTo = 0;


        //methods
        public CharacterState() {
            this.mood = 0;
            this.volatility = 0;
        }

        public void setMood(double m)
        {
            mood = m;
        }

        public double getMood()
        {
            return mood;
        }

        public void setVolatility(double v)
        {
            volatility = v;
        }

        public double getVolatility()
        {
            return volatility;
        }

        public void SetTalked(char f, double amount) {
            if (f == 'i') {
                talkedTo += amount;
            } else if (f == 'd') {
                talkedTo -= amount;
            }
        }

        public bool CheckTalked() {
            if (talkedTo == 10000000.0) {
                return true;
            }
            return false;
        }

    

    }
}
