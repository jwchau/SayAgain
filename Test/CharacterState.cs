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
        public CharacterState(double mood, double volatility) {
            this.mood = mood;
            this.volatility = volatility;
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

        public void DecreaseMood() {

        }

    }
}
