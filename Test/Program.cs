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
            //Testing Values---------------------------------------------
            string[] currentMadeMemories = { "Greeting","Indifferent" };
            int FNC = 0;
            //-----------------------------------------------------------

            DialogueParsing r = new DialogueParsing(@"C:\Users\leogo_000\Documents\GitHub\SayAgain\Test\playertutorial_json.json");
            Selector s = new Selector();

            List<DialogueObj> responseList = s.ChooseDialog(FNC, r, currentMadeMemories);

            Console.WriteLine("First String in List -> " + responseList.ElementAt(0).content);

            SA myGame = new SA();
            myGame.Run();

        }
    }
}
