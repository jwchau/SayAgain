﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Drawing;
//eventually make textbox into class for whole dialogue box (including name box)

namespace Test {
    class DialogueBox : Drawable {

        static UInt32 SCREEN_WIDTH = VideoMode.DesktopMode.Width;
        static UInt32 SCREEN_HEIGHT = VideoMode.DesktopMode.Height;
        Vector2f scale = new Vector2f(SCREEN_WIDTH / 1920, SCREEN_HEIGHT / 1080);
        private Text name, dialogue;
        Task currentTask;
        uint dialogueFontSize = 40;
        uint nameFontSize = 55;

        static Dictionary<string, Sprite> spriteDict = new Dictionary<string, Sprite>();

        public bool init = false;
        string tag; //AI or player

        public bool animationStart = false;
        bool awaitInput = false;

        CancellationTokenSource cts;
        public List<Text> dialoguePanes = new List<Text>();
        public int printTime = 30;
        public bool active = false;
        public int elementIndex = 0;
        public bool done = false;
        GameState state;

        Sprite dialogueBoxSprite;

        public CircleShape cursor;
        public float OGcursorX;
        public float OGcursorY;
        public float FcursorX;
        public float FcursorY;
        float iterator = 0.5f;
        public Boolean lastDialogue = false;

        Font speechFont = new Font("../../Art/UI_Art/fonts/ticketing/TICKETING/ticketing.ttf");

        public void setInit(bool b) {
            init = b;
        }

        public bool getAwaitInput() {
            return awaitInput;
        }

        public void setPrintTime(int i) {
            printTime = i;
        }
        public int getElementIndex() {
            return elementIndex;
        }

        public bool getAnimationStart() {
            return animationStart;
        }

        public string getPrintShit() {
            return tag + " - animStart: " + animationStart + "\n" +
                   "        awaitInput: " + awaitInput + "\n" +
                   "        dialoguePanesLength: " + dialoguePanes.Count + "\n" +
                   "        init: " + init + "\n" +
                   "        active: " + active;
        }

        public bool checkNext() {
            if (this.printTime != 0 && this.getAnimationStart() && !this.getAwaitInput()) {
                printTime = 0;
                return false;
            } else if (elementIndex < dialoguePanes.Count) {
                //Console.WriteLine("\n---------- CHECK NEXT");
                if (cts != null) {
                    cts.Cancel();
                }
                cts = new CancellationTokenSource();
                currentTask = Task.Run(async () => {
                    printTime = 30;
                    await animateText(cts.Token);
                }, cts.Token);
                //Console.WriteLine("CHECKNEXT RETURN FALSE");
                return false;
            } else {
                if (tag == "AI" || tag == "player") {
                    state.startTimer("game");
                }

                awaitInput = false;
                //Console.WriteLine("CHECKNEXT RETURN TRUE");
                return true;
            }
        }


