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
        public float xpos = 0, ypos = 0, xscale = 1, yscale = 1;
        public bool canTalk = false;
        public CharacterState state;
        public enum spriteEmotion { happy, angry, neutral, sad };
        public abstract void setSpriteEmotion(spriteEmotion e);  
        public abstract void checkFNC();
        
        public void dim()
        {

            foreach (Sprite s in sprites)
            {
                s.Color = new Color(s.Color.R, s.Color.G, s.Color.B, 180);
            }
        }

        public void undim()
        {

            foreach (Sprite s in sprites)
            {
                s.Color = new Color(s.Color.R, s.Color.G, s.Color.B, 255);
            }
        }

        public void active(bool b)
        {
            if (b)
            {
                canTalk = true;
                undim();
            } else if (!b)
            {
                canTalk = false;
                dim();
            }
        }
  
        public void setSprite(List<Sprite> s)
        {
            sprites = s;
        }


        public void Draw(RenderTarget target, RenderStates states)
        {
            float framerate = 4f;

            sprites[index].Position = new Vector2f(xpos, ypos);
            sprites[index].Scale = new Vector2f(xscale, yscale);

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
        
        public void click()
        {
            if (sprites != null)
            {
                /*if (Mouse.GetPosition().X >= (x * SA.getW())
                    && Mouse.GetPosition().X <= x * (SA.getW()) + sprites[0].GetGlobalBounds().Width
                    && Mouse.GetPosition().Y >= (y * SA.getH())
                    && Mouse.GetPosition().Y <= (y * SA.getH()) + sprites[0].GetGlobalBounds().Height)
                */  

                    //http://stackoverflow.com/questions/23530360/how-do-you-make-a-clickable-sprite-in-sfml
                    if (sprites[0].GetGlobalBounds().Contains
                        (Mouse.GetPosition().X, Mouse.GetPosition().Y)) {
                            Console.WriteLine("clicked mom");
                        }

            }
}


        public Character() {

            state = new CharacterState();

        }
    }
}
