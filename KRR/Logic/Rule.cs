using System.Collections.Generic;
using KRR.Logic.Rules;
using KRR.Logic.TruthTable;
using System.Linq;

namespace KRR.Logic
{
   public class Rule
    {
        public static bool always;
        public static CausesIf never;

        public bool neverBool;

        public static List<CausesIf> causesIfRules;
        public static List<ReleasesIf> releasesIfRules;
        public static List<Always> alwaysRules;
        public static List<Observable> observableRules;
        public static bool agentActionOrList = false;

        public Rule()
        {
            //for result
            //check for OrList
            always = true;
            //check if causes ever executable
            never = null;

            causesIfRules = new List<CausesIf>();
            releasesIfRules = new List<ReleasesIf>();
            alwaysRules = new List<Always>();
            observableRules = new List<Observable>();

        }

        public void AddRule(IRule rule)
        {
            switch (rule.Name)
            {
                case "causes":
                    causesIfRules.Add((CausesIf)rule);
                    break;
                case "releases":
                    releasesIfRules.Add((ReleasesIf)rule);
                    break;
                case "always":
                    alwaysRules.Add((Always)rule);
                    break;
                case "observable":
                    observableRules.Add((Observable)rule);
                    break;
                default:
                    break;
            }
        }


        
        

     class DistinctItemComparer : IEqualityComparer<State>
        {

            public bool Equals(State x, State y)
            {
                return x.isEqual(y);
            }

            public int GetHashCode(State obj)
            {
                return obj.Fluents.GetHashCode();
            }
        }
     

        private List<State> checkDelete(List<State> input)
        {
            //var distinctItems = input.Distinct();
            var distinctItems = input.Distinct(new DistinctItemComparer());
            return distinctItems.ToList();
        }

