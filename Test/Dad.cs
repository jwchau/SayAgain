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
    class Dad : Character
    {
        Texture t = new Texture("../../Art/dadMaster.png");
        List<Sprite> angrysprites = new List<Sprite>();
        List<Sprite> happysprites = new List<Sprite>();
        List<Sprite> neutralsprites = new List<Sprite>();
        List<Sprite> sadsprites = new List<Sprite>();




        public override void checkFNC()
        {
            throw new NotImplementedException();
        }

        public override void setSpriteEmotion(spriteEmotion e)
        {
            switch (e)
            {
                case spriteEmotion.angry:
                    setSprite(angrysprites);
                    break;
                case spriteEmotion.happy:
                    setSprite(happysprites);
                    break;
                case spriteEmotion.sad:
                    //dad has no sad emotion
                    break;
                case spriteEmotion.neutral:
                    setSprite(neutralsprites);
                    break;
            }
        }

        public Dad()
        {
            

            //determine size and position
            xpos = 100;
            ypos = 200;
            xscale = 0.5f;
            yscale = 0.5f;
            xscale = 1.2f;
            yscale = 1.2f;

            for (int i = 0; i < (343 * 4); i += 343)
            {
                neutralsprites.Add(new Sprite(t, new IntRect(i, 0, 343, 454))); //btw might get extra sprite if sizes no precise
            }
            for (int i = 0; i < (343 * 4); i += 343)
            {
                happysprites.Add(new Sprite(t, new IntRect(i, 454, 343, 454))); //second row of sprites; happy epression 
            }
            for (int i = 0; i < (343 * 4); i += 343)
            {
                angrysprites.Add(new Sprite(t, new IntRect(i, 454 * 2, 343, 454)));
            }


        }
    }
}