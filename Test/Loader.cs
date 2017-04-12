using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Loader
    {
        //Creates and loads Json Objects
<<<<<<< HEAD
        public DialogueParsing playerDialogueObj1 = new DialogueParsing(@"../../playerTutorial.json");
        public DialogueParsing alexDialogueObj1 = new DialogueParsing(@"../../alexTutorial.json");
=======
        public DialogueParsing playerDialogueObj1 = new DialogueParsing(@"../../player.json");
        public DialogueParsing NPCDialogueObj = new DialogueParsing(@"../../dad.json");
>>>>>>> 24292412928b907bdb0e2cd81f7a16bf1fc4e303

        //Creates and loads music and sound files


        //Creates and loads art and sprites


        public Loader() { }
        ~Loader() { }
    }
}
