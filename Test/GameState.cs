using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Drawing;

namespace Test
{
    class GameState
    {
        public GameState()
        {
            currentState = "menu";
            currentMenuState = "start";
        }

        string currentState;
        string currentMenuState;

        public string GetState()
        {
            return currentState;
        }

        public void SetState(string state)
        {
            if (state != "menu" && state != "game" && state != "pause")
            {
                throw new FormatException();
            }
            currentState = state;
        }

        public string GetMenuState()
        {
            return currentMenuState;
        }

        public void SetMenuState(string state)
        {
            if (state != "start" && state != "settings" && state != "pause")
            {
                throw new FormatException();
            }
            currentMenuState = state;
        }
    }
}
