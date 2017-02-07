using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Audio;
using SFML.Window;
using SFML.System;

//holds UI elements such as buttons, input fields, TextBoxes, etc
namespace Test
{
    class UIManager {
        //constructor
        public UIManager() {

            /* TEMPORARY CODE REMOVE AND CLEAN LATER*/
            string[] tone = new string[] { "Blunt", "Indifferent", "Compassionate", "Hesitant" };
            string[] jsondialogue = new string[] { "Blunt Dialogue.", "Indifferent Dialogue.", "Compassionate Dialogue.", "Hesitant Dialogue." };
            int xPos = (int)SCREEN_WIDTH / tone.Length;
            for (int i = 1; i <= tone.Length; i++) {
                addButton(new UIButton(xPos / 2 + (i - 1) * xPos, SCREEN_HEIGHT - SCREEN_HEIGHT / 4, tone[i - 1], jsondialogue[i - 1]));
            }
            ////////////////////////////////////////////////
        }


        List<UIButton> buttons = new List<UIButton>(); //our tone buttons
        List<UITextBox> playerDialogues = new List<UITextBox>();
        static UInt32 SCREEN_WIDTH = VideoMode.DesktopMode.Width;
        static UInt32 SCREEN_HEIGHT = VideoMode.DesktopMode.Height;

        /// temp jill holding place
        protected DialogueBox dialogueBox;
        protected Boolean init;
        protected View fullScreenView;
        /// </summary>



        string[] dialogueArray;

        //methods
        public DialogueBox getDialogueBox() {
            return this.dialogueBox;
        }


        public Boolean getInit() { 
            return init;
        }

        public List<UIButton> getButtons() {
            return buttons;
        }

        public List<UITextBox> getPlayerDialogues() {
            return playerDialogues;
        }

        public void addButton(UIButton b) {
            buttons.Add(b);
        }

        public void DrawDialogueBox(RenderWindow window) {
            if (init) {
                    window.Draw(dialogueBox);

            }

        }

        public void DrawUI(RenderWindow window, GameState State, UIManager ui, StartMenu sta, StartMenu pau, StartMenu set) {
            //////>>>>>clearColor to magenta
            window.SetView(fullScreenView);
            if (State.GetState() == "menu") {
                if (State.GetMenuState() == "start") {
                    window.Draw(sta);
                } else {
                    window.Draw(set);
                    //Console.WriteLine("in menu, in settings, draw settings");
                }


            } else {
                //Draw text box background box
                RectangleShape textBackground = new RectangleShape(new SFML.System.Vector2f(SCREEN_WIDTH, SCREEN_HEIGHT / 5));
                textBackground.Position = new SFML.System.Vector2f(0, SCREEN_HEIGHT - (SCREEN_HEIGHT / 5));
                textBackground.FillColor = Color.Black;
                window.Draw(textBackground);

                var dialogues = ui.getPlayerDialogues();

                for (var i = 0; i < dialogues.Count; i++) {
                    window.Draw(dialogues[i]);
                }
                var buttons = ui.getButtons();

                for (var i = 0; i < buttons.Count; i++) {
                    window.Draw(buttons[i]);
                }

                if (State.GetState() == "pause") {
                    pau.DrawBG(window);
                    if (State.GetMenuState() == "pause") {
                        window.Draw(pau);
                    } else if (State.GetMenuState() == "settings") {
                        window.Draw(set);
                    }

                }
            }
        }

