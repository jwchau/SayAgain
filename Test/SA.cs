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
        public static Font Adore64 = new Font(new FileStream("Content/Fonts/Adore64.ttf", FileMode.Open));
    }


    class SA : Game
    {
        static UInt32 SCREEN_WIDTH = VideoMode.DesktopMode.Width;
        static UInt32 SCREEN_HEIGHT = VideoMode.DesktopMode.Height;

        Dictionary<SFML.Window.Keyboard.Key, bool[]> keys = new Dictionary<SFML.Window.Keyboard.Key, bool[]>();

        Text instruction;

        UIManager ui_man = new UIManager();
        StartMenu startMenu;
        StartMenu settingsMenu;

        UITextBox TextBox = new UITextBox(SCREEN_WIDTH, SCREEN_HEIGHT/5, 0, SCREEN_HEIGHT - SCREEN_HEIGHT / 5, "HELLO WORLD!");
        //UISpeechBox SpeechBox = new UISpeechBox(700, 150, 50, 50, "Say Again by team babble fish", "Alex");

        InputManager ManagerOfInput = new InputManager();
        Text dialogue;
        Text name;
        static Color color = Color.Black;
        DialogueBox dialogueBox;
        Boolean init;
        Boolean started = false;
        int printTime;

        AlexState Alex = new AlexState(4.0,6.9);

        double[] nums = { -1, 2, 3, 4,
                            1, 2, 3, 4,
                            1, 2, 3, 4
                            };

        ToneEffects tfx = new ToneEffects();
        ContextFilter cf;
        Relationships rs = new Relationships();

        public SA() : base(VideoMode.DesktopMode.Width, VideoMode.DesktopMode.Height, "Say Again?", Color.Magenta)
        {

            window.KeyPressed += onKeyPressed;
            window.KeyReleased += onKeyReleased;
            window.MouseButtonPressed += onMouseButtonPressed;
            window.MouseButtonReleased += onMouseButtonReleased;
            window.MouseMoved += onMouseMoved;

        }

        private void onMouseMoved(object sender, MouseMoveEventArgs e)
        {
            if (State.GetState() == "game")
            {
                if (ManagerOfInput.GetMouseDown())
                {
                    ManagerOfInput.SetMouseMove(true);
                    ManagerOfInput.SetMousePos(e.X, e.Y);
                }
            }

        }

        private void onMouseButtonReleased(object sender, MouseButtonEventArgs e)    
        {

            ManagerOfInput.SetMouseMove(false);
            ManagerOfInput.SetMouseDown(false);
            ManagerOfInput.SetMouseRelease(true);

            var buttons = ui_man.getButtons();
            if (State.GetState() == "game")
            {
                for (var i = 0; i < buttons.Count; i++)
                {
                    if (buttons[i].GetSelected())
                    {
                        var selectedBounds = buttons[i].getRectBounds();
                        var boxBounds = TextBox.getBoxBounds();
                        //first check top left of textbox
                        if (selectedBounds.Top + selectedBounds.Height >= boxBounds.Top)
                        {
                            //CHECK MATRIX BS
                            double[,] final = tfx.MatrixMult(tfx, cf);
                            Console.WriteLine(final[2, 3]);

                            //Console.WriteLine("YO");
                            TextBox.getBoxText().DisplayedString = buttons[i].getNewDialogue();
                            //buttons.RemoveAt(i);
                            buttons[i].snapBack();
                            TextBox.setBoxColor(Color.Black); //to make sure it overwrites the hover color
                            buttons[i].SetSelected(false);
                            break;
                        }
                        else
                        {
                            buttons[i].snapBack();
                            buttons[i].SetSelected(false);
                        }
                    }
                }
            } else if(State.GetState() == "pause")//If game is paused
            {
                for (var i = 0; i < buttons.Count; i++)
                {
                    if (buttons[i].GetSelected())
                    {
                        buttons[i].snapBack();
                        buttons[i].SetSelected(false);
                    }
                }
            }

        }

        private void onMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            ManagerOfInput.SetMousePos(e.X, e.Y);
            ManagerOfInput.SetMouseRelease(false);
            ManagerOfInput.SetMouseDown(true);

            if (State.GetState() == "game")
            {
                var buttons = ui_man.getButtons();
                for (var i = 0; i < buttons.Count; i++)
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
            else if (State.GetState() == "menu") {
                // Menu Traversal Logic
                if (State.GetMenuState() == "start") //If Current Menu State is the Start Menu
                {
                    // Pass the current menu's buttons, along with a list of tuples symbolizing:
                    //      Tuple(ButtonText, TargetState, AnonymousFunction)
                    updateMenuState(startMenu.getMenuButtons(), new List<Tuple<string, string, Task>> {
                        new Tuple<string, string, Task>("Game Start", "game", new Task(() => {})),
                        new Tuple<string, string, Task>("Settings", "settings", new Task(() => {}))
                    });

                }
                else if (State.GetMenuState() == "settings") //If Current Menu State is the Settings Menu
                {
                    updateMenuState(settingsMenu.getMenuButtons(), new List<Tuple<string, string, Task>> {
                        new Tuple<string, string, Task>("8K GAMING", "settings", new Task(() => { if(clearColor == Color.Magenta) clearColor = Color.Cyan; // Change background clear color
                                                                                                  else clearColor = Color.Magenta; })),
                        new Tuple<string, string, Task>("<- Back", "start", new Task(() => {}))
                    });
                    
                }
            }
        }

        // Handle Menu Traversal and Game Launching
        private void updateMenuState(List<MenuButton> buttons, List<Tuple<string, string, Task>> mappings) {
            // Get Mouse Position
            var MousePos = ManagerOfInput.GetMousePos();

            // Loop through current menu's buttons
            for (var i = 0; i < buttons.Count; i++)
            {
                // If mouse position is over current button
                if (buttons[i].Contains(MousePos[0], MousePos[1]))
                {
                    // Find what this button is suppose to do
                    for(var j = 0; j < mappings.Count; j++)
                    {
                        // Found button being clicked
                        if (buttons[i].getMenuButtonText().DisplayedString == mappings[j].Item1)
                        {
                            // Change either game state or menu state based off of button's target state
                            if (mappings[j].Item2 == "game")
                            {
                                State.SetState(mappings[j].Item2);

                            } else {

                                State.SetMenuState(mappings[j].Item2);

                            }
                            mappings[j].Item3.Start();

                            break;
                        }
                    }
                    break;
                }
            }

        }

        private void onKeyReleased(object sender, KeyEventArgs e)
        {
            keys[e.Code] = new bool[] { false, e.Shift, e.Control, e.Alt };
        }

        private void onKeyPressed(object sender, KeyEventArgs e)
        {
            init = true;

            if(e.Code == Keyboard.Key.Escape)
            {
                window.Close();
            }

            if (e.Code == Keyboard.Key.P)
            {
                if (State.GetState() == "pause")
                {
                    State.SetState("game");
                }
                else if (State.GetState() == "game") {
                    State.SetState("pause");
                }
            }

            if (e.Code == Keyboard.Key.M)
            {
                dialogueBox = new DialogueBox(700, 150, 50, 50, "kitty kat", Color.Black);

                name = new Text("Alex", FontObjects.Adore64, 24);
                name.Position = new Vector2f(dialogueBox.x + 40, dialogueBox.y - 25);
                name.Color = color;

                Console.WriteLine("Initialize");
                string line = "say again by team babble fish";

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
            //load stuff for main menu

            startMenu = new StartMenu("start");
            settingsMenu = new StartMenu("settings");

            cf = new ContextFilter("school", nums);

            //intialize AI dialogue box
            string[] tone = new string[] { "Rude", "Kind", "Calm", "Sarcastic" };
            string[] jsondialogue = new string[] { "Rude Dialogue", "Kind Dialogue", "Calm Dialogue","Sarcastic Dialogue" };
            //initialize list of buttons
            int xPos = (int)SCREEN_WIDTH/tone.Length;
            for (int i = 1; i <= tone.Length; i++)
            {
                ui_man.addButton(new UIButton(xPos/2 + (i-1) * xPos, SCREEN_HEIGHT - SCREEN_HEIGHT / 4, tone[i-1], jsondialogue[i-1]));
            }
        


        }

        protected override void Initialize()
        {

            instruction = new Text("press m for dialogue \n"+
                "press space to speed up", FontObjects.Adore64, 24);


        }




        //async means this function can run separate from main app.
        //operate in own time and thread
        async Task animateText(string chatter) 
        {
            
            if (!started)
            {
                started = true;
                int i = 0;
                dialogue = new Text("", FontObjects.Adore64, 24);
                dialogue.Position = new Vector2f(50, 70);
                dialogue.Color = color;
                while (i < chatter.Length)
                {
                    dialogue.DisplayedString = (string.Concat(dialogue.DisplayedString, chatter[i++]));
                    await Task.Delay(printTime); //equivalent of putting thread to sleep
                }
                started = false;
            }
            // Do asynchronous work.
            
        }



        protected override void Update()
        {
            if (State.GetState() == "game") {
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
        }

        protected override void Draw()
        {
            window.Clear(clearColor);
            if (State.GetState() == "menu")
            {
                if(State.GetMenuState() == "start")
                {
                    window.Draw(startMenu);
                } else
                {
                    window.Draw(settingsMenu);
                }
                

            } else if (State.GetState() != "menu") {
                
                window.Draw(instruction);
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
                    //window.Draw(dialogueBox);
                    //window.Draw(dialogue);
                    //window.Draw(name);
                }
                if(State.GetState() == "pause")
                {

                }
            }

        }
    }
}
