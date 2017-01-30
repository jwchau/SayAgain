using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Selector
    {
        public string ChooseDialog(int FNC, DialogParsing r, string[] memories)
        {
            bool memoryCheck;
            int counter;
            //checks for memory requirements first (ones with no memorys are also added)
            Dictionary<int, string> possibleChoices = new Dictionary<int, string>();
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
                    possibleChoices.Add(r.r.Dialogues.ElementAt(i).FNC,
                                  r.r.Dialogues.ElementAt(i).content);
                }
            }

            //checks for FNC requirements for whats left
            for (int i = 0; i < possibleChoices.Count; i++)
            {
                if (FNC == 0)
                {
                    if (possibleChoices.ElementAt(i).Key != 0)
                    {
                        possibleChoices.Remove(possibleChoices.ElementAt(i).Key);
                        i--;
                    }
                }
                if (FNC > 0)
                {
                    if (possibleChoices.ElementAt(i).Key > FNC || possibleChoices.ElementAt(i).Key < 0)
                    {
                        possibleChoices.Remove(possibleChoices.ElementAt(i).Key);
                        i--;
                    }
                }
                if (FNC < 0)
                {

                    if (possibleChoices.ElementAt(i).Key < FNC || possibleChoices.ElementAt(i).Key > 0)
                    {
                        possibleChoices.Remove(possibleChoices.ElementAt(i).Key);
                        i--;
                    }
                }
            }

            //prints values to choose from
            Console.WriteLine("number of possible choices = " + possibleChoices.Count);
            for (int i = 0; i < possibleChoices.Count; i++)
            {
                Console.WriteLine("One response = " + possibleChoices.ElementAt(i).Value);
            }


            //picks whats left (for now it picks at random)
            Random holder = new Random();
            int randomNumber = holder.Next(0, possibleChoices.Count);
            string response = "";

            if (possibleChoices.Count == 1)
            {
                response = possibleChoices.ElementAt(0).Value;
            }
            else
            {
                response = possibleChoices.ElementAt(randomNumber).Value;
            }

            return response;
        }
        public Selector() { }
        ~Selector() { }

        //Debugging printer
        //Console.WriteLine("number of possible choices = " + possibleChoices.Count);
        //for (int i = 0; i < possibleChoices.Count; i++)
        //{
        //    Console.WriteLine("One response = " + possibleChoices.ElementAt(i).Value);
        //}
    }
}
