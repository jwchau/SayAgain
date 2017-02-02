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
    class UIManager
    {
        //constructor
        public UIManager() {

            /* TEMPORARY CODE REMOVE AND CLEAN LATER*/
            string[] tone = new string[] { "Blunt", "Indifferent", "Compassionate", "Hesitant" };
            string[] jsondialogue = new string[] { "Blunt Dialogue.", "Indifferent Dialogue.", "Compassionate Dialogue.", "Hesitant Dialogue." };
            int xPos = (int)SCREEN_WIDTH / tone.Length;
            for (int i = 1; i <= tone.Length; i++)
            {
                addButton(new UIButton(xPos / 2 + (i - 1) * xPos, SCREEN_HEIGHT - SCREEN_HEIGHT / 4, tone[i - 1], jsondialogue[i - 1]));
            }
            ////////////////////////////////////////////////
        }


        List<UIButton> buttons = new List<UIButton>(); //our tone buttons
        List<UITextBox> playerDialogues = new List<UITextBox>();
        static UInt32 SCREEN_WIDTH = VideoMode.DesktopMode.Width;
        static UInt32 SCREEN_HEIGHT = VideoMode.DesktopMode.Height;

        string[] dialogueArray;

        //methods
        public List<UIButton> getButtons() {
            return buttons;
        }

        public List<UITextBox> getPlayerDialogues() {
            return playerDialogues;
        }

        public void addButton(UIButton b) {
            buttons.Add(b);
        }

        public void produceTextBoxes(string dialogue) {

            
            //Pushes onto the playerDialogues.

            //break up the dialogue by periods.
            dialogueArray = dialogue.Split('.');

            uint x = 5;
//<<<<<<< HEAD
            uint y = SCREEN_HEIGHT - ((SCREEN_HEIGHT/5));
            Font tempFont = new Font("../../Fonts/Adore64.ttf");
//=======
            //uint y = SCREEN_HEIGHT - ((SCREEN_HEIGHT/5)) + 5;
            //Font tempFont = new Font("Fonts/Adore64.ttf");
//>>>>>>> 7053ad4965a1d2a0a06ba3c32d7a0dc2b320bfdb

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
//<<<<<<< HEAD
            uint y = SCREEN_HEIGHT - ((SCREEN_HEIGHT / 5));
            Font tempFont = new Font("../../Fonts/Adore64.ttf");
//=======
            //uint y = SCREEN_HEIGHT - ((SCREEN_HEIGHT / 5)) + 5;
            //Font tempFont = new Font("Fonts/Adore64.ttf");
//>>>>>>> 7053ad4965a1d2a0a06ba3c32d7a0dc2b320bfdb

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
