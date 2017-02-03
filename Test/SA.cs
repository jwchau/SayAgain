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

namespace Test {
    class FontObjects {

        public static Font Adore64 = new Font(new FileStream("../../Fonts/Adore64.ttf", FileMode.Open));
        //public static Font Adore64 = new Font(new FileStream("Content/Fonts/Adore64.ttf", FileMode.Open));
        //public static Font Adore64 = new Font(new FileStream(@"C:\Users\leogo_000\Documents\GitHub\SayAgain\Test\Fonts/Adore64.ttf", FileMode.Open));
    }

    class SA : Game {
        Sprite mom;

        // Screen width and height
        static UInt32 SCREEN_WIDTH = VideoMode.DesktopMode.Width;
        static UInt32 SCREEN_HEIGHT = VideoMode.DesktopMode.Height;

        //Dictionary of Keyboard keys and their corresponding states.
        // MOVE TO MANAGER OF INPUT 
        Dictionary<SFML.Window.Keyboard.Key, bool[]> keys = new Dictionary<SFML.Window.Keyboard.Key, bool[]>();

        UITextBox TextBox = new UITextBox(0, SCREEN_HEIGHT - (SCREEN_HEIGHT / 5), "HELLO WORLD!");
        InputManager ManagerOfInput = new InputManager();




        Dictionary<String, DialogueBox> chars = new Dictionary<String, DialogueBox>();
        

        // Dialogue box and dialogue box custom color
        static Color color = Color.Black;
        DialogueBox alexDialogueBox, dadDialogueBox, momDialogueBox;

        // Dialogue init bool
        // FOX WITH TIMER
        Boolean init;

        // Different screen modes
        public View fullScreenView, alexDialogueView, dadDialogueView, momDialogueView, charView;

        // UI Manager Object
        UIManager ui_man = new UIManager();

        // Declaration of different menu states
        StartMenu startMenu;
        StartMenu settingsMenu;

        StartMenu pauseMenu;

        // Character declaration
        AlexState Alex = new AlexState(4.0, 6.9);

        double[] nums = { -1, 2, 3, 4,
                            1, 2, 3, 4,
                            1, 2, 3, 4
                            };

        ToneEffects tfx = new ToneEffects();
        ContextFilter cf;
        Relationships rs = new Relationships();


        public SA() : base(VideoMode.DesktopMode.Width, VideoMode.DesktopMode.Height, "Say Again?", Color.Magenta) {

            window.KeyPressed += onKeyPressed;
            window.KeyReleased += onKeyReleased;
            window.MouseButtonPressed += onMouseButtonPressed;
            window.MouseButtonReleased += onMouseButtonReleased;
            window.MouseMoved += onMouseMoved;

        }

        private void onMouseMoved(object sender, MouseMoveEventArgs e) {
            if (State.GetState() == "game") {
                if (ManagerOfInput.GetMouseDown()) {
                    ManagerOfInput.SetMouseMove(true);
                    ManagerOfInput.SetMousePos(e.X, e.Y);
                }
            }

        }

