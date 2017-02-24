using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test {
    class DramaManager {
        public DramaManager() {
            Alex = new CharacterState("alex");
            Mom = new CharacterState("mom");
            Dad = new CharacterState("dad");
        }

        private CharacterState Alex;
        private CharacterState Mom;
        private CharacterState Dad;
        static Relationships ship = new Relationships();

        public double getDadFNC()
        {
            return ship.getDadFNC();
        }
        public double getAlexFNC()
        {
            return ship.getAlexFNC();
        }
        public double getMomFNC()
        {
            return ship.getMomFNC();
        }


        List<GameMatrix> matrices = new List<GameMatrix>();

        public CharacterState getAlex() {
            return Alex;
        }
        public CharacterState getMom() {
            return Mom;
        }
        public CharacterState getDad() {
            return Dad;
        }
    }
}
