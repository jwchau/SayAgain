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
    class Dad : Character, Drawable
    {
        static FileStream f = new FileStream("../../Art/dadMaster.png", FileMode.Open);
        Texture t = new Texture(f);
        static FileStream a = new FileStream("../../Art/armMaster.png", FileMode.Open);
        Texture arm = new Texture(a);
      
        List<Sprite> angrysprites = new List<Sprite>();
        List<Sprite> happysprites = new List<Sprite>();
        List<Sprite> neutralsprites = new List<Sprite>();
        List<Sprite> sprites = new List<Sprite>();
        Sprite angryarm = new Sprite();
        Sprite neutralarm = new Sprite();
        String expr;



        public override void checkFNC()
        {
            throw new NotImplementedException();
        }

        public override void setSpriteEmotion(spriteEmotion e)
        {
            switch (e)
            {
                case spriteEmotion.angry:
                    sprites = angrysprites;
                    expr = "angry";
                    break;
                case spriteEmotion.happy:
                    sprites = happysprites;
                    expr = "happy";
                    break;
                case spriteEmotion.sad:
                    //dad has no sad emotion
                    break;
                case spriteEmotion.neutral:
                    sprites = neutralsprites;
                    expr = "neutral";
                    break;
            }
        }

        public override void Draw(RenderTarget target, RenderStates states)
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


            if (expr.Equals("angry"))
            {
                angryarm.Position = new Vector2f(xpos, ypos+225); 
                target.Draw(angryarm);
            }
            else
            {
                neutralarm.Position = new Vector2f(xpos, ypos+225);
                target.Draw(neutralarm);
            }


        }

        public Dad()
        {
            

            //determine size and position
            xpos = 100;
            ypos = 200;
            xscale = 0.5f;
            yscale = 0.5f;
            xscale = 1f;
            yscale = 1f;

            neutralarm = new Sprite(arm, new IntRect(0, 0, 320, 229));
            angryarm = new Sprite(arm, new IntRect(0, 229, 320, 229));

            for (int i = 0; i < (343 * 4); i += 343)
            {
                angrysprites.Add(new Sprite(t, new IntRect(i, 0, 343, 454))); //btw might get extra sprite if sizes no precise
            }
            for (int i = 0; i < (343 * 4); i += 343)
            {
                happysprites.Add(new Sprite(t, new IntRect(i, 454, 343, 454))); //second row of sprites; happy epression 
            }
            for (int i = 0; i < (343 * 4); i += 343)
            {
                neutralsprites.Add(new Sprite(t, new IntRect(i, 454 * 2, 343, 454)));
            }


        }
    }
}