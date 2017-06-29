using System.Collections.Generic;
using KRR.Logic.Rules;
using KRR.Logic.TruthTable;
using System.Linq;
using System.Text.RegularExpressions;
using System;

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

        //public static List<string> queriesList = new List<string>();
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
            List<string> queriesList = new List<string>();
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

                        queriesList.Add(causesIfRule.evaluator.Original);
                        // State changedStateReleases = new State(currentState);
                        // states.Add(changedStateReleases.changeList(causesIfRule.change));

                        //bool[] formulaResult = causesIfRule.evaluator.GetResultData();

                        //// check if causes ever executable
                        //neverBool = true;
                        //for (int i = 0; i < formulaResult.Length; i++)
                        //{
                        //    if (formulaResult[i])
                        //    {
                        //        neverBool = false;
                        //    }
                        //}
                        //if (neverBool)
                        //{
                        //    never = causesIfRule;
                        //}
                        ////-------
                        
                        // for (int i = 0; i < formulaResult.Length; i++)
                        //{
                        //    if (formulaResult[i])
                        //    {
                        //        State changed = new State(currentState);
                        //        List<Fluent> toChange = new List<Fluent>();
                        //        foreach (Fluent fluent in currentState.Fluents)
                        //        {
                        //            if (causesIfRule.evaluator.EvalPlan.ContainsKey(fluent.Name))
                        //            {
                        //                toChange.Add(new Fluent(fluent.Name,causesIfRule.evaluator.EvalPlan[fluent.Name].fieldResult[i]));
                        //            }
                        //        }
                        //        changed.changeList(toChange);
                        //        states.Add(changed);
                        //    }
                        //}

                        //changedStateCauses.changeList(causesIfRule.change);
                     
                    }
                    else
                    {
                        always = false;
                        agentActionOrList = true;
                    }
                }
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
                        //if (MainWindow.alwaysEvaluator != null)
                        //{
                            //bool[] formulaResult = MainWindow.alwaysEvaluator.GetResultData();

                            //State changedStateReleases = new State(currentState);
                            //State chengendFluentToTrue = changedStateReleases.changeFluent(new Fluent(releasesIfRule.change[0].Name, true));
                            //State chengendFluentToFalse = changedStateReleases.changeFluent(new Fluent(releasesIfRule.change[0].Name, false));

                            //bool trueAdded = false;
                            //bool falseAdded = false;

                            //for (int i = 0; i < formulaResult.Length; i++)
                            //{
                            //    if (formulaResult[i])
                            //    {
                            //        if (MainWindow.alwaysEvaluator.EvalPlan.ContainsKey(releasesIfRule.change[0].Name))
                            //        {

                            //            if (MainWindow.alwaysEvaluator.EvalPlan[releasesIfRule.change[0].Name].fieldResult[i])
                            //            {
                            //                if (!trueAdded)
                            //                {
                            //                    states.Add(chengendFluentToTrue);
                            //                    trueAdded = true;
                            //                }
                            //            }
                            //            else
                            //            {
                            //                if (!falseAdded)
                            //                {
                            //                    states.Add(chengendFluentToFalse);
                            //                    falseAdded = true;
                            //                }
                            //            }
                            //        }
                            //    }
                            //}
                        //}
                        //else {

                            //State changedStateReleases = new State(currentState);

                            //states.Add(changedStateReleases.changeFluent(new Fluent(releasesIfRule.change[0].Name, true)));

                            //changedStateReleases = new State(currentState);

                            //states.Add(changedStateReleases.changeFluent(new Fluent(releasesIfRule.change[0].Name, false)));
                            //Console.WriteLine("Next STATE______________________________________________");
                            //Console.WriteLine(changedStateReleases);
                            queriesList.Add("(" + releasesIfRule.change[0].Name + "∨¬" + releasesIfRule.change[0].Name+")");
                        //}
                    }
                    else
                    {
                        always = false;
                        agentActionOrList = true;
                    }
                }
            }
            if (queriesList.Count > 0)
            {
                string mainQuery = "";
                foreach (string item in queriesList)
                {
                    string temp = "";
                    if (!Regex.IsMatch(item, @"^[a-zA-Z]+$"))
                    {
                        if (item[0] != '(')
                        {
                            temp = '(' + item + ')';
                        }
                        else
                        {
                            temp = item;
                        }

                    }
                    else
                    {
                        temp = item;
                    }

                    mainQuery += temp + "∧";
                }
                neverBool = true;

                if (MainWindow.alwaysEvaluator != null)
                {
                    string alwaysQuery = MainWindow.alwaysEvaluator.Original;
                    if (!Regex.IsMatch(alwaysQuery, @"^[a-zA-Z]+$"))
                    {
                        if (alwaysQuery[0] != '(')
                        {
                            alwaysQuery = '(' + alwaysQuery + ')';
                        }
                    }

                    string queryWithAlways = mainQuery + alwaysQuery;

                    Dictionary<string, char> dictAlways = ConvertFluentsToChar(queryWithAlways);
                    String convertedTextAlways = ReplaceFluentsWithChar(dictAlways, queryWithAlways);

                    Evaluator mainEvaluatorAlways = new Evaluator(convertedTextAlways, queryWithAlways);
                    mainEvaluatorAlways.FindEvalPlan();
                    mainEvaluatorAlways.EvaluateQuery(dictAlways);

                    bool[] formulaResultAlways = mainEvaluatorAlways.GetResultData();

                    for (int i = 0; i < formulaResultAlways.Length; i++)
                    {
                        if (formulaResultAlways[i])
                        {
                            neverBool = false;
                        }
                    }

                    if (neverBool)
                    {
                        agentActionOrList = false;
                        return new List<State>();
                    }

                }
                mainQuery = mainQuery.Substring(0, mainQuery.Length - 1);

                Dictionary<string, char> dict = ConvertFluentsToChar(mainQuery);
                String convertedText = ReplaceFluentsWithChar(dict, mainQuery);

                Evaluator mainEvaluator = new Evaluator(convertedText, mainQuery);
                mainEvaluator.FindEvalPlan();
                mainEvaluator.EvaluateQuery(dict);

                //State changedStateReleases = new State(currentState);
                //states.Add(changedStateReleases.changeList(causesIfRule.change));

                bool[] formulaResult = mainEvaluator.GetResultData();

                // check if causes ever executable
                
                for (int i = 0; i < formulaResult.Length; i++)
                {
                    if (formulaResult[i])
                    {
                        neverBool = false;
                    }
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
                            if (mainEvaluator.EvalPlan.ContainsKey(fluent.Name))
                            {
                                toChange.Add(new Fluent(fluent.Name, mainEvaluator.EvalPlan[fluent.Name].fieldResult[i]));
                            }
                        }
                        changed.changeList(toChange);
                        states.Add(changed);
                    }
                }
            }
            //changedStateCauses.changeList(causesIfRule.change);


            //output should be list of states coz of releasescan produce more than 1 state


          return checkAlways(Main.allPossibleStates, states, Main.alwaysOrList);
            //return states;
        }
        public List<State> checkAlways(List<State> allPossibleStates, List<State> outputStates, List<List<Fluent>> alwaysCheckOrList)
        {
            List<State> clearStates = new List<State>();
            foreach (State output in outputStates)
            {
                if (!output.checkOrList(alwaysCheckOrList))
                {
                    State minChangesState = null;
                    int countMinChanges = 1000;
                    //check for minimum changes
                    foreach (State possibleState in allPossibleStates)
                    {
                        if (minChangesState == null)
                        {
                            minChangesState = possibleState;
                        }

                        int changes = 0;
                        foreach (Fluent tempFluent in output.Fluents)
                        {
                            if (!possibleState.check(tempFluent))
                            {
                                changes++;
                            }
                        }
                        if (changes < countMinChanges)
                        {
                            countMinChanges = changes;
                            minChangesState = possibleState;
                        }
                    }
                    clearStates.Add(minChangesState);
                }
                else
                {
                    clearStates.Add(output);
                }
            }
            return clearStates;
        }
        
        public String ReplaceFluentsWithChar(Dictionary<string, char> dict, string query)
        {

            //foreach (KeyValuePair<string, char> pair in dict)
            //{
            //    query = query.Replace(pair.Key.ToString(), pair.Value.ToString());
            //}

            //return query;


            string result = query;
            foreach (KeyValuePair<string, char> pair in dict)
            {
                string replace = pair.Value.ToString();
                result = Regex.Replace(result, string.Format(@"\b{0}\b", pair.Key.ToString()), replace);
            }

            return result;


        }

        public Dictionary<string, char> ConvertFluentsToChar(string inputText)
        {

            Dictionary<string, char> dict = new Dictionary<string, char>();

            List<char> lstChar = new List<char>()
                        //{'a','b','c','d','e','f', 'g','h', 'i','j','k','l','m','n','o'};
            { 'α', 'β', 'γ', 'δ', 'ε', 'ζ', 'η', 'θ', 'ι', 'κ', 'λ', 'μ', 'ν', 'ξ', 'ο', 'π', 'ρ', 'σ', 'τ', 'υ', 'φ', 'χ', 'ψ', 'ω'};


            string fluent = "";

            for (int i = 0; i < inputText.Length; i++)
            {
                if (Char.IsLetter(inputText[i]) == true && i != inputText.Length - 1)
                {
                    fluent += inputText[i];
                }
                else if (fluent != "" && (Evaluator.prec.Contains(inputText[i]) == true || inputText[i] == ' '))
                {
                    if (!dict.ContainsKey(fluent))
                    {
                        dict.Add(fluent, lstChar[0]);
                        fluent = "";
                        lstChar.RemoveAt(0);
                    }
                    else
                    {
                        fluent = "";
                    }

                }

                else if (Char.IsLetter(inputText[i]) == true && i == inputText.Length - 1)
                {
                    fluent += inputText[i];
                    if (!dict.ContainsKey(fluent))
                    {
                        dict.Add(fluent, lstChar[0]);
                    }
                }

                else if (i == inputText.Length - 1)
                {
                    if (!dict.ContainsKey(fluent))
                    {
                        fluent += inputText[i];
                        dict.Add(fluent, lstChar[0]);
                    }
                }
            }

            return dict;

        }
        //------------------------------drawing initial graph----------------------------------------------------


        public List<State> drawGraph(State currentState)
        {
          
            bool checkEmptyEffect = false;
            List<State> states = new List<State>();

            List<Agent_Action> action_agent_list = new List<Agent_Action>();
            foreach (Agent agent in MainWindow.agents)
            {
                foreach (Action action in MainWindow.actions)
                {
                    action_agent_list.Add(new Agent_Action(agent, action));
                }
            }
            foreach (Agent_Action agent_action in action_agent_list)
            {
                List<string> queriesList = new List<string>();


                foreach (CausesIf causesIfRule in causesIfRules)
                {
                    if (agent_action.isEqual(causesIfRule.agent_action))
                    {
                        if (currentState.checkOrList(causesIfRule._if))
                        {
                            queriesList.Add(causesIfRule.evaluator.Original);
                        }
                        //empty effect
                        else
                        {
                            if (!states.Contains(currentState))
                            {
                                State tempState = new State(currentState);
                                tempState.agentAction = agent_action;
                                states.Add(tempState);
                            }
                        }
                    }

                }


                foreach (ReleasesIf releasesIfRule in releasesIfRules)
                {
                    if (agent_action.isEqual(releasesIfRule.agent_action))
                    {
                        if (currentState.checkOrList(releasesIfRule._if))
                        {
                            queriesList.Add("(" + releasesIfRule.change[0].Name + "∨¬" + releasesIfRule.change[0].Name + ")");

                        }
                        //empty effect
                        else
                        {
                            if (!states.Contains(currentState))
                            {
                                State tempState = new State(currentState);
                                tempState.agentAction = agent_action;
                                states.Add(tempState);
                            }
                        }
                    }

                }

                if (queriesList.Count > 0)
                {
                    string mainQuery = "";
                    foreach (string item in queriesList)
                    {
                        string temp = "";
                        if (!Regex.IsMatch(item, @"^[a-zA-Z]+$"))
                        {
                            if (item[0] != '(')
                            {
                                temp = '(' + item + ')';
                            }
                            else
                            {
                                temp = item;
                            }

                        }
                        else
                        {
                            temp = item;
                        }

                        mainQuery += temp + "∧";
                    }
                    mainQuery = mainQuery.Substring(0, mainQuery.Length - 1);

                    Dictionary<string, char> dict = ConvertFluentsToChar(mainQuery);
                    String convertedText = ReplaceFluentsWithChar(dict, mainQuery);

                    Evaluator mainEvaluator = new Evaluator(convertedText, mainQuery);
                    mainEvaluator.FindEvalPlan();
                    mainEvaluator.EvaluateQuery(dict);

                    bool[] formulaResult = mainEvaluator.GetResultData();

                    for (int i = 0; i < formulaResult.Length; i++)
                    {
                        if (formulaResult[i])
                        {
                            State changed = new State(currentState);
                            changed.agentAction = agent_action;
                            List<Fluent> toChange = new List<Fluent>();
                            foreach (Fluent fluent in currentState.Fluents)
                            {
                                if (mainEvaluator.EvalPlan.ContainsKey(fluent.Name))
                                {
                                    toChange.Add(new Fluent(fluent.Name, mainEvaluator.EvalPlan[fluent.Name].fieldResult[i]));
                                }
                            }
                            changed.changeList(toChange);
                            states.Add(changed);
                        }
                    }
                }
            }
            //output should be list of states coz of releasescan produce more than 1 state
            return this.checkDelete(states);
        }
    }
}
