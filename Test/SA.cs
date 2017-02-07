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

<<<<<<< HEAD

            if (e.Code == Keyboard.Key.D)
            {
                init = true;

                dialogueBox.view.Viewport = new FloatRect(0.0f, 0f, 0.35f, 0.2f); 
                dialogueBox.renderDialogue("whos ur daddy im ur daddy whos ur daddy im ur daddy "+
                    "whos ur daddy im ur daddy whos ur daddy im ur daddy " +
                    "whos ur daddy im ur daddy whos ur daddy im ur daddy " +
                    "whos ur daddy im ur daddy whos ur daddy im ur daddy ", "Dad");
            }



            if (e.Code == Keyboard.Key.A) {
                init = true;
                dialogueBox.view.Viewport = new FloatRect(0.3f, 0f, 0.35f, 0.2f);
                dialogueBox.renderDialogue("im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis im alexis ", "Alex");
            }
=======
            if (e.Code == Keyboard.Key.M) {
                ui_man.StartDialogueBox();
            }

            if (e.Code == Keyboard.Key.Space) {
                ui_man.SetPrintTime(0);
>>>>>>> master

            if (e.Code == Keyboard.Key.M)
            {
                dialogueBox.view.Viewport = new FloatRect(0.63f, 0f, 0.35f, 0.2f);
                init = true;
                dialogueBox.renderDialogue("mushroom mom mushroom mom mushroom mom mushroom mom mushroom mom mushroom mom mushroom mom mushroom mom mushroom mom ", "Mom");
            }
<<<<<<< HEAD



            if (!keys.ContainsKey(e.Code)) {
                keys.Add(e.Code, new bool[] { true, e.Shift, e.Control, e.Alt });
            } else {
                keys[e.Code] = new bool[] { true, e.Shift, e.Control, e.Alt };
            }
=======
>>>>>>> master
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


            mom = new Sprite(texture);

            //the view of the whole game
            var temp1 = window.DefaultView;
            //the view port is the whole window
<<<<<<< HEAD
            fullScreenView.Viewport = new FloatRect(0, 0, 1, 1);
            window.SetView(fullScreenView);
            dialogueBox = new DialogueBox(0, 0, 710, 150);



            charView = new View(mom.GetGlobalBounds());
            charView.Viewport = new FloatRect(0.7f, 0.3f, 0.23f, 0.5f);

=======
            temp1.Viewport = new FloatRect(0, 0, 1, 1);
            window.SetView(temp1);
            ui_man.setDialogueBox();// = new DialogueBox(0, 0, 710, 150);
            var temp2 = new View(ui_man.dialogueBoxBounds());
            //where i want to view it (inside dialogueBox)
            temp2.Viewport = new FloatRect(0.165f, 0f, 0.65f, 0.27f);
            ui_man.setViews(temp1, temp2);
>>>>>>> master
        }



        protected override void Update() {
<<<<<<< HEAD
            if (State.GetState() == "game") {


                // Timer update
                if (State.getCountDown() > 0) {
                    //as long as you are not out of time
                    State.setNewTime((DateTime.Now.Ticks / 10000000) - State.getTimeDiff());
                    State.setCountDown(9 - (State.getNewTime() - State.getGameTime()));

                }

                // Get the current UI Textboxes from the UI Manager
                var playerDialogues = ui_man.getPlayerDialogues();

                // Get the mouse coordinates from Input Manager
                var MouseCoord = ManagerOfInput.GetMousePos();

                // If the mouse is currently dragging
                if (ManagerOfInput.GetMouseDown()) {
                    // Get tonal buttons from UI Manager
                    var buttons = ui_man.getButtons();

                    // Loop through buttons
                    for (var i = 0; i < buttons.Count; i++) {
                        // Find button currently being interacted with
                        if (buttons[i].GetSelected()) {
                            // Move the button around the screen
                            buttons[i].translate(MouseCoord[0], MouseCoord[1]);

                            // Check collision with UI Textboxes
                            // Loop through UI Textboxes
                            for (var j = 0; j < playerDialogues.Count; j++) {
                                // If the mouse just came from inside a UI Textbox
                                if (playerDialogues[j].wasMouseIn()) {
                                    if (!playerDialogues[j].Contains(MouseCoord[0], MouseCoord[1])) {
                                        // Reset the color to match its previous color
                                        playerDialogues[j].setBoxColor(playerDialogues[j].getBoxColor("prev"));
                                        // Mouse has now left the UI Textbox so set it to false
                                        playerDialogues[j].setMouseWasIn(false);
                                    }

                                    // If mouse just came from outside the UI Textbox
                                } else {
                                    if (playerDialogues[j].Contains(MouseCoord[0], MouseCoord[1])) {
                                        // Update previous color to current color of the UI Textbox
                                        playerDialogues[j].setPrevColor(playerDialogues[j].getBoxColor("curr"));
                                        // Update current color to selected tonal button color
                                        playerDialogues[j].setBoxColor(buttons[i].getTonalColor());
                                        // Mouse is now inside a UI Textbox, so set it to true
                                        playerDialogues[j].setMouseWasIn(true);

                                    }
                                }

                            }

                        }
                    }

                }

            } else if (State.GetState() == "pause") {
                State.setPauseTime(State.getNewTime());
                double a = State.getPauseTime();
                double b = DateTime.Now.Ticks / 10000000;
                State.setTimeDiff(b - a);

            }
        }

        protected override void Draw() {
            window.SetView(fullScreenView);

            window.Clear(clearColor);
            if (State.GetState() == "menu") {
                if (State.GetMenuState() == "start") {
                    window.Draw(startMenu);
                } else {
                    window.Draw(settingsMenu);
                }


            } else if (State.GetState() != "menu") {
                window.SetView(charView);
                window.Draw(mom);

                window.SetView(fullScreenView);
                //Draw text box background box
                RectangleShape textBackground = new RectangleShape(new SFML.System.Vector2f(SCREEN_WIDTH, SCREEN_HEIGHT / 5));
                textBackground.Position = new SFML.System.Vector2f(0, SCREEN_HEIGHT - (SCREEN_HEIGHT / 5));
                textBackground.FillColor = Color.Black;
                window.Draw(textBackground);


                var dialogues = ui_man.getPlayerDialogues();

                for (var i = 0; i < dialogues.Count; i++) {
                    window.Draw(dialogues[i]);
                }
                var buttons = ui_man.getButtons();

                for (var i = 0; i < buttons.Count; i++) {
                    window.Draw(buttons[i]);
                }

                if (init && dialogueBox.active) {
                    //UNCOMMENT
                    window.Draw(dialogueBox);
                }




                if (State.GetState() == "pause") {
                    pauseMenu.DrawBG(window);
                    if (State.GetMenuState() == "pause") {
                        window.Draw(pauseMenu);
                    } else if (State.GetMenuState() == "settings") {
                        window.Draw(settingsMenu);
                    }

                }
            }


=======
            ui_man.Icantevenwiththiscode3(State, ManagerOfInput);
        }

        protected override void Draw() {
            //////>>>>>clearColor to magenta
            window.Clear(Color.Magenta);
            ui_man.Icantevenwiththiscode(window);
            ui_man.Icantevenwiththiscode2(window, State, ui_man, startMenu, pauseMenu, settingsMenu);

>>>>>>> master
        }
    }
}
