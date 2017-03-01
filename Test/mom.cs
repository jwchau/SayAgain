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
    class Mom: Character
    {
        static FileStream f = new FileStream("../../Art/momsprites.png", FileMode.Open);
        Texture t = new Texture(f);
        List<Sprite> angrysprites = new List<Sprite>();
        List<Sprite> happysprites = new List<Sprite>();
        List<Sprite> neutralsprites = new List<Sprite>();
        List<Sprite> sadsprites = new List<Sprite>();
        int w = 361;

        public override void setAngry()
        {
            Character.setSprite(angrysprites);
        }
        public override void setHappy()
        {
            Character.setSprite(happysprites);
        }
        public override void setNeutral()
        {
            Character.setSprite(neutralsprites);
        }

        public override void setSad()
        {
            Character.setSprite(sadsprites);
        }

        public Mom()
        {
            for (int i = 0; i < (361 * 7); i += w)
            {
                angrysprites.Add(new Sprite(t, new IntRect(i, 0, w, 465))); //btw might get extra sprite if sizes no precise

            }
            for (int i = 0; i < (361 * 9); i += w)
            {
                happysprites.Add(new Sprite(t, new IntRect(i, 465, w, 465))); //second row of sprites; happy epression 
            }
            for (int i = 0; i < (361 * 4); i += w)
            {
                neutralsprites.Add(new Sprite(t, new IntRect(i, 465 * 2, w, 465)));
            }
            for (int i = 0; i < (361 * 4); i += w)
            {
                sadsprites.Add(new Sprite(t, new IntRect(i, 465 * 3, w, 465)));
            }


        }
    }
}