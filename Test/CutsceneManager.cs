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
    class CutsceneManager
    {
        public Sprite mom;

        public View view { get; private set; }


        /*public void LoadSprites()
        {
            FileStream angrymom = new FileStream("../../Art/mom_angry.png", FileMode.Open);
        }*/

        public void MakeMom()/*Draw(RenderTarget target, RenderStates states)*/
        {
            IntRect first = new IntRect(0, 0, 360, 450);
            FileStream f = new FileStream("../../Art/mom_angry.png", FileMode.Open);
            Texture t = new Texture(f);
            f.Close();
            mom = new Sprite(t, first);




            /*View resetView = target.GetView();
            target.SetView(view);
            target.SetView(resetView);*/
        }

    }

    class Mom : Drawable
    {
        public void Draw(RenderTarget target, RenderStates states)
        {

        }

        public Mom()
        {

        }
    }



}
