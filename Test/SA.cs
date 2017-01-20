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

        UI_button testButt = new UI_button(50,40,40,"SUH DUDE");
        UI_button testButt2 = new UI_button(30, 100, 100, "DUDE");

     


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
            ManagerOfInput.SetMouseInput(e.X, e.Y);
           /* if (ManagerOfInput.GetMouseRelease() == true)
            {
                ManagerOfInput.SetMouseMove(false);

            }
            else {
                ManagerOfInput.SetMouseMove(true);
            }*/

            
        }

        private void onMouseButtonReleased(object sender, MouseButtonEventArgs e)    
        {
            /* ManagerOfInput.SetMouseRelease(true);
             ManagerOfInput.SetMouseDown(false);*/
            
        }

        private void onMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            

            /*ManagerOfInput.SetMouseRelease(false);
            ManagerOfInput.SetMouseDown(true);
            ManagerOfInput.SetMouseInput(e.X, e.Y);*/

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
           // Console.WriteLine(ManagerOfInput.GetMX() + " : " + ManagerOfInput.GetMY());
            if (ManagerOfInput.CheckCollision(testButt.getRectBounds()) &&
                Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                Console.WriteLine("blah");
                testButt.translate(ManagerOfInput.GetMX(), ManagerOfInput.GetMY());
                //if (ManagerOfInput.GetMouseMove())
                //{
                //    Console.WriteLine("We're dragging bois");
                //}
            }
        }

        protected override void Draw()
        {
            
            window.Draw(testButt.getUI_ButtonRect());
            window.Draw(testButt.getUI_ButtonText());
            //window.Draw(testButt2.getUI_ButtonRect());
            //window.Draw(testButt2.getUI_ButtonText());
        }
    }
}
