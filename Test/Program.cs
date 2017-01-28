using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] currentMadeMemories = { "School", "Divorce", "Worship" };
            int FNC = 50;

            DialogParsing r = new DialogParsing(@"C:\Users\leogo_000\Documents\GitHub\SayAgain\Test\sampleJSON.json");
            Selector s = new Selector();

            string response = s.ChooseDialog(FNC, r, currentMadeMemories);

            //Console.WriteLine("response = " + response);

            //Console.WriteLine(r.r.Dialogues[0].memory.ElementAt(1));
            //Console.WriteLine(r.r.Dialogues[1].content);
            //Console.WriteLine(r.r.Dialogues[2].content);

            SA myGame = new SA();
            myGame.Run();
        }
    }
}
