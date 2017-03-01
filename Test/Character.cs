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
        static List<Sprite> angrysprites = new List<Sprite>(); //frustrated sprite
        static List<Sprite> happysprites = new List<Sprite>(); //cooperative
        static List<Sprite> neutralsprites = new List<Sprite>(); //neutral
        static List<Sprite> sprites { get; set; }//= new List<Sprite>(); //current sprite
        static bool angry { get; set; }
        static bool neutral { get; set; }
        static bool happy { get; set; }

        DateTime time = DateTime.Now;



        public static void setAngry(List<Sprite> s)
        {
            angrysprites = s;
        }

        public static void setHappy(List<Sprite> s)
        {
            happysprites = s;
        }

        public static void setNeutral(List<Sprite> s)
        {
            neutralsprites = s;
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

    
        static public void changeToAngry()
        {

            angry = true;
            happy = false;
            neutral = false;
            sprites = angrysprites;

        }

        static public void changeToHappy()
        {
            happy = true;
            angry = false;
            neutral = false;
            sprites = happysprites;

        }
        static public void changeToNeutral()
        {
            neutral = true;
            happy = false;
            angry = false;
            sprites = neutralsprites;

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
                angrysprites.Add(new Sprite(t, new IntRect(i, 0, w, 465))); //btw might get extra sprite if sizes no precise
            }
            for (int i = 0; i < (361 * 9); i += w)
            {
                happysprites.Add(new Sprite(t, new IntRect(i, 465, w, 465))); //second row of sprites; happy epression 
            }


        }
    }
}
