using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;


namespace Test
{
    abstract class Game
    {
        protected RenderWindow window;
        protected Color clearColor;
        protected GameState State = new GameState();

        public Game(uint width, uint height, string title, Color clearColor)
        {
            this.window = new RenderWindow(new VideoMode(width, height), title, Styles.Close);
            this.clearColor = clearColor;

            // Set-up Events
            window.Closed += onClosed;
            window.KeyPressed += onKeyPressed;
        }

        public void Run()
        {
            LoadContent();
            Initialize();

            while (window.IsOpen)
            {
                window.DispatchEvents();
                Update();
                
                Draw();
                window.Display();
            }
        }

        protected abstract void LoadContent();
        protected abstract void Initialize();
        protected abstract void Update();
        protected abstract void Draw();

        private void onClosed(object sender, EventArgs e)
        {
            window.Close();
        }

        private void onKeyPressed(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.Code);
        }
    }
}
