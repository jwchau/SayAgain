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
using Newtonsoft.Json;

namespace Test
{

    class SA : Game
    {
        Sprite mom, alex, dad;
        public SA() : base(VideoMode.DesktopMode.Width, VideoMode.DesktopMode.Height, "Say Again?", Color.Magenta)
        {

            window.KeyPressed += onKeyPressed;
            window.KeyReleased += onKeyReleased;
            window.MouseButtonPressed += onMouseButtonPressed;
            window.MouseButtonReleased += onMouseButtonReleased;
            window.MouseMoved += onMouseMoved;

        }

        #region screen resize math
        private void screenHelper()
        {

            var DesktopX = (double)VideoMode.DesktopMode.Width;
            var DesktopY = (double)VideoMode.DesktopMode.Height;
            var WindowX = (double)window.Size.X;
            var WindowY = (double)window.Size.Y;
            scaleFactorX = DesktopX / WindowX;
            scaleFactorY = DesktopY / WindowY;
        }
        #endregion

        private void onMouseMoved(object sender, MouseMoveEventArgs e)
        {
            ManagerOfInput.OnMouseMoved(State, e.X, e.Y);

        }

        private void onMouseButtonReleased(object sender, MouseButtonEventArgs e)
        {

            ManagerOfInput.onMouseButtonReleased();

            ManagerOfInput.checkTargets(State, D_Man);

            ui_man.applyTones(e.X, e.Y);
        }

        private void onMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {

            ManagerOfInput.onMouseButtonPressed(e.X, e.Y);

            ManagerOfInput.GamePlay(State, buttons, e.X, e.Y);

            ManagerOfInput.MenuPlay(State, menus, e.X, e.Y);

            if(State.getGameTimer("game").Contains(e.X, e.Y) && State.getGameTimer("game").getStart())
            {
                State.getGameTimer("game").setCountDown(0);
            }
        }

        private void onKeyReleased(object sender, KeyEventArgs e)
        {
        }

        private void onKeyPressed(object sender, KeyEventArgs e)
        {

            if (State.GetState() == "game")
            {
                if (e.Code == Keyboard.Key.Space)
                {
                    dialogueBox.setPrintTime(0);
                    playerDialogueBox.setPrintTime(0);
                }

                if (e.Code == Keyboard.Key.N)
                {
                    dialogueBox.checkNext();
                }

                if (e.Code == Keyboard.Key.P)
                {
                    // Toggles game state between game and pause
                    State.TogglePause();
                }
            }

            //responseList = s.ChooseDialog(FNC, Load.playerDialogueObj1, currentMadeMemories, currentMilestones, currentTone, currentContext);



            if (e.Code == Keyboard.Key.A || e.Code == Keyboard.Key.M || e.Code == Keyboard.Key.D)
            {
                //init = true;
                responseListAlex = s.ChooseDialog(D_Man.getAlexFNC(), Load.alexDialogueObj1, currentMadeMemories, currentMilestones, currentTone, currentContext);
                dialogueBox.createCharacterDB(e, responseListAlex.ElementAt(0).content);
                //State.getGameTimer("game").startTimer();
            }
        }

        #region Timer Action Placeholder
        public void TimerAction()
        {

            updateTargetFNC();
            //update currentmademeories, currentmilestones, currenttone, currentcontext
            updateCurrents(); //updates everything besides FNC
            loadDialogues();
        }

        public void updateTargetFNC()
        {

            //load tonal matrix
            //get targets from player
            //get context
            //load context matrix
            //meth;
        }

        //after timer runs out update the current stuff
        private void updateCurrents()
        {
            if (responseList.ElementAt(0).nextContext.Count == 1)
            {
                if (responseList.ElementAt(0).nextContext[0] != "")
                {
                    if (!currentMadeMemories.Contains(currentContext))
                    {
                        currentMadeMemories.Add(currentContext);
                    }
                    currentContext = responseList.ElementAt(0).nextContext[0];

                }
            }
            else
            {
                Console.WriteLine("player choice");
            }

            if (!responseList.ElementAt(0).consequence.Equals(""))
            {
                currentMilestones.Add(responseList.ElementAt(0).consequence);
            }


            currentTone = ui_man.getPlayerDialogues()[0].getTone();

            Console.WriteLine("THE TONE I DRAGGED IN WAS: " + currentTone);
        }
        #endregion



