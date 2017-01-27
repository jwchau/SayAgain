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




namespace Test
{

    class FontObjects
    {
        public static Font Adore64 = new Font(new FileStream("../../Fonts/Adore64.ttf", FileMode.Open));
    }


    class SA : Game
    {

        Dictionary<SFML.Window.Keyboard.Key, bool[]> keys = new Dictionary<SFML.Window.Keyboard.Key, bool[]>();


        UIManager ui_man = new UIManager();

        UITextBox TextBox = new UITextBox(800, 100, 0, 500, "HELLO WORLD!");

        InputManager ManagerOfInput = new InputManager();
        Text name, dialogue;
        static Color color = Color.Black;
        DialogueBox dialogueBox;
        Text[] arr;
        Boolean init;
        int elementIndex = 0;
        int printTime;
        public View fullScreenView, scrollview;

        public SA() : base(800, 600, "Say Again?", Color.Magenta)
        {

            window.KeyPressed += onKeyPressed;
            window.KeyReleased += onKeyReleased;
            window.MouseButtonPressed += onMouseButtonPressed;
            window.MouseButtonReleased += onMouseButtonReleased;
            window.MouseMoved += onMouseMoved;



        }

        private void onMouseMoved(object sender, MouseMoveEventArgs e)
        {
            if(ManagerOfInput.GetMouseDown())
            {
                ManagerOfInput.SetMouseMove(true);
                ManagerOfInput.SetMousePos(e.X, e.Y);
            }

        }

