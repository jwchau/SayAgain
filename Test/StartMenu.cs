using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;

namespace Test
{
    class StartMenu : UIElement
    {
        public StartMenu(string type) {
            if (type == "start")
            {
                MenuButtons.Add(new MenuButton(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 3, "Game Start"));
                MenuButtons.Add(new MenuButton(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 3 + 200, "Settings"));
            } else if(type == "settings")
            {
                MenuButtons.Add(new MenuButton(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 3, "8K GAMING"));
                MenuButtons.Add(new MenuButton(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 3 + 200, "<- Back"));
            }
        }

        static UInt32 SCREEN_WIDTH = VideoMode.DesktopMode.Width;
        static UInt32 SCREEN_HEIGHT = VideoMode.DesktopMode.Height;

        List<MenuButton> MenuButtons = new List<MenuButton>();

        public override void Draw(RenderTarget target, RenderStates states)
        {
            foreach (var butt in MenuButtons) {
                target.Draw(butt.getMenuButtonRect());
                target.Draw(butt.getMenuButtonText());
            }
            
        }

        public List<MenuButton> getMenuButtons() {
            return MenuButtons;
        }
    }
}
