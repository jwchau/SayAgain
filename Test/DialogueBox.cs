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
        public float w, h, x, y;//, x2, y2, OffsetX, OffsetY, OffsetX2, OffsetY2;
        public Text name, dialogue;
        //public Dialogue line;
        public RectangleShape box;
        public RectangleShape nameBox;


        public void Draw(RenderTarget target, RenderStates states) {
            target.Draw(box);
            target.Draw(nameBox);
            //target.Draw(line);

        }
        public FloatRect GetBounds()
        {
            FloatRect f = new FloatRect(box.GetGlobalBounds().Left,
                nameBox.GetGlobalBounds().Top,
                box.GetGlobalBounds().Width,
                (box.GetGlobalBounds().Top + box.GetGlobalBounds().Height) - nameBox.GetGlobalBounds().Top);
            return f;
        }
        public FloatRect GetGlobalBounds()
        {
            return box.GetGlobalBounds();
        }

        
        public Text BufferName(String speaker) {
            Text name = new Text(speaker, FontObjects.Adore64, 24);
            name.Position = new Vector2f(nameBox.Position.X, nameBox.Position.Y + 15);
            name.Color = Color.Black;
            return name;
        }

        public Text BufferDialogue(String s) {
            Text dialogue = new Text(s, FontObjects.Adore64, 24);
            dialogue.Position = new Vector2f(box.Position.X, box.Position.Y + 20);
            dialogue.Color = Color.Black;
            return dialogue;
        }

    
        public DialogueBox(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.w = width;
            this.h = height;

            box = new RectangleShape(new Vector2f(this.w, this.h));
            box.Position = new Vector2f(this.x - 40, this.y + 35);
            box.OutlineThickness = 3;
            box.OutlineColor = Color.Black;

            nameBox = new RectangleShape(new Vector2f(this.w - 600, this.h - 100));
            nameBox.Position = new Vector2f(this.x, this.y);
            nameBox.OutlineThickness = 3;
            nameBox.OutlineColor = Color.Black;
        }
    
    /*public DialogueBox()
        {
            this.w = 710;
            this.h = 150;
            this.x = 0;
            this.y = 0;
            this.c = Color.Black;

            
            box = new RectangleShape(new Vector2f(w, h));
            box.Position = new Vector2f(x, y);
            box.OutlineThickness = 3;
            box.OutlineColor = c;
            
            nameBox = new RectangleShape(new Vector2f(w-600, h-100));
            nameBox.Position = new Vector2f(this.x + 40, this.y - 35);
            nameBox.OutlineThickness = 3;
            nameBox.OutlineColor = c;

            //line = new Dialogue(x, y, content, c);
            //name = new Text(content, FontObjects.Adore64, 24);

        }*/



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