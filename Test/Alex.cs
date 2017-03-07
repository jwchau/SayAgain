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
        private View _view;
        static FileStream f = new FileStream("../../Art/alexMaster.png", FileMode.Open);
        Texture t = new Texture(f);
        List<Sprite> angrysprites = new List<Sprite>();
        List<Sprite> happysprites = new List<Sprite>();
        List<Sprite> neutralsprites = new List<Sprite>();

        public override View view
        {
            get
            {
                return _view;
            }

            set
            {
                _view = value;
            }
        }

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
                    _view = new View(angrysprites[0].GetGlobalBounds());
                    break;
                case spriteEmotion.happy:
                    setSprite(happysprites);
                    _view = new View(happysprites[0].GetGlobalBounds());
                    break;
                case spriteEmotion.sad:
                    //alex has no sad emotions
                    break;
                case spriteEmotion.neutral:
                    setSprite(neutralsprites);
                    _view = new View(neutralsprites[0].GetGlobalBounds());
                    break;
            }

            _view.Viewport = new FloatRect(x, y, w, h);
        } 



        public Alex()
        {
            //determine size and position
            w = .18f;
            h = .4f;
            x = .2f;
            y = .3f;

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

                /*Sprite s = new Sprite(t, new IntRect(i, 449 * 2, 337, 449));
                s.Color = new Color(s.Color.R, s.Color.G, s.Color.B, 180);
                angrysprites.Add(s);*/
                angrysprites.Add(new Sprite(t, new IntRect(i, 449 * 2, 337, 449)));
            }
        }
    }
}
