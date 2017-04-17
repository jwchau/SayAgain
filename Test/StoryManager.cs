using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//greetdad, settype PP, 
namespace Test
{
    class StoryManager
    {

        protected string dialogueType;
        public enum type { plotpoint, transition };
        protected List<String> nextPreconditions;
        protected string currentNode;
        protected int numberOfChildren;

        static Dictionary<String, Tuple<List<String>, List<String>>> plot_dict
            = new Dictionary < String, Tuple<List<String>, List<String>>>();
        List<String> next_nodes = new List<String>();
        List<String> preconditions = new List<String>();

        void addNode(String s, List<String> n, List<String> p)
        {

            Tuple<List<String>, List<String>> value =
                new Tuple<List<String>, List<String>>(n, p);
            plot_dict.Add(s, value);
            clear();

        }
        
        public string getCurrentNode()
        {
            return currentNode;
        }

        void clear()
        {
            next_nodes = new List<String>();
            preconditions = new List<String>();
        }


        
        public string getDialogueType()
        {
            return dialogueType;
        }

        public void setDialogueType(type t)
        {
            dialogueType = t.ToString();
        }

        public void setTypePlotNode()
        {
            dialogueType = type.transition.ToString();
        }

        public void setTypeTransition()
        {
            dialogueType = "transition";
        }

        public void findNextPossibleNodes()
        {
            numberOfChildren = 0;
            Console.WriteLine("Current node: " + currentNode);
            Console.WriteLine("Possible next nodes: ");
            if (plot_dict[currentNode].Item1 != null)
            {
                //the string name of each child node
                foreach (var n in plot_dict[currentNode].Item1)
                {
                    numberOfChildren += 1;
                    Console.WriteLine("- " + n);
                    if (plot_dict[n].Item2 != null)
                    {   
                        foreach (var c in plot_dict[n].Item2)
                        {
                            Console.WriteLine(">>> With precondition: ");
                            Console.WriteLine(">>> " + c);

                            nextPreconditions.Add(c);

                        }
                    }
                }
            }
            checkIfPreconSatisfied();
        }

        public void checkIfPreconSatisfied()
        {
            int childSatisfied = -1;//track which child of currentNode has satisfied preconditions
            bool satisfied = false;//true whenever a child is satisfied. ASSUMES that children dont have overlapping preconditions
            foreach (var p in nextPreconditions)
            {
                //if p has a ' in it (multiple preconditions)
                //then separate the two conitions, parse for whether FNC or plot point requirement
                //Console.WriteLine(p);
                if (p.Contains(","))
                {
                    childSatisfied += 1;
                    string tmp = p.Replace(" ", String.Empty);
                    var array = tmp.Split(',')[1];
                    //for each thing in array satisfied, satisfied = true, else false and break
                }

            }
        }
        public StoryManager() {


            nextPreconditions = new List<String> ();
            
            currentNode = "MomTellsPlayerTalkToAlex";
            setDialogueType(type.plotpoint);


            //TODO: all blow up nodes reachable from any point

            next_nodes.Add("MomTellsPlayerTalkToAlex");
            next_nodes.Add("MomAdmitsJob");
            addNode("GreetMom", next_nodes, preconditions);

            next_nodes.Add("MomReconcilesDad");
            next_nodes.Add("MomBlowsUp");
            next_nodes.Add("GreetDad");
            next_nodes.Add("GreetAlex");
            preconditions.Add("Mom: LF-MF");
            addNode("MomTellsPlayerTalkToAlex", next_nodes, preconditions);
            
            preconditions.Add("Mom: MC-HC");
            preconditions.Add("AlexAdmitsNeglect");
            preconditions.Add("DadAccusesMom");
            addNode("MomAdmitsJob", next_nodes, preconditions);


            next_nodes.Add("DadAccusesMom");
            next_nodes.Add("DadBlowsUp");
            addNode("GreetDad", next_nodes, preconditions);

            preconditions.Add("Dad: MC-HC");
            next_nodes.Add("MomBlowsUp");
            next_nodes.Add("DadApologizesMom");
            addNode("DadAccusesMom", next_nodes, preconditions);

            preconditions.Add("Dad: HC");
            preconditions.Add("MomAdmitsJob, Dad: LN-HC");
            next_nodes.Add("DadApologizesAlex");
            addNode("DadApologizesMom", next_nodes, preconditions);

            preconditions.Add("AlexAdmitsNeglect, Dad: LC-HC");
            preconditions.Add("Dad: HC");
            addNode("DadApologizesAlex", next_nodes, preconditions);

            preconditions.Add("DadApologizesMom, Mom: LN-HC");
            next_nodes.Add("AlexAdmitsNeglect");
            addNode("MomReconcilesDad", next_nodes, preconditions);

            next_nodes.Add("AlexAdmitsNeglect");
            next_nodes.Add("GreetMom");
            next_nodes.Add("GreetDad");
            addNode("GreetAlex", next_nodes, preconditions);

            preconditions.Add("Alex: LC-HC");
            next_nodes.Add("AlexReconcilesPlayer");
            addNode("AlexAdmitsNeglect", next_nodes, preconditions);

            preconditions.Add("Alex: LC-HC");
            next_nodes.Add("AlexReconcilesMom");
            addNode("AlexReconcilesPlayer", next_nodes, preconditions);

            
            preconditions.Add("Alex: LC-HC, MomAdmitsJob");
            next_nodes.Add("AlexReconcilesDad");
            addNode("AlexReconcilesMom", next_nodes, preconditions);

            preconditions.Add("Alex: LC-HC, DadApologizesAlex");
            addNode("AlexReconcilesDad", next_nodes, preconditions);

            preconditions.Add("Dad: HF");
            addNode("DadBlowsUp", next_nodes, preconditions);

            preconditions.Add("Alex: HF");
            addNode("AlexBlowsUp", next_nodes, preconditions);

            preconditions.Add("Mom: HF");
            addNode("MomBlowsUp", next_nodes, preconditions);

            findNextPossibleNodes();
        }

    }
}
