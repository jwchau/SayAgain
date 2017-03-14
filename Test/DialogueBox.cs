using System;
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

namespace Test
{
    class DialogueBox : Drawable
    {

        static UInt32 SCREEN_WIDTH = VideoMode.DesktopMode.Width;
        static UInt32 SCREEN_HEIGHT = VideoMode.DesktopMode.Height;
        public float w, h, x, y;//, x2, y2, OffsetX, OffsetY, OffsetX2, OffsetY2;
        private Text name, dialogue;
        //public Dialogue line;
        public RectangleShape box;
        public RectangleShape nameBox;
        Task currentTask;
        
        Boolean init = false;
        string tag; //AI or player

        bool animationStart = true;
        bool awaitInput = false;

        CancellationTokenSource cts;
        Text[] arr = { };
        public int printTime;
        public bool active = false;
        int elementIndex = 0;
        GameState state;

        Sprite dialogueBoxSprite;

        public View view { get; private set; }

        // public View playerView { get; private set;}

        Font speechFont = new Font("../../Art/UI_Art/fonts/ticketing/TICKETING/ticketing.ttf");

        public void setInit(bool b) {
            init = b;
        }

        public void forward()
        {
            if (currentTask == null || currentTask.IsCompleted)
            {
                getNext();
                checkEnd();
            }
        }
        public void setPrintTime(int i)
        {
            printTime = i;
        }
        public int getElementIndex()
        {
            return elementIndex;
        }

        public int getArrLength()
        {
            return arr.Length;
        }

        public bool getAnimationStart()
        {
            return animationStart;
        }

        public void checkEnd()
        {
            if (getElementIndex() >= getArrLength())
            {
                active = false;
            }
        }

        public void getNext()
        {
            elementIndex += 1;
            if (elementIndex < arr.Length)
            {
                if (cts != null)
                {
                    cts.Cancel();
                }
                cts = new CancellationTokenSource();
                currentTask = Task.Run(async () =>
                { //Task.Run puts on separate thread
                    printTime = 60;
                    await animateText(arr[elementIndex], cts.Token); //await pauses thread until animateText() is completed
                }, cts.Token);
            }
        }

        public void checkNext()
        {
            getNext();
            checkEnd();
        }

        public void loadNewDialogue(string speaker, string content)
        {
            
            if (speaker == "alex")
            {
                dialogueBoxSprite = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/speechbubbleright.png"));
                dialogueBoxSprite.Position = new Vector2f(SCREEN_WIDTH / 2 - (dialogueBoxSprite.GetGlobalBounds().Width / 2), SCREEN_HEIGHT / 5);

                name = new Text(speaker.ToUpper(), speechFont, 35);
                name.Position = new Vector2f(5, dialogueBoxSprite.GetGlobalBounds().Left + 30);
                dialogue = new Text(content, speechFont, 32);
                dialogue.Position = new Vector2f(dialogueBoxSprite.GetGlobalBounds().Left + 10, dialogueBoxSprite.GetGlobalBounds().Top + 30);
            }
            else if (speaker == "dad")
            {
                dialogueBoxSprite = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/speechbubbleleft.png"));
                dialogueBoxSprite.Position = new Vector2f(SCREEN_WIDTH / 4 - (dialogueBoxSprite.GetGlobalBounds().Width / 2), SCREEN_HEIGHT / 5);

                name = new Text(speaker.ToUpper(), speechFont, 35);
                name.Position = new Vector2f(5, dialogueBoxSprite.GetGlobalBounds().Left + 30);
                dialogue = new Text(content, speechFont, 32);
                dialogue.Position = new Vector2f(dialogueBoxSprite.GetGlobalBounds().Left + 10, dialogueBoxSprite.GetGlobalBounds().Top + 30);
            }
            else if (speaker == "mom")
            {
                dialogueBoxSprite = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/speechbubbleright.png"));
                dialogueBoxSprite.Position = new Vector2f(3 * SCREEN_WIDTH / 4 - (dialogueBoxSprite.GetGlobalBounds().Width / 2), SCREEN_HEIGHT / 5);

                name = new Text(speaker.ToUpper(), speechFont, 35);
                name.Position = new Vector2f(5, dialogueBoxSprite.GetGlobalBounds().Left + 30);
                dialogue = new Text(content, speechFont, 32);
                dialogue.Position = new Vector2f(dialogueBoxSprite.GetGlobalBounds().Left + 10, dialogueBoxSprite.GetGlobalBounds().Top + 30);

            }
            else if (speaker == "player")
            {
                dialogueBoxSprite = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/psb.png"));
                dialogueBoxSprite.Position = new Vector2f(0, (float)(SCREEN_HEIGHT * 0.74));

                name = new Text("YOU", speechFont, 40);
                name.Position = new Vector2f(5, dialogueBoxSprite.GetGlobalBounds().Left + 30);
                dialogue = new Text(content, speechFont, 32);
                dialogue.Position = new Vector2f(dialogueBoxSprite.GetGlobalBounds().Left + 10, dialogueBoxSprite.GetGlobalBounds().Top + 30);
            }
            name.Color = Color.White;
            dialogue.Color = Color.White;
            renderDialogue(content);
        }

