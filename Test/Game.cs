using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SFML.Window;
using SFML.Graphics;


namespace Test
{
    abstract class Game
    {
        protected static RenderWindow window;
        protected Color clearColor;

        //////////////////////////////////////////////////////////////////////////////////////////////
        //Screen defaults
        static protected UInt32 SCREEN_WIDTH = VideoMode.DesktopMode.Width;
        static protected UInt32 SCREEN_HEIGHT = VideoMode.DesktopMode.Height;
        //Menus
        protected StartMenu startMenu;// = new StartMenu("start");
        protected StartMenu settingsMenu;// = new StartMenu("settings");
        protected StartMenu pauseMenu;// = new StartMenu("pause");
        //Input Manager
        protected InputManager ManagerOfInput = new InputManager();
        //User Inferface Manager
        protected UIManager ui_man = new UIManager();
        //Matrices
        protected ToneEffects tfx = new ToneEffects();
        protected ContextFilter cf;
        protected Relationships rs = new Relationships();
        /////////////////////////////////////////////////////////////////////////////////////////////

        protected GameState State = new GameState();

        public Game(uint width, uint height, string title, Color clearColor)
        {
            window = new RenderWindow(new VideoMode(width, height), title, Styles.Default);
            this.clearColor = clearColor;

            // Set-up Events
            window.Closed += onClosed;
            window.KeyPressed += onKeyPressed;
            window.Resized += onResize;
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

        private void onResize(object sender, EventArgs e) {
            
        }

        private void onClosed(object sender, EventArgs e)
        {
            window.Close();
        }

        private void onKeyPressed(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.Code);
            if (e.Code.Equals(Keyboard.Key.Escape)) {
                window.Close();
            }
        }
    }
}
