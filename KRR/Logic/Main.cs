using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KRR.Logic.Rules;
using KRR.Logic.TruthTable;

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

        //create a form
        public static System.Windows.Forms.Form form;
        //create a viewer object
        public static  Microsoft.Msagl.GraphViewerGdi.GViewer viewer;
        public static Microsoft.Msagl.Drawing.Graph graph;
        public static string tree ="";
        //create a form
        public static System.Windows.Forms.Form form1;
        //create a viewer object
        public static Microsoft.Msagl.GraphViewerGdi.GViewer viewer1;
        public static Microsoft.Msagl.Drawing.Graph graph1;
        public static List<List<Fluent>> goalOrList;


        public static void drawGraph(List<Fluent> init, List<Fluent> goal, List<Fluent> allFluents, Rule rules)
        {
            //create a form
            form1 = new System.Windows.Forms.Form();
            //create a viewer object
            viewer1 = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            //create a graph object
            graph1= new Microsoft.Msagl.Drawing.Graph("graph");


            List<Fluent> allFluentsCopy = new List<Fluent>();
            foreach (Fluent item in allFluents)
            {
                allFluentsCopy.Add(item);
            }
            List<State> allPossibleStates = new List<State>();



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
           
                for (int i = 0; i < matrix.Count; i++)
                {
                    var a = new State();
                    //if all fluents size > 
                    for (int j = 0; j < matrix[0].Length; j++)
                    {
                        a.addFluent(new Fluent(allFluents[j].Name, matrix[i][j]));
                    }
                allPossibleStates.Add(a);
                
            }
            int k = 0;
            foreach (State state in allPossibleStates)
            {
                k++;
                state.name = "S-" + k;
            }
            foreach (State state in allPossibleStates)
            {
                graph1.AddNode(state.ToString()).Attr.Shape = Microsoft.Msagl.Drawing.Shape.Ellipse;

                if (goal != null)
                {
                    if(goal.Count>0)
                    if (state.checkList(goal))
                    {
                        graph1.FindNode(state.ToString()).Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightPink;
                    }
                }
                if (init != null)
                {
                    if(init.Count>0)
                    if (state.checkList(init))
                    {
                        graph1.FindNode(state.ToString()).Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightSkyBlue;
                    }
                }
                foreach (State drawState in rules.drawGraph(state))
                {
                    foreach(State check in allPossibleStates)
                    {
                        if (check.isEqual(drawState))
                        {
                            drawState.name = check.name;
                            break;
                        }
                    }
                    bool draw = true;
                    foreach(var a in graph1.Edges.ToList())
                    {
                        if(a.Attr.Id== state.name + drawState.name+drawState.agentAction.ToString())
                        {
                            draw = false;
                            break;
                        }
                    }

                    if (draw)
                    {
                        graph1.AddEdge(state.ToString(), drawState.agentAction.ToString(), drawState.ToString()).Attr.Id = state.name + drawState.name+ drawState.agentAction.ToString();
                    }

                }
             
               

               
            }
            //bind the graph to the viewer 
            viewer1.Graph =
            graph1;

            //associate the viewer with the form
            form1.SuspendLayout();
            viewer1.Dock =
             System.Windows.Forms.DockStyle.Fill;
            form1.Controls.Add(viewer1);
            form1.ResumeLayout();
            ///show the form
           // form1.ShowDialog();


        }
        public static List<List<Fluent>> getOrList(Evaluator evaluator)
        {
            List<List<Logic.Fluent>> orList = new List<List<Logic.Fluent>>();

            if (evaluator != null)
            {
                bool[] formulaResult = evaluator.GetResultData();
                bool neverBool = true;
                for (int i = 0; i < formulaResult.Length; i++)
                {
                    if (formulaResult[i])
                    {
                        neverBool = false;
                    }
                }
                if (neverBool)
                {
                    orList.Add(new List<Fluent> { new Fluent("-", false) });

                }
                else
                {

                    for (int i = 0; i < formulaResult.Length; i++)
                    {
                        if (formulaResult[i])
                        {

                            List<Fluent> andList = new List<Fluent>();
                            foreach (Fluent fluent in MainWindow.allFluents)
                            {
                                if (evaluator.EvalPlan.ContainsKey(fluent.Name))
                                {
                                    andList.Add(new Fluent(fluent.Name, evaluator.EvalPlan[fluent.Name].fieldResult[i]));
                                }
                            }

                            orList.Add(andList);
                        }
                    }
                }
            }
            return orList;
        }

        public static void TheMostImportantMethod(Agent agent,Evaluator goalEvaluator,Rule rules, Evaluator initiallyEvaluator, List<Fluent> allFluents,
            List<Agent_Action> queries)
        {
           
            List<List<Fluent>> initiallyOrLıst = getOrList(initiallyEvaluator);
            goalOrList = getOrList(goalEvaluator);

            if (MainWindow.alwaysEvaluator != null)
            {
                foreach (CausesIf causesEvaluator in Rule.causesIfRules)
                {
                    causesEvaluator.checngeCauses(MainWindow.AlwaysHeader);
                }
            }
            MainWindow.AlwaysHeader = "";

            //create a form
            form = new System.Windows.Forms.Form();
            //create a viewer object
            viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            //create a graph object
           graph = new Microsoft.Msagl.Drawing.Graph("graph");


            alwaysAgent = true;
            neverAgent = true;
            agentToCheck = agent;

            goalNever = true;
            goalAlways = true;
           // Goal = _goal;

            lastQuery = -1;
            always = true;
            result = "";
            Rule.never = null;
            Rule.always = true;
            

            List<State> possibleInitialStates = new List<State>();

            //List<Fluent> temp = new List<Fluent>();
            //foreach (Fluent fluent in allFluents)
            //{
            //    temp.Add(fluent);
            //}
            //    //Create temp list with not setted Fluents
            //foreach (Fluent fluent in temp)
            //{
            //    foreach (Fluent intializedFluent in intializedFluents)
            //    {
            //        if (intializedFluent.Name == fluent.Name)
            //        {
            //            allFluents.Remove(fluent);
            //            break;
            //        }
            //    }
            //}
            //if (intializedFluents.Count > 0)
            //{
            //allFluents = temp;
            //}



            //int n = allFluents.Count;
            ////Create all possible combinations
            //List<bool[]> matrix = new List<bool[]>();
            //double count = Math.Pow(2, n);
            ////if all fluents.count == 0 then matrix also should be 0 elemetns but not 2^0=1
            //if (n == 0)
            //{
            //    count = 0;
            //}

            //for (int i = 0; i < count; i++)
            //{
            //    string str = Convert.ToString(i, 2).PadLeft(n, '0');
            //    bool[] boolArr = str.Select((x) => x == '1').ToArray();

            //    matrix.Add(boolArr);
            //}
            ////if all fluents were initiallized then just add initially fluents
            //if (matrix.Count == 0)
            //{
            //    var a = new State();
            //    foreach (Fluent intializedFluent in intializedFluents)
            //    {
            //        a.addFluent(intializedFluent);
            //    }
            //    possibleInitialStates.Add(a);
            //}
            //else {
            //    for (int i = 0; i < matrix.Count; i++)
            //    {
            //        var a = new State();
            //        foreach (Fluent intializedFluent in intializedFluents)
            //        {
            //            a.addFluent(intializedFluent);
            //        }

            //        //if all fluents size > 
            //        for (int j = 0; j < matrix[0].Length; j++)
            //        {
            //            a.addFluent(new Fluent(allFluents[j].Name, matrix[i][j]));
            //        }
            //        possibleInitialStates.Add(a);

            //    }
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

            for (int i = 0; i < matrix.Count; i++)
            {
                var a = new State();
                //if all fluents size > 
                for (int j = 0; j < matrix[0].Length; j++)
                {
                    a.addFluent(new Fluent(allFluents[j].Name, matrix[i][j]));
                }
                possibleInitialStates.Add(a);
                /*--------------------               possibleInitialStates contains  all possible states   -             ---------------------*/
            }

            List<State> tempStates = new List<State>();
            foreach (State possibleInitialState in possibleInitialStates)
            {
                if(possibleInitialState.checkOrList(initiallyOrLıst))
                     tempStates.Add(possibleInitialState);
            }

            possibleInitialStates = tempStates;
            foreach (State item in possibleInitialStates)
            {
                Console.WriteLine(item);
            }

            Logic.Main.Queries = queries;
            Logic.Main.Rules = rules;
            doRecursion(possibleInitialStates, 0,"",null);

            //result output
            //program
            if (MainWindow.choosenType == 2)
            {
                if (Logic.Main.Queries.Count != lastQuery)
                {
                    result += "false";

                    //for (int i = 0; i < Logic.Main.Queries.Count; i++)
                    //{
                    //    if (i > lastQuery - 1)
                    //    {
                    //        result += "Q" + (i + 1) + " " + Logic.Main.Queries[i].ToString() + " - not executable \n";
                    //    }
                    //}
                }
                else
                {
                    if (MainWindow.choosenPossibly == 1)
                    {
                        if (always)
                        {
                            result += "true";
                        }
                        else
                        {
                            result += "false";
                        }
                    }
                    else
                    {
                        result += "true";
                    }

                }
            }
            //goal
            if (MainWindow.choosenType == 0)
            {
                if (goalEvaluator != null)
                {
                    if (goalEvaluator.EvalPlan.Count > 0)
                    {

                        if (goalNever)
                        {
                            result += "False";
                        }
                        else
                        {
                            if (MainWindow.choosenPossibly == 1)
                            {
                                if (goalAlways)
                                {
                                    result += "True";
                                }
                                else
                                {
                                    result += "False";
                                }
                            }
                            else
                            {
                                result += "True";
                            }
                        }
                    }
                }
            }
            //agent
            if (MainWindow.choosenType == 1)
            {
                if (agentToCheck != null)
                {

                    if (neverAgent)
                    {
                        result += "False";
                    }
                    else
                    {
                        if (MainWindow.choosenPossibly == 1)
                        {
                            if (alwaysAgent)
                            {
                                result += "True";
                            }
                            else
                            {
                                result += "False";
                            }
                        }
                        else
                        {
                            result += "True";
                        }
                    }
                }
            }
            Console.WriteLine("Result -----------  " +result);

            //bind the graph to the viewer 
            viewer.Graph =
            graph;

            //associate the viewer with the form
            form.SuspendLayout();
            viewer.Dock =
             System.Windows.Forms.DockStyle.Fill;
            form.Controls.Add(viewer);
            form.ResumeLayout();
            ///show the form
            //form.ShowDialog();
            Console.WriteLine("8---------------------------------------8");
            Console.WriteLine(tree);
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
                if (queryNumber == Queries.Count)
                {
                    //check for goal always/ever/never
                    if (Goal != null && queryNumber != 0)
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
                }

                string nodeId = parent+node;
                if (parent == "")
                {
                    graph.AddNode(nodeId + state.ToString()).Attr.Shape = Microsoft.Msagl.Drawing.Shape.Ellipse;
                    graph.FindNode(nodeId + state.ToString()).Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightSkyBlue;
                }
                if (parent != "")
                {
                    graph.AddEdge(parent + parentState.ToString(), nodeId + state.ToString()).LabelText= Queries[queryNumber-1].ToString();
                    graph.FindNode(nodeId + state.ToString()).Attr.Shape = Microsoft.Msagl.Drawing.Shape.Ellipse;
                }
                if (queryNumber == Queries.Count)
                {
                    //check for goal always/ever/never
                    if (Goal != null && queryNumber != 0)
                    {

                        if (state.checkOrList(goalOrList))
                        {
                            goalNever = false;
                            graph.FindNode(nodeId + state.ToString()).Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightPink;
                        }
                        else
                        {
                            goalAlways = false;
                        }
                    }
                }

                tree += "parent : " + parent + " , node : " + nodeId + " ; \n";
              
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
