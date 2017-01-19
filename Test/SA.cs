using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace Test
{
    class SA : Game
    {

        Dictionary<SFML.Window.Keyboard.Key, bool[]> keys = new Dictionary<SFML.Window.Keyboard.Key, bool[]>();
        Font testFont;
        Text testText;

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
            testFont = new Font("Content/ARCADECLASSIC.ttf");
            testText = new Text("TESTING", testFont);
            
        }

        protected override void Initialize()
        {
            
        }

        protected override void Update()
        {

        }

        protected override void Draw()
        {
            window.Draw(testText);
        }
    }
}
