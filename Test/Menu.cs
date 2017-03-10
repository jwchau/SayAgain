using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace Test
{
    class Menu : UIElement
    {
        public Menu(string type)
        {
            this.type = type;
            if (type == "start")
            {
                MenuButtons.Add(new MenuButton(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 3, "Start"));
                MenuButtons.Add(new MenuButton(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 3 + 200, "Settings"));
            }
            else if (type == "settings")
            {
                MenuButtons.Add(new MenuButton(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 3, "Sound"));
                MenuButtons.Add(new MenuButton(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 3 + 200, "Back"));
            }
            else if (type == "pause")
            {
                MenuButtons.Add(new MenuButton(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 3 - 100, "Back"));
                MenuButtons.Add(new MenuButton(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 3 + 200, "Settings"));
                MenuButtons.Add(new MenuButton(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 3 + 500, "Quit"));
                pauseBG = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/pausemenu.png"));
                pauseBG.Position = new Vector2f(SCREEN_WIDTH / 2 - pauseBG.GetGlobalBounds().Width / 2, SCREEN_HEIGHT / 2 - pauseBG.GetGlobalBounds().Height / 2);
            }
        }

        static UInt32 SCREEN_WIDTH = VideoMode.DesktopMode.Width;
        static UInt32 SCREEN_HEIGHT = VideoMode.DesktopMode.Height;
        string type;
        List<MenuButton> MenuButtons = new List<MenuButton>();
        Sprite pauseBG;

        //public void DrawBG(RenderWindow target)
        //{
        //    target.Draw(pauseBG);
        //}
        public void DrawPauseBG(RenderTarget target)
        {
            target.Draw(pauseBG);
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            foreach (var butt in MenuButtons)
            {
                target.Draw(butt);
            }

        }

        public List<MenuButton> getMenuButtons()
        {
            return MenuButtons;
        }

        public void SweepButtons(int x, int y, double scalex, double scaley)
        {
            var buttons = getMenuButtons();
            for (var i = 0; i < buttons.Count; i++)
            {
                buttons[i].setHover((int)(x * scalex), (int)(y * scaley));
            }
        }
    }
}
