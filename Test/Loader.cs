﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Loader
    {
        //Creates and loads Json Objects
        public DialogueParsing playerDialogueObj1 = new DialogueParsing(@"../../player.json");
        public DialogueParsing NPCDialogueObj = new DialogueParsing(@"../../dad.json");

        //Creates and loads music and sound files


        //Creates and loads art and sprites


        public Loader() { }
        ~Loader() { }
    }
}
