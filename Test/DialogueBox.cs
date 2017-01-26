using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Drawing;
//eventually make textbox into class for whole dialogue box (including name box)

namespace Test
{
    class DialogueBox : Drawable
    {
        public float w, h, x, y, x2, y2, OffsetX, OffsetY, OffsetX2, OffsetY2;
        public Text name, dialogue;
        public Color c;
        //public Dialogue line;
        public RectangleShape box;
        public RectangleShape nameBox;

        public void Draw(RenderTarget target, RenderStates states) {
            target.Draw(box);
            target.Draw(nameBox);
            //target.Draw(line);

        }
        public FloatRect GetGlobalBounds()
        {
            return box.GetGlobalBounds();
        }

        /*
        public void RenderName(String speaker) {
            Text name = new Text(speaker, FontObjects.Adore64, 24);
            name.Position = new Vector2f(OffsetX2, OffsetY2);
            name.Color = c;
        }
        public void RenderDialogue(String s) {
            Text dialogue = new Text(s, FontObjects.Adore64, 24);
            dialogue.Position = new Vector2f(OffsetX, OffsetY);
            dialogue.Color = c;
        }

    */

    public DialogueBox()
        {
            this.w = 710;
            this.h = 150;
            this.x = 50;
            this.y = 50;
            this.OffsetX = this.x + 30;
            this.OffsetY = this.y + 20;
            this.x2 = x + 40;
            this.y2 = y - 35;
            this.OffsetX2 = this.x2 + 5;
            this.OffsetY2 = this.y2 + 10;
            this.c = Color.Black;

            
            box = new RectangleShape(new Vector2f(w, h));
            box.Position = new Vector2f(x, y);
            box.OutlineThickness = 3;
            box.OutlineColor = c;
            
            nameBox = new RectangleShape(new Vector2f(w-600, h-100));
            nameBox.Position = new Vector2f(x2, y2);
            nameBox.OutlineThickness = 3;
            nameBox.OutlineColor = c;

            //line = new Dialogue(x, y, content, c);
            //name = new Text(content, FontObjects.Adore64, 24);

        }



}
    
/*    class Dialogue: Drawable
    {
        public float x, y;
        public string content;
        public Color Color;
        public Text dialogueText;
        public void Draw(RenderTarget target, RenderStates states) {
            target.Draw(dialogueText);
        }

        public Dialogue(float x, float y, string content, Color c)
        {
            this.x = x;
            this.y = y;
            this.content = content;
            this.Color = c;
            dialogueText = new Text(content, FontObjects.Adore64, 24);
            dialogueText.Color = Color;
            dialogueText.Position = new Vector2f(x, y);
       
    } }*/
}