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
        public UIManager()
        {

            /* TEMPORARY CODE REMOVE AND CLEAN LATER*/
            tone[] tonez = new tone[] { tone.Blunt, tone.Indifferent, tone.Compassionate, tone.Hesitant };
            string[] jsondialogue = new string[] { "Blunt Dialogue.", "Indifferent Dialogue.", "Compassionate Dialogue.", "Hesitant Dialogue." };
            int xPos = (int)SCREEN_WIDTH / tonez.Length;
            for (int i = 1; i <= tonez.Length; i++)
            {
                addButton(new UIButton(xPos / 2 + (i - 1) * xPos, SCREEN_HEIGHT - SCREEN_HEIGHT / 4, tonez[i - 1], jsondialogue[i - 1]));
            }
            ////////////////////////////////////////////////
        }


        List<UIButton> buttons = new List<UIButton>(); //our tone buttons
        List<UITextBox> playerDialogues = new List<UITextBox>();
        static UInt32 SCREEN_WIDTH = VideoMode.DesktopMode.Width;
        static UInt32 SCREEN_HEIGHT = VideoMode.DesktopMode.Height;

        string[] dialogueArray;

        //methods
        public List<UIButton> getButtons()
        {
            return buttons;
        }

        public List<UITextBox> getPlayerDialogues()
        {
            return playerDialogues;
        }

        public void addButton(UIButton b)
        {
            buttons.Add(b);
        }

        public List<UITextBox> produceTextBoxes2(string Dialogue)
        {
            Console.WriteLine("AM I HERE????");
            dialogueArray = Dialogue.Split('.', '!', '?');
            //dialogue Array now holds all the sentences
            foreach (var dialogue in dialogueArray) {
                Console.WriteLine(dialogue);
            }

            List<string> words = new List<string>();

            //length takes care of the differences between 1 sentence or multiple sentences
            int length = 0;
            if (dialogueArray.Length > 1)
            {
                length = dialogueArray.Length - 1;
            }
            else {
                length = dialogueArray.Length;
            }

            for (int i = 0; i < length; i++)
            {
                dialogueArray[i] += ".";
                string[] temp = dialogueArray[i].Split(' '); //my name is Raman. //1 cluster
                Console.WriteLine(temp);
                for (int j = 0; j < temp.Length; j++)
                {

                    string word = temp[j].Trim();
                    if (word != "")
                    {
                        words.Add(word);
                     
                    }
                }
            }
            int cluster = 0;
            string baseString = ""; //last known string of characters that does fit
            bool newLine = false;
            uint x = 5;
            uint y = SCREEN_HEIGHT - ((SCREEN_HEIGHT / 5)) + 5;
            for (int word = 0; word < words.Count; word++)
            {
                string tempString = baseString;

                Font tempFont = new Font("../../Fonts/Adore64.ttf");
                //Console.WriteLine(words[word]);


                if (word != 0 && words[word - 1].Contains('.') != true && !newLine)
                {
                    tempString += " ";
                }

                tempString += words[word];

                Text tempText = new Text(tempString, tempFont);

                if (x + tempText.GetGlobalBounds().Width > SCREEN_WIDTH - 5)
                {
                    //did not fit, make a text box out of the last fit string
                    //reset x and y

                    playerDialogues.Add(new UITextBox(x, y, baseString, cluster));


                    word--;
                    y += (uint)tempText.GetGlobalBounds().Height + 10;
                    x = 5;
                    tempString = "";
                    baseString = "";
                    newLine = true;

                }
                else if (words[word].Contains('.') || words[word].Contains('!') || words[word].Contains('?'))
                {
                    //word with a period meaning the end of a sentence.
                    // playerDialogues.Add(new UITextBox(x, y, tempString))
                    baseString = tempString;
                    playerDialogues.Add(new UITextBox(x, y, baseString, cluster));



                    x += (uint)tempText.GetGlobalBounds().Width + 10;
                    baseString = "";
                    tempString = "";
                    newLine = false;
                    cluster++;
                }
                else if (x + tempText.GetGlobalBounds().Width < SCREEN_WIDTH - 5)
                {
                    //update baseString
                    baseString = tempString;
                    newLine = false;

                }
            }
            return playerDialogues;
        }

        public void reset(CharacterState Alex, CharacterState Mom, CharacterState Dad, List<DialogueObj>  responseList)
        {

            ////we need to see what tones got dragged
            ////do matrix operations to spit out FNC
            bool gotTone = false;
            tone Tone = tone.Root;

            Console.WriteLine("First content in List -> " + responseList.ElementAt(0).content);
            Console.WriteLine("First context in List -> " + responseList.ElementAt(0).context);
            Console.WriteLine("First milestone in List -> " + responseList.ElementAt(0).milestone[0]);
            //Console.WriteLine("First target 1 in List -> " + responseList.ElementAt(0).target);


            foreach (var dialogue in playerDialogues)
            {
                if (dialogue.getAffected() && !gotTone)
                {
                    Console.WriteLine(dialogue.getTone());
                    Tone = dialogue.getTone();
                    gotTone = true;
                }

                dialogue.setAffected(false);
                dialogue.setBoxColor(Color.Red);
                dialogue.setPrevColor(Color.Red);
                dialogue.setTone(tone.Root);
            }

            if (responseList.ElementAt(0).nextContext != "") {
                playerDialogues = produceTextBoxes2(responseList.ElementAt(0).content);
            }


            int FNC = 0;

           // responseList = s.ChooseDialog(FNC, sampleJSON, currentMadeMemories, currentMilestones, tone.Blunt, ElementAt(0).nextContext);
            //List<DialogueObj> responseList = s.ChooseDialog(0, r, currentMadeMemories);
            //string newDialogue = responseList.ElementAt(0).content;
            //Console.WriteLine(newDialogue);
        }

        public tone getTone()
        {
            return playerDialogues[0].getTone();
        }

        public void updateText(int pos, string newDialogue, Color color)
        {

            //List<bool> positionOfAffected = new List<bool>(); //remembers who were affected
            //List<Color> dialogueColors = new List<Color>();

            //for (int i = 0; i < playerDialogues.Count; i++)
            //{
            //    positionOfAffected.Add(playerDialogues[i].getAffected());
            //    dialogueColors.Add(playerDialogues[i].getBoxColor("curr"));

            //}

            //playerDialogues.Clear();

            //dialogueArray[pos] = newDialogue;


            //uint x = 5;
            //uint y = SCREEN_HEIGHT - ((SCREEN_HEIGHT / 5));
            //Font tempFont = new Font("../../Fonts/Adore64.ttf");


            //for (int i = 0; i < dialogueArray.Count() - 1; i++)
            //{

            //    Text tempText = new Text(dialogueArray[i], tempFont);
            //    if (x + tempText.GetGlobalBounds().Width < SCREEN_WIDTH - 5)
            //    {

            //        playerDialogues.Add(new UITextBox(x, y, dialogueArray[i]));
            //        x += (uint)tempText.GetGlobalBounds().Width + 10;
            //    }
            //    else
            //    {

            //        y += (uint)tempText.GetGlobalBounds().Height + 10;

            //        x = 5;
            //        playerDialogues.Add(new UITextBox(x, y, dialogueArray[i]));
            //        x += (uint)tempText.GetGlobalBounds().Width + 10;
            //    }

            //    playerDialogues[i].setAffected(positionOfAffected[i]);
            //    if (playerDialogues[i].getAffected())
            //    {
            //        playerDialogues[i].setBoxColor(dialogueColors[i]);
            //    }
            //}

            //dialogueColors.Clear();
        }

        public void updateClusterColors(UITextBox self, List<UITextBox> playerDialogues, Color c, bool f)
        {
            int cluster = self.getCluster();
            for (int i = 0; i < playerDialogues.Count; i++)
            {
                if (playerDialogues[i].getCluster() == cluster && playerDialogues[i] != self)
                {
                    if (!f)
                    {
                        playerDialogues[i].setBoxColor(playerDialogues[i].getBoxColor("prev"));
                        //playerDialogues[i].setMouseWasIn(false);
                    }
                    else
                    {
                        playerDialogues[i].setPrevColor(playerDialogues[i].getBoxColor("curr"));
                        playerDialogues[i].setBoxColor(c);
                        //playerDialogues[i].setMouseWasIn(true);
                    }
                }

            }
        }

    }
}
