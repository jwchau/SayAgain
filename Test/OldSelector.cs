using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class OldSelector
    {
<<<<<<< HEAD
        public List<DialogueObj> ChooseDialog(int FNC, DialogueParsing r, string[] memories)
        {
            bool memoryCheck;
            int counter;
            //checks for memory requirements first (ones with no memorys are also added)
            List<DialogueObj> possibleChoices = new List<DialogueObj>();

            //iterates through the json list
            for (int i = 0; i < r.r.Dialogues.Count; i++)
            {
                memoryCheck = false;
                counter = 0;
                if (r.r.Dialogues.ElementAt(i).memory.Count == 0)
                {
                    memoryCheck = true;
                }
                else
                {
                    //iterates through any memorys from json element
                    for (int a = 0; a < r.r.Dialogues.ElementAt(i).memory.Count; a++)
                    {
                        //iterates through currentMade memories
                        for (int e = 0; e < memories.Length; e++)
                        {
                            if (r.r.Dialogues.ElementAt(i).memory[a].CompareTo(memories[e]) == 0)
                            {
                                counter++;
                            }
                        }
                    }
                    //check to see if require memorys are there
                    if (counter == r.r.Dialogues.ElementAt(i).memory.Count)
                    {
                        memoryCheck = true;
                    }
                }
                //if present, add to list
                if (memoryCheck)
                {
                    possibleChoices.Add(new DialogueObj(r.r.Dialogues.ElementAt(i).content,
                        r.r.Dialogues.ElementAt(i).memory, r.r.Dialogues.ElementAt(i).FNC,
                        r.r.Dialogues.ElementAt(i).speaker, r.r.Dialogues.ElementAt(i).target));
                }
            }

            //checks for FNC requirements for whats left
            for (int i = 0; i < possibleChoices.Count; i++)
            {
                if (FNC == 0)
                {
                    if (possibleChoices.ElementAt(i).FNC != 0)
                    {
                        possibleChoices.Remove(possibleChoices.ElementAt(i));
                        i--;
                    }
                }
                if (FNC > 0)
                {
                    if (possibleChoices.ElementAt(i).FNC > FNC || possibleChoices.ElementAt(i).FNC < 0)
                    {
                        possibleChoices.Remove(possibleChoices.ElementAt(i));
                        i--;
                    }
                }
                if (FNC < 0)
                {

                    if (possibleChoices.ElementAt(i).FNC < FNC || possibleChoices.ElementAt(i).FNC > 0)
                    {
                        possibleChoices.Remove(possibleChoices.ElementAt(i));
                        i--;
                    }
=======
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
>>>>>>> 24292412928b907bdb0e2cd81f7a16bf1fc4e303
                }
            }

            //sends results or returns empty value
<<<<<<< HEAD
            if(possibleChoices.Count == 0)
            {
                possibleChoices.Add(new DialogueObj());
                possibleChoices.ElementAt(0).content = "returned empty string";
            }

            return possibleChoices;
=======
            if(responseList.Count == 0)
            {
                responseList.Add(new DialogueObj());
            }

            return responseList;
>>>>>>> 24292412928b907bdb0e2cd81f7a16bf1fc4e303
        }
        public OldSelector() { }
        ~OldSelector() { }
    }
}
