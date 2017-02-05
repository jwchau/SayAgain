using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;

namespace Test
{
    class UIButton : UIElement
    {
        //constructor
        public UIButton(float x, float y, string content, string newDialogue)
        {

            this.x = x;
            this.y = y;
            buttonText = new Text(content, buttonTextFont);
            buttonText.Position = new SFML.System.Vector2f(x - buttonText.GetGlobalBounds().Width / 2, y);

            buttonText.Color = new Color(227, 215, 207);

            rect = new RectangleShape(new SFML.System.Vector2f(buttonText.GetGlobalBounds().Width + 25, 45));
            rect.Position = new SFML.System.Vector2f(x - buttonText.GetGlobalBounds().Width / 2 - 7, y - 5);
            rect.FillColor = Color.Black;
            Color bgColor = new Color(86, 82, 100);
            rect.FillColor = bgColor;
            this.newDialogue = newDialogue;
            tonalColor = buttonTonalColors[content];
        }

        //fields
        static UInt32 SCREEN_WIDTH = VideoMode.DesktopMode.Width;
        static UInt32 SCREEN_HEIGHT = VideoMode.DesktopMode.Height;

        Font buttonTextFont = new Font("Fonts/Adore64.ttf");
        Text buttonText;
        RectangleShape rect;
        string newDialogue;
        bool selected = false;
        int mouseOffsetX = 0;
        int mouseOffsetY = 0;
        Color tonalColor;

        //methods
        //String eventHandler;

        public float getX()
        {
            return x;
        }

        public float getY()
        {
            return y;
        }

        public void setX(float newX)
        {
            x = newX;
        }

        public void setY(float newY)
        {
            y = newY;
        }
        public Text getUIButtonText()
        {
            return buttonText;
        }

        public FloatRect getRectBounds()
        {
            return rect.GetGlobalBounds();
        }

        public void SetMouseOffset(int x, int y)
        {
            mouseOffsetX = x;
            mouseOffsetY = y;
        }

        public void SetSelected(bool val)
        {
            selected = val;
        }

        public bool GetSelected()
        {
            return selected;
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

        public void snapBack()
        {
            rect.Position = new SFML.System.Vector2f(x - buttonText.GetGlobalBounds().Width / 2 - 7, y - 5);
            buttonText.Position = new SFML.System.Vector2f(x - buttonText.GetGlobalBounds().Width / 2, y);
        }

        public void translate(int x, int y)
        {
            var bounds = getRectBounds();
            var newXPos = x - mouseOffsetX;
            var newYPos = y - mouseOffsetY;
            if (x - mouseOffsetX < 0)
            {
                newXPos = 0;
            }
            else if (x - mouseOffsetX + (int)bounds.Width > SCREEN_WIDTH)
            {
                newXPos = (int)SCREEN_WIDTH - (int)bounds.Width;
            }

            if (y - mouseOffsetY < 0)
            {
                newYPos = 0;
            }
            else if (y - mouseOffsetY + (int)bounds.Height > (int)SCREEN_HEIGHT)
            {
                newYPos = (int)SCREEN_HEIGHT - (int)bounds.Height;
            }

            rect.Position = new SFML.System.Vector2f(newXPos, newYPos);
            buttonText.Position = new SFML.System.Vector2f(newXPos + 8, newYPos + 5);

        }

        public string getNewDialogue()
        {
            return newDialogue;
        }

        public Color getTonalColor()
        {
            return tonalColor;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(rect);
            target.Draw(buttonText);
        }
    
    }
}
