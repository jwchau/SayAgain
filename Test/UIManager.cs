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
        protected View fullScreenView, scrollview;
        /// </summary>



        string[] dialogueArray;

        //methods
        public DialogueBox getDialogueBox() {
            return dialogueBox;
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
        /// <summary>
        //////////////////////////////////start of i cant even with this code.
        /// </summary>
        public void Icantevenwiththiscode(RenderWindow window) {
                if (init) {
                        //window.Draw(dialogueBox);
                        //window.Draw(dialogue);
                        //window.Draw(name);
                }

                if (init && dialogueBox.active) {
                    //UNCOMMENT
                    window.SetView(scrollview);
                    window.Draw(dialogueBox);
                    window.Draw(dialogueBox.dialogue);
                    window.Draw(dialogueBox.name);

            }
        }

        /// <summary>
        /// ///////////////////////////////////////////////I cant even with this code part 2 yay!!!
        /// </summary>
        public void Icantevenwiththiscode2(RenderWindow window, GameState State, UIManager ui, StartMenu sta, StartMenu pau, StartMenu set) {
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



        public FloatRect dialogueBoxBounds() {
            return dialogueBox.GetBounds();
        }

        public void setDialogueBox() {
            dialogueBox = new DialogueBox(0, 0, 710, 150);
        }

        public void setViews(View fs, View vp) {
            fullScreenView = fs;
            scrollview = vp;
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

        public void StartDialogueBox() {
            init = true;
            this.dialogueBox.renderDialogue("I took my love, I took it down " +
                "Climbed a mountain and I turned around " +
                "And I saw my reflection in the snow covered hills " +
                "'Til the landslide brought it down " +
                "Oh, mirror in the sky " +
                "What is love? " +
                "Can the child within my heart rise above? " +
                "Can I sail through the changin' ocean tides? " +
                "Can I handle the seasons of my life? " +
                "Well, I've been afraid of changin' " +
                "'Cause I've built my life around you " +
                "But time makes you bolder " +
                "Even children get older " +
                "And I'm getting older, too", "Alex");
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
