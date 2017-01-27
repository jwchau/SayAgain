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
                dialogueBox.getNext();
            }
            if (e.Code == Keyboard.Key.M)
            {
                dialogueBox.setElementIndex(0);
                dialogueBox.renderDialogue("I took my love, I took it down "+
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
                    "And I'm getting older, too", "Alex");
            }



            if (e.Code == Keyboard.Key.Space)
            {
                dialogueBox.printTime = 0;
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
            scrollview.Viewport = new FloatRect(0.15f, 0.03f, 0.7f, 0.28f)/*(0.1f, 0.05f, 0.8f, 0.3f)*/;

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
                window.Draw(dialogueBox.dialogue);
                window.Draw(dialogueBox.name);
            }

        }
    }
}
