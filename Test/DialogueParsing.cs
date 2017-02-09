using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Test
{
    public class DialogueParsing
    {
        public RootObject r = new RootObject();
        private string filename = "";
        private void MakeParse()
        {
            string json_file = System.IO.File.ReadAllText(filename);
            r = JsonConvert.DeserializeObject<RootObject>(json_file);
        }

        RootObject GetObject() { return r; }
        public DialogueParsing(string file)
        {
            filename = file;
            MakeParse();
        }
        public DialogueParsing() { }
        ~DialogueParsing() { }
    }

    public class DialogueObj
    {
        public string content { get; set; }
        public List<string> memory { get; set; }
        public List<string> context { get; set; }
        public List<string> milestone { get; set; }
        public int FNC { get; set; }
        public string speaker { get; set; }
        public List<int> target { get; set; }

        public DialogueObj(string newContent, List<string> newMemory, List<string> newContext, List<string> newMilestone, int newFNC, string newSpeaker, List<int> newTarget)
        {
            content = newContent; memory = newMemory; context = newContext; milestone = newMilestone; FNC = newFNC; speaker = newSpeaker; target = newTarget;
        }

        public DialogueObj() { }
        ~DialogueObj() { }
    }

    public class RootObject
    {
        public List<DialogueObj> Dialogues { get; set; }
    }

}
