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


        /*public void LoadSprites()
        {
            FileStream angrymom = new FileStream("../../Art/mom_angry.png", FileMode.Open);
        }*/

       

    }

    class Mom : Drawable
    {
        int index = 0;
        List<Sprite> sprites = new List<Sprite>();

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(sprites[index++]);
            if (index > 7) { index = 0; }
        }

        public Mom()
        {
            int w = 360;
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
