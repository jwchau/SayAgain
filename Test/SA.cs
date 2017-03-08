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

namespace Test {

    class SA : Game {
        

        public View fullScreenView, charView;
        Character mom, alexis, dad;


        public SA() : base(VideoMode.DesktopMode.Width, VideoMode.DesktopMode.Height, "Say Again?", Color.Magenta)
        {
            
            window.KeyPressed += onKeyPressed;
            window.KeyReleased += onKeyReleased;
            window.MouseButtonPressed += onMouseButtonPressed;
            window.MouseButtonReleased += onMouseButtonReleased;
            window.MouseMoved += onMouseMoved;

        }

        #region screen resize math
        private void screenHelper() {
            var DesktopX = (double)VideoMode.DesktopMode.Width;
            var DesktopY = (double)VideoMode.DesktopMode.Height;
            var WindowX = (double)window.Size.X;
            var WindowY = (double)window.Size.Y;
            scaleFactorX = DesktopX / WindowX;
            scaleFactorY = DesktopY / WindowY;
        }
        #endregion

        private void onMouseMoved(object sender, MouseMoveEventArgs e) {
            ManagerOfInput.OnMouseMoved(State, e.X, e.Y);
           
            ui_man.SweepButtons(e.X, e.Y, scaleFactorX, scaleFactorY);

            if (D_Man.getAlex().Contains(e.X, e.Y)) {
                D_Man.getAlex().setHover(true);
            } else if (D_Man.getMom().Contains(e.X, e.Y)) {
                D_Man.getMom().setHover(true);
            } else if (D_Man.getDad().Contains(e.X, e.Y)) {
                D_Man.getDad().setHover(true);
            } else {
                D_Man.getAlex().setHover(false);
                D_Man.getMom().setHover(false);
                D_Man.getDad().setHover(false);
            }

        }

        private void onMouseButtonReleased(object sender, MouseButtonEventArgs e) {

            ManagerOfInput.onMouseButtonReleased();

            if (playerChoice) {
                //ManagerOfInput.checkTargets(State, D_Man);
                if (D_Man.getAlex().Contains(e.X, e.Y) == true) {
                    currentContext = nextContextDict["Alex"];
                    loadDialogues();
                    playerChoice = false;
                    //COME BACK HERE
                }

                else if (D_Man.getMom().Contains(e.X, e.Y) == true) {
                    currentContext = nextContextDict["Mom"];
                    loadDialogues();
                    playerChoice = false;
                    //COME BACK HERE
                }

                else if (D_Man.getDad().Contains(e.X, e.Y) == true) {
                    currentContext = nextContextDict["Dad"];
                    loadDialogues();
                    playerChoice = false;
                    State.getGameTimer("game").resetTimer();
                    State.getGameTimer("game").startTimer();
                    //restart the timer
                    //COME BACK HERE
                }

                //d.getAlex().targetCheck(MouseX, MouseY);
                //d.getMom().targetCheck(MouseX, MouseY);
                //d.getDad().targetCheck(MouseX, MouseY);


            }


            ui_man.applyTones(e.X, e.Y);
        }

        private void onMouseButtonPressed(object sender, MouseButtonEventArgs e) {


            ManagerOfInput.onMouseButtonPressed(e.X, e.Y);

            ManagerOfInput.GamePlay(State, buttons, e.X, e.Y, scaleFactorX, scaleFactorY);

            ManagerOfInput.MenuPlay(State, menus, e.X, e.Y);

            if (State.getGameTimer("game").Contains(e.X, e.Y) && State.getGameTimer("game").getStart()) {
                State.getGameTimer("game").setCountDown(0);
            }
        }

        private void onKeyReleased(object sender, KeyEventArgs e) {
        }

        private void onKeyPressed(object sender, KeyEventArgs e) {

            if (State.GetState() == "game") {
                if (e.Code == Keyboard.Key.Space) {
                    dialogueBox.setPrintTime(0);
                    playerDialogueBox.setPrintTime(0);
                }

                if (e.Code == Keyboard.Key.N) {
                    dialogueBox.checkNext();
                }

                if (e.Code == Keyboard.Key.P) {
                    // Toggles game state between game and pause
                    State.TogglePause();
                }
            }

            //responseList = s.ChooseDialog(FNC, Load.playerDialogueObj1, currentMadeMemories, currentMilestones, currentTone, currentContext);



            //if (e.Code == Keyboard.Key.A || e.Code == Keyboard.Key.M || e.Code == Keyboard.Key.D)
            //{
            //    //init = true;
            //    responseListAlex = s.ChooseDialog(D_Man.getAlexFNC(), Load.alexDialogueObj1, currentMadeMemories, currentMilestones, currentTone, currentContext);
            //    dialogueBox.createCharacterDB(e, responseListAlex.ElementAt(0).content);
            //    //State.getGameTimer("game").startTimer();
            //}
        }

