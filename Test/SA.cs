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

        UIButton[] buttons = new UIButton[] {
            new UIButton(80, 100, 40, "SUH DUDE"),
            new UIButton(40, 20, 80, "woah"),
            new UIButton(50, 200, 200, "wubalubadubdub")
        };
        //UIButton testButt = new UIButton(50,40,40,"SUH DUDE");
        //UI_button testButt2 = new UI_button(30, 100, 100, "DUDE");

        InputManager ManagerOfInput = new InputManager();

        public SA() : base(800, 600, "Say Again?", Color.Magenta)
        {

            window.KeyPressed += onKeyPressed;
            window.KeyReleased += onKeyReleased;
            window.MouseButtonPressed += onMouseButtonPressed;
            window.MouseButtonReleased += onMouseButtonReleased;
            window.MouseMoved += onMouseMoved;

        }

        private void onMouseMoved(object sender, MouseMoveEventArgs e)
        {
            if(ManagerOfInput.GetMouseDown())
            {
                ManagerOfInput.SetMouseMove(true);
                ManagerOfInput.SetMousePos(e.X, e.Y);
            }
        }

        private void onMouseButtonReleased(object sender, MouseButtonEventArgs e)    
        {
            ManagerOfInput.SetMouseMove(false);
            ManagerOfInput.SetMouseDown(false);
            ManagerOfInput.SetMouseRelease(true);
            for (var i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].GetSelected())
                {
                    buttons[i].SetSelected(false);
                }
            }
        }

        private void onMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            for(var i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].Contains(e.X, e.Y))
                {
                    var bounds = buttons[i].getRectBounds();
                    buttons[i].SetMouseOffset(e.X - (int)bounds.Left, e.Y - (int)bounds.Top);
                    buttons[i].SetSelected(true);
                }
            }
            
            ManagerOfInput.SetMousePos(e.X, e.Y);
            ManagerOfInput.SetMouseRelease(false);
            ManagerOfInput.SetMouseDown(true);
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
           
        }

        protected override void Initialize()
        {
            
        }



        protected override void Update()
        {
            if (ManagerOfInput.GetMouseDown())
            {
                var MouseCoord = ManagerOfInput.GetMousePos();
                for (var i = 0; i < buttons.Length; i++)
                {
                    if (buttons[i].GetSelected())
                    {
                        buttons[i].translate(MouseCoord[0], MouseCoord[1]);
                    }
                }
            }
        }

        protected override void Draw()
        {
            for (var i = 0; i < buttons.Length; i++)
            {
                window.Draw(buttons[i].getUIButtonRect());
                window.Draw(buttons[i].getUIButtonText());
            }
            
        }
    }
}