        public void renderDialogue(String s)
        {
            active = true;
            elementIndex = 0;
            if (cts != null)
            {
                cts.Cancel();
            }
            cts = new CancellationTokenSource();

            arr = createStrings(dialogue);
            currentTask = Task.Run(async () =>
            { //Task.Run puts on separate thread
                printTime = 60;
                await animateText(arr[elementIndex], cts.Token); //await pauses thread until animateText() is completed

            }, cts.Token);
        }

        public Text[] createStrings(Text line)
        {
            float maxw = w - 26;
            float maxh = GetMaxTextHeight();
            List<Text> list = new List<Text>();

            // split dialogue into words
            String[] s = line.DisplayedString.Split(' ');
            line.DisplayedString = "";
            float currentLineWidth = 0;
            foreach (String word in s)
            {
                Text t = new Text(word + " ", speechFont, 24);
                float wordSizeWithSpace = t.GetGlobalBounds().Width;
                if (currentLineWidth + wordSizeWithSpace > maxw)
                {
    
                    line.DisplayedString += "\n";
                    currentLineWidth = 0;
                    if (line.GetGlobalBounds().Height > maxh)
                    {
                        list.Add(line);
                        line = new Text("", speechFont, 24);
                    }
                }

                line.DisplayedString += (t.DisplayedString);
                currentLineWidth += wordSizeWithSpace;
            }

            // Add the last one
            if (line.DisplayedString != "")
            {
                list.Add(line);

            }
            /*for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }*/

            return list.ToArray();

        }

        //async means this function can run separate from main app.
        //operate in own time and thread
        public async Task animateText(Text line, CancellationToken ct)
        {
            animationStart = true;
            state.resetTimer("game");
            int i = 0;
            dialogue.DisplayedString = "";
            while (i < line.DisplayedString.Length)
            {
                if (ct.IsCancellationRequested)
                {
                    ct.ThrowIfCancellationRequested();
                }
                if (state.GetState() != "pause")
                {
                    dialogue.DisplayedString = (string.Concat(dialogue.DisplayedString, line.DisplayedString[i++]));
                    await Task.Delay(printTime); //equivalent of putting thread to sleep
                }
            }
            // Do asynchronous work.
            if (tag == "AI") 
            {
                state.startTimer("game");
            }
            animationStart = false; //done animating
            awaitInput = true;

        }


        public void Draw(RenderTarget target, RenderStates states)
        {
            if (init)
            {
                target.Draw(dialogueBoxSprite);
                target.Draw(name);
                target.Draw(dialogue);
            }
        }
        public FloatRect GetBounds()
        {
            FloatRect f = new FloatRect(box.GetGlobalBounds().Left - 20,
                nameBox.GetGlobalBounds().Top - 20,
                box.GetGlobalBounds().Width + 20,
                (box.GetGlobalBounds().Top + box.GetGlobalBounds().Height + 20) - nameBox.GetGlobalBounds().Top);
            return f;
        }
        public FloatRect GetGlobalBounds()
        {
            return box.GetGlobalBounds();
        }

        public float GetMaxTextHeight()
        {
            return dialogueBoxSprite.GetGlobalBounds().Height - 20;
        }

        public Text BufferName(String speaker)
        {
            Text n = new Text(speaker.ToUpper(), speechFont, 32);
            if (tag == "PLAYER")
            {
                nameBox.Size = new Vector2f(n.GetGlobalBounds().Width + 30, this.h - 110);
                n.Position = new Vector2f(nameBox.Position.X + 12, nameBox.Position.Y);
                n.Color = Color.Black;
            } else
            {
                n.Position = new Vector2f(dialogueBoxSprite.Position.X + 55, dialogueBoxSprite.Position.Y + 7);
                n.Color = Color.White;
            }
            return n;
        }

        public void BufferDialogue(String s)
        {

        }


        public DialogueBox(float x, float y, float width, float height, GameState state, string tag)
        {
            //this.x = x;
            //this.y = y;
            //this.w = width;
            //this.h = height;
            this.state = state;
            this.tag = tag;

            //if (tag == "AI")
            //{
            //    view = new View(new FloatRect(new Vector2f(0, 0), new Vector2f(500, 248)));
            //}
            //else
            //{
            //    box = new RectangleShape(new Vector2f(this.w, this.h));
            //    box.Position = new Vector2f(this.x - 40, this.y + 35);
            //    box.OutlineThickness = 3;
            //    box.OutlineColor = Color.Black;

            //    nameBox = new RectangleShape(new Vector2f(this.w - 575, this.h - 100));
            //    nameBox.Position = new Vector2f(this.x, this.y);
            //    nameBox.OutlineThickness = 3;
            //    nameBox.OutlineColor = Color.Black;
            //    view = new View(GetBounds());
            //}
            
        }
    }
}