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
        //private View _view;
        Texture t = new Texture("../../Art/alexMaster.png");
        List<Sprite> angrysprites = new List<Sprite>();
        List<Sprite> happysprites = new List<Sprite>();
        List<Sprite> neutralsprites = new List<Sprite>();


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
            //determine size and position
            xpos = 700;
            ypos = 400;
            xscale = 1;
            yscale = 1;

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
                
                angrysprites.Add(new Sprite(t, new IntRect(i, 449 * 2, 337, 449)));
            }
        }
    }
}
