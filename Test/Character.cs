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
        int index = 0;
        List<Sprite> sprites = new List<Sprite>();
        DateTime time = DateTime.Now;
        public float x = 0, y = 0, w = 0, h = 0;

        public abstract View view { get; set; }

        public enum spriteEmotion { happy, angry, neutral, sad };
        public abstract void setSpriteEmotion(spriteEmotion e);  
        public abstract void checkFNC();
        public abstract void setPosition();
        
        public void dim()
        {
            foreach (Sprite s in sprites)
            {
                s.Color = Color.Black;
            }
        }
  
        public void setSprite(List<Sprite> s)
        {
            sprites = s;
        }


        public void Draw(RenderTarget target, RenderStates states)
        {

            View resetView = target.GetView();
            target.SetView(view);
            
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

            target.SetView(resetView);

        }

        
        public Character()
        {
            

        }
    }
}
