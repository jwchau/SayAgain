using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SFML.Graphics;
using SFML.Window;
using System.Drawing;

namespace Test
{

    class FontObjects
    {
        public static Font Adore64 = new Font(new FileStream("../../Fonts/Adore64.ttf", FileMode.Open));
    }


    class SA : Game
    {
        Dictionary<SFML.Window.Keyboard.Key, bool[]> keys = new Dictionary<SFML.Window.Keyboard.Key, bool[]>();
        Font font;
        Text text;
        

        public SA() : base(800, 600, "Say Again?", Color.Magenta)
        {
            window.KeyPressed += onKeyPressed;
            window.KeyReleased += onKeyReleased;
        }

        private void onKeyReleased(object sender, KeyEventArgs e)
        {
            keys[e.Code] = new bool[] { false, e.Shift, e.Control, e.Alt };
        }

        private void onKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Space)
            {
                text.Position = new SFML.System.Vector2f(400, 300);
            }
            if (!keys.ContainsKey(e.Code))
            {
                keys.Add(e.Code, new bool[] { true, e.Shift, e.Control, e.Alt });
            } else
            {
                keys[e.Code] = new bool[] { true, e.Shift, e.Control, e.Alt };
            }
        }



        protected override void LoadContent()
        {
           
            Console.WriteLine("LoadContent");

        }

        protected override void Initialize()
        {
            text = new Text("say again by team babble fish", FontObjects.Adore64, 24);
            text.Color = Color.Black;
            Console.WriteLine("Initialize");

        }

        protected override void Update()
        {

        }

        protected override void Draw()
        {
            window.Draw(text);
            
        }
    }
}
