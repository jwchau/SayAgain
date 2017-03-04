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
    class Mom : Character
    {
        new View view;
        static FileStream f = new FileStream("../../Art/momsprites.png", FileMode.Open);
        Texture t = new Texture(f);
        List<Sprite> angrysprites = new List<Sprite>();
        List<Sprite> happysprites = new List<Sprite>();
        List<Sprite> neutralsprites = new List<Sprite>();
        List<Sprite> sadsprites = new List<Sprite>();
        int w = 361;

        public override void setPosition()
        {
            throw new NotImplementedException();
        }

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
                    setSprite(angrysprites);
                    break;
                case spriteEmotion.neutral:
                    setSprite(neutralsprites);
                    break;
            }
        } 

        public Mom()
        {
            //setView(new FloatRect(1, 2, 3, 4));
            for (int i = 0; i < (w * 7); i += w)
            {
                angrysprites.Add(new Sprite(t, new IntRect(i, 0, w, 465))); //btw might get extra sprite if sizes no precise
            }
            for (int i = 0; i < (w * 9); i += w)
            {
                happysprites.Add(new Sprite(t, new IntRect(i, 465, w, 465))); //second row of sprites; happy epression 
            }
            for (int i = 0; i < (w * 4); i += w)
            {
                neutralsprites.Add(new Sprite(t, new IntRect(i, 465 * 2, w, 465)));
            }
            for (int i = 0; i < (w * 4); i += w)
            {
                sadsprites.Add(new Sprite(t, new IntRect(i, 465 * 3, w, 465)));
            }


        }
    }
}