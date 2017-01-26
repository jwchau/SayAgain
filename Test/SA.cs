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

        Text instruction;

        UIManager ui_man = new UIManager();

        UITextBox TextBox = new UITextBox(800, 100, 0, 500, "HELLO WORLD!");
        //UISpeechBox SpeechBox = new UISpeechBox(700, 150, 50, 50, "Say Again by team babble fish", "Alex");

        InputManager ManagerOfInput = new InputManager();
        Text dialogue;
        Text name;
        static Color color = Color.Black;
        DialogueBox dialogueBox;
        Boolean init;
        Boolean started = false;
        int printTime;
        public View gameView, scrollview;

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
                if (scrollview != null)
                {
                    Console.WriteLine("press n ");

                    scrollview.Move(new Vector2f(100, 100));
                }
            }
            if (e.Code == Keyboard.Key.M)
            {
                dialogueBox = new DialogueBox(700, 150, 50, 50, "kitty kat", Color.Black);

                name = new Text("Alex", FontObjects.Adore64, 24);
                name.Position = new Vector2f(dialogueBox.x2, dialogueBox.y2 + 10);
                name.Color = color;

                Console.WriteLine("Initialize");
                string line = "so much dope that it broke the scale " +
                    "they say crack kills, but my crack sells " +
                    "my brother in the kitchen and he wrappin a bale " +
                    "louis v my bag and louis v on my belt " +
                    "oh my god thats my baby caroline u divine " +
                    "killaa west side ~ bad thing bad bad bad thing";


                Task.Run(async () => { //Task.Run puts on separate thread
                    printTime = 100;
                    await animateText(line); //await pauses thread until animateText() is completed
                });
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



        protected override void LoadContent()
        {
            //intialize AI dialogue box

            //initialize list of buttons
            ui_man.addButton(new UIButton(80, 100, 40, "SUH DUDE","I AM DIALOGUE 1"));
            ui_man.addButton(new UIButton(40, 20, 80, "woah","I AM DIALOGUE 2"));
            //Console.WriteLine("LoadContent");


        }

        protected override void Initialize()
        {
            //the view of the whole game
            gameView = window.GetView();
            //the view port is the whole window
            gameView.Viewport = new FloatRect(0, 0, 1, 1);
            window.SetView(gameView);

        }
        



        //async means this function can run separate from main app.
        //operate in own time and thread
        async Task animateText(string chatter) 
        {
            


            if (!started)
            {
                bool flag = true;
                
            
                started = true;
                int i = 0;
                dialogue = new Text("", FontObjects.Adore64, 20);
                dialogue.Position = new Vector2f(dialogueBox.x+25, dialogueBox.y + 20);
                dialogue.Color = color;
                //what i want to view(dialogue text)
                //scrollview = new View (dialogueBox.GetGlobalBounds());
                //where i want to view it (inside dialogueBox)
                //scrollview.Viewport = new FloatRect(0f, 0f, 1f,.1f/*dialogueBox.x2,
                  //  dialogueBox.y2 + 10, dialogueBox.w, dialogueBox.h*/);

                //scrollview.Center = new Vector2f(200,200);

                if (flag)
                {
                    Console.WriteLine(flag);
                    //window.SetView(scrollview);
                    flag = false;

                }
                //window.Draw(new Text("scrollview text", FontObjects.Adore64, 24));
                uint maxw = (uint)dialogueBox.w - 150;
                uint curw = 0;
                float maxh = dialogueBox.h;
                float curh = 0;
                while (i < chatter.Length)
                {
                    

                    dialogue.DisplayedString = (string.Concat(dialogue.DisplayedString, chatter[i++]));
                    curw += dialogue.CharacterSize;
                   

                    if (curw > maxw && Char.IsWhiteSpace(chatter[i - 1]))
                    {
                  
                        dialogue.DisplayedString = (string.Concat(dialogue.DisplayedString, "\n"));
                        Console.WriteLine("line break");
                        curw = 0;
                    }

                    curh = dialogue.GetGlobalBounds().Height;
                    if (curh > maxh)
                    {
                        Console.WriteLine("too tall");
                    }
                    
                    await Task.Delay(printTime); //equivalent of putting thread to sleep
                }
                started = false;
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
            
            window.Draw(TextBox.getBox());
            window.Draw(TextBox.getBoxText());

            // window.Draw(SpeechBox.getNameBox());
            // window.Draw(SpeechBox.getNameBoxText());
            //  window.Draw(SpeechBox.getSpeechBox());
            //  window.Draw(SpeechBox.getSpeechBoxText());

            var buttons = ui_man.getButtons();
            for (var i = 0; i < buttons.Count; i++)
            {
                window.Draw(buttons[i].getUIButtonRect());
                window.Draw(buttons[i].getUIButtonText());
            }

            if (init)
            {
                window.Draw(dialogueBox);
                window.Draw(dialogue);
                window.Draw(name);
            }

        }
    }
}
