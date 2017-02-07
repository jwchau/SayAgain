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

namespace Test {
    
    class SA : Game {
        // Character declaration
        private CharacterState Alex = new CharacterState(4.0, 6.9);


        public SA() : base(VideoMode.DesktopMode.Width, VideoMode.DesktopMode.Height, "Say Again?", Color.Magenta) {

            window.KeyPressed += onKeyPressed;
            window.KeyReleased += onKeyReleased;
            window.MouseButtonPressed += onMouseButtonPressed;
            window.MouseButtonReleased += onMouseButtonReleased;
            window.MouseMoved += onMouseMoved;

        }

        private void onMouseMoved(object sender, MouseMoveEventArgs e) {
            ManagerOfInput.MouseMoveCheck(State.GetState(),e.X,e.Y);
        }

        private void onMouseButtonReleased(object sender, MouseButtonEventArgs e) {
            ManagerOfInput.MouseReleasedCheck(State.GetState(), ui_man, tfx,cf);
        }

        private void onMouseButtonPressed(object sender, MouseButtonEventArgs e) {
            ManagerOfInput.MouseClickedCheck(State, ui_man, startMenu, pauseMenu, settingsMenu, e.X, e.Y);
        }

        private void onKeyReleased(object sender, KeyEventArgs e) {
        }

        private void onKeyPressed(object sender, KeyEventArgs e) {


            if (e.Code == Keyboard.Key.N) {
                ui_man.DialogueNextEndCheck();
            }

            if (e.Code == Keyboard.Key.P) {
                ManagerOfInput.PKeyCheck(State);
            }

            if (e.Code == Keyboard.Key.M) {
                ui_man.StartDialogueBox();
            }

            if (e.Code == Keyboard.Key.Space) {
                ui_man.SetPrintTime(0);

            }
        }


        /// <summary>
        /// LOADCONTENT AND INITIALIZE ARE THE SAME THING. (FIX TOGETHER) RED RED RED RED RED RED RED RED RED BLUE RED RED RED RED RED RED RED
        /// </summary>

        protected override void LoadContent() {
            //context filter load, 4testing
            double[] nums = { -1, 2, 3, 4,
                               1, 2, 3, 4,
                               1, 2, 3, 4, };
            cf = new ContextFilter("school", nums);

            //player manipulated sentences, 4testing
            string test = "My name is Raman. My name is Michael. My name is John. My name is Jill. My name is Yuna. My name is Leo. My name is Koosha.";
            ui_man.produceTextBoxes(test);

        }

        protected override void Initialize() {
            //the view of the whole game
            var temp1 = window.DefaultView;
            //the view port is the whole window
            temp1.Viewport = new FloatRect(0, 0, 1, 1);
            window.SetView(temp1);
            ui_man.setDialogueBox();// = new DialogueBox(0, 0, 710, 150);
            var temp2 = new View(ui_man.dialogueBoxBounds());
            //where i want to view it (inside dialogueBox)
            temp2.Viewport = new FloatRect(0.165f, 0f, 0.65f, 0.27f);
            ui_man.setViews(temp1, temp2);
        }



        protected override void Update() {
            ui_man.Icantevenwiththiscode3(State, ManagerOfInput);
        }

        protected override void Draw() {
            //////>>>>>clearColor to magenta
            window.Clear(Color.Magenta);
            ui_man.Icantevenwiththiscode(window);
            ui_man.Icantevenwiththiscode2(window, State, ui_man, startMenu, pauseMenu, settingsMenu);

        }
    }
}
