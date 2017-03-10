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
    class MenuButton : UIElement
    {
        public MenuButton(float x, float y, string content)
        {
            this.x = x;
            this.y = y;
            this.content = content;
            menuButtonSprite = new Sprite(new Texture(buttonSpritePaths[content][0]));
            menuButtonSpriteHighlight = new Sprite(new Texture(buttonSpritePaths[content][1]));
            menuButtonSprite.Position = new Vector2f(x - menuButtonSprite.GetGlobalBounds().Width/2, y - menuButtonSprite.GetGlobalBounds().Height / 2);
            menuButtonSpriteHighlight.Position = new Vector2f(x - menuButtonSprite.GetGlobalBounds().Width / 2, y - menuButtonSprite.GetGlobalBounds().Height / 2);

        }

        static UInt32 SCREEN_WIDTH = VideoMode.DesktopMode.Width;
        static UInt32 SCREEN_HEIGHT = VideoMode.DesktopMode.Height;

        Sprite menuButtonSprite;
        Sprite menuButtonSpriteHighlight;
        string content;
        bool hover = false;

        public void setHover(int mouseX, int mouseY)
        {
            hover = Contains(mouseX, mouseY);
        }

        public Sprite getMenuButtonSprite()
        {
            return menuButtonSprite;
        }

        public string getMenuButtonContent()
        {
            return content;
        }
        
        public FloatRect getRectBounds()
        {
            return menuButtonSprite.GetGlobalBounds();
        }

        public bool Contains(int mouseX, int mouseY)
        {
            FloatRect bounds = getRectBounds();
            if (mouseX >= bounds.Left && mouseX <= bounds.Left + (bounds.Width - 4) && mouseY >= bounds.Top && mouseY <= bounds.Top + (bounds.Height - 4))
            {
                return true;
            }
            return false;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            //target.Draw(rect);
            if (hover)
            {
                target.Draw(menuButtonSpriteHighlight);
            }
            else
            {
                target.Draw(menuButtonSprite);
            }
            //target.Draw(buttonText);
        }
    }
}
