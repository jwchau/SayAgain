﻿using System;
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
        public static Font Adore64 = new Font(new FileStream(@"C:\Users\leogo_000\Documents\GitHub\SayAgain\Test\Fonts/Adore64.ttf", FileMode.Open));
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
        bool pause = false;


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
            else 
            updateMenuState(e.X,e.Y);


            //
            //if updatestate( currentmenu , 8k gmaing , -> gamek)
            //if updatestte ( currentmenu , 8k gaming , -> menu)
            //
            //
            //
            //else if (State.GetState() == "menu") {
            //    //detection of clicks on menu buttons
            //    if (State.GetMenuState() == "start")
            //    {
            //       // updateMenuState(current menu, string, target);
            //        var buttons = startMenu.getMenuButtons();
            //        for (var i = 0; i < buttons.Count; i++)
            //        {
            //            if (buttons[i].Contains(e.X, e.Y))
            //            {
            //                var bounds = buttons[i].getRectBounds();
            //                if (buttons[i].getMenuButtonText().DisplayedString == "Game Start")
            //                {
            //                    State.SetState("game");
            //                }
            //                else if (buttons[i].getMenuButtonText().DisplayedString == "Settings")
            //                {
            //                    State.SetMenuState("settings");
            //                }
            //            }
            //        }
            //    } else
            //    {
            //        var buttons = settingsMenu.getMenuButtons();
            //        for (var i = 0; i < buttons.Count; i++)
            //        {
            //            if (buttons[i].Contains(e.X, e.Y))
            //            {
            //                var bounds = buttons[i].getRectBounds();
            //                if (buttons[i].getMenuButtonText().DisplayedString == "8K GAMING")
            //                {
            //                    State.SetState("game");
            //                }
            //                else if (buttons[i].getMenuButtonText().DisplayedString == "<- Back")
            //                {
            //                    State.SetMenuState("start");
            //                }
            //            }
            //        }
            //    }

            //    ManagerOfInput.SetMousePos(e.X, e.Y);
            //    ManagerOfInput.SetMouseRelease(false);
            //    ManagerOfInput.SetMouseDown(true);


            //}
        }

        private void updateMenuState(int x, int y) {
            if (State.GetState() == "game")
            {
                var buttons = ui_man.getButtons();
                for (var i = 0; i < buttons.Count; i++)
                {
                    if (buttons[i].Contains(x,y))
                    {
                        var bounds = buttons[i].getRectBounds();
                        buttons[i].SetMouseOffset(x - (int)bounds.Left, y - (int)bounds.Top);
                        buttons[i].SetSelected(true);
                    }
                }

                ManagerOfInput.SetMousePos(x, y);
                ManagerOfInput.SetMouseRelease(false);
                ManagerOfInput.SetMouseDown(true);
            }
            else if (State.GetState() == "menu")
            {
                //detection of clicks on menu buttons
                if (State.GetMenuState() == "start")
                {
                    // updateMenuState(parms);
                    var buttons = startMenu.getMenuButtons();
                    for (var i = 0; i < buttons.Count; i++)
                    {
                        if (buttons[i].Contains(x, y))
                        {
                            var bounds = buttons[i].getRectBounds();
                            if (buttons[i].getMenuButtonText().DisplayedString == "Game Start")
                            {
                                State.SetState("game");
                            }
                            else if (buttons[i].getMenuButtonText().DisplayedString == "Settings")
                            {
                                State.SetMenuState("settings");
                            }
                        }
                    }
                }
                else
                {
                    var buttons = settingsMenu.getMenuButtons();
                    for (var i = 0; i < buttons.Count; i++)
                    {
                        if (buttons[i].Contains(x, y))
                        {
                            var bounds = buttons[i].getRectBounds();
                            if (buttons[i].getMenuButtonText().DisplayedString == "8K GAMING")
                            {
                                State.SetState("game");
                            }
                            else if (buttons[i].getMenuButtonText().DisplayedString == "<- Back")
                            {
                                State.SetMenuState("start");
                            }
                        }
                    }
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

            //intialize AI dialogue box
            string[] tone = new string[] { "suh dood", "a dood", "doodet", "pc" };
            string[] jsondialogue = new string[] { "long ass-sentence", "short hyphen ass-sentence", "aye gains", "words" };
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
                    //menu stuff goes here
                    window.Draw(startMenu);
                } else
                {
                    window.Draw(settingsMenu);
                }
                

            } else if (State.GetState() != "menu") {
                
                // change to GameState if (!pause) { 
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
