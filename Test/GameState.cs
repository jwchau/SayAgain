using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Drawing;
namespace Test {
    class GameState {
        public GameState() {
            currentState = "menu";
            currentMenuState = "start";
            sound_man.playMusic("Mom");
            playerDialogueBox = new DialogueBox(this, "PLAYER");
            dialogueBox = new DialogueBox(this, "AI");
            playerDialogueBox.animationStart = true;
            playerDialogueBox.init = true;
        }
        string currentState;
        string currentMenuState;
        //Sound Manager
        public SoundManager sound_man = new SoundManager();
        Dictionary<string, GameTimer> DictGameTimer = new Dictionary<string, GameTimer>();
        //Jill's fields and variables
        public DialogueBox dialogueBox;
        public DialogueBox playerDialogueBox;
        public string dialogueIndex;
        public bool interjection = false;
        public bool skip = false;
        int counter = 0;
        static List<DialogueObj> stateResponseListExpo;
        static List<DialogueObj> stateResponseListNPCExpo;

        public void advanceConversation(string speaker, List<DialogueObj> responseList, List<DialogueObj> responseListNPC) {
            if (currentState == "tutorial") {
                if (dialogueIndex == null) {

                    playerDialogueBox.loadNewDialogue("player", stateResponseListExpo[0].content);
                    dialogueIndex = "player";

                } else if (dialogueIndex == "AI") {

                    Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! STEP: ENTERING AI ADVANCE CONVERSATION");


                    if (playerDialogueBox.active && !dialogueBox.active) {
                        Console.WriteLine("~~~~~~~~~~~~~~~~~ STEP: AI INDEX SOMETHING I I WEHAT DIS");
                        if (playerDialogueBox.printTime != 0 && playerDialogueBox.getAnimationStart() && !playerDialogueBox.getAwaitInput()) {
                            playerDialogueBox.setPrintTime(0);
                        } else {
                            if (playerDialogueBox.checkNext()) {
                                playerDialogueBox.init = false;
                                playerDialogueBox.active = false;
                                dialogueBox.init = true;
                                dialogueBox.active = true;
                                Console.WriteLine("~~~~~~~~~~~~~~~~~ STEP: AI INDEX OF ADVANCE CONVERSATION");
                                dialogueBox.loadNewDialogue(stateResponseListNPCExpo[0].speaker, stateResponseListNPCExpo[0].content);
                                Console.WriteLine("~~~~~~~~~~~~~~~~~ STEP: PLAYER BOX ACTIVE I JUST LOADED THE NPC DIALOGUE");
                                dialogueIndex = "root";
                            }
                        }
                    } else if (dialogueBox.active && !playerDialogueBox.active) {
                        Console.WriteLine("~~~~~~~~~~~~~~~~~ STEP: AI INDEX SOMETHING ELSE");
                        if (dialogueBox.printTime != 0 && dialogueBox.getAnimationStart() && !dialogueBox.getAwaitInput()) {
                            dialogueBox.setPrintTime(0);
                        } else {
                            if (dialogueBox.checkNext()) {
                                if (stateResponseListNPCExpo[0].inext == "") {
                                    dialogueIndex = "player";
                                    
                                    dialogueBox.loadNewDialogue(stateResponseListNPCExpo[0].speaker, stateResponseListNPCExpo[0].content);
                                    Console.WriteLine("~~~~~~~~~~~~~~~~~ STEP: DIALOGUE BOX ACTIVE I JUST LOADED THE NPC DIALOGUE");
                                } else if (stateResponseListNPCExpo[0].inext == "player") {
                                    dialogueIndex = "root";
                                    Console.WriteLine(" @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ AI INDEX RESPONSE LIST CONTENT: " + stateResponseListExpo[0].content);
                                    playerDialogueBox.init = true;
                                    playerDialogueBox.active = true;
                                    dialogueBox.init = false;
                                    dialogueBox.active = false;
                                    playerDialogueBox.loadNewDialogue("player", stateResponseListExpo[0].content);
                                }
                            }
                        }
                    }
                } else if (dialogueIndex == "root") {
                    Console.WriteLine("=~=~=~=~=~=~=~=~= STEP: ROOT INDEX OF ADVANCE CONVERSATION ~=~==~=~=~=~=~=");
                    dialogueBox.init = false;
                    playerDialogueBox.init = true;
                    dialogueIndex = "player";

                } else if (dialogueIndex == "player") {
                    dialogueBox.init = false;
                    dialogueBox.active = false;
                    Console.WriteLine("~~~~~~~~~~~~~~~~~ STEP: PLAYER INDEX OF ADVANCE CONVERSATION");
                    Console.WriteLine("~~~~~~~~~~~~~~~~~ STEP: PLAYER INDEX ADVANCE CONVERSATION CONTENT IS: " + stateResponseListExpo[0].content);
                    Console.WriteLine("~~~~~~~~~~~~~~~~~ STEP: PLAYER INDEX ADVANCE CONVERSATION ANIMATION START IS: "+playerDialogueBox.animationStart);
                    Console.WriteLine("~~~~~~~~~~~~~~~~~ STEP: PLAYER INDEX ADVANCE CONVERSATION AWAIT INPUT IS: " + playerDialogueBox.getAwaitInput());
                    Console.WriteLine("~~~~~~~~~~~~~~~~~ STEP: PLAYER INDEX ADVANCE CONVERSATION NEXT IS: " + stateResponseListExpo[0].next);

                    if (playerDialogueBox.active && !dialogueBox.active) {
                        if (playerDialogueBox.printTime != 0 && playerDialogueBox.getAnimationStart() && !playerDialogueBox.getAwaitInput()) {
                            playerDialogueBox.setPrintTime(0);
                        } else {
                            if (playerDialogueBox.checkNext()) {
                                Console.WriteLine("adv convo player: " + stateResponseListExpo[0].content);
                                if (stateResponseListExpo[0].next == "") {
                                    if (stateResponseListExpo[0].skip == "true") {
                                        dialogueIndex = "root";
                                    } else {
                                        // dialogueIndex = "AI";
                                        playerDialogueBox.loadNewDialogue("player", stateResponseListExpo[0].content);

                                    }
                                } else {
                                    Console.WriteLine("~~~~~~~~~~~~~~~~~ STEP: PLAYER INDEX ADVANCE CONVERSATION NPC CONTENT IS: " + stateResponseListNPCExpo[0].content);
                                    dialogueBox.init = true;
                                    playerDialogueBox.active = false;
                                    playerDialogueBox.init = false;
                                    dialogueBox.loadNewDialogue(stateResponseListNPCExpo[0].speaker, stateResponseListNPCExpo[0].content);
                                    Console.WriteLine("~~~~~~~~~~~~~~~~~ STEP: AI JUST LOADED THE NPC DIALOGUE");
                                    dialogueIndex = "AI";
                                }

                            }
                        }
                    }
                } else if (dialogueIndex == "interject") {

                }




            } else if (currentState == "game") {
                Console.WriteLine("DI: " + dialogueIndex + ", speaker: " + speaker + ", content: " + (responseList != null ? responseList[0].content : ""));
                //counter++;
                if (dialogueIndex == null) {
                    // Inital state of conversation. Load dad inital text and "increment" index
                    //dialogueBox.loadNewDialogue("dad", "Hey! It's great having you back home.");
                    //dialogueIndex = "AI";
                } else if (dialogueIndex == "AI") {
                    playerDialogueBox.init = false;
                    playerDialogueBox.active = false;
                    dialogueBox.active = true;
                    if (dialogueBox.active && !playerDialogueBox.active) {
                        if (dialogueBox.printTime != 0 && dialogueBox.getAnimationStart() && !dialogueBox.getAwaitInput()) {
                            dialogueBox.setPrintTime(0);
                        } else if (dialogueBox.checkNext()) {
                            dialogueIndex = "root";
                            dialogueBox.active = false;
                            playerDialogueBox.active = false;
                        }
                    }
                } else if (dialogueIndex == "root") {
                    //Console.WriteLine("I'M SHOWING SHIT");
                    dialogueBox.init = false;
                    playerDialogueBox.init = true;
                    if (skip) {
                        dialogueBox.active = false;
                        playerDialogueBox.loadNewDialogue("player", responseList[0].content);
                        //skip = false;
                    }
                    dialogueIndex = "player";
                } else if (dialogueIndex == "player") {
                    dialogueBox.init = false;
                    dialogueBox.active = false;
                    if (playerDialogueBox.active && !dialogueBox.active) {
                        if (playerDialogueBox.printTime != 0 && playerDialogueBox.getAnimationStart() && !playerDialogueBox.getAwaitInput()) {
                            playerDialogueBox.setPrintTime(0);
                        } else {
                            if (playerDialogueBox.checkNext()) {
                                playerDialogueBox.active = false;
                                dialogueBox.active = true;
                                dialogueBox.init = true;
                                if (responseListNPC[0].inext == "") {
                                    //if there is no interjector
                                    dialogueIndex = "AI";

                                    dialogueBox.loadNewDialogue(speaker, responseListNPC[0].content);
                                } else {

                                    //if there is an interjector
                                    dialogueIndex = "interject";
                                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~ SET INTERJECT");
                                    dialogueBox.loadNewDialogue(speaker, responseListNPC[0].content);
                                }
                                // Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~ THE CONTENT IS ")
                            }
                        }
                    }
                } else if (dialogueIndex == "interject") {

                    if (dialogueBox.active && !playerDialogueBox.active) {
                        if (dialogueBox.printTime != 0 && dialogueBox.getAnimationStart() && !dialogueBox.getAwaitInput()) {
                            dialogueBox.setPrintTime(0);
                        } else {
                            if (dialogueBox.checkNext()) {

                                dialogueBox.loadNewDialogue(speaker, responseListNPC[0].content);
                                //if (dialogueBox.checkNext()) {
                                //Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~ ENTERED INTERJECT");
                                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~ INTERJECT CONTENT IS: " + responseListNPC[0].content);

                                if (responseListNPC[0].inext == "") {
                                    dialogueIndex = "AI";
                                    //dialogueBox.active = false;
                                    //playerDialogueBox.active = false;
                                }
                                Console.WriteLine(responseListNPC[0].inext);
                                Console.WriteLine(dialogueBox.getPrintShit());
                                //}
                            }
                        }
                    }

                }
            }
        }

