using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class OldSelector
    {
        public List<DialogueObj> ChooseDialog(DialogueParsing r, string now, string currTone)
        {
            List<DialogueObj> responseList = new List<DialogueObj>();
            for(int i = 0; i < r.r.Dialogues.Count; i++)
            {
                var curr = r.r.Dialogues[i];
                if (curr.id == now && currTone == curr.tone)
                {
                    //Console.WriteLine("found 1");
                    responseList.Add(curr);
                    return responseList;
                }
            }

            //sends results or returns empty value
            if(responseList.Count == 0)
            {
                responseList.Add(new DialogueObj());
            }

            return responseList;
        }

        public List<DialogueObj> ChooseDialog2(DialogueParsing r,string currNode, int NPCFNC)
        {
            List<DialogueObj> responseList = new List<DialogueObj>();
            var best = new DialogueObj();
            for (int i = 0; i < r.r.Dialogues.Count; i++)
            {
                if(Math.Abs(r.r.Dialogues.ElementAt(i).FNC - NPCFNC) < best.FNC)
                {
                    best = r.r.Dialogues.ElementAt(i);
                }
            }

            //sends results or returns empty value
            if (responseList.Count == 0)
            {
                responseList.Add(new DialogueObj());
                return responseList;
            }

            responseList.Add(best);
            return responseList;
        }
        public OldSelector() { }
        ~OldSelector() { }
    }
}
