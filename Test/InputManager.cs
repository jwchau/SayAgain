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

        public void SetMouseInput(int x, int y) {
            MouseX = x;
            MouseY = y;
        }

        public int GetMX()
        {
            return MouseX;
        }

        public int GetMY()
        {
            return MouseY;
        }

        public void SetMouseDown(bool Mouse)
        {
            MouseDown = Mouse;
        }

        public void SetMouseRelease(bool Mouse)
        {
            MouseRelease = Mouse;
        }

        public void SetMouseMove(bool Mouse)
        {
            MouseMove = Mouse;
        }

        public bool GetMouseDown()
        {
            return MouseDown;
        }

        public bool GetMouseRelease()
        {
            return MouseRelease;
        }

        public bool GetMouseMove()
        {
            return MouseMove;
        }

        public bool CheckCollision(FloatRect bounds)
        {
            if (MouseX >= bounds.Left && MouseX <= bounds.Left + bounds.Width && MouseY >= bounds.Top && MouseY <= bounds.Top + bounds.Height)
            {
                return true;
            }
            return false;
        }

    }
}