        private void onMouseButtonReleased(object sender, MouseButtonEventArgs e)    
        {
            ManagerOfInput.SetMouseMove(false);
            ManagerOfInput.SetMouseDown(false);
            ManagerOfInput.SetMouseRelease(true);

            var buttons = ui_man.getButtons();
            for (var i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].GetSelected())
                {
                    var selectedBounds = buttons[i].getRectBounds();
                    var boxBounds = TextBox.getBoxBounds();
                    //first check top left of textbox
                    if (selectedBounds.Top + selectedBounds.Height >= boxBounds.Top)
                    {
                        //Console.WriteLine("YO");
                        TextBox.getBoxText().DisplayedString = buttons[i].getNewDialogue();
                        buttons.RemoveAt(i);
                        TextBox.setBoxColor(Color.Black); //to make sure it overwrites the hover color
                        break;
                    }
                    else {
                        buttons[i].snapBack();
                        buttons[i].SetSelected(false);
                    }
                }
            }

           


         
        }

        private void onMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            var buttons = ui_man.getButtons();
            for(var i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].Contains(e.X, e.Y))
                {
                    var bounds = buttons[i].getRectBounds();
                    buttons[i].SetMouseOffset(e.X - (int)bounds.Left, e.Y - (int)bounds.Top);
                    buttons[i].SetSelected(true);
                }
            }
            
            ManagerOfInput.SetMousePos(e.X, e.Y);
            ManagerOfInput.SetMouseRelease(false);
            ManagerOfInput.SetMouseDown(true);
        }

        private void onKeyReleased(object sender, KeyEventArgs e)
        {
            keys[e.Code] = new bool[] { false, e.Shift, e.Control, e.Alt };
        }

        private void onKeyPressed(object sender, KeyEventArgs e)
        {
            init = true;
            if (e.Code == Keyboard.Key.N)
            {
                if (elementIndex < arr.Length)
                {
                    if (currentTask == null || currentTask.IsCompleted)
                    {
                        elementIndex += 1;
                        currentTask = Task.Run(async () =>
                        { //Task.Run puts on separate thread
                            printTime = 60;
                            await animateText(arr[elementIndex]); //await pauses thread until animateText() is completed
                        });
                    }
                }
            }
            if (e.Code == Keyboard.Key.M)
            {
                elementIndex = 0;
                renderDialogue("so much dope that it broke the scale " +
                "they say crack kills, but my crack sells " +
                "my brother in the kitchen and he wrappin a bale " +
                "louis v my bag and louis v on my belt " +
                "oh my god thats my baby caroline u divine " +
                "she my trap queen she my trap queen", "Alex");
            }



            if (e.Code == Keyboard.Key.Space)
            {
                printTime = 0;
            }

            if (!keys.ContainsKey(e.Code))
            {
                keys.Add(e.Code, new bool[] { true, e.Shift, e.Control, e.Alt });
            } else
            {
                keys[e.Code] = new bool[] { true, e.Shift, e.Control, e.Alt };
            }
        }

        Task currentTask;

        public void renderDialogue(String s, String speaker) { 
        
            if (currentTask == null || currentTask.IsCompleted)
            {
                name = dialogueBox.BufferName(speaker);
                dialogue = dialogueBox.BufferDialogue("");
                Text tmp = new Text(s, FontObjects.Adore64, 24);

                arr = createStrings(tmp);
                currentTask = Task.Run(async () =>
                { //Task.Run puts on separate thread
                    printTime = 60;
                    await animateText(arr[elementIndex]); //await pauses thread until animateText() is completed
                });
            }


        }
        protected override void LoadContent()
        {
            

            //initialize list of buttons
            ui_man.addButton(new UIButton(80, 100, 40, "SUH DUDE","I AM DIALOGUE 1"));
            ui_man.addButton(new UIButton(40, 20, 80, "woah","I AM DIALOGUE 2"));
            //Console.WriteLine("LoadContent");


        }

        protected override void Initialize()
        {

            //the view of the whole game
            fullScreenView = window.DefaultView;
            //the view port is the whole window
            fullScreenView.Viewport = new FloatRect(0, 0, 1, 1);
            window.SetView(fullScreenView);
            dialogueBox = new DialogueBox(0,0,710,150);
            scrollview = new View(dialogueBox.GetBounds());
            //where i want to view it (inside dialogueBox)
            scrollview.Viewport = new FloatRect(0.1f, 0.05f, 0.8f, 0.3f);

        }

        public Text[] createStrings(Text line)
        {
            float maxw = dialogueBox.w;
            float maxh = dialogueBox.GetMaxTextHeight();
            List<Text> list = new List<Text>();
            // Console.WriteLine(tmp);

            // split dialogue into words
            String[] s = line.DisplayedString.Split(' ');
            line.DisplayedString = "";
            float currentLineWidth = 0;
            foreach (String word in s)
            {
                Text t = new Text(word + " ", FontObjects.Adore64, 24);
                float wordSizeWithSpace = t.GetGlobalBounds().Width;
                if (currentLineWidth + wordSizeWithSpace > maxw)
                {
                    
                    line.DisplayedString += "\n";
                    currentLineWidth = 0;
                    if (line.GetGlobalBounds().Height > maxh)
                    {
                        list.Add(line);
                        line = new Text("", FontObjects.Adore64, 24);
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

            return list.ToArray();
            /*
            Task.Run(async () => { //Task.Run puts on separate thread
                printTime = 60;
                await animateText(line); //await pauses thread until animateText() is completed
            });*/
        }

        //async means this function can run separate from main app.
        //operate in own time and thread
        async Task animateText(Text line)
        {
            int i = 0;
            dialogue.DisplayedString = "";
            while (i < line.DisplayedString.Length)
            {
                dialogue.DisplayedString = (string.Concat(dialogue.DisplayedString, line.DisplayedString[i++]));
                await Task.Delay(printTime); //equivalent of putting thread to sleep
            }
             
            // Do asynchronous work.
            
        }

        protected override void Update()
        {
            
            if (ManagerOfInput.GetMouseDown())
            {
                var MouseCoord = ManagerOfInput.GetMousePos();
                var buttons = ui_man.getButtons();
                for (var i = 0; i < buttons.Count; i++)
                {
                    if (buttons[i].GetSelected())
                    {
                        buttons[i].translate(MouseCoord[0], MouseCoord[1]);
                        //check collision with textbox
                        var selectedBounds = buttons[i].getRectBounds();
                        var boxBounds = TextBox.getBoxBounds();
                        //change color if the button is hovering over the textbox
                        if (selectedBounds.Top + selectedBounds.Height >= boxBounds.Top)
                        {
                            TextBox.setBoxColor(Color.Blue);
                        }
                        else {
                            TextBox.setBoxColor(Color.Black);
                        }

                    }
                }
            }
        }

        protected override void Draw()
        {
            window.SetView(fullScreenView);
            window.Draw(TextBox.getBox());
            window.Draw(TextBox.getBoxText());

            var buttons = ui_man.getButtons();
            for (var i = 0; i < buttons.Count; i++)
            {
                window.Draw(buttons[i].getUIButtonRect());
                window.Draw(buttons[i].getUIButtonText());
            }

            if (init)
            {
                window.SetView(scrollview);
                window.Draw(dialogueBox);
                window.Draw(dialogue);
                window.Draw(name);
            }

        }
    }
}
