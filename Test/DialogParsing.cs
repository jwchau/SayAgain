using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Test
{
    class DialogParsing
    {
        public RootObject r = new RootObject();
        private string filename = "";
        private void MakeParse()
        {
            string json_file = System.IO.File.ReadAllText(filename);
            r = JsonConvert.DeserializeObject<RootObject>(json_file);
        }

        RootObject GetObject() { return r; }
        public DialogParsing(string file)
        {
            filename = file;
            MakeParse();
        }
        public DialogParsing() { }
        ~DialogParsing() { }
    }

    public class DialogueObj
    {
        public string content { get; set; }
        public List<string> memory { get; set; }
        public int FNC { get; set; }
    }

    public class RootObject
    {
        public List<DialogueObj> Dialogues { get; set; }
    }
}
