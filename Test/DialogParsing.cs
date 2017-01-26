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
            if (filename != "") MakeParse();
        }
        public DialogParsing() { }
    }

    public class DialogueObj
    {
        public string content { get; set; }
        public List<object> memory { get; set; }
        public int FNC { get; set; }
    }

    public class RootObject
    {
        public List<DialogueObj> Dialogues { get; set; }
    }
}
