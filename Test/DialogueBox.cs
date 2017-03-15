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
        Vector2f scale = new Vector2f(SCREEN_WIDTH / 1920, SCREEN_HEIGHT / 1080);
        private Text name, dialogue;
        //public Dialogue line;
        public RectangleShape box;
        public RectangleShape nameBox;
        Task currentTask;
        uint dialogueFontSize = 40;
        uint nameFontSize = 55;

        static Dictionary<string, Sprite> spriteDict = new Dictionary<string, Sprite>();
        
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
                dialogueBoxSprite = spriteDict["right"];
                dialogueBoxSprite.Position = new Vector2f(SCREEN_WIDTH / 2 - (dialogueBoxSprite.GetGlobalBounds().Width / 2), SCREEN_HEIGHT / 5);

                name = new Text(speaker.ToUpper(), speechFont, nameFontSize);
                name.Position = new Vector2f(dialogueBoxSprite.GetGlobalBounds().Left + ((118 * scale.X) - name.GetGlobalBounds().Width / 2), dialogueBoxSprite.GetGlobalBounds().Top + ((22 * scale.Y) - name.GetGlobalBounds().Height));
                dialogue = new Text(content, speechFont, dialogueFontSize);
                dialogue.Position = new Vector2f(dialogueBoxSprite.GetGlobalBounds().Left + (uint)(SCREEN_WIDTH * 0.004), dialogueBoxSprite.GetGlobalBounds().Top + (uint)(SCREEN_HEIGHT * 0.046));
            }
            else if (speaker == "dad")
            {
                dialogueBoxSprite = spriteDict["left"];
                dialogueBoxSprite.Position = new Vector2f((float)(SCREEN_WIDTH*0.21) - (dialogueBoxSprite.GetGlobalBounds().Width / 2), (float)(SCREEN_HEIGHT*0.19) - (dialogueBoxSprite.GetGlobalBounds().Height / 2));

                name = new Text(speaker.ToUpper(), speechFont, nameFontSize);
                name.Position = new Vector2f(dialogueBoxSprite.GetGlobalBounds().Left + ((118*scale.X) - name.GetGlobalBounds().Width/2), dialogueBoxSprite.GetGlobalBounds().Top + ((22 * scale.Y) - name.GetGlobalBounds().Height));
                dialogue = new Text(content, speechFont, dialogueFontSize);
                dialogue.Position = new Vector2f(dialogueBoxSprite.GetGlobalBounds().Left + (uint)(SCREEN_WIDTH*0.004), dialogueBoxSprite.GetGlobalBounds().Top + (uint)(SCREEN_HEIGHT*0.046));
            }
            else if (speaker == "mom")
            {
                dialogueBoxSprite = spriteDict["right"];
                dialogueBoxSprite.Position = new Vector2f(3 * SCREEN_WIDTH / 4 - (dialogueBoxSprite.GetGlobalBounds().Width / 2), SCREEN_HEIGHT / 5);

                name = new Text(speaker.ToUpper(), speechFont, nameFontSize);
                name.Position = new Vector2f(dialogueBoxSprite.GetGlobalBounds().Left + ((118 * scale.X) - name.GetGlobalBounds().Width / 2), dialogueBoxSprite.GetGlobalBounds().Top + ((22 * scale.Y) - name.GetGlobalBounds().Height));
                dialogue = new Text(content, speechFont, dialogueFontSize);
                dialogue.Position = new Vector2f(dialogueBoxSprite.GetGlobalBounds().Left + (uint)(SCREEN_WIDTH * 0.004), dialogueBoxSprite.GetGlobalBounds().Top + (uint)(SCREEN_HEIGHT * 0.046));

            }
            else if (speaker == "player")
            {
                dialogueBoxSprite = spriteDict["player"];
                dialogueBoxSprite.Position = new Vector2f(0, (float)(SCREEN_HEIGHT * 0.74));

                name = new Text("YOU", speechFont, nameFontSize + 40);
                name.Position = new Vector2f(dialogueBoxSprite.GetGlobalBounds().Left + ((354 * scale.X) - name.GetGlobalBounds().Width / 2), dialogueBoxSprite.GetGlobalBounds().Top + ((32 * scale.Y) - name.GetGlobalBounds().Height));
                dialogue = new Text(content, speechFont, dialogueFontSize + 40);
                dialogue.Position = new Vector2f(dialogueBoxSprite.GetGlobalBounds().Left + (uint)(SCREEN_WIDTH * 0.004), dialogueBoxSprite.GetGlobalBounds().Top + (uint)(SCREEN_HEIGHT * 0.062));
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
                Console.WriteLine("ARR at " + elementIndex + ": " + arr[elementIndex].DisplayedString);
                printTime = 60;
                await animateText(arr[elementIndex], cts.Token); //await pauses thread until animateText() is completed

            }, cts.Token);
        }

        public Text[] createStrings(Text line)
        {
            float maxw = dialogueBoxSprite.GetGlobalBounds().Width;
            float maxh = GetMaxTextHeight();
            List<Text> list = new List<Text>();
            Text newline = new Text(line.DisplayedString, line.Font, dialogueFontSize);
            // split dialogue into words
            String[] s = newline.DisplayedString.Split(' ');
            newline.DisplayedString = "";
            float currentLineWidth = 0;
            foreach (String word in s)
            {
                Console.WriteLine("WORD: " + word);
                Text t = new Text(word + " ", speechFont, dialogueFontSize);
                float wordSizeWithSpace = t.GetGlobalBounds().Width;
                if (currentLineWidth + wordSizeWithSpace > maxw)
                {

                    newline.DisplayedString += "\n";
                    currentLineWidth = 0;
                    if (newline.GetGlobalBounds().Height*1.5 > maxh)
                    {
                        list.Add(newline);
                        newline = new Text("", speechFont, dialogueFontSize);
                    }
                }

                newline.DisplayedString += (t.DisplayedString);
                currentLineWidth += wordSizeWithSpace;
            }



            // Add the last one
            if (newline.DisplayedString != "")
            {
                list.Add(newline);

            }
            for (int i = 0; i < list.Count; i++)
            {
                //Console.WriteLine("LIST!: " + list[i].DisplayedString);
            }

            return list.ToArray();

        }

        public void AlertSoundMan()
        {
            //send signal to sound man
        }

        //async means this function can run separate from main app.
        //operate in own time and thread
        public async Task animateText(Text line, CancellationToken ct)
        {
            Console.WriteLine("LINE: " + line.DisplayedString);
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
                    Console.WriteLine("Line at " + i + ": " + line.DisplayedString[i]);
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

        public float GetMaxTextHeight()
        {
            return 156 * (SCREEN_HEIGHT / 1080);
        }

        public DialogueBox(GameState state, string tag)
        {
            this.state = state;
            this.tag = tag;

            if(!spriteDict.ContainsKey("left"))
            {
                spriteDict.Add("left", new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/speechbubbleleft.png")));
                spriteDict["left"].Scale = scale;
            }
            if (!spriteDict.ContainsKey("right"))
            {
                spriteDict.Add("right", new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/speechbubbleright.png")));
                spriteDict["right"].Scale = scale;
            }
            if (!spriteDict.ContainsKey("player"))
            {
                spriteDict.Add("player", new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/psb.png")));
                spriteDict["player"].Scale = scale;
            }
            
        }
    }
}