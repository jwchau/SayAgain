using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace Test
{
    class SA : Game
    {

        Dictionary<SFML.Window.Keyboard.Key, bool[]> keys = new Dictionary<SFML.Window.Keyboard.Key, bool[]>();

        List<UIButton> buttons = new List<UIButton>();

        UITextBox TextBox = new UITextBox(800, 100, 0, 500, "HELLO WORLD!");
        //UISpeechBox SpeechBox = new UISpeechBox(700, 150, 50, 50, "Say Again by team babble fish", "Alex");

        InputManager ManagerOfInput = new InputManager();

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

            for (var i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].GetSelected())
                {
                    var selectedBounds = buttons[i].getRectBounds();
                    var boxBounds = TextBox.getBoxBounds();
                    //first check top left of textbox
                    if (selectedBounds.Top + selectedBounds.Height >= boxBounds.Top)
                    {
                        Console.WriteLine("YO");
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
            buttons.Add(new UIButton(80, 100, 40, "SUH DUDE","I AM DIALOGUE 1"));
            buttons.Add(new UIButton(40, 20, 80, "woah","I AM DIALOGUE 2"));

        }

        protected override void Initialize()
        {
            
        }



        protected override void Update()
        {
            if (ManagerOfInput.GetMouseDown())
            {
                var MouseCoord = ManagerOfInput.GetMousePos();
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

            for (var i = 0; i < buttons.Count; i++)
            {
                window.Draw(buttons[i].getUIButtonRect());
                window.Draw(buttons[i].getUIButtonText());
            }

        }
    }
}
