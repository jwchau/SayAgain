using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;

namespace Test
{
    class MenuButton : UIElement
    {
        public MenuButton(float x, float y, string content)
        {
            this.x = x;
            this.y = y;
            buttonText = new Text(content, buttonTextFont);
            buttonText.Color = new Color(227, 215, 207);

            buttonText.Position = new SFML.System.Vector2f(x - buttonText.GetGlobalBounds().Width/2, y);

            rect = new RectangleShape(new SFML.System.Vector2f(buttonText.GetGlobalBounds().Width + 30, buttonText.GetGlobalBounds().Height + 20));
            rect.Position = new SFML.System.Vector2f(x - buttonText.GetGlobalBounds().Width/2 - 10, y - 7);
            Color myColor = new Color(112, 102, 119);
            rect.FillColor = myColor;
        }

        static UInt32 SCREEN_WIDTH = VideoMode.DesktopMode.Width;
        static UInt32 SCREEN_HEIGHT = VideoMode.DesktopMode.Height;

        Font buttonTextFont = new Font("Fonts/Adore64.ttf");
        Text buttonText;
        RectangleShape rect;

        public RectangleShape getMenuButtonRect()
        {
            return rect;
        }

        public Text getMenuButtonText()
        {
            return buttonText;
        }

        public FloatRect getRectBounds()
        {
            return rect.GetGlobalBounds();
        }

        public bool Contains(int mouseX, int mouseY)
        {
            FloatRect bounds = getRectBounds();
            if (mouseX >= bounds.Left && mouseX <= bounds.Left + bounds.Width && mouseY >= bounds.Top && mouseY <= bounds.Top + bounds.Height)
            {
                return true;
            }
            return false;
        }
    }
}
