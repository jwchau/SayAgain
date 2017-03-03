using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Drawing;

namespace Test
{
    class Alex: Character
    {
        new View view;
        static FileStream f = new FileStream("../../Art/alexMaster.png", FileMode.Open);
        Texture t = new Texture(f);
        List<Sprite> angrysprites = new List<Sprite>();
        List<Sprite> happysprites = new List<Sprite>();
        List<Sprite> neutralsprites = new List<Sprite>();


        public override void checkFNC()
        {

        }
        public override void setAngry()
        {
            setSprite(angrysprites);
        }
        public override void setHappy()
        {
            setSprite(happysprites);
        }
        public override void setNeutral()
        {
            setSprite(neutralsprites);
        }

        public override void setSad()
        {
            //Alex has no sad expression
        }

        public Alex()
        {

            //setView(new FloatRect(1,2,3,4));

            for (int i = 0; i < (361 * 4); i += 361)
            {
                neutralsprites.Add(new Sprite(t, new IntRect(i, 0, 361, 449)));
            }
            for (int i = 0; i < (361 * 9); i += 361)
            {
                happysprites.Add(new Sprite(t, new IntRect(i, 449, 361, 449)));
            }
            for (int i = 0; i < (337 * 9); i += 337)
            {
                angrysprites.Add(new Sprite(t, new IntRect(i, 449*2, 337, 449)));
            }
        }
    }
}
