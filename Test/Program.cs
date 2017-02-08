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

            List<DialogueObj> responseList = s.ChooseDialog(FNC, Load.playerDialogueObj1, currentMadeMemories);

            Console.WriteLine("First String in List -> " + responseList.ElementAt(0).content);

            SA myGame = new SA();
            myGame.Run();

        }
    }
}