        public void setResponseList(string tag, List<DialogueObj> rl) {
            if (tag == "NPC") {
                stateResponseListNPCExpo = rl;
            } else if (tag == "player") {
                stateResponseListExpo = rl;
            }
        }
        //Timer for keeping track of time given to the player
        public GameTimer getGameTimer(string tag) {
            return DictGameTimer[tag];
        }
        public void addTimer(string name, double initTime, Action T) {
            if (DictGameTimer.ContainsKey(name)) {
                DictGameTimer[name] = new GameTimer(name, initTime, T);
            } else {
                DictGameTimer.Add(name, new GameTimer(name, initTime, T));
            }
        }
        public string GetState() {
            return currentState;
        }
        public void SetState(string state) {
            if (state != "menu" && state != "game" && state != "pause" && state != "tutorial") {
                throw new FormatException();
            }
            currentState = state;
            if (currentState == "tutorial" && currentMenuState == "start") {

                advanceConversation("", null, null);
            }
            if (currentState == "game") {
                throw new FormatException();
            }
            
        }
        public string GetMenuState() {
            return currentMenuState;
        }
        public void SetMenuState(string state) {
            if (state != "start" && state != "settings" && state != "pause") {
                throw new FormatException();
            }
            currentMenuState = state;
        }
        public void updateTimerz() {
            foreach (var pair in DictGameTimer) {
                //pair.Value is a gameTimer
                //pair.Key is the label of the game Timer;
                if (pair.Value.getStart()) {
                    pair.Value.updateTimer();
                } else {
                    if (pair.Value.getCountDown() == 0) {
                        //DO STUFF BEFORE RESTARTING
                        //Process Player dialogue
                        if (pair.Value != null) {
                            pair.Value.doTask();
                        }
                    }
                }
            }
        }
        public void stopTimerz(string key) {
            DictGameTimer[key].stopTimer();
        }
        public void startTimer(string key) {
            DictGameTimer[key].startTimer();
        }
        public void resetTimer(string key) {
            DictGameTimer[key].resetTimer();
        }
        // Handle Menu Traversal and Game Launching
        public void updateMenuState(int[] mouseCoords, List<MenuButton> buttons, List<Tuple<string, string, Task>> mappings) {
            // Loop through current menu's buttons
            for (var i = 0; i < buttons.Count; i++) {
                // If mouse position is over current button
                if (buttons[i].Contains(mouseCoords[0], mouseCoords[1])) {
                    // Find what this button is suppose to do
                    for (var j = 0; j < mappings.Count; j++) {
                        // Found button being clicked
                        if (buttons[i].getMenuButtonContent() == mappings[j].Item1) {
                            // Do button action
                            mappings[j].Item3.Start();
                            // Change either game state or menu state based off of button's target state
                            if (mappings[j].Item2 == "tutorial") {
                                SetState(mappings[j].Item2);
                                //DictGameTimer["game"].startTimer();
                            } else if (mappings[j].Item2 == "menu") {
                                SetState(mappings[j].Item2);
                                SetMenuState("start");
                            } else {
                                SetMenuState(mappings[j].Item2);
                            }
                            break;
                        }
                    }
                    break;
                }
            }
        }
        public void TogglePause() {
            
            //if (GetState() == "pause") {
            //    SetState("game");
            //    SetMenuState("start");
            //} else if (GetState() == "game") {
            //    SetState("pause");
            //    SetMenuState("pause");
            //}
        }
    }
}