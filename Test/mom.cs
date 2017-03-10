﻿using System;
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
    class Mom : Character
    {
        Texture t = new Texture("../../Art/momsprites.png");
        List<Sprite> angrysprites = new List<Sprite>();
        List<Sprite> happysprites = new List<Sprite>();
        List<Sprite> neutralsprites = new List<Sprite>();
        List<Sprite> sadsprites = new List<Sprite>();




        public override void checkFNC()
        {
            throw new NotImplementedException();
        }

        public override void setSpriteEmotion(spriteEmotion e)
        {
            switch (e)
            {
                case spriteEmotion.angry:
                    setSprite(angrysprites);
                    break;
                case spriteEmotion.happy:
                    setSprite(happysprites);
                    break;
                case spriteEmotion.sad:
                    setSprite(angrysprites);
                    break;
                case spriteEmotion.neutral:
                    setSprite(neutralsprites);
                    break;
            }
        } 

        public Mom()
        {

            //determine size and position
            xpos = 1300;
            ypos = 300;
            xscale = 0.5f;
            yscale = 0.5f;
            xscale = 1;
            yscale = 1;

            for (int i = 0; i < (361 * 7); i += 361)
            {
                angrysprites.Add(new Sprite(t, new IntRect(i, 0, 361, 465))); //btw might get extra sprite if sizes no precise
            }
            for (int i = 0; i < (361 * 9); i += 361)
            {
                happysprites.Add(new Sprite(t, new IntRect(i, 465, 361, 465))); //second row of sprites; happy epression 
            }
            for (int i = 0; i < (361 * 4); i += 361)
            {
                neutralsprites.Add(new Sprite(t, new IntRect(i, 465 * 2, 361, 465)));
            }
            for (int i = 0; i < (361 * 4); i += 361)
            {
                sadsprites.Add(new Sprite(t, new IntRect(i, 465 * 3, 361, 465)));
            }
            
        }
    }
}