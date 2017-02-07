using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Test
{
    class InputManager
    {
        public InputManager() {

        }

        //fields
        int MouseX;
        int MouseY;
        bool MouseDown = false;
        bool MouseRelease = false;
        bool MouseMove = false;

        //check game and mouse pressed->set position
        public void MouseMoveCheck(string state, int x, int y) {
            if (state == "game") {
                if (this.GetMouseDown()) {
                    this.SetMouseMove(true);
                    this.SetMousePos(x,y);
                }
            }
        }






        /////////////////////////////////////////////////BUILT-IN
        public void SetMousePos(int x, int y) {
            MouseX = x;
            MouseY = y;
        }

        public int[] GetMousePos()
        {
            return new int[] { MouseX, MouseY };
        }

        public void SetMouseDown(bool value)
        {
            MouseDown = value;
        }

        public bool GetMouseDown()
        {
            return MouseDown;
        }

        public void SetMouseRelease(bool value)
        {
            MouseRelease = value;
        }

        public bool GetMouseRelease()
        {
            return MouseRelease;
        }

        public void SetMouseMove(bool value)
        {
            MouseMove = value;
        }

        public bool GetMouseMove()
        {
            return MouseMove;
        }

        public void printMouseStuff()
        {
            Console.WriteLine(MouseDown + ", " + MouseMove + ", " + MouseRelease);
        }

        public bool CheckCollision(FloatRect bounds)
        {
            if (MouseX >= bounds.Left && MouseX <= bounds.Left + bounds.Width && MouseY >= bounds.Top && MouseY <= bounds.Top + bounds.Height)
            {
                return true;
            }
            return false;
        }
        //////////////////////////////////////////////////////////////////////BUILT-IN
    }
}
