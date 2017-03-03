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
    abstract class Character: Drawable
    {
        public static View view;
        int index = 0;
        List<Sprite> sprites = new List<Sprite>();
        DateTime time = DateTime.Now;

        public abstract void setHappy();
        public abstract void setAngry();
        public abstract void setNeutral();
        public abstract void setSad();       
        public abstract void checkFNC();
  
        public void setSprite(List<Sprite> s)
        {
            sprites = s;
        }

        public static void setView(FloatRect f)
        {
            view = new View(f);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {

            //View resetView = target.GetView();
            //target.SetView(view);
            
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

            //target.SetView(resetView);

        }

        
        public Character()
        {
            

        }
    }
}
