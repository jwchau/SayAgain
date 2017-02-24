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
    class CutsceneManager
    {
        public Sprite mom;

        public View view { get; private set; }


    }

    class Mom : Drawable
    {
        int index = 0;
        List<Sprite> sprites = new List<Sprite>();
        DateTime time = DateTime.Now;

        public void Draw(RenderTarget target, RenderStates states)
        {
            
        
            target.Draw(sprites[index]);

            float framerate = 4f;

            if ((DateTime.Now - time).TotalMilliseconds > (1000f / framerate))
            {
                time = DateTime.Now;
                if (++index >= sprites.Count)
                {
                    index = 0;               
                }
            }
            
        }

        public Mom()
        {
            int w = 361;
            IntRect first = new IntRect(0, 0, w, 450);
            FileStream f = new FileStream("../../Art/mom_angry.png", FileMode.Open);
            Texture t = new Texture(f);
            f.Close();
            for (int i = 0; i < t.Size.X; i += w)
            {
                sprites.Add(new Sprite(t, new IntRect(i, 0, w, 450))); //btw might get extra sprite if sizes no precise
            }
      
        }
    }



}
