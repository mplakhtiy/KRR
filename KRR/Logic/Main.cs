using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KRR.Logic.Rules;

namespace KRR.Logic
{
    internal class Main
    {

        public static Rule Rules;
        public static List<Agent_Action> Queries;
        public static  List<Fluent> Goal;
        //check if states always not null
        public static bool always;
        public static string result="";

        //check if goal condition is always satisfied
        public static bool goalAlways;//for always 
        public static bool goalNever;//for ever

        //number of last executable query
        public static int lastQuery;

        //check if an agent was involved always / ever never
        public static bool alwaysAgent;
        public static bool neverAgent;
        public static Agent agentToCheck;

        public static void TheMostImportantMethod(Agent agent,List<Fluent> _goal,Rule rules, List<Fluent> intializedFluents, List<Fluent> allFluents,
            List<Agent_Action> queries)
        {
            alwaysAgent = true;
            neverAgent = true;
            agentToCheck = agent;

            goalNever = true;
            goalAlways = true;
            Goal = _goal;

            lastQuery = -1;
            always = true;
            result = "";
            Rule.never = null;
            Rule.always = true;
            
            

            List<State> possibleInitialStates = new List<State>();
            List<Fluent> temp = new List<Fluent>();
            foreach (Fluent fluent in allFluents)
            {
                temp.Add(fluent);
            }
                //Create temp list with not setted Fluents
            foreach (Fluent fluent in temp)
            {
                foreach (Fluent intializedFluent in intializedFluents)
                {
                    if (intializedFluent.Name == fluent.Name)
                    {
                        allFluents.Remove(fluent);
                        break;
                    }
                }
            }
            //if (intializedFluents.Count > 0)
            //{
            //allFluents = temp;
            //}



            int n = allFluents.Count;
            //Create all possible combinations
            List<bool[]> matrix = new List<bool[]>();
            double count = Math.Pow(2, n);
            //if all fluents.count == 0 then matrix also should be 0 elemetns but not 2^0=1
            if (n == 0)
            {
                count = 0;
            }
            
            for (int i = 0; i < count; i++)
            {
                string str = Convert.ToString(i, 2).PadLeft(n, '0');
                bool[] boolArr = str.Select((x) => x == '1').ToArray();

                matrix.Add(boolArr);
            }
            //if all fluents were initiallized then just add initially fluents
            if (matrix.Count == 0)
            {
                var a = new State();
                foreach (Fluent intializedFluent in intializedFluents)
                {
                    a.addFluent(intializedFluent);
                }
                possibleInitialStates.Add(a);
            }
            else {
                for (int i = 0; i < matrix.Count; i++)
                {
                    var a = new State();
                    foreach (Fluent intializedFluent in intializedFluents)
                    {
                        a.addFluent(intializedFluent);
                    }

                    //if all fluents size > 
                    for (int j = 0; j < matrix[0].Length; j++)
                    {
                        a.addFluent(new Fluent(allFluents[j].Name, matrix[i][j]));
                    }
                    possibleInitialStates.Add(a);

                }
            }
            foreach (State possibleInitialState in possibleInitialStates)
            {
                Console.WriteLine(possibleInitialState);
            }

            Logic.Main.Queries = queries;
            Logic.Main.Rules = rules;
            doRecursion(possibleInitialStates, 0,"",null);

            //result output
            if (Logic.Main.Queries.Count != lastQuery)
            {
                result += "Program is never executable!!! \n";

                for(int i= 0;i<Logic.Main.Queries.Count;i++)
                {
                    if (i > lastQuery-1)
                    {
                        result+="Q"+(i+1)+" "+ Logic.Main.Queries[i].ToString()+" - not executable \n";
                    }
                }
            }
            else
            {
                if (always)
                {
                    result += "Program is always executable \n";
                }
                else
                {
                    result += "Program is not alsways executable , only sometimes \n";
                }
            }
            if (Goal != null)
            {
                if (Goal.Count > 0)
                {

                    if (goalNever)
                    {
                        result += "Goal is never satisfied! \n";
                    }
                    else
                    {
                        if (!goalAlways)
                        {
                            result += "Goal is sometimes satisfied! \n";
                        }
                        else
                        {
                            result += "Goal is always satisfied \n";
                        }
                    }
                }
            }
            if (agentToCheck.Name != null)
            {

                if (neverAgent)
                {
                    result += " Agent " + agentToCheck.Name + " is never involved to Program! \n";
                }
                else
                {
                    if (!alwaysAgent)
                    {
                        result += " Agent " + agentToCheck.Name + " is sometimes involved to Program! \n";
                    }
                    else
                    {
                        result += " Agent " + agentToCheck.Name + " is always involved to Program! \n";
                    }
                }
            }
            Console.WriteLine(result);
            
        }

