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
        public UITextBox(float x, float y, string dialogue) {
            //UITextBoxFont = new Font("../../Fonts/Adore64.ttf");
            //public UITextBox(float width, float height, float x, float y, string dialogue) {
            UITextBoxFont = new Font(@"C:\Users\leogo_000\Documents\GitHub\SayAgain\Test\Fonts/Adore64.ttf");
            UITextBoxText = new Text(dialogue, UITextBoxFont);
            UITextBoxText.Position = new Vector2f(x, y);
            box = new RectangleShape(new Vector2f(UITextBoxText.GetGlobalBounds().Width + 5, UITextBoxText.GetGlobalBounds().Height + 5));
            box.Position = new Vector2f(x, y);
            box.FillColor = Color.Red;
            UITextBoxText.Color = Color.White;
            this.affected = false;
        }

        Font UITextBoxFont;
        Text UITextBoxText;
        RectangleShape box;
        bool affected = false;

        public RectangleShape getBox()
        {
            return box;
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

        public Color getBoxColor()
        {
            return box.FillColor;
        }

        public bool getAffected() {
            return affected;
        }

        public void setAffected(bool b) {
            affected = b;
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

        public void UpdateText() { }
    }
}
