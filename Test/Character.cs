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
        public bool canTalk = false;

        public abstract View view { get; set; }

        public enum spriteEmotion { happy, angry, neutral, sad };
        public abstract void setSpriteEmotion(spriteEmotion e);  
        public abstract void checkFNC();
        public abstract void setPosition();
        
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

        public void click()
        {
            
            Console.WriteLine("check if click");
            Console.WriteLine((x * SA.getW()));
            Console.WriteLine((x * SA.getW() + sprites[0].GetLocalBounds().Width));
            Console.WriteLine(Mouse.GetPosition().X);
            if (sprites != null)
            {
                if (Mouse.GetPosition().X >= (x * SA.getW())
                    && Mouse.GetPosition().X <= x * (SA.getW()) + sprites[0].GetGlobalBounds().Width
                    && Mouse.GetPosition().Y >= (y * SA.getH())
                    && Mouse.GetPosition().Y <= (y * SA.getH()) + sprites[0].GetGlobalBounds().Height)


                    /*Mouse.GetPosition().X >= sprites[0].Position.X 
                    && Mouse.GetPosition().X <= sprites[0].Position.X + sprites[0].GetGlobalBounds().Width
                    && Mouse.GetPosition().Y >= sprites[0].Position.Y
                    && Mouse.GetPosition().Y <= sprites[0].Position.Y + sprites[0].GetGlobalBounds().Height)
                    */


                    /*http://stackoverflow.com/questions/23530360/how-do-you-make-a-clickable-sprite-in-sfml
                     * sprites[0].GetGlobalBounds().Contains
                     * (Mouse.GetPosition().X, Mouse.GetPosition().Y))*/
                        {
                            Console.WriteLine("mouse over sprite");
                        }

            }
}


public Character()
{


}
}
}