        private void onMouseButtonReleased(object sender, MouseButtonEventArgs e) {

            ManagerOfInput.SetMouseMove(false);
            ManagerOfInput.SetMouseDown(false);
            ManagerOfInput.SetMouseRelease(true);
            var MouseCoord = ManagerOfInput.GetMousePos();

            var buttons = ui_man.getButtons();

            if (State.GetState() == "game") {
                for (var i = 0; i < buttons.Count; i++) {
                    if (buttons[i].GetSelected()) {
                        //CHECK MATRIX BS
                        double[,] final = tfx.MatrixMult(tfx, cf);
                        Console.WriteLine(final[2, 3]);


                        var playerDialogues = ui_man.getPlayerDialogues();

                        for (var j = 0; j < playerDialogues.Count; j++) {

                            var boxBounds = playerDialogues[j].getBoxBounds();
                            //change color if the button is hovering over the textbox

                            if (playerDialogues[j].Contains(MouseCoord[0], MouseCoord[1])) {
                                playerDialogues[j].setPrevColor(playerDialogues[j].getBoxColor("curr"));
                                playerDialogues[j].setBoxColor(buttons[i].getTonalColor());

                                playerDialogues[j].changeDialogue(buttons[i].getNewDialogue());
                                playerDialogues[j].setAffected(true);
                                ui_man.updateText(j, buttons[i].getNewDialogue());
                                break;
                            }

                        }
                        buttons[i].snapBack();
                        buttons[i].SetSelected(false);
                        break;

                    }
                }
            } else if (State.GetState() == "pause")//If game is paused
            {
                for (var i = 0; i < buttons.Count; i++) {
                    if (buttons[i].GetSelected()) {
                        buttons[i].snapBack();
                        buttons[i].SetSelected(false);
                    }
                }
            }


        }

        private void onMouseButtonPressed(object sender, MouseButtonEventArgs e) {

            ManagerOfInput.SetMousePos(e.X, e.Y);
            ManagerOfInput.SetMouseRelease(false);
            ManagerOfInput.SetMouseDown(true);

            if (State.GetState() == "game") {
                var buttons = ui_man.getButtons();
                for (var i = 0; i < buttons.Count; i++) {
                    if (buttons[i].Contains(e.X, e.Y)) {
                        var bounds = buttons[i].getRectBounds();
                        buttons[i].SetMouseOffset(e.X - (int)bounds.Left, e.Y - (int)bounds.Top);
                        buttons[i].SetSelected(true);
                    }
                }

                ManagerOfInput.SetMousePos(e.X, e.Y);
                ManagerOfInput.SetMouseRelease(false);
                ManagerOfInput.SetMouseDown(true);
            } else if (State.GetState() == "menu") {
                // Menu Traversal Logic
                if (State.GetMenuState() == "start") //If Current Menu State is the Start Menu
                {
                    // Pass the current menu's buttons, along with a list of tuples symbolizing:
                    //      Tuple(ButtonText, TargetState, AnonymousFunction)
                    updateMenuState(startMenu.getMenuButtons(), new List<Tuple<string, string, Task>> {
                        new Tuple<string, string, Task>("Game Start", "game", new Task(() => {})),
                        new Tuple<string, string, Task>("Settings", "settings", new Task(() => {}))
                    });

                } else if (State.GetMenuState() == "settings") //If Current Menu State is the Settings Menu
                  {
                    updateMenuState(settingsMenu.getMenuButtons(), new List<Tuple<string, string, Task>> {
                        new Tuple<string, string, Task>("8K GAMING", "settings", new Task(() => { if(clearColor == Color.Magenta) clearColor = Color.Cyan; // Change background clear color
                                                                                                  else clearColor = Color.Magenta; })),
                        new Tuple<string, string, Task>("<- Back", "start", new Task(() => {}))
                    });

                }
            } else if (State.GetState() == "pause") {
                Console.WriteLine(State.GetState());
                if (State.GetMenuState() == "pause") {
                    updateMenuState(pauseMenu.getMenuButtons(), new List<Tuple<string, string, Task>> {
                        new Tuple<string, string, Task>("Back to Game", "game", new Task(() => {})),
                        new Tuple<string, string, Task>("Settings", "settings", new Task(() => {})),
                        new Tuple<string, string, Task>("Quit", "menu", new Task(() => {}))
                    });
                } else if (State.GetMenuState() == "settings") {
                    updateMenuState(settingsMenu.getMenuButtons(), new List<Tuple<string, string, Task>> {
                        new Tuple<string, string, Task>("8K GAMING", "settings", new Task(() => { if(clearColor == Color.Magenta) clearColor = Color.Cyan; // Change background clear color
                                                                                                  else clearColor = Color.Magenta; })),
                        new Tuple<string, string, Task>("<- Back", "pause", new Task(() => {}))
                    });
                }
            }
        }