        public void loadNewDialogue(string speaker, string content) {
            if (speaker == "alex") {
                dialogueBoxSprite = spriteDict["right"];
                dialogueBoxSprite.Position = new Vector2f(SCREEN_WIDTH / 2 - (dialogueBoxSprite.GetGlobalBounds().Width / 2), SCREEN_HEIGHT / 5);

                name = new Text(speaker.ToUpper(), speechFont, nameFontSize);
                name.Position = new Vector2f(dialogueBoxSprite.GetGlobalBounds().Left + ((118 * scale.X) - name.GetGlobalBounds().Width / 2), dialogueBoxSprite.GetGlobalBounds().Top + ((22 * scale.Y) - name.GetGlobalBounds().Height));
                dialogue = new Text(content, speechFont, dialogueFontSize);
                dialogue.Position = new Vector2f(dialogueBoxSprite.GetGlobalBounds().Left + (uint)(SCREEN_WIDTH * 0.004), dialogueBoxSprite.GetGlobalBounds().Top + (uint)(SCREEN_HEIGHT * 0.04));

            } else if (speaker == "dad") {
                dialogueBoxSprite = spriteDict["left"];
                dialogueBoxSprite.Position = new Vector2f((float)(SCREEN_WIDTH * 0.21) - (dialogueBoxSprite.GetGlobalBounds().Width / 2), (float)(SCREEN_HEIGHT * 0.19) - (dialogueBoxSprite.GetGlobalBounds().Height / 2));

                name = new Text(speaker.ToUpper(), speechFont, nameFontSize);
                name.Position = new Vector2f(dialogueBoxSprite.GetGlobalBounds().Left + ((118 * scale.X) - name.GetGlobalBounds().Width / 2), dialogueBoxSprite.GetGlobalBounds().Top + ((22 * scale.Y) - name.GetGlobalBounds().Height));
                dialogue = new Text(content, speechFont, dialogueFontSize);
                dialogue.Position = new Vector2f(dialogueBoxSprite.GetGlobalBounds().Left + (uint)(SCREEN_WIDTH * 0.004), dialogueBoxSprite.GetGlobalBounds().Top + (uint)(SCREEN_HEIGHT * 0.04));

            } else if (speaker == "mom") {
                dialogueBoxSprite = spriteDict["right"];
                dialogueBoxSprite.Position = new Vector2f(3 * SCREEN_WIDTH / 4 - (dialogueBoxSprite.GetGlobalBounds().Width / 2), SCREEN_HEIGHT / 5);

                name = new Text(speaker.ToUpper(), speechFont, nameFontSize);
                name.Position = new Vector2f(dialogueBoxSprite.GetGlobalBounds().Left + ((118 * scale.X) - name.GetGlobalBounds().Width / 2), dialogueBoxSprite.GetGlobalBounds().Top + ((22 * scale.Y) - name.GetGlobalBounds().Height));
                dialogue = new Text(content, speechFont, dialogueFontSize);
                dialogue.Position = new Vector2f(dialogueBoxSprite.GetGlobalBounds().Left + (uint)(SCREEN_WIDTH * 0.004), dialogueBoxSprite.GetGlobalBounds().Top + (uint)(SCREEN_HEIGHT * 0.04));

            } else if (speaker == "player") {
                dialogueBoxSprite = spriteDict["player"];
                dialogueBoxSprite.Position = new Vector2f(0, (float)(SCREEN_HEIGHT * 0.74));

                name = new Text("YOU", speechFont, nameFontSize);
                name.Position = new Vector2f(dialogueBoxSprite.GetGlobalBounds().Left + ((354 * scale.X) - name.GetGlobalBounds().Width / 2), dialogueBoxSprite.GetGlobalBounds().Top + ((32 * scale.Y) - name.GetGlobalBounds().Height));
                dialogue = new Text(content, speechFont, dialogueFontSize);
                dialogue.Position = new Vector2f(dialogueBoxSprite.GetGlobalBounds().Left + (uint)(SCREEN_WIDTH * 0.004), dialogueBoxSprite.GetGlobalBounds().Top + (uint)(SCREEN_HEIGHT * 0.062));

            }

            // Cursor initializations
            if (tag == "AI") {
                OGcursorX = dialogueBoxSprite.GetGlobalBounds().Left + dialogueBoxSprite.GetGlobalBounds().Width - (float)(dialogueBoxSprite.GetGlobalBounds().Width * 0.05);
                OGcursorY = dialogueBoxSprite.GetGlobalBounds().Top + dialogueBoxSprite.GetGlobalBounds().Height - (float)(dialogueBoxSprite.GetGlobalBounds().Height * .3);
            } else {
                OGcursorX = (float)(SCREEN_WIDTH * 0.95);
                OGcursorY = (float)(SCREEN_HEIGHT * 0.95);
            }
            cursor.Position = new Vector2f(OGcursorX, OGcursorY);

            cursor.Origin = new Vector2f(cursor.GetGlobalBounds().Width / 2, cursor.GetGlobalBounds().Height / 2);
            FcursorX = OGcursorX + 3;
            FcursorY = OGcursorY + 3;
            // ----------------------

            name.Color = Color.White;
            dialogue.Color = Color.White;

            awaitInput = false;
            active = true;
            elementIndex = 0;

            renderDialogue(content);

        }

