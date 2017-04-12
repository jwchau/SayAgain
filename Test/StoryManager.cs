using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class StoryManager
    {
<<<<<<< HEAD
        public List<String> allNodes = new List<String>();
        public List<String> remainingNodes = new List<String>();
        public List<String> eliminatedNodes = new List<String>();
        public List<String> possibleNodes = new List<String>();
=======
 
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

        public void print()
        {
           /* foreach (var kvp in plot_dict)
            {

                Console.WriteLine(kvp.Key);
                if (!kvp.Value.Item1[0].Equals(null))
                {
                    Console.WriteLine(kvp.Value.Item1[0]);
                }
                if (!kvp.Value.Item1[1].Equals(null)) { 
                    Console.WriteLine(kvp.Value.Item1[1]);
                }

          
            }
  */
        }
        
        void clear()
        {
            next_nodes = null;
            next_nodes = new List<String>();
            preconditions = null;
            preconditions = new List<String>();
        }
>>>>>>> 24292412928b907bdb0e2cd81f7a16bf1fc4e303

        public StoryManager()

        {
<<<<<<< HEAD

        }
=======
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
/*

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
            */
        }

>>>>>>> 24292412928b907bdb0e2cd81f7a16bf1fc4e303
    }
}
