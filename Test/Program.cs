using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Data;
using System.ComponentModel;

namespace Test {
    public enum tone {
        Blunt = 0,
        Indifferent = 1,
        Compassionate = 2,
        Hesitant = 4,
        Root = 8,
    }


    class Program {

        protected static SA myGame;

        public static SA getGame() {
            return myGame;
        }

        public void Form1()
        {
            Thread t = new Thread(new ThreadStart(Form1));
            t.Start();
            Thread.Sleep(500);
            t.Abort();
        }

        static void Main(string[] args) {
            //run loading screen
            Application.Run(new Form1());

            Program.myGame = new SA();
            myGame.Run();

        }
    }
}