        public void renderDialogue(String s) {
            dialoguePanes.Clear();
            //Console.WriteLine("\n---------- RENDER DIALOGUE");
            if (cts != null) {
                cts.Cancel();
            }
            cts = new CancellationTokenSource();

            dialoguePanes = createStrings();
            currentTask = Task.Run(async () => { //Task.Run puts on separate thread
                printTime = 30;
                await animateText(cts.Token); //await pauses thread until animateText() is completed

            }, cts.Token);
            //Console.WriteLine("\n---------- END OF RENDER DIALOGUE");
        }

        public List<Text> createStrings() {

            // Fields for max width/height of dialogue box, and current width/height of the chopped dialogue
            float maxw;
            float maxh;
            float currw = 0;
            float currh = 0;

            // The chopped dialogue that fits in one pane
            string line = "";

            // Boolean to handle when to end 
            bool end = false;

            // Style and colors for the text object
            Text.Styles style = Text.Styles.Regular;
            Color color = Color.White;
            Color thoughtColor = new Color(172, 172, 172);

            // List of chopped dialogues 
            List<Text> panes = new List<Text>();

            // Split the dialogue string by spaces into an array
            string[] words = dialogue.DisplayedString.Split(' ');

            // Set max width and height based off of the current dialogue box sprite
            if (tag == "AI") {
                maxw = cursor.GetGlobalBounds().Left - dialogueBoxSprite.GetGlobalBounds().Left;
                maxh = (float)(dialogueBoxSprite.GetGlobalBounds().Height * 0.4);
            } else {
                maxw = cursor.GetGlobalBounds().Left - dialogueBoxSprite.GetGlobalBounds().Left;
                maxh = (float)(dialogueBoxSprite.GetGlobalBounds().Height * 0.7);
            }

            // Loop through all of the words
            for (int i = 0; i < words.Count(); i++) {

                // Save current word
                string currWord = words[i];

                // Check for italic start condition
                if (currWord.Contains("<")) {
                    style = Text.Styles.Italic;
                    color = thoughtColor;
                    currWord = currWord.Replace("<", "");
                }

                // Check for italic end condition
                if (currWord.Contains(">")) {
                    currWord = currWord.Replace(">", "");
                    end = true;
                }

                // Check if next word has an italic start condition then end current pane
                if (words[((i + 1 < words.Count()) ? i + 1 : i)].Contains("<")) {
                    end = true;
                    style = Text.Styles.Regular;
                }

                // Temp text object for sizing
                Text temp = new Text(currWord + " ", speechFont, dialogueFontSize);

                // Reset current height
                if (currh == 0) currh += (float)(temp.GetGlobalBounds().Height * 1.5);

                // If temp string's width plus the current width is less than the width of the dialogue box sprite
                if (temp.GetGlobalBounds().Width + currw <= maxw) {
                    line += currWord + " ";
                    currw += temp.GetGlobalBounds().Width;

                    // Else If temp string's height plus the current height is less than the height of the dialogue box sprite
                } else if ((temp.GetGlobalBounds().Height * 1.5) + currh <= maxh) {
                    line += "\n" + currWord + " ";
                    currh += (float)(temp.GetGlobalBounds().Height * 1.5);
                    currw = temp.GetGlobalBounds().Width;

                    // Else end that shit
                } else {
                    end = true;
                }

                // If we're at the end of the array and end hasn't been met yet, end it.
                if (!end && i == words.Count() - 1) end = true;

                // If we have reached an end, package the current string and put it in a text object with the current style and color
                // Reset current width, height, line, and end for the next iteration
                if (end) {
                    Text pane = new Text(line, speechFont, dialogueFontSize);
                    pane.Style = style;
                    pane.Color = color;
                    panes.Add(pane);
                    end = false;
                    line = "";
                    currw = 0;
                    currh = 0;
                }
                // Reset the style if we have found an italic termination symbol
                if (words[i].Contains(">") && style == Text.Styles.Italic) {
                    style = Text.Styles.Regular;
                    color = Color.White;
                }

            }

            return panes;

        }

