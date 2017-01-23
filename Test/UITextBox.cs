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
    class UITextBox //The box where the dialogue will appear, not clickable or draggable
    {
        public UITextBox(float width, float height, float x, float y, string dialogue) {
            UITextBoxFont = new Font("Content/ARCADECLASSIC.ttf");
            UITextBoxText = new Text(dialogue, UITextBoxFont);
            UITextBoxText.Position = new Vector2f(x, y);
            box = new RectangleShape(new Vector2f(width, height));
            box.Position = new Vector2f(x, y);
            box.FillColor = Color.Black;
            UITextBoxText.Color = Color.White;
        }

        public RectangleShape getBox() {
            return box;
        }

        public Text getBoxText() {
            return UITextBoxText;
        }

        public FloatRect getBoxBounds() {
           return box.GetGlobalBounds();
        }


        public void changeDialogue(string newDialogue) {
            UITextBoxText = new Text(newDialogue, UITextBoxFont);
        }

        public void setBoxColor(Color color) {
            box.FillColor = color;
        }

        Font UITextBoxFont;
        Text UITextBoxText;
        RectangleShape box;
    }
}
