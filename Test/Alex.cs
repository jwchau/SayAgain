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
    class Alex : Character
    {
        new View view;
        static FileStream f = new FileStream("../../Art/alexMaster.png", FileMode.Open);
        Texture t = new Texture(f);
        List<Sprite> angrysprites = new List<Sprite>();
        List<Sprite> happysprites = new List<Sprite>();
        List<Sprite> neutralsprites = new List<Sprite>();

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
                    //alex has no sad emotions
                    break;
                case spriteEmotion.neutral:
                    setSprite(neutralsprites);
                    break;
            }

        } 
        public Alex()
        {

            //setView(new FloatRect(0,0,0,50));

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
