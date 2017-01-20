using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;


namespace Test
{
    class DragText : SA
    {
        string fontPath;
        Font font;
        string message;
        Text text;

        float xPos;
        float yPos;
        uint size;
        Color msgColor;

        public DragText(string fontPath, string message, float xPos, float yPos, uint size, Color msgColor)
        {
            this.fontPath = fontPath;
            font = new Font(fontPath);
            this.message = message;
            text = new Text(this.message, font, size);
            this.msgColor = msgColor;
            text.Color = msgColor;

            this.xPos = xPos;
            this.yPos = yPos;
            this.size = size;

        }

        void Draw()
        {
            Transform position = new Transform();
        }
    }
}