        public void AlertSoundMan() {
            //send signal to sound man
        }

        //async means this function can run separate from main app.
        //operate in own time and thread
        public async Task animateText(CancellationToken ct) {
            Text line = dialoguePanes[elementIndex];

            animationStart = true;
            awaitInput = false;

            state.resetTimer("game");
            dialogue.DisplayedString = "";
            dialogue.Style = line.Style;
            dialogue.Color = line.Color;

            int i = 0;
            while (i < line.DisplayedString.Length) {
                if (ct.IsCancellationRequested) {
                    ct.ThrowIfCancellationRequested();
                }
                if (state.GetState() != "pause") {
                    if (printTime != 0) {
                        if (i == line.DisplayedString.Length - 2) {
                            printTime = 0;
                        } else if (".!?".Contains(line.DisplayedString[i])) {
                            if (!(".!?".Contains(line.DisplayedString[i - 1]))) {
                                printTime *= 14;
                            }
                        } else if (",".Contains(line.DisplayedString[i])) {
                            printTime *= 10;
                        } else {
                            printTime = 30;
                        }
                    }
                    dialogue.DisplayedString = (string.Concat(dialogue.DisplayedString, line.DisplayedString[i++]));

                    await Task.Delay(printTime); //equivalent of putting thread to sleep
                }
            }
            // Do asynchronous work.
            if (elementIndex < dialoguePanes.Count - 1) {
                cursor.Rotation = 180;
            } else {
                cursor.Rotation = 90;
            }

            animationStart = false; //done animating
            awaitInput = true;
            elementIndex++;

        }

        public void Draw(RenderTarget target, RenderStates states) {
            if (init) {
                target.Draw(dialogueBoxSprite);
                target.Draw(name);
                target.Draw(dialogue);

                if (awaitInput) {
                    if (cursor.Rotation == 90) {
                        if (cursor.Position.X > FcursorX) {
                            iterator *= -1;
                        }


                        if (cursor.Position.X < OGcursorX) {
                            iterator *= -1;
                        }
                        cursor.Position = new Vector2f(cursor.Position.X + iterator, cursor.Position.Y);
                    } else {
                        if (cursor.Position.Y > FcursorY) {
                            iterator *= -1;
                        }


                        if (cursor.Position.Y < OGcursorY) {
                            iterator *= -1;
                        }
                        cursor.Position = new Vector2f(cursor.Position.X, cursor.Position.Y + iterator);
                    }


                    target.Draw(cursor);
                }
            }
        }

        private uint getFontSize() {
            return (uint)((SCREEN_WIDTH / 1920) * 27);
        }


        public DialogueBox(GameState state, string tag) {
            this.state = state;
            this.tag = tag;
            if (tag == "AI") {
                cursor = new CircleShape((SCREEN_WIDTH / 1920) * 10, 3);
                dialogueFontSize = getFontSize();
                nameFontSize = getFontSize() + 20;
            } else {
                cursor = new CircleShape((SCREEN_WIDTH / 1920) * 20, 3);
                dialogueFontSize = getFontSize() + 20;
                nameFontSize = getFontSize() + 30;
            }

            cursor.Rotation = 180;

            if (!spriteDict.ContainsKey("left")) {
                spriteDict.Add("left", new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/speechbubbleleft.png")));
                spriteDict["left"].Scale = scale;
            }
            if (!spriteDict.ContainsKey("right")) {
                spriteDict.Add("right", new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/speechbubbleright.png")));
                spriteDict["right"].Scale = scale;
            }
            if (!spriteDict.ContainsKey("player")) {
                spriteDict.Add("player", new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/psb.png")));
                spriteDict["player"].Scale = scale;
            }



        }
    }
}