        public List<State> checkRules(Agent_Action agentAction, State currentState)
        {
            agentActionOrList = false;
            //Console.WriteLine("CURRENT STATE______________________________________________");

            //Console.WriteLine(currentState);

            //Console.WriteLine(agentAction.agent.Name + "   :    " + agentAction.action.Name);
            bool causes = false;
            List<State> states = new List<State>();
            State changedStateCauses = new State(currentState);
            foreach (CausesIf causesIfRule in causesIfRules)
            {
                
                if (agentAction.isEqual(causesIfRule.agent_action))
                {
                     if (currentState.checkOrList(causesIfRule._if))
                    {
                        // State changedStateReleases = new State(currentState);
                        // states.Add(changedStateReleases.changeList(causesIfRule.change));

                        bool[] formulaResult = causesIfRule.evaluator.GetResultData();

                        // check if causes ever executable
                        neverBool = true;
                        for (int i = 0; i < formulaResult.Length; i++)
                        {
                            if (formulaResult[i])
                            {
                                neverBool = false;
                            }
                        }
                        if (neverBool)
                        {
                            never = causesIfRule;
                        }
                        //-------
                        
                         for (int i = 0; i < formulaResult.Length; i++)
                        {
                            if (formulaResult[i])
                            {
                                State changed = new State(currentState);
                                List<Fluent> toChange = new List<Fluent>();
                                foreach (Fluent fluent in currentState.Fluents)
                                {
                                    if (causesIfRule.evaluator.EvalPlan.ContainsKey(fluent.Name))
                                    {
                                        toChange.Add(new Fluent(fluent.Name,causesIfRule.evaluator.EvalPlan[fluent.Name].fieldResult[i]));
                                    }
                                }
                                changed.changeList(toChange);
                                states.Add(changed);
                            }
                        }

                        //changedStateCauses.changeList(causesIfRule.change);
                        causes = true;
                    }
                    else
                    {
                        always = false;
                        agentActionOrList = true;
                    }
                }
            }
            if (causes)
            {
                //Console.WriteLine("CAUSES____________________________");
                //Console.WriteLine("Next STATE______________________________________________");
                //Console.WriteLine(changedStateCauses);
                //states.Add(changedStateCauses);

                

                return checkDelete( states);
            }
            foreach (Always alwaysRule in alwaysRules)
            {

            }
            foreach (Observable observableRule in observableRules)
            {

            }
            //Console.WriteLine("Releases_________________");
            foreach (ReleasesIf releasesIfRule in releasesIfRules)
            {
                if (agentAction.isEqual(releasesIfRule.agent_action))
                {
                    if (currentState.checkOrList(releasesIfRule._if))
                    {
                        if (MainWindow.alwaysEvaluator != null)
                        {
                            bool[] formulaResult = MainWindow.alwaysEvaluator.GetResultData();

                            State changedStateReleases = new State(currentState);
                            State chengendFluentToTrue = changedStateReleases.changeFluent(new Fluent(releasesIfRule.change[0].Name, true));
                            State chengendFluentToFalse = changedStateReleases.changeFluent(new Fluent(releasesIfRule.change[0].Name, false));

                            bool trueAdded = false;
                            bool falseAdded = false;

                            for (int i = 0; i < formulaResult.Length; i++)
                            {
                                if (formulaResult[i])
                                {
                                    if (MainWindow.alwaysEvaluator.EvalPlan.ContainsKey(releasesIfRule.change[0].Name))
                                    {

                                        if (MainWindow.alwaysEvaluator.EvalPlan[releasesIfRule.change[0].Name].fieldResult[i])
                                        {
                                            if (!trueAdded)
                                            {
                                                states.Add(chengendFluentToTrue);
                                                trueAdded = true;
                                            }
                                        }
                                        else
                                        {
                                            if (!falseAdded)
                                            {
                                                states.Add(chengendFluentToFalse);
                                                falseAdded = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else {

                            State changedStateReleases = new State(currentState);

                            states.Add(changedStateReleases.changeFluent(new Fluent(releasesIfRule.change[0].Name, true)));

                            changedStateReleases = new State(currentState);

                            states.Add(changedStateReleases.changeFluent(new Fluent(releasesIfRule.change[0].Name, false)));
                            //Console.WriteLine("Next STATE______________________________________________");
                            //Console.WriteLine(changedStateReleases);
                        }
                    }
                    else
                    {
                        always = false;
                        agentActionOrList = false;
                    }
                }
            }



            //output should be list of states coz of releasescan produce more than 1 state
            return states;
        }


        //------------------------------drawing initial graph----------------------------------------------------


        public List<State> drawGraph(State currentState)
        {
            agentActionOrList = false;

            bool causes = false;
            List<State> states = new List<State>();
            State changedStateCauses = new State(currentState);
            foreach (CausesIf causesIfRule in causesIfRules)
            {
                    if (currentState.checkOrList(causesIfRule._if))
                    {

                        bool[] formulaResult = causesIfRule.evaluator.GetResultData();

           
                        for (int i = 0; i < formulaResult.Length; i++)
                        {
                            if (formulaResult[i])
                            {
                                State changed = new State(currentState);
                                List<Fluent> toChange = new List<Fluent>();
                                foreach (Fluent fluent in currentState.Fluents)
                                {
                                    if (causesIfRule.evaluator.EvalPlan.ContainsKey(fluent.Name))
                                    {
                                        toChange.Add(new Fluent(fluent.Name, causesIfRule.evaluator.EvalPlan[fluent.Name].fieldResult[i]));
                                    }
                                }
                                changed.changeList(toChange);
                            changed.agentAction = causesIfRule.agent_action;
                                states.Add(changed);
                            }
                        }
                        causes = true;
                    }
                    else
                    {
                        //State changed = new State(currentState);
                        //changed.agentAction = causesIfRule.agent_action;
                        //states.Add(changed);
                    }
            }
            if (causes)
            {
                return states;
            }

            foreach (ReleasesIf releasesIfRule in releasesIfRules)
            {
               
                    if (currentState.checkOrList(releasesIfRule._if))
                    {
                        State changed = new State(currentState);

                    changed.agentAction = releasesIfRule.agent_action;
                    changed.changeFluent(new Fluent(releasesIfRule.change[0].Name, true));
                    states.Add(changed);

                        changed = new State(currentState);
                    changed.changeFluent(new Fluent(releasesIfRule.change[0].Name, false));
                    changed.agentAction = releasesIfRule.agent_action;
                    states.Add(changed);

                    }
                    else
                    {
                        //State changed = new State(currentState);
                        //changed.agentAction = releasesIfRule.agent_action;
                        //states.Add(changed);
                    }
                
            }
            
            //output should be list of states coz of releasescan produce more than 1 state
            return states;
        }
    }
}
