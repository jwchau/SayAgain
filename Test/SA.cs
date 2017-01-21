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
        Text dialogue;
        Text name;
        RectangleShape dialogueBox;
        RectangleShape nameBox;
        Boolean started = false;
        int printTime;


        public SA() : base(800, 600, "Say Again?", Color.Magenta)
        {
            window.KeyPressed += onKeyPressed;
            window.KeyReleased += onKeyReleased;
        }

        private void onKeyReleased(object sender, KeyEventArgs e)
        {
            keys[e.Code] = new bool[] { false, e.Shift, e.Control, e.Alt };
        }

        private void onKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.M)
            {
                
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
           
            Console.WriteLine("LoadContent");

        }

        protected override void Initialize()
        {
            name = new Text("Alex", FontObjects.Adore64, 24);
            name.Position = new Vector2f(90, 15);
            name.Color = Color.Black;
            nameBox = new RectangleShape(new Vector2f(150, 50));
            nameBox.OutlineThickness = 3;
            nameBox.OutlineColor = Color.Black;
            nameBox.Position = new Vector2f(90, 15);
            dialogueBox = new RectangleShape(new Vector2f(700, 150));
            dialogueBox.Position = new Vector2f(50, 50);
            dialogueBox.OutlineThickness = 3;
            dialogueBox.OutlineColor = Color.Black; 
            Console.WriteLine("Initialize");
            string line = "say again by team babble fish";

            Task.Run(async () => { //Task.Run puts on separate thread
                printTime = 100;
                await animateText(line); //await pauses thread until animateText() is completed
            });
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
                dialogue.Position = new Vector2f(50, 100);
                dialogue.Color = Color.Black;
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

        }

        protected override void Draw()
        {

            window.Draw(dialogueBox);
            window.Draw(dialogue);
            window.Draw(nameBox);
            window.Draw(name);


        }
    }
}