        public void loadDialogues()
        {

            if (currentTone != tone.Root)
            {

                responseList = s.ChooseDialog(FNC, Load.playerDialogueObj1, currentMadeMemories, currentMilestones, currentTone, currentContext); //loads "what are you staring at?"
                responseListAlex = s.ChooseDialog(D_Man.getAlexFNC(), Load.alexDialogueObj1, currentMadeMemories, currentMilestones, currentTone, currentContext);//loads "yeah I missed you too?"
                ui_man.dialogueLoadOrder(State, playerDialogueBox, dialogueBox, responseList, responseListAlex);
                loadedAIDialogueOnce = true;

                //give this responselist to playerdialogue box
                Console.WriteLine("Affected Dialogue: " + responseList.ElementAt(0).content);
                //dialogueBox.loadNewDialogue("player", responseList.ElementAt(0).content);
                updateCurrents();
                responseList = s.ChooseDialog(FNC, Load.playerDialogueObj1, currentMadeMemories, currentMilestones, tone.Root, currentContext); //loads "how is school?"
                //give this response list to root player uitextbox.

                ui_man.reset(responseList);

            }
            else
            {
                //responseList = s.ChooseDialog(FNC, Load.playerDialogueObj1, currentMadeMemories, currentMilestones, currentTone, currentContext);
                //ui_man.reset(responseList);
                State.getGameTimer("game").resetTimer();
                State.getGameTimer("game").startTimer();
            }
        }

        protected override void Initialize()
        {
            Texture texturemom, texturealex, texturedad;
            FileStream m = new FileStream("../../Art/momdemo.png", FileMode.Open);
            FileStream a = new FileStream("../../Art/alexdemo.png", FileMode.Open);
            FileStream d = new FileStream("../../Art/daddemo.png", FileMode.Open);
            texturemom = new Texture(m);
            texturealex = new Texture(a);
            texturedad = new Texture(d);
            mom = new Sprite(texturemom);
            alex = new Sprite(texturealex);
            dad = new Sprite(texturedad);
            mom.Position = new Vector2f(1200, 350);
            dad.Position = new Vector2f(400, 325);
            alex.Position = new Vector2f(800, 400);

            //Originally in LoadContent/////////////////////////////////////////////////////////////////////////////////
            // Create Character states

            LoadInitialPreReqs();

            responseList = s.ChooseDialog(FNC, Load.playerDialogueObj1, currentMadeMemories, currentMilestones, currentTone, currentContext);

            string FirstDialogue = responseList[0].content;
            ui_man.produceTextBoxes2(FirstDialogue);
            //timeflag
            State.addTimer("game", 10, new Action(() => { TimerAction(); }));
            /////////////////////////////////////////////////////////////////////////////////////////////////////////

            fullScreenView = window.DefaultView;
            fullScreenView.Viewport = new FloatRect(0, 0, 1, 1);
            window.SetView(fullScreenView);

            dialogueBox = new DialogueBox(0, 0, 710, 150, State, "AI");
            playerDialogueBox = new DialogueBox(0, 0, 710, 150, State, "PLAYER");

            buttons = ui_man.getButtons();
            menus.Add(startMenu); menus.Add(settingsMenu); menus.Add(pauseMenu);
        }

        private void LoadInitialPreReqs()
        {

            currentMadeMemories.Add("");

            currentMilestones.Add("");

            currentContext = "AlexTalksPlayer";
            FNC = 0;
        }

