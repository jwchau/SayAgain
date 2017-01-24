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
    class UIElement
    {
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


        //methods
        void Draw(){ }

    }
}