        #region Timer Action Placeholder
        public void TimerAction() {

            updateTargetFNC();
            //update currentmademeories, currentmilestones, currenttone, currentcontext
            updateCurrents(); //updates everything besides FNC

            loadDialogues();

        }

        public void updateTargetFNC() {

            //load tonal matrix
            //get targets from player
            //get context
            //load context matrix
            //meth;
        }

        //after timer runs out update the current stuff
        private void updateCurrents() {
            List<string> nextContext = responseList[0].nextContext;
            if (nextContext.Count == 1) {
                if (nextContext[0] != "") {
                    if (!currentMadeMemories.Contains(currentContext)) {
                        currentMadeMemories.Add(currentContext);
                    }
                    currentContext = nextContext[0];

                }

                playerChoice = false;
            } else {

                Console.WriteLine("player choice");
                for (int i = 0; i != nextContext.Count; i++) {
                    if (nextContext[i][0] == 'A') {
                        Console.WriteLine("Ayyyyy");
                        D_Man.activateCharacterChoice("Alex");
                        if (!nextContextDict.ContainsKey("Alex")) {
                            nextContextDict.Add("Alex", nextContext[i]);
                        } else {
                            nextContextDict["Alex"] = nextContext[i];
                        }
                    } else if (nextContext[i][0] == 'M') {
                        Console.WriteLine("Mmmmm");
                        D_Man.activateCharacterChoice("Mom");
                        if (!nextContextDict.ContainsKey("Mom")) {
                            nextContextDict.Add("Mom", nextContext[i]);
                        } else {
                            nextContextDict["Mom"] = nextContext[i];
                        }
                    } else if (nextContext[i][0] == 'D') {
                        Console.WriteLine("Deez Nutz");
                        D_Man.activateCharacterChoice("Dad");
                        if (!nextContextDict.ContainsKey("Dad")) {
                            nextContextDict.Add("Dad", nextContext[i]);
                        } else {
                            nextContextDict["Dad"] = nextContext[i];
                        }
                    }
                }
                //currentcontext = whatever player clicked on 
                playerChoice = true;
                //State.getGameTimer("game").stopTimer();

            }

            if (!responseList.ElementAt(0).consequence.Equals("")) {
                currentMilestones.Add(responseList.ElementAt(0).consequence);
            }


            currentTone = ui_man.getPlayerDialogues()[0].getTone();

            Console.WriteLine("THE TONE I DRAGGED IN WAS: " + currentTone);
        }
        #endregion



        public void loadDialogues() {

            if (currentTone != tone.Root) {

                responseList = s.ChooseDialog(FNC, Load.playerDialogueObj1, currentMadeMemories, currentMilestones, currentTone, currentContext); //loads "what are you staring at?"
                responseListAlex = s.ChooseDialog(D_Man.getAlexFNC(), Load.alexDialogueObj1, currentMadeMemories, currentMilestones, currentTone, currentContext);//loads "yeah I missed you too?"
                ui_man.dialogueLoadOrder(State, playerDialogueBox, dialogueBox, responseList, responseListAlex, playerChoice);
                loadedAIDialogueOnce = true;

                //give this responselist to playerdialogue box
                Console.WriteLine("Affected Dialogue: " + responseList.ElementAt(0).content);
                //dialogueBox.loadNewDialogue("player", responseList.ElementAt(0).content);
                updateCurrents();
                responseList = s.ChooseDialog(FNC, Load.playerDialogueObj1, currentMadeMemories, currentMilestones, tone.Root, currentContext); //loads "how is school?"
                //give this response list to root player uitextbox.

                ui_man.reset(responseList);

            } else {
                //responseList = s.ChooseDialog(FNC, Load.playerDialogueObj1, currentMadeMemories, currentMilestones, currentTone, currentContext);
                //ui_man.reset(responseList);
                State.getGameTimer("game").resetTimer();
                State.getGameTimer("game").startTimer();
            }
        }

        protected override void Initialize() {

            toneBar = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/tonebar.png"));

            toneBar.Position = new Vector2f(6, 794);

            textBackground = new RectangleShape(new Vector2f(SCREEN_WIDTH, SCREEN_HEIGHT / 5));
            textBackground.Position = new Vector2f(0, SCREEN_HEIGHT - (float)(SCREEN_HEIGHT * 0.19));
            textBackground.FillColor = new Color(67, 65, 69);
            textBackground.OutlineColor = Color.White;
            textBackground.OutlineThickness = 2;

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


            mom = new Mom();
            mom.setSpriteEmotion(Character.spriteEmotion.happy);
            mom.active(true);
            mom.state.setMood(5f);
            Console.WriteLine(mom.state.getMood());

            alexis = new Alex();
            alexis.setSpriteEmotion(Character.spriteEmotion.angry);
            alexis.active(true);

            dad = new Dad();
            dad.setSpriteEmotion(Character.spriteEmotion.happy);
            dad.active(true);

        




            dialogueBox = new DialogueBox(0, 0, 710, 150, State, "AI");
            playerDialogueBox = new DialogueBox(0, 0, 710, 150, State, "PLAYER");

            buttons = ui_man.getButtons();
            menus.Add(startMenu); menus.Add(settingsMenu); menus.Add(pauseMenu);
        }

