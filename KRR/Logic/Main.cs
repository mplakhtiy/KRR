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


        public static void TheMostImportantMethod(Rule rules, List<Fluent> intializedFluents, List<Fluent> allFluents,
            List<Agent_Action> queries)
        {

            List<State> possibleInitialStates = new List<State>();
            List<Fluent> temp = new List<Fluent>();

            //Create temp list with not setted Fluents
            foreach (Fluent fluent in allFluents)
            {
                foreach (Fluent intializedFluent in intializedFluents)
                {
                    if (intializedFluent.Name != fluent.Name)
                    {
                        temp.Add(fluent);
                    }
                }
            }

            allFluents = temp;


            int n = allFluents.Count;
            //Create all possible combinations
            List<bool[]> matrix = new List<bool[]>();
            double count = Math.Pow(2, n);
            for (int i = 0; i < count; i++)
            {
                string str = Convert.ToString(i, 2).PadLeft(n, '0');
                bool[] boolArr = str.Select((x) => x == '1').ToArray();

                matrix.Add(boolArr);
            }

            for (int i = 0; i < matrix.Count; i++)
            {
                var a = new State();
                foreach (Fluent intializedFluent in intializedFluents)
                {
                    a.addFluent(intializedFluent);
                }
                for (int j = 0; j < matrix[0].Length; j++)
                {
                    a.addFluent(new Fluent(allFluents[j].Name, matrix[i][j]));
                }
                possibleInitialStates.Add(a);

            }
            //foreach (State possibleInitialState in possibleInitialStates)
            //{
            //    Console.WriteLine(possibleInitialState);
            //}

            Logic.Main.Queries = queries;
            Logic.Main.Rules = rules;
            doRecursion(possibleInitialStates, 0);


            System.Windows.Forms.Form frmTree = new TreeForm();

            frmTree.ShowDialog();
        }


        public static void doRecursion(List<State> StateList, int queryNumber)
        {
            if (queryNumber < Logic.Main.Queries.Count)
            {
                foreach (State state in StateList)
                {
                    //Console.WriteLine(state);
                    //Console.WriteLine("-------------------------------------");
                    doRecursion(Rules.checkRules(Queries[queryNumber], state), queryNumber + 1);


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
