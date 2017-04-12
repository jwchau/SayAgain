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

<<<<<<< HEAD
=======

//todo: share random
//multiple interesting frames
>>>>>>> 24292412928b907bdb0e2cd81f7a16bf1fc4e303
namespace Test
{
    
    abstract class Character: Drawable
    {
<<<<<<< HEAD
        public int index = 0;

=======

        protected int[] FNCSpectrum = new int[3];
        protected int currentFNC;

        public int index = 0;

        protected Random r = new Random();
        protected int rnd;


>>>>>>> 24292412928b907bdb0e2cd81f7a16bf1fc4e303
        private List<Sprite> lipsprites = new List<Sprite>();
        private List<Sprite> sprites = new List<Sprite>();

        public DateTime time = DateTime.Now;
        protected float xpos, ypos, xscale, yscale;
        protected bool canTalk = false;
        public CharacterState state;
        public enum spriteEmotion { happy, angry, neutral, sad };
        public abstract void setSpriteEmotion(spriteEmotion e);  
        public abstract void checkFNC();

        protected uint SCREEN_WIDTH = VideoMode.DesktopMode.Width;
        protected uint SCREEN_HEIGHT = VideoMode.DesktopMode.Height;

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


        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            
<<<<<<< HEAD
            //float framerate = 4f;
            //sprites[index].Position = new Vector2f(xpos, ypos);
            //Console.WriteLine("WIDTH: " + sprites[index].GetGlobalBounds().Width + " height: " + sprites[index].GetGlobalBounds().Height);
            //// sprites[index].Scale = new Vector2f(SCREEN_WIDTH / sprites[index].GetGlobalBounds().Width, SCREEN_HEIGHT / sprites[index].GetGlobalBounds().Height);

            //target.Draw(sprites[index]);
            //if ((DateTime.Now - time).TotalMilliseconds > (1400f / framerate))
            //{
            //    time = DateTime.Now;
            //    if (++index >= sprites.Count)
            //    {
            //        index = 0;
            //    }
            //}
        }
        
=======
        }
        
        public int[] getSpectrum()
        {
            return FNCSpectrum;
        }

        public int getCurrentFNC()
        {
            return currentFNC;
        }

        public void changeFNC()
        {

        }
>>>>>>> 24292412928b907bdb0e2cd81f7a16bf1fc4e303
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
<<<<<<< HEAD
                            Console.WriteLine("clicked mom");
=======
                            //Console.WriteLine("clicked mom");
>>>>>>> 24292412928b907bdb0e2cd81f7a16bf1fc4e303
                        }

            }
}


        public Character() {

            state = new CharacterState();

        }
    }
}