        // Handle Menu Traversal and Game Launching
        private void updateMenuState(List<MenuButton> buttons, List<Tuple<string, string, Task>> mappings) {
            // Get Mouse Position
            var MousePos = ManagerOfInput.GetMousePos();

            // Loop through current menu's buttons
            for (var i = 0; i < buttons.Count; i++) {
                // If mouse position is over current button
                if (buttons[i].Contains(MousePos[0], MousePos[1])) {
                    // Find what this button is suppose to do
                    for (var j = 0; j < mappings.Count; j++) {
                        // Found button being clicked
                        if (buttons[i].getMenuButtonText().DisplayedString == mappings[j].Item1) {
                            // Do button action
                            mappings[j].Item3.Start();

                            // Change either game state or menu state based off of button's target state
                            if (mappings[j].Item2 == "game") {
                                State.SetState(mappings[j].Item2);
                            } else if (mappings[j].Item2 == "menu") {
                                State.SetState(mappings[j].Item2);
                                State.SetMenuState("start");
                            } else {
                                State.SetMenuState(mappings[j].Item2);
                            }

                            break;
                        }
                    }
                    break;

                }
            }

        }

        private void onKeyReleased(object sender, KeyEventArgs e) {
            keys[e.Code] = new bool[] { false, e.Shift, e.Control, e.Alt };
        }

        private void onKeyPressed(object sender, KeyEventArgs e) {

            if (e.Code == Keyboard.Key.N)
            {
                alexDialogueBox.forward();

            }


            if (e.Code == Keyboard.Key.Space)
            {
                alexDialogueBox.setPrintTime(0);

            }

            if (e.Code == Keyboard.Key.P) {
                if (State.GetState() == "pause") {
                    State.SetState("game");
                    State.SetMenuState("start");

                } else if (State.GetState() == "game") {
                    State.SetState("pause");
                    State.SetMenuState("pause");
                }
            }


            if (e.Code == Keyboard.Key.D)
            {
                init = true;

                chars["Mom"].active = false;
                chars["Alex"].active = false;
                dadDialogueBox.renderDialogue("I took my love, I took it down " +
                    "Climbed a mountain and I turned around " +
                    "And I saw my reflection in the snow covered hills " +
                    "'Til the landslide brought it down " +
                    "Oh, mirror in the sky " +
                    "What is love? " +
                    "Can the child within my heart rise above? " +
                    "Can I sail through the changin' ocean tides? " +
                    "Can I handle the seasons of my life? " +
                    "Well, I've been afraid of changin' " +
                    "'Cause I've built my life around you " +
                    "But time makes you bolder " +
                    "Even children get older " +
                    "And I'm getting older, too", "Dad", chars);
            }



            if (e.Code == Keyboard.Key.A) {
                init = true;

                chars["Mom"].active = false;
                chars["Dad"].active = false;
                alexDialogueBox.renderDialogue("I took my love, I took it down " +
                    "Climbed a mountain and I turned around " +
                    "And I saw my reflection in the snow covered hills " +
                    "'Til the landslide brought it down " +
                    "Oh, mirror in the sky " +
                    "What is love? " +
                    "Can the child within my heart rise above? " +
                    "Can I sail through the changin' ocean tides? " +
                    "Can I handle the seasons of my life? " +
                    "Well, I've been afraid of changin' " +
                    "'Cause I've built my life around you " +
                    "But time makes you bolder " +
                    "Even children get older " +
                    "And I'm getting older, too", "Alex", chars);
            }

            if (e.Code == Keyboard.Key.M)
            {
                init = true;
                chars["Alex"].active = false;
                chars["Dad"].active = false;
                momDialogueBox.renderDialogue("I took my love, I took it down " +
                    "Climbed a mountain and I turned around " +
                    "And I saw my reflection in the snow covered hills " +
                    "'Til the landslide brought it down " +
                    "Oh, mirror in the sky " +
                    "What is love? " +
                    "Can the child within my heart rise above? " +
                    "Can I sail through the changin' ocean tides? " +
                    "Can I handle the seasons of my life? " +
                    "Well, I've been afraid of changin' " +
                    "'Cause I've built my life around you " +
                    "But time makes you bolder " +
                    "Even children get older " +
                    "And I'm getting older, too", "Mom", chars);
            }



            if (!keys.ContainsKey(e.Code)) {
                keys.Add(e.Code, new bool[] { true, e.Shift, e.Control, e.Alt });
            } else {
                keys[e.Code] = new bool[] { true, e.Shift, e.Control, e.Alt };
            }
        }




