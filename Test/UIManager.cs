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
            string[] tone = new string[] { "Rude", "Kind", "Calm", "Sarcastic" };
            string[] jsondialogue = new string[] { "Rude Dialogue.", "Kind Dialogue.", "Calm Dialogue.", "Sarcastic Dialogue." };
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
            uint y = SCREEN_HEIGHT - ((SCREEN_HEIGHT/5));
            Font tempFont = new Font("../../Fonts/Adore64.ttf");

            for (int i = 0; i < dialogueArray.Count() -1; i++) {
                
                if (dialogueArray[i][0] == ' ')
                {
                    dialogueArray[i] = dialogueArray[i].Substring(1);
                }
                dialogueArray[i] += ".";

                Text tempText = new Text(dialogueArray[i], tempFont);
                if (x + tempText.GetGlobalBounds().Width < SCREEN_WIDTH - 5)
                {

                    playerDialogues.Add(new UITextBox(x, y, dialogueArray[i]));
                    x += (uint)tempText.GetGlobalBounds().Width + 15;
                }
                else
                {

                    y += (uint)tempText.GetGlobalBounds().Height + 10;

                    x = 5;
                    playerDialogues.Add(new UITextBox(x, y, dialogueArray[i]));
                    x += (uint)tempText.GetGlobalBounds().Width + 15;
                }

                //Make textboxes using the dialogues.

            }

        }

        public void updateText(int pos, string newDialogue) {

            List<bool> positionOfAffected = new List<bool>();//remembers who were affected
            for (int i = 0; i < playerDialogues.Count; i++)
            {
                positionOfAffected.Add(playerDialogues[i].getAffected());
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
                    x += (uint)tempText.GetGlobalBounds().Width + 15;
                }
                else
                {

                    y += (uint)tempText.GetGlobalBounds().Height + 10;

                    x = 5;
                    playerDialogues.Add(new UITextBox(x, y, dialogueArray[i]));
                    x += (uint)tempText.GetGlobalBounds().Width + 15;
                }

                playerDialogues[i].setAffected(positionOfAffected[i]);
                if (playerDialogues[i].getAffected())
                {
                    playerDialogues[i].setBoxColor(Color.Blue);
                }
            }

            foreach (var stuff in playerDialogues)
            {
                Console.WriteLine(stuff.getAffected());
            }
        }

    }
}
