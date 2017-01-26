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
            DialogParsing r = new DialogParsing(@"C:\Users\leogo_000\Documents\GitHub\SayAgain\Test\sampleJSON.json");

            Console.WriteLine(r.r.Dialogues[0].content);
            Console.WriteLine(r.r.Dialogues[1].content);
            Console.WriteLine(r.r.Dialogues[2].content);

            SA myGame = new SA();
            myGame.Run();
        }
    }
}