        protected override void LoadContent() {
            //load stuff for main menu

            startMenu = new StartMenu("start");
            settingsMenu = new StartMenu("settings");
            pauseMenu = new StartMenu("pause");

            cf = new ContextFilter("school", nums);

            string test = "My name is Raman. My name is Michael. My name is John. My name is Jill. My name is Yuna. My name is Leo. My name is Koosha.";
            ui_man.produceTextBoxes(test);



        }

        protected override void Initialize() {
            Texture texture;
            FileStream f = new FileStream("../../Art/angrymom.png", FileMode.Open);
            texture = new Texture(f);


            mom = new Sprite(texture);

            //the view of the whole game
            fullScreenView = window.DefaultView;
            //the view port is the whole window
            fullScreenView.Viewport = new FloatRect(0, 0, 1, 1);
            window.SetView(fullScreenView);
            alexDialogueBox = new DialogueBox(0, 0, 710, 150);
            alexDialogueView = new View(alexDialogueBox.GetBounds());
            alexDialogueView.Viewport = new FloatRect(0.3f, 0f, 0.35f, 0.2f);

            dadDialogueBox = new DialogueBox(0, 0, 710, 150);
            dadDialogueView = new View(dadDialogueBox.GetBounds());
            dadDialogueView.Viewport = new FloatRect(0.0f, 0f, 0.35f, 0.2f);

            momDialogueBox = new DialogueBox(0, 0, 710, 150);
            momDialogueView = new View(momDialogueBox.GetBounds());
            momDialogueView.Viewport = new FloatRect(0.63f, 0f, 0.35f, 0.2f);


            chars.Add("Mom",momDialogueBox);
            chars.Add("Dad",dadDialogueBox);
            chars.Add("Alex",alexDialogueBox);

            charView = new View(mom.GetGlobalBounds());
            charView.Viewport = new FloatRect(0.3f, 0f, 0.1f, 0.1f);

        }



