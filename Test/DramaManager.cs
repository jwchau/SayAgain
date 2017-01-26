using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class DramaManager
    {
        public DramaManager() { }

        AlexState Alex;
        MomState Mom;
        DadState Dad;

        List<GameMatrix> matrices = new List<GameMatrix>();
    }
}
