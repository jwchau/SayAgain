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
    class Character: Drawable
    {
        int index = 0;
        List<Sprite> fsprites = new List<Sprite>(); //frustrated sprite
        List<Sprite> csprites = new List<Sprite>(); //cooperative
        List<Sprite> nsprites = new List<Sprite>(); //neutral
        List<Sprite> sprites = new List<Sprite>(); //current sprite
        bool frus, neu, coop;
        DateTime time = DateTime.Now;

        public void Draw(RenderTarget target, RenderStates states)
        {
            float framerate = 4f;
            
            target.Draw(sprites[index]);
            if ((DateTime.Now - time).TotalMilliseconds > (1400f / framerate))
            {
                time = DateTime.Now;
                if (++index >= sprites.Count)
                {
                    index = 0;
                }
            }
        }

    
        public void changeToFrus()
        {

            frus = true;
            coop = false;
            neu = false;
            sprites = fsprites;

        }

        public void changeToCoop()
        {
            coop = true;
            frus = false;
            neu = false;
            sprites = csprites;

        }
        public void changeToNeu()
        {
            neu = true;
            coop = false;
            frus = false;
            sprites = nsprites;

        }


        public Character()
        {
            int w = 361;
            IntRect first = new IntRect(0, 0, w, 450);
            FileStream f = new FileStream("../../Art/momsprites.png", FileMode.Open);
            Texture t = new Texture(f);
            f.Close();
            for (int i = 0; i < (361 * 7); i += w)
            {
                fsprites.Add(new Sprite(t, new IntRect(i, 0, w, 465))); //btw might get extra sprite if sizes no precise
            }
            for (int i = 0; i < (361 * 9); i += w)
            {
                csprites.Add(new Sprite(t, new IntRect(i, 465, w, 465))); //second row of sprites; happy epression 
            }


        }
    }
}
