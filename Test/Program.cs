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

            Loader Load = new Loader();
            Selector s = new Selector();

            List<DialogueObj> responseList = s.ChooseDialog(FNC, Load.sampleDialogueObj, currentMadeMemories);

            Console.WriteLine("First content in List -> " + responseList.ElementAt(0).content);
            Console.WriteLine("First context in List -> " + responseList.ElementAt(0).context[0]);
            Console.WriteLine("First milestone in List -> " + responseList.ElementAt(0).milestone[0]);
            Console.WriteLine("First target 1 in List -> " + responseList.ElementAt(0).target[0]);

            SA myGame = new SA();
            myGame.Run();

        }
    }
}
