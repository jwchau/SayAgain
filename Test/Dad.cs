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
        Texture t = new Texture("../../Art/dadMaster.png");
        Texture arm = new Texture("../../Art/armMaster.png");
      
        Dictionary<string, List<Sprite>> sprites = new Dictionary<string, List<Sprite>>() { { "angry", new List<Sprite>() },
                                                                                            { "happy", new List<Sprite>() },
                                                                                            { "neutral", new List<Sprite>() }
                                                                                           };
        Sprite angryarm = new Sprite();
        Sprite neutralarm = new Sprite();
        string expr;



        public override void checkFNC()
        {
            throw new NotImplementedException();
        }

        public override void setSpriteEmotion(spriteEmotion e)
        {
            if (e.ToString() != "sad") expr = e.ToString();
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            
            float framerate = 4f;

            target.Draw(sprites[expr][index]);
            if ((DateTime.Now - time).TotalMilliseconds > (1400f / framerate))
            {
                time = DateTime.Now;
                if (++index >= sprites.Count)
                {
                    index = 0;
                }
            }


            if (expr == "angry") target.Draw(angryarm);
            if (expr != "neutral") target.Draw(neutralarm);

        }

        public Dad()
        {

            //determine size and position
            xpos = (float)(SCREEN_WIDTH * .21);
            ypos = (float)(SCREEN_HEIGHT * 0.28);

            xscale = SCREEN_WIDTH / 1920;
            yscale = SCREEN_HEIGHT / 1080;

            neutralarm = new Sprite(arm, new IntRect(0, 0, 320, 229));
            angryarm = new Sprite(arm, new IntRect(0, 229, 320, 229));

            for (int i = 0; i < (343 * 4); i += 343)
            {

                sprites["angry"].Add(new Sprite(t, new IntRect(i, 0, 343, 454))); //btw might get extra sprite if sizes no precise
                sprites["angry"][sprites["angry"].Count - 1].Scale = new Vector2f(xscale, yscale);
                sprites["angry"][sprites["angry"].Count - 1].Position = new Vector2f(xpos - sprites["angry"][0].GetGlobalBounds().Width / 2, ypos);

            }
            for (int i = 0; i < (343 * 4); i += 343)
            {
                sprites["happy"].Add(new Sprite(t, new IntRect(i, 454, 343, 454))); //second row of sprites; happy epression
                sprites["happy"][sprites["happy"].Count - 1].Scale = new Vector2f(xscale, yscale);
                sprites["happy"][sprites["happy"].Count - 1].Position = new Vector2f(xpos - sprites["happy"][0].GetGlobalBounds().Width / 2, ypos);
            }
            for (int i = 0; i < (343 * 4); i += 343)
            {

                sprites["neutral"].Add(new Sprite(t, new IntRect(i, 454 * 2, 343, 454)));
                sprites["neutral"][sprites["neutral"].Count - 1].Scale = new Vector2f(xscale, yscale);
                sprites["neutral"][sprites["neutral"].Count - 1].Position = new Vector2f(xpos - sprites["neutral"][0].GetGlobalBounds().Width / 2, ypos);

            }
            neutralarm.Scale = sprites["neutral"][0].Scale;
            angryarm.Scale = sprites["angry"][0].Scale;

            neutralarm.Position = new Vector2f(sprites["neutral"][0].Position.X, sprites["neutral"][0].Position.Y + 450);
            angryarm.Position = new Vector2f(sprites["neutral"][0].Position.X, sprites["neutral"][0].Position.Y + 450);
        }
    }
}