        private void LoadInitialPreReqs() {

            currentMadeMemories.Add("");

            currentMilestones.Add("");


            currentContext = "AlexTalksPlayer";

            FNC = 0;
        }




 

        protected override void Update() {
            screenHelper();
            if (State.GetState() == "game") {
                if (startOnce) {
                    State.getGameTimer("game").startTimer();
                    startOnce = false;
                }

                if (playerChoice && State.getGameTimer("game").getStart()) {
                    State.getGameTimer("game").stopTimer();
                } //jank fix

                // Update the game timerz
                State.updateTimerz();

                // Get the current UI Textboxes from the UI Manager
                var playerDialogues = ui_man.getPlayerDialogues();

                // Get the mouse coordinates from Input Manager
                var MouseCoord = ManagerOfInput.GetMousePos();

                // If the mouse is currently dragging
                if (ManagerOfInput.GetMouseDown()) {

                    // Loop through buttons
                    for (var i = 0; i < buttons.Count; i++) {
                        // Find button currently being interacted with
                        if (buttons[i].GetSelected()) {
                            // Move the button around the screen
                            buttons[i].translate(MouseCoord[0], MouseCoord[1], window.Size.X, window.Size.Y);

                            // Check collision with UI Textboxes
                            // Loop through UI Textboxes
                            for (var j = 0; j < playerDialogues.Count; j++) {
                                // If the mouse just came from inside a UI Textbox
                                if (playerDialogues[j].wasMouseIn()) {
                                    if (!playerDialogues[j].Contains((int)(MouseCoord[0]*scaleFactorX), (int)(MouseCoord[1]*scaleFactorY))) {
                                        // Mouse has now left the UI Textbox so set it to false
                                        playerDialogues[j].setMouseWasIn(false);
                                        // Reset the color to match its previous color
                                        playerDialogues[j].setBoxColor(playerDialogues[j].getBoxColor("prev"));
                                        // Update the rest of the buttons in the cluster
                                        ui_man.updateClusterColors(playerDialogues[j], playerDialogues, playerDialogues[j].getBoxColor("prev"), false);

                                    }

                                    // If mouse just came from outside the UI Textbox
                                } else {
                                    if (playerDialogues[j].Contains((int)(MouseCoord[0] * scaleFactorX), (int)(MouseCoord[1] * scaleFactorY))) {
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
                if (playerDialogueBox.getAnimationStart() == false && loadedAIDialogueOnce == true /*&& playerChoice == false*/) {
                    if (responseListAlex[0].content != "returned empty string") {
                        dialogueBox.setInit(true);
                        dialogueBox.loadNewDialogue("alex", responseListAlex.ElementAt(0).content); //this makes the timer happen after the animation is done
                        loadedAIDialogueOnce = false;
                        if (dialogueBox.getAnimationStart() == false) {
                            loadedAIDialogueOnce = true;
                        }
                    }

                }

            } else if (State.GetState() == "pause") {
                State.getGameTimer("game").PauseTimer();

            }


        }
        //Ensures that AI dialogue doesnt get loaded more than once per timer done
        bool loadedAIDialogueOnce = false;

        bool playerChoice = false;

        protected override void Draw() {

            window.Clear(clearColor);
            if (State.GetState() != "menu") {
                
                window.Draw(mom);
                window.Draw(alexis);
                window.Draw(dad);

            }

            window.Draw(playerDialogueBox);
            window.Draw(dialogueBox);


            window.SetView(fullScreenView);
            if (State.GetState() == "menu") {
                if (State.GetMenuState() == "start") {
                    window.Draw(startMenu);
                } else {
                    window.Draw(settingsMenu);
                }
            } else {

                //Draw text box background box
                window.Draw(textBackground);

                var dialogues = ui_man.getPlayerDialogues();

                for (var i = 0; i < dialogues.Count; i++) {
                    window.Draw(dialogues[i]);
                }

                window.Draw(toneBar);
                var buttons = ui_man.getButtons();

                for (var i = 0; i < buttons.Count; i++) {
                    window.Draw(buttons[i]);
                }
                if (playerChoice) {
                    window.Draw(D_Man);
                }


                if (State.GetState() == "pause") {

                    pauseMenu.DrawBG(window);
                    if (State.GetMenuState() == "pause") {
                        window.Draw(pauseMenu);
                    } else if (State.GetMenuState() == "settings") {
                        window.Draw(settingsMenu);

                    }

                }
                window.Draw(State.getGameTimer("game")); //this is the timer circle
            }


        }


    }
}
