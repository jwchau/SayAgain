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
    class UITextBox : UIElement//The box where the dialogue will appear, not clickable or draggable
    {
        public UITextBox(float x, float y, string dialogue, int cluster)
        {
            UITextBoxText = new Text(dialogue, UITextBoxFont, getFontSize());
            UITextBoxText.Position = new Vector2f(x + 3, y + 12);
            box = new RectangleShape(new Vector2f(UITextBoxText.GetGlobalBounds().Width + 10, (float)(UITextBoxText.GetGlobalBounds().Height*1.5) + 10));
            box.Position = new Vector2f(x, y + 17);
            box.FillColor = buttonTonalColors["Default"];
            prevColor = buttonTonalColors["Default"];
            UITextBoxText.Color = new Color(67, 65, 69);
            this.affected = false;
            this.cluster = cluster;
        }

        Font UITextBoxFont = new Font("../../Art/UI_Art/fonts/ticketing/TICKETING/ticketing.ttf");
        Text UITextBoxText;
        RectangleShape box;
        bool affected = false;
        Color prevColor;
        bool mouseWasIn = false;
        int cluster = -1;
        tone tone = tone.Root;
        UInt32 SCREEN_WIDTH = VideoMode.DesktopMode.Width;
        UInt32 SCREEN_HEIGHT = VideoMode.DesktopMode.Height;

        private uint getFontSize()
        {

            return (uint)((SCREEN_WIDTH / 1920) * 30);
        }

        public RectangleShape getBox()
        {
            return box;
        }

        public tone getTone() {
            return tone;
        }

        public void setTone(tone tonez) {
            tone = tonez;
        }

        public int getCluster()
        {
            return cluster;
        }

        public Text getBoxText()
        {
            return UITextBoxText;
        }

        public FloatRect getBoxBounds()
        {
            return box.GetGlobalBounds();
        }


        public void changeDialogue(string newDialogue)
        {
            UITextBoxText.DisplayedString = newDialogue;
        }

        public void setBoxColor(Color color)
        {
            box.FillColor = color;
        }

        public void setPrevColor(Color color)
        {
            prevColor = color;
        }

        public Color getBoxColor(string s)
        {
            if (s == "curr") return box.FillColor;
            return prevColor;
        }

        public bool getAffected()
        {
            return affected;
        }

        public void setAffected(bool b)
        {
            affected = b;
        }

        public void setMouseWasIn(bool w)
        {
            mouseWasIn = w;
        }

        public bool wasMouseIn()
        {
            return mouseWasIn;
        }

        public bool Contains(int mouseX, int mouseY)
        {
            FloatRect bounds = getBoxBounds();
            if (mouseX >= bounds.Left && mouseX <= bounds.Left + bounds.Width && mouseY >= bounds.Top && mouseY <= bounds.Top + bounds.Height)
            {
                return true;
            }
            return false;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(box);
            target.Draw(UITextBoxText);
        }



        public string getDialogue()
        {
            return UITextBoxText.DisplayedString;
        }
    }
}
