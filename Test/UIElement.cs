using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;

//
namespace Test
{
    class UIElement : Drawable
    {
        //constructor
        public UIElement()
        {
            this.size = 0;
            this.x = 0;
            this.y = 0;
        }

        //fields
        protected float size;
        protected float x;
        protected float y;
        protected string type;
        protected Dictionary<string, Color> buttonTonalColors = new Dictionary<string, Color>() {
            {"Default", new Color(112, 102, 119)}, // Default
            {"Blunt",  new Color(108, 75, 117) }, //Purple
            {"Indifferent", new Color(78, 79, 160)}, // Blue
            {"Compassionate", new Color(33, 192, 89)}, // Green
            {"Hesitant", new Color(242, 210, 65)} // Yellow
        };

        //methods
        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            //target.Draw();
        }

    }
}