        public void UpdateTimer(GameState State, InputManager ManagerOfInput) {
            if (State.GetState() == "game") {

                // Timer update
                if (State.getCountDown() > 0) {
                    //as long as you are not out of time
                    State.setNewTime((DateTime.Now.Ticks / 10000000) - State.getTimeDiff());
                    State.setCountDown(9 - (State.getNewTime() - State.getGameTime()));

                }

                // Get the current UI Textboxes from the UI Manager
                var playerDialogues = this.getPlayerDialogues();

                // Get the mouse coordinates from Input Manager
                var MouseCoord = ManagerOfInput.GetMousePos();

                // If the mouse is currently dragging
                if (ManagerOfInput.GetMouseDown()) {
                    // Get tonal buttons from UI Manager
                    var buttons = this.getButtons();

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


        public FloatRect dialogueBoxBounds() {
            return dialogueBox.GetBounds();
        }

        public void setDialogueBox() {
            dialogueBox = new DialogueBox(0, 0, 710, 150);
        }

        public void setViews(View fs) {
            fullScreenView = fs;
        }

        public void setDialogueBoxPos(FloatRect f)
        {
            this.dialogueBox.view.Viewport = f;
        }
        public void SetPrintTime(int t) {
            this.dialogueBox.setPrintTime(t);
        }



        public void DialogueNextEndCheck() {
            this.dialogueBox.getNext();
            this.dialogueBox.checkEnd();
            if (this.dialogueBox.getElementIndex() == this.dialogueBox.getArrLength()) {
                this.dialogueBox.active = false;
            }
        }

        public void RenderDialogue(string s, string sp) {
            init = true;
            this.dialogueBox.renderDialogue(s, sp);
        }

        public void produceTextBoxes(string dialogue) {

            
            //Pushes onto the playerDialogues.

            //break up the dialogue by periods.
            dialogueArray = dialogue.Split('.');

            uint x = 5;
            uint y = SCREEN_HEIGHT - ((SCREEN_HEIGHT/5));
            Font tempFont = new Font("../../Fonts/Adore64.ttf");

            for (int i = 0; i < dialogueArray.Count() - 1; i++) {
                
                if (dialogueArray[i][0] == ' ')
                {
                    dialogueArray[i] = dialogueArray[i].Substring(1);
                }
                dialogueArray[i] += ".";

                Text tempText = new Text(dialogueArray[i], tempFont);
                if (x + tempText.GetGlobalBounds().Width < SCREEN_WIDTH - 5)
                {

                    playerDialogues.Add(new UITextBox(x, y, dialogueArray[i]));
                    x += (uint)tempText.GetGlobalBounds().Width + 10;
                }
                else
                {

                    y += (uint)tempText.GetGlobalBounds().Height + 10;

                    x = 5;
                    playerDialogues.Add(new UITextBox(x, y, dialogueArray[i]));
                    x += (uint)tempText.GetGlobalBounds().Width + 10;
                }

                //Make textboxes using the dialogues.

            }

        }

        public void updateText(int pos, string newDialogue) {

            List<bool> positionOfAffected = new List<bool>(); //remembers who were affected
            List<Color> dialogueColors = new List<Color>();

            for (int i = 0; i < playerDialogues.Count; i++)
            {
                positionOfAffected.Add(playerDialogues[i].getAffected());
                dialogueColors.Add(playerDialogues[i].getBoxColor("curr"));

            }

            playerDialogues.Clear();

            dialogueArray[pos] = newDialogue;


            uint x = 5;
            uint y = SCREEN_HEIGHT - ((SCREEN_HEIGHT / 5));
            Font tempFont = new Font("../../Fonts/Adore64.ttf");


            for (int i = 0; i < dialogueArray.Count() - 1; i++)
            {

                Text tempText = new Text(dialogueArray[i], tempFont);
                if (x + tempText.GetGlobalBounds().Width < SCREEN_WIDTH - 5)
                {

                    playerDialogues.Add(new UITextBox(x, y, dialogueArray[i]));
                    x += (uint)tempText.GetGlobalBounds().Width + 10;
                }
                else
                {

                    y += (uint)tempText.GetGlobalBounds().Height + 10;

                    x = 5;
                    playerDialogues.Add(new UITextBox(x, y, dialogueArray[i]));
                    x += (uint)tempText.GetGlobalBounds().Width + 10;
                }

                playerDialogues[i].setAffected(positionOfAffected[i]);
                if (playerDialogues[i].getAffected())
                {
                    playerDialogues[i].setBoxColor(dialogueColors[i]);
                }
            }

            dialogueColors.Clear();
        }

    }
}