        public static void doRecursion(List<State> StateList, int queryNumber, string parent, State parentState)
        {
            // if _if condition does not hold then empty effect
            if (StateList.Count==0 && Rule.agentActionOrList)
            {
                StateList.Add(parentState);
            }

            int node = 0;
            //check for executable always/ever/never
            if (StateList.Count == 0)
            {
                always = false;
                if (queryNumber != 0)
                {
                    
                    if (Queries[queryNumber - 1].agent.Name == agentToCheck.Name)
                    {
                        alwaysAgent = false;
                    }
                }
            }
            else {
                if (queryNumber != 0)
                {
                    if (Queries[queryNumber - 1].agent.Name == agentToCheck.Name)
                    {
                        neverAgent = false;
                    }
                }
                if (lastQuery < queryNumber )
                {
                    lastQuery = queryNumber;
                }
            }
           
            foreach (State state in StateList)
            {
                //check for goal always/ever/never
                if (Goal != null && queryNumber!=0)
                {
                    if (state.checkList(Goal))
                    {
                        goalNever = false;
                    }
                    else
                    {
                        goalAlways = false;
                    }
                }
                string nodeId = parent + "-" + queryNumber + "-";
                // tree.AddTreeDataTableRow(nodeId, parent, Queries[queryNumber].ToString(), "State: "+ state.ToString());

                node++;
                Console.WriteLine(state);
                Console.WriteLine("-------------------------------------");
                if (queryNumber < Logic.Main.Queries.Count)
                {
                    doRecursion(Rules.checkRules(Queries[queryNumber], state), queryNumber + 1, nodeId,state);
                }
            }
        }

        //private static void Main(string[] args)
        //{
        //    //  GetCombination(new List<bool> { true, false });

        //    State initialState = new State();

        //    Fluent alive = new Fluent("Alive", true);
        //    Fluent loaded = new Fluent("Loaded", false);
        //    Fluent walking = new Fluent("Walking", false);


        //    List<Fluent> initializedFluents = new List<Fluent>() { alive };
        //    List<Fluent> allFluents = new List<Fluent>() { alive, loaded, walking };


        //    Agent bob = new Agent("Bob");

        //    KRR.Logic.Action shoot = new KRR.Logic.Action("Shoot");
        //    KRR.Logic.Action load = new KRR.Logic.Action("Load");

        //    Agent_Action bobShoot = new Agent_Action(bob, shoot);
        //    Agent_Action bobLoad = new Agent_Action(bob, load);


        //    Rule rules = new Rule();

        //    //CausesIf casesShoot1 = new CausesIf(bobShoot, new List<Fluent>() { new Fluent("Loaded", false) },
        //    //    new List<List<Fluent>>());
        //    //CausesIf casesShoot2 = new CausesIf(bobShoot,
        //    //    new List<Fluent>() { new Fluent("Alive", false), new Fluent("Walking", false), },
        //    //    new List<List<Fluent>>() { new List<Fluent>() { new Fluent("Loaded", true) } });
        //    ReleasesIf releasesShoot1 = new ReleasesIf(bobShoot,
        //        new List<Fluent>() { new Fluent("Alive", false), new Fluent("Walking", false), new Fluent("Loaded", false) },
        //        new List<List<Fluent>>() { new List<Fluent>() { new Fluent("Loaded", true) } });
        //    ReleasesIf releasesShoot2 = new ReleasesIf(bobShoot,
        //        new List<Fluent>() { new Fluent("Alive", true), new Fluent("Walking", true), new Fluent("Loaded", false) },
        //        new List<List<Fluent>>() { new List<Fluent>() { new Fluent("Loaded", true) } });



        //    CausesIf casesLoad = new CausesIf(bobLoad, new List<Fluent>() { new Fluent("Loaded", true) },
        //        new List<List<Fluent>>());


        //    rules.AddRule(casesLoad);

        //    //rules.AddRule(casesShoot2);
        //    //rules.AddRule(casesShoot1);
        //    rules.AddRule(releasesShoot1);
        //    rules.AddRule(releasesShoot2);



        //    List<Agent_Action> queries = new List<Agent_Action>();
        //    queries.Add(bobLoad);
        //    queries.Add(bobShoot);
        //    queries.Add(bobLoad);
        //    queries.Add(bobShoot);
        //    TheMostImportantMethod(rules, initializedFluents, allFluents, queries);

        //}
    }
}
