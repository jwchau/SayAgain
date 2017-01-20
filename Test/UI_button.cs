using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;

namespace Test
{
    class UI_button:UIElement
    {
        //constructor
        public UI_button(float size, float x, float y, string content) {
            this.size = size;
            this.x = x;
            this.y = y;

            testFont = new Font("Content/ARCADECLASSIC.ttf");
            testText = new Text(content, testFont);
            testText.Position = new SFML.System.Vector2f(x, y);

            rect = new RectangleShape(new SFML.System.Vector2f(size, size));
            rect.Position = new SFML.System.Vector2f(x, y);
            rect.FillColor = Color.Black;
            //rect.OutlineThickness = 3.5f;
            //rect.OutlineColor = Color.Red;
            Color myColor = new Color(177, 177, 177);
            rect.FillColor = myColor;
        }

        //fields
        Font testFont;
        Text testText;
        RectangleShape rect;
        private bool drag = false;



        //methods
        String eventHandler;


        public RectangleShape getUI_ButtonRect() {
            return rect;
        }

        public FloatRect getRectBounds()
        {
            return rect.GetGlobalBounds();
        }

        public Text getUI_ButtonText() {
            return testText;
        }

        public void SetDraggable(bool d)
        {
            this.drag = d;
        }

        public void translate(int x, int y)
        {

            float OffsetX = x - rect.GetGlobalBounds().Left;
            float OffsetY = y - rect.GetGlobalBounds().Top;

            Console.WriteLine(OffsetX + ": " + OffsetY);

            rect.Position = new SFML.System.Vector2f(x, y);
            testText.Position = new SFML.System.Vector2f(x, y);

        }

        public void Draw() {
            //need access to window
        }
    }
}
