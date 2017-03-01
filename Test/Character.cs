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
        /*static List<Sprite> angrysprites = new List<Sprite>(); //frustrated sprite
        static List<Sprite> happysprites = new List<Sprite>(); //cooperative
        static List<Sprite> neutralsprites = new List<Sprite>(); //neutral*/
        static List<Sprite> sprites = new List<Sprite>();

        public abstract void setHappy();
        public abstract void setAngry();
        public abstract void setNeutral();
        public abstract void setSad();



        DateTime time = DateTime.Now;

        public static void setSprite(List<Sprite> s)
        {
            sprites = s;
        }

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


        public Character()
        {


        }
    }
}
