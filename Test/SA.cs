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


        public View fullScreenView, charView;
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
            /*
                        if (e.Code == Keyboard.Key.N)
                        {
                            dialogueBox.forward();

            <<<<<<< HEAD=======
                        }
                        */

            if (e.Code == Keyboard.Key.Space)
            {
                ui_man.SetPrintTime(0);
            }

            if (e.Code == Keyboard.Key.N) {
                ui_man.DialogueNextEndCheck();

            }

            if (e.Code == Keyboard.Key.P) {
                ManagerOfInput.PKeyCheck(State);
            }



            if (e.Code == Keyboard.Key.D)
            {
                //init = true;

                ui_man.SetDialogueViewBox();
                ui_man.RenderDialogue("whos ur daddy im ur daddy whos ur daddy im ur daddy "+
                    "whos ur daddy im ur daddy whos ur daddy im ur daddy " +
                    "whos ur daddy im ur daddy whos ur daddy im ur daddy " +
                    "whos ur daddy im ur daddy whos ur daddy im ur daddy ", "Dad");
            }



            if (e.Code == Keyboard.Key.A) {
                ui_man.SetDialogueViewBox();
                ui_man.RenderDialogue("im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis ", "Alex");
            }


            if (e.Code == Keyboard.Key.M)
            {
                ui_man.SetDialogueViewBox();
                ui_man.RenderDialogue("mushroom mom mushroom mom mushroom mom mushroom mom mushroom mom mushroom mom mushroom mom mushroom mom mushroom mom ", "Mom");
            }

/*
            if (!keys.ContainsKey(e.Code)) {
                keys.Add(e.Code, new bool[] { true, e.Shift, e.Control, e.Alt });
            } else {
                keys[e.Code] = new bool[] { true, e.Shift, e.Control, e.Alt };
            }*/

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
            Texture texture;
            FileStream f = new FileStream("../../Art/angrymom.png", FileMode.Open);
            texture = new Texture(f);


            //mom = new Sprite(texture);

            //the view of the whole game
            //var temp1 = window.DefaultView;
            fullScreenView = window.DefaultView;
            fullScreenView.Viewport = new FloatRect(0, 0, 1, 1);
            window.SetView(fullScreenView);
            //the view port is the whole window

            //fullScreenView.Viewport = new FloatRect(0, 0, 1, 1);
            //window.SetView(fullScreenView);
            //dialogueBox = new DialogueBox(0, 0, 710, 150);
            ui_man.setDialogueBox();


            //charView = new View(mom.GetGlobalBounds());
            //charView.Viewport = new FloatRect(0.7f, 0.3f, 0.23f, 0.5f);

/*master =======
            temp1.Viewport = new FloatRect(0, 0, 1, 1);
            window.SetView(temp1);
            ui_man.setDialogueBox();// = new DialogueBox(0, 0, 710, 150);
            


*/

/*
    


    */


            ui_man.Icantevenwiththiscode3(State, ManagerOfInput);
        }

        protected override void Update()
        {
            if (ui_man.getDialogueBox().active)
            {
                //UNCOMMENT
                window.Draw(ui_man.getDialogueBox());
            }
            
        }
            
        protected override void Draw() {
            //////>>>>>clearColor to magenta
            window.Clear(Color.Magenta);
            ui_man.Icantevenwiththiscode(window);
            ui_man.Icantevenwiththiscode2(window, State, ui_man, startMenu, pauseMenu, settingsMenu);


        }
    }
}
