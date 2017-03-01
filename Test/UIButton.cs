using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;

namespace Test {
    class UIButton : UIElement {
        //constructor
        public UIButton(float x, float y, tone content, string newDialogue) {

            this.newDialogue = newDialogue;
            this.buttonTone = content;

            buttonText = new Text(content.ToString(), buttonTextFont);
            buttonText.Position = new SFML.System.Vector2f(x - buttonText.GetGlobalBounds().Width / 2, y);

            rect = new RectangleShape(new SFML.System.Vector2f(buttonText.GetGlobalBounds().Width + 7, buttonText.GetGlobalBounds().Height + 10));
            rect.Position = new SFML.System.Vector2f(x - buttonText.GetGlobalBounds().Width / 2, y);

            this.x = x - buttonText.GetGlobalBounds().Width / 2;
            this.y = y;

            tonalColor = buttonTonalColors[content.ToString()];
            Color bgColor = buttonTonalColors[content.ToString()];
            rect.FillColor = bgColor;
        }

        //fields
        static UInt32 SCREEN_WIDTH = VideoMode.DesktopMode.Width;
        static UInt32 SCREEN_HEIGHT = VideoMode.DesktopMode.Height;

        Font buttonTextFont = new Font("../../Fonts/Adore64.ttf");
        Text buttonText;
        RectangleShape rect;
        string newDialogue;
        bool selected = false;
        int mouseOffsetX = 0;
        int mouseOffsetY = 0;
        Color tonalColor;
        tone buttonTone;

        //methods
        //String eventHandler;

        public SFML.System.Vector2f getRectSize() {
            return rect.Size;
        }

        public void setButtonColor(Color c) {
            rect.FillColor = c;
        }

        public float getX() {
            return x;
        }

        public float getY() {
            return y;
        }

        public void setX(float newX) {
            x = newX;
        }

        public void setY(float newY) {
            y = newY;
        }
        public Text getUIButtonText() {
            return buttonText;
        }

        public tone getTone() {
            return buttonTone;
        }

        public FloatRect getRectBounds() {
            return rect.GetGlobalBounds();
        }

        public void SetMouseOffset(int x, int y) {
            mouseOffsetX = x;
            mouseOffsetY = y;
        }

        public void SetSelected(bool val) {
            selected = val;
        }

        public void setTonalColor(Color c) {
            this.tonalColor = c;
        }

        public bool GetSelected() {
            return selected;
        }

        public bool Contains(int mouseX, int mouseY) {
            FloatRect bounds = getRectBounds();
            if (mouseX >= bounds.Left && mouseX <= bounds.Left + bounds.Width && mouseY >= bounds.Top && mouseY <= bounds.Top + bounds.Height) {
                return true;
            }
            return false;
        }

        public void snapBack() {
            rect.Position = new SFML.System.Vector2f(x, y);
            buttonText.Position = new SFML.System.Vector2f(x, y);
        }

        public void translate(int x, int y, double winx, double winy) {
            var temp = screenHelper(winx, winy);
            var bounds = getRectBounds();
            double newXPos = x - mouseOffsetX;
            double newYPos = y - mouseOffsetY;

            if (x - mouseOffsetX < 0) {
                newXPos = 0;
            } else if (x - mouseOffsetX + bounds.Width > winx) {
                newXPos = winx - bounds.Width;
            }

            if (y - mouseOffsetY < 0) {
                newYPos = 0;
            } else if (y - mouseOffsetY + bounds.Height > winy) {
                newYPos = (float)winy - bounds.Height;
            }

            rect.Position = new SFML.System.Vector2f((float)newXPos, (float)newYPos);
            buttonText.Position = new SFML.System.Vector2f((float)newXPos, (float)newYPos);
        }

        #region screen helper
        private Tuple<double, double> screenHelper(double winx, double winy) {
            var DesktopX = (double)VideoMode.DesktopMode.Width;
            var DesktopY = (double)VideoMode.DesktopMode.Height;
            return new Tuple<double, double>(DesktopX / winx, DesktopY / winy);
        }
        #endregion

        public string getNewDialogue() {
            return newDialogue;
        }

        public Color getTonalColor() {
            return tonalColor;
        }

        public override void Draw(RenderTarget target, RenderStates states) {
            target.Draw(rect);
            target.Draw(buttonText);
        }

    }
}
