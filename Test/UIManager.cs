using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Audio;
using SFML.Window;
using SFML.System;
using System.Text.RegularExpressions;

//holds UI elements such as buttons, input fields, TextBoxes, etc
namespace Test {
    class UIManager {
        //constructor
        public UIManager() {

            /* TEMPORARY CODE REMOVE AND CLEAN LATER*/
            tone[] tonez = new tone[] { tone.Blunt, tone.Indifferent, tone.Compassionate, tone.Hesitant };
            string[] jsondialogue = new string[] { "Blunt Dialogue.", "Indifferent Dialogue.", "Compassionate Dialogue.", "Hesitant Dialogue." };
            int xPos = (int)SCREEN_WIDTH / tonez.Length;
<<<<<<< HEAD
            Console.WriteLine(SCREEN_HEIGHT);
=======
            //Console.WriteLine(SCREEN_HEIGHT);
>>>>>>> 24292412928b907bdb0e2cd81f7a16bf1fc4e303
            for (int i = 1; i <= tonez.Length; i++) {
                addButton(new UIButton(xPos / 2 + (i - 1) * xPos, (float)(SCREEN_HEIGHT - SCREEN_HEIGHT*0.26), tonez[i - 1], jsondialogue[i - 1]));
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


        #region SweepButtons
        public void SweepButtons(int x, int y, double scalex, double scaley) {
            var buttons = getButtons();
            for (var i = 0; i < buttons.Count; i++) {
                buttons[i].setHover((int)(x * scalex), (int)(y * scaley));
            }
        }
        #endregion

<<<<<<< HEAD
        public List<UITextBox> produceTextBoxes2(string Dialogue) {
            //Console.WriteLine("AM I HERE????");
            dialogueArray = Regex.Split(Dialogue, @"(?=\.)|(?<=\!)|(?=\?)");
            //dialogue Array now holds all the sentences
            foreach (var dialogue in dialogueArray) {
                //Console.WriteLine(dialogue);
            }

            List<string> words = new List<string>();

            //length takes care of the differences between 1 sentence or multiple sentences
            int length = 0;
            if (dialogueArray.Length > 1) {
                length = dialogueArray.Length - 1;
            } else {
                length = dialogueArray.Length;
            }

            for (int i = 0; i < length; i++) {
                dialogueArray[i] += ".";
                string[] temp = dialogueArray[i].Split(' '); //my name is Raman. //1 cluster
                                                             // Console.WriteLine(temp);
                for (int j = 0; j < temp.Length; j++) {

                    string word = temp[j].Trim();
                    if (word != "") {
                        words.Add(word);

                    }
                }
            }
            int cluster = 0;
            string baseString = ""; //last known string of characters that does fit
            bool newLine = false;
            uint x = 5;
            uint y = SCREEN_HEIGHT - ((SCREEN_HEIGHT / 5)) + 5;
            Font tempFont = new Font("../../Fonts/Adore64.ttf");
            for (int word = 0; word < words.Count; word++) {
                string tempString = baseString;

                //Console.WriteLine(words[word]);


                if (word != 0 && words[word - 1].Contains('.') != true && !newLine) {
                    tempString += " ";
                }

                tempString += words[word];

                Text tempText = new Text(tempString, tempFont);

                if (x + tempText.GetGlobalBounds().Width > SCREEN_WIDTH - 5) {
                    //did not fit, make a text box out of the last fit string
                    //reset x and y

                    playerDialogues.Add(new UITextBox(x, y, baseString, cluster));


                    word--;
                    y += (uint)tempText.GetGlobalBounds().Height + 10;
                    x = 5;
                    tempString = "";
                    baseString = "";
                    newLine = true;

                } else if (words[word].Contains('.') || words[word].Contains('!') || words[word].Contains('?')) {
                    //word with a period meaning the end of a sentence.
                    // playerDialogues.Add(new UITextBox(x, y, tempString))
                    baseString = tempString;
                    playerDialogues.Add(new UITextBox(x, y, baseString, cluster));



                    x += (uint)tempText.GetGlobalBounds().Width + 10;
                    baseString = "";
                    tempString = "";
                    newLine = false;
                    cluster++;
                } else if (x + tempText.GetGlobalBounds().Width < SCREEN_WIDTH - 5) {
                    //update baseString
                    baseString = tempString;
                    newLine = false;
                }
            }
            return playerDialogues;

=======
        public List<UITextBox> produceTextBoxes(string Dialogue)
        {
            dialogueArray = Dialogue.Split(' ');
            List<string> words = new List<string>();
            foreach (var dialogue in dialogueArray)
            {
                //Console.WriteLine(dialogue);
            }

            string tempString = dialogueArray[0];
            Font tempFont = new Font("../../Art/UI_Art/fonts/ticketing/TICKETING/ticketing.ttf");
            uint x = 5;
            uint y = SCREEN_HEIGHT - ((SCREEN_HEIGHT / 5)) + 5;
            int cluster = 0;

            int length = 0;
            if (dialogueArray.Length > 1)
            {
                length = dialogueArray.Length - 1;
            }
            else
            {
                length = dialogueArray.Length;
            }

            //Console.WriteLine("THE LENGTH OF THE DIALOGUE IS: " + dialogueArray.Length);

            if (dialogueArray.Length != 1)
            {

                for (int word = 1; word < dialogueArray.Length; word++)
                {
                    Text tempText = new Text(tempString + ' ' + dialogueArray[word], tempFont, getFontSize()); //current word + the next one after
                    if (tempText.GetGlobalBounds().Width + 10 >= SCREEN_WIDTH)
                    {
                        //time to go the next line
                        //Console.WriteLine("NEXT LINE TIME!!!");
                        playerDialogues.Add(new UITextBox(x, y, tempString, cluster));
                        y += (uint)tempText.GetGlobalBounds().Height + 10;
                        tempString = dialogueArray[word];
                        if (word == dialogueArray.Length - 1)
                        {
                            playerDialogues.Add(new UITextBox(x, y, tempString, cluster));
                            y += (uint)tempText.GetGlobalBounds().Height + 10;
                        }

                    }
                    else
                    {
                        //Console.WriteLine("tempString before adding the next word: " + tempString);
                        tempString += ' ';
                        tempString += dialogueArray[word];
                        //Console.WriteLine("tempString after adding the next word: " + tempString);
                        if (word == dialogueArray.Length - 1)
                        {
                            playerDialogues.Add(new UITextBox(x, y, tempString, cluster));
                            y += (uint)tempText.GetGlobalBounds().Height + 10;
                        }
                    }

                }
            }
            else if (dialogueArray.Length == 1)
            {
                playerDialogues.Add(new UITextBox(x, y, tempString, cluster));
            }

            return playerDialogues;

        }

        private uint getFontSize()
        {

            return (uint)((SCREEN_WIDTH / 1920) * 80);
>>>>>>> 24292412928b907bdb0e2cd81f7a16bf1fc4e303
        }

        public void reset(List<DialogueObj> responseList) {
            bool gotTone = false;
            tone Tone = tone.Root;

            foreach (var dialogue in playerDialogues) {
                if (dialogue.getAffected() && !gotTone) {
                    //Console.WriteLine(dialogue.getTone());

                    Tone = dialogue.getTone();
                    gotTone = true;
                }

                dialogue.setAffected(false);
                dialogue.setBoxColor(Color.Red);
                dialogue.setPrevColor(Color.Red);
                dialogue.setTone(tone.Root);
            }
            playerDialogues.Clear();

<<<<<<< HEAD
            produceTextBoxes2(responseList.ElementAt(0).content);
=======
            produceTextBoxes(responseList.ElementAt(0).content);
>>>>>>> 24292412928b907bdb0e2cd81f7a16bf1fc4e303
        }

        public tone getTone() {
            return playerDialogues[0].getTone();
        }

        public void updateClusterColors(UITextBox self, List<UITextBox> playerDialogues, Color c, bool f) {

            int cluster = self.getCluster();
            for (int i = 0; i < playerDialogues.Count; i++) {
                
                if (playerDialogues[i].getCluster() == cluster && playerDialogues[i] != self) {
                    if (!f) {
                        playerDialogues[i].setBoxColor(playerDialogues[i].getBoxColor("prev"));
                       
                        //playerDialogues[i].setMouseWasIn(false);
                    } else {
                        playerDialogues[i].setPrevColor(playerDialogues[i].getBoxColor("curr"));
                        playerDialogues[i].setBoxColor(c);

                        //playerDialogues[i].setMouseWasIn(true);
                    }
                }

            }
        }

        #region SA_applyTones
        public void applyTones(int x, int y) {
            // Applying tones to Text Boxes
            for (var i = 0; i < buttons.Count; i++) {
                if (buttons[i].GetSelected()) {
                    //CHECK MATRIX BS
                    // Move to character state
                    //double[,] final = tfx.MatrixMult(tfx, cf);
                    //Console.WriteLine(final[2, 3]);
<<<<<<< HEAD
                    Console.WriteLine("HEY THE BUTTON I AM DRAGGING IS: " + buttons[i].getTone().ToString());
=======
                    //Console.WriteLine("HEY THE BUTTON I AM DRAGGING IS: " + buttons[i].getTone().ToString());
>>>>>>> 24292412928b907bdb0e2cd81f7a16bf1fc4e303
                    // Get UI Text Boxes
                    var playerDialogues = this.getPlayerDialogues();

                    for (var j = 0; j < playerDialogues.Count; j++) {
                        var boxBounds = playerDialogues[j].getBoxBounds();
                        //change color if the button is hovering over the textbox

<<<<<<< HEAD
                        if (playerDialogues[j].Contains(x, y)) {
=======
                        if (playerDialogues[j].Contains(buttons[i])) {
>>>>>>> 24292412928b907bdb0e2cd81f7a16bf1fc4e303

                            for (int k = 0; k < playerDialogues.Count; k++) {
                                playerDialogues[k].setPrevColor(playerDialogues[k].getBoxColor("curr"));
                                playerDialogues[k].setBoxColor(buttons[i].getTonalColor());
                                playerDialogues[k].setAffected(true);
                                playerDialogues[k].setTone(buttons[i].getTone());
<<<<<<< HEAD
                                Console.WriteLine("MY TONE IS: " + playerDialogues[0].getTone());
=======
                                //Console.WriteLine("MY TONE IS: " + playerDialogues[0].getTone());
>>>>>>> 24292412928b907bdb0e2cd81f7a16bf1fc4e303

                            }
                            break;
                        }
                    }
                    buttons[i].snapBack();
                    buttons[i].SetSelected(false);
                    break;
                }
            }
        }
        #endregion

        public void dialogueLoadOrder(GameState state, DialogueBox player, DialogueBox AI, List<DialogueObj> responseList, List<DialogueObj> responseListAlex, bool playerChoice)
        {


            if (!playerChoice && responseList[0].content != "returned empty string")
            {

                player.setInit(true);
                player.loadNewDialogue("player", responseList.ElementAt(0).content);
            }

            //check timer done
            //   run player animation
            //check player animation done
            //   run ai animation
            //check ai animation done
            //   update currents
            //   reset UITextBox with root dialogue

            //if (state.getGameTimer("game").getCountDown() == 0)
            //{
            //    AI.setInit(false);
            //    player.setInit(true);
            //    player.loadNewDialogue("player", responseList.ElementAt(0).content);
            //}
            //if (player.getAnimationStart() == false)
            //{
            //    AI.setInit(true);
            //    AI.loadNewDialogue("alex", responseListAlex.ElementAt(0).content);
            //    player.setInit(false);
            //}

        }

    }
}