        protected override void Update() {
            if (State.GetState() == "game") {


                // Timer update
                if (State.getCountDown() > 0) {
                    //as long as you are not out of time
                    State.setNewTime((DateTime.Now.Ticks / 10000000) - State.getTimeDiff());
                    State.setCountDown(9 - (State.getNewTime() - State.getGameTime()));

                }

                // Get the current UI Textboxes from the UI Manager
                var playerDialogues = ui_man.getPlayerDialogues();

                // Get the mouse coordinates from Input Manager
                var MouseCoord = ManagerOfInput.GetMousePos();

                // If the mouse is currently dragging
                if (ManagerOfInput.GetMouseDown()) {
                    // Get tonal buttons from UI Manager
                    var buttons = ui_man.getButtons();

                    // Loop through buttons
                    for (var i = 0; i < buttons.Count; i++) {
                        // Find button currently being interacted with
                        if (buttons[i].GetSelected()) {
                            // Move the button around the screen
                            buttons[i].translate(MouseCoord[0], MouseCoord[1]);

                            // Check collision with UI Textboxes
                            // Loop through UI Textboxes
                            for (var j = 0; j < playerDialogues.Count; j++) {
                                // If the mouse just came from inside a UI Textbox
                                if (playerDialogues[j].wasMouseIn()) {
                                    if (!playerDialogues[j].Contains(MouseCoord[0], MouseCoord[1])) {
                                        // Reset the color to match its previous color
                                        playerDialogues[j].setBoxColor(playerDialogues[j].getBoxColor("prev"));
                                        // Mouse has now left the UI Textbox so set it to false
                                        playerDialogues[j].setMouseWasIn(false);
                                    }

                                    // If mouse just came from outside the UI Textbox
                                } else {
                                    if (playerDialogues[j].Contains(MouseCoord[0], MouseCoord[1])) {
                                        // Update previous color to current color of the UI Textbox
                                        playerDialogues[j].setPrevColor(playerDialogues[j].getBoxColor("curr"));
                                        // Update current color to selected tonal button color
                                        playerDialogues[j].setBoxColor(buttons[i].getTonalColor());
                                        // Mouse is now inside a UI Textbox, so set it to true
                                        playerDialogues[j].setMouseWasIn(true);

                                    }
                                }

                            }

                        }
                    }

                }

            } else if (State.GetState() == "pause") {
                State.setPauseTime(State.getNewTime());
                double a = State.getPauseTime();
                double b = DateTime.Now.Ticks / 10000000;
                State.setTimeDiff(b - a);

            }
        }

        protected override void Draw() {
            window.SetView(fullScreenView);


            window.Clear(clearColor);
            if (State.GetState() == "menu") {
                if (State.GetMenuState() == "start") {
                    window.Draw(startMenu);
                } else {
                    window.Draw(settingsMenu);
                }


            } else if (State.GetState() != "menu") {
                
                window.Draw(mom);


                //Draw text box background box
                RectangleShape textBackground = new RectangleShape(new SFML.System.Vector2f(SCREEN_WIDTH, SCREEN_HEIGHT / 5));
                textBackground.Position = new SFML.System.Vector2f(0, SCREEN_HEIGHT - (SCREEN_HEIGHT / 5));
                textBackground.FillColor = Color.Black;
                window.Draw(textBackground);


                var dialogues = ui_man.getPlayerDialogues();

                for (var i = 0; i < dialogues.Count; i++) {
                    window.Draw(dialogues[i]);
                }
                var buttons = ui_man.getButtons();

                for (var i = 0; i < buttons.Count; i++) {
                    window.Draw(buttons[i]);
                }

                if (init) {
                    //    window.Draw(alexDialogueBox);
                    //    window.Draw(dialogue);
                    //    window.Draw(name);
                }

                if (init && alexDialogueBox.active) {
                    //UNCOMMENT
                    window.SetView(alexDialogueView);
                    window.Draw(alexDialogueBox);
                    window.Draw(alexDialogueBox.dialogue);
                    window.Draw(alexDialogueBox.name);

                }

                if (init && dadDialogueBox.active)
                {
                    Console.WriteLine("Why");
                    //UNCOMMENT
                    window.SetView(dadDialogueView);
                    window.Draw(dadDialogueBox);
                    window.Draw(dadDialogueBox.dialogue);
                    window.Draw(dadDialogueBox.name);

                }

                if (init && momDialogueBox.active)
                {
                    Console.WriteLine("Why");
                    //UNCOMMENT
                    window.SetView(momDialogueView);
                    window.Draw(momDialogueBox);
                    window.Draw(momDialogueBox.dialogue);
                    window.Draw(momDialogueBox.name);

                }


                if (State.GetState() == "pause") {
                    pauseMenu.DrawBG(window);
                    if (State.GetMenuState() == "pause") {
                        window.Draw(pauseMenu);
                    } else if (State.GetMenuState() == "settings") {
                        window.Draw(settingsMenu);
                    }

                }
            }


        }
    }
}
