using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;

//
namespace Test {
    class UIElement : Drawable {
        //constructor
        public UIElement() {
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
            {"Default", new Color(214, 214, 214)}, // Red
            {"Blunt",  new Color(179, 97, 255) }, //Purple
            {"Indifferent", new Color(56, 120, 255)}, // Blue
            {"Compassionate", new Color(102, 255, 149)}, // Green
            {"Hesitant", new Color(255, 225, 50)} // Yellow
        };

        protected Dictionary<string, List<string>> buttonSpritePaths = new Dictionary<string, List<string>> {
            {"Default", new List<string>() {"", ""}}, // Red
            {"Blunt",   new List<string>() {"../../Art/UI_Art/buttons n boxes/purplebuttontext.png", "../../Art/UI_Art/buttons n boxes/purplebuttontexthighlight.png"}}, //Purple
            {"Indifferent", new List<string>() {"../../Art/UI_Art/buttons n boxes/bluebuttontext.png", "../../Art/UI_Art/buttons n boxes/bluebuttontexthighlight.png"}}, // Blue
            {"Compassionate", new List<string>() {"../../Art/UI_Art/buttons n boxes/greenbuttontext.png", "../../Art/UI_Art/buttons n boxes/greenbuttontexthighlight.png"}}, // Green
            {"Hesitant", new List<string>() {"../../Art/UI_Art/buttons n boxes/yellowbuttontext.png", "../../Art/UI_Art/buttons n boxes/yellowbuttontexthighlight.png"}} // Yellow
        };

        //methods
        public virtual void Draw(RenderTarget target, RenderStates states) {
            //target.Draw();
        }

        public bool inRange(int value, float min, float max) {
            return value >= min && value <= max;
        }

    }
}
