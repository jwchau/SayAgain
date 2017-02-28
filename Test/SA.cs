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

namespace Test
{

    class SA : Game
    {

        Character mom;

        public View fullScreenView, charView;
        // Character declaration
        private CharacterState Alex = new CharacterState(4.0, 6.9);


        public SA() : base(VideoMode.DesktopMode.Width, VideoMode.DesktopMode.Height, "Say Again?", Color.Magenta)
        {

            window.KeyPressed += onKeyPressed;
            window.KeyReleased += onKeyReleased;
            window.MouseButtonPressed += onMouseButtonPressed;
            window.MouseButtonReleased += onMouseButtonReleased;
            window.MouseMoved += onMouseMoved;

        }

        private void onMouseMoved(object sender, MouseMoveEventArgs e)
        {
            ManagerOfInput.MouseMoveCheck(State.GetState(), e.X, e.Y);
        }

        private void onMouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            ManagerOfInput.MouseReleasedCheck(State.GetState(), ui_man, tfx, cf);
        }

        private void onMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            ManagerOfInput.MouseClickedCheck(State, ui_man, startMenu, pauseMenu, settingsMenu, e.X, e.Y);
        }

        private void onKeyReleased(object sender, KeyEventArgs e)
        {
        }

        private void onKeyPressed(object sender, KeyEventArgs e)
        {


            if (e.Code == Keyboard.Key.Space)
            {
                ui_man.SetPrintTime(0);
            }


            if (e.Code == Keyboard.Key.N) {

                ui_man.DialogueNext();

            }

            if (e.Code == Keyboard.Key.P)
            {
                ManagerOfInput.PKeyCheck(State);
            }



            if (e.Code == Keyboard.Key.D)
            {
                //init = true;
                ui_man.setDialogueBoxPos(new FloatRect(0.0f, 0f, 0.35f, 0.2f));
                ui_man.RenderDialogue("whos ur daddy im ur daddy whos ur daddy im ur daddy " +
                    "whos ur daddy im ur daddy whos ur daddy im ur daddy " +
                    "whos ur daddy im ur daddy whos ur daddy im ur daddy " +
                    "whos ur daddy im ur daddy whos ur daddy im ur daddy ", "Dad");
            }



            if (e.Code == Keyboard.Key.A)
            {
                ui_man.setDialogueBoxPos(new FloatRect(0.3f, 0f, 0.35f, 0.2f));
                ui_man.RenderDialogue("im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis ", "Alex");
            }


            if (e.Code == Keyboard.Key.M)
            {
                ui_man.setDialogueBoxPos(new FloatRect(0.63f, 0f, 0.35f, 0.2f));
                ui_man.RenderDialogue("mushroom mom mushroom mom mushroom mom mushroom mom mushroom mom mushroom mom mushroom mom mushroom mom mushroom mom ", "Mom");
            }

        }



        protected override void LoadContent()
        {
            double[] nums = { -1, 2, 3, 4,
                               1, 2, 3, 4,
                               1, 2, 3, 4, };
            cf = new ContextFilter("school", nums);

            //player manipulated sentences, 4testing
            string test = "My name is Raman. My name is Michael. My name is John. My name is Jill. My name is Yuna. My name is Leo. My name is Koosha.";
            ui_man.produceTextBoxes(test);

        }

        protected override void Initialize()
        {
            /*Texture texture;
            FileStream f = new FileStream("../../Art/angrymom.png", FileMode.Open);
            texture = new Texture(f);*/
            fullScreenView = window.DefaultView;
            fullScreenView.Viewport = new FloatRect(0, 0, 1, 1);
            window.SetView(fullScreenView);
            ui_man.setDialogueBox();
            ui_man.setViews(fullScreenView);

            mom = new Character();
            mom.changeToCoop();

        }

        protected override void Update()
        {
            ui_man.UpdateTimer(State, ManagerOfInput);


        }
        


        protected override void Draw()
        {
            
           
            window.Clear(Color.Magenta);
            ui_man.DrawDialogueBox(window);
            ui_man.DrawUI(window, State, ui_man, startMenu, pauseMenu, settingsMenu);

            window.Draw(mom);
            
        }


    }
}