        protected override void Update()
        {
            if (State.GetState() == "game")
            {

                // Update the game timerz
                State.updateTimerz();

                // Get the current UI Textboxes from the UI Manager
                var playerDialogues = ui_man.getPlayerDialogues();

                // Get the mouse coordinates from Input Manager
                var MouseCoord = ManagerOfInput.GetMousePos();

                // If the mouse is currently dragging
                if (ManagerOfInput.GetMouseDown())
                {

                    // Loop through buttons
                    for (var i = 0; i < buttons.Count; i++)
                    {
                        // Find button currently being interacted with
                        if (buttons[i].GetSelected())
                        {
                            // Move the button around the screen
                            buttons[i].translate(MouseCoord[0], MouseCoord[1]);

                            // Check collision with UI Textboxes
                            // Loop through UI Textboxes
                            for (var j = 0; j < playerDialogues.Count; j++)
                            {
                                // If the mouse just came from inside a UI Textbox
                                if (playerDialogues[j].wasMouseIn())
                                {
                                    if (!playerDialogues[j].Contains(MouseCoord[0], MouseCoord[1]))
                                    {
                                        // Mouse has now left the UI Textbox so set it to false
                                        playerDialogues[j].setMouseWasIn(false);
                                        // Reset the color to match its previous color
                                        playerDialogues[j].setBoxColor(playerDialogues[j].getBoxColor("prev"));
                                        // Update the rest of the buttons in the cluster
                                        ui_man.updateClusterColors(playerDialogues[j], playerDialogues, playerDialogues[j].getBoxColor("prev"), false);

                                    }

                                    // If mouse just came from outside the UI Textbox
                                }
                                else
                                {
                                    if (playerDialogues[j].Contains(MouseCoord[0], MouseCoord[1]))
                                    {
                                        // Mouse is now inside a UI Textbox, so set it to true
                                        playerDialogues[j].setMouseWasIn(true);
                                        // Update previous color to current color of the UI Textbox
                                        playerDialogues[j].setPrevColor(playerDialogues[j].getBoxColor("curr"));
                                        // Update current color to selected tonal button color
                                        playerDialogues[j].setBoxColor(buttons[i].getTonalColor());
                                        // Update the rest of the buttons in the cluster
                                        ui_man.updateClusterColors(playerDialogues[j], playerDialogues, buttons[i].getTonalColor(), true);

                                    }
                                }

                            }

                        }
                    }

                }

                // ui_man.dialogueLoadOrder(State, playerDialogueBox, dialogueBox, responseList, responseListAlex);
                if (playerDialogueBox.getAnimationStart() == false && loadedAIDialogueOnce == true)
                {
                    dialogueBox.setInit(true);
                    dialogueBox.loadNewDialogue("alex", responseListAlex.ElementAt(0).content);
                    loadedAIDialogueOnce = false;
                    if (dialogueBox.getAnimationStart() == false) {
                        loadedAIDialogueOnce = true;
                    }
                        
                }

            }
            else if (State.GetState() == "pause")
            {
                State.getGameTimer("game").PauseTimer();

            }


        }
        //Ensures that AI dialogue doesnt get loaded more than once per timer done
        bool loadedAIDialogueOnce = false;

        protected override void Draw()
        {

            window.Clear(clearColor);
            if (State.GetState() != "menu")
            {
                window.Draw(mom);
                window.Draw(alex);
                window.Draw(dad);
            }

            window.Draw(playerDialogueBox);
            window.Draw(dialogueBox);


            window.SetView(fullScreenView);
            if (State.GetState() == "menu")
            {
                if (State.GetMenuState() == "start")
                {
                    window.Draw(startMenu);
                }
                else
                {
                    window.Draw(settingsMenu);
                }

            }
            else
            {

                //Draw text box background box
                RectangleShape textBackground = new RectangleShape(new Vector2f(SCREEN_WIDTH, SCREEN_HEIGHT / 5));
                textBackground.Position = new Vector2f(0, SCREEN_HEIGHT - (SCREEN_HEIGHT / 5));
                textBackground.FillColor = Color.Black;
                window.Draw(textBackground);

                var dialogues = ui_man.getPlayerDialogues();

                for (var i = 0; i < dialogues.Count; i++)
                {
                    window.Draw(dialogues[i]);
                }
                var buttons = ui_man.getButtons();

                for (var i = 0; i < buttons.Count; i++)
                {
                    window.Draw(buttons[i]);
                }
                window.Draw(D_Man.getAlex());
                window.Draw(D_Man.getMom());
                window.Draw(D_Man.getDad());


                if (State.GetState() == "pause")
                {
                    pauseMenu.DrawBG(window);
                    if (State.GetMenuState() == "pause")
                    {
                        window.Draw(pauseMenu);
                    }
                    else if (State.GetMenuState() == "settings")
                    {
                        window.Draw(settingsMenu);
                    }

                }
                window.Draw(State.getGameTimer("game")); //this is the timer circle
            }

        }
    }
}
