using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;

namespace Test
{
    class CharacterState : Drawable{
        //fields
        private string who;
        //private double mood;
        //private double volatility;
        //private double goal;
        double talkedTo = 0;
        static bool[] targets = { false,false,false }; // 0 = alex 1 = mom 2 = dad

        Color color;

        RectangleShape characterRect = new RectangleShape(new Vector2f(75, 75));

        //fields made for drawing character box, temp
        int x = 0;
        int y = 10;

        //methods
        public CharacterState(string who) {
            this.who = who;
            characterRect.OutlineColor = Color.White;
            if (who == "alex")
            {
                this.x = 10;
               
                this.color = Color.Red;
            }
            else if (who == "mom")
            {
                this.x = 110;

                this.color = Color.Black;
            }
            else if (who == "dad") {
                this.x = 210;

                this.color = Color.Green;
            }

            characterRect.Position = new Vector2f(x, y);
            characterRect.FillColor = this.color;
            //this.mood = mood;
            //this.volatility = volatility;
        }

        public void SetTalked(char f, double amount) {
            if (f == 'i') {
                talkedTo += amount;
            } else if (f == 'd') {
                talkedTo -= amount;
            }
        }

        public bool CheckTalked() {
            if (talkedTo == 10000000.0) {
                return true;
            }
            return false;
        }

        public void DecreaseMood() {

        }

        public void setHover(bool b)
        {
            if (b)
            {
                characterRect.FillColor = Color.White;
            }
            else
            {
                characterRect.FillColor = this.color;
                //Console.WriteLine("i forgot what i said");
            }
        }

        public bool Contains(int mouseX, int mouseY)
        {
            FloatRect bounds = characterRect.GetGlobalBounds();
            if (mouseX >= bounds.Left && mouseX <= bounds.Left + bounds.Width && mouseY >= bounds.Top && mouseY <= bounds.Top + bounds.Height)
            {
                return true;
            }
            return false;
        }

        //#region InputManager_targetCheck
        //public void targetCheck(int x, int y) {
        //    if (this.Contains(x, y)) this.setTargets(this.who);
        //}
        //#endregion

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(characterRect);

        }

    }
}
