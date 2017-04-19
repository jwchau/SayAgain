﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

/*
       "content": "plot point with fnc 0. Root tone.",
      "tone": "Root",
      "id": "4",
      "FNC": "-99999",
      "bucket": "4"
        "content": "plot point with fnc 0. Root tone.",
      "tone": "Root",
      "plotpoint": "GreetDad",
      "id": "1",
      "FNC": "0",
      "finished": "fin"

*/


namespace Test {
    public class DialogueParsing {
        public RootObject r = new RootObject();
        private string filename = "";
        private void MakeParse() {
            string json_file = System.IO.File.ReadAllText(filename);
            r = JsonConvert.DeserializeObject<RootObject>(json_file);
        }

        RootObject GetObject() { return r; }
        public DialogueParsing(string file) {
            filename = file;
            MakeParse();
        }
        public DialogueParsing() { }
        ~DialogueParsing() { }
    }

    public class DialogueObj {
        public string content;
        public string tone;
        public string plotpoint;
        public string id;
        public string next;
        public double FNC;
        public double bucket;
        public string speaker;
        public string finished;

        public DialogueObj(string newContent, string newTonalPreReq, string id, string next) { //for linked list reading
            content = newContent; tone = newTonalPreReq; this.id = id; this.next = next; this.FNC = 2 ^ 16;
        }

        public DialogueObj(string content, string tone, string plotpoint, string id, string FNC, string finished, string speaker) { //for plot point lines
            this.content = content; this.tone = tone; this.plotpoint = plotpoint; this.id = id; this.FNC = double.Parse(FNC); this.finished = finished; this.speaker = speaker;
        }

        public DialogueObj(string c, string t, string id, string f, string b, string s){ //for transitino lines
            this.content = c; this.tone = t; this.id = id; this.FNC = double.Parse(f); this.bucket = double.Parse(b); this.speaker = s;
        }

        public DialogueObj() {
            content = "returned empty string";
            tone = "";
            plotpoint = "";
            id = "";
            next = "";
            finished = "";
            speaker = "";
            bucket = -(2 ^ 16);
            FNC = 2 ^ 16;
        }
        ~DialogueObj() { }
    }

    public class RootObject {
        public List<DialogueObj> Dialogues { get; set; }
    }

}
