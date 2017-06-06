using System.Collections.Generic;
using KRR.Logic.Rules;

namespace KRR.Logic
{
   public class Rule
    {
        private List<CausesIf> causesIfRules;
        private List<ReleasesIf> releasesIfRules;
        private List<Always> alwaysRules;
        private List<Observable> observableRules;

        public Rule()
        {
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

        public List<State> checkRules(Agent_Action agentAction, State currentState)
        {

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
                        changedStateCauses.changeList(causesIfRule.change);
                        causes = true;
                    }
                }
            }
            if (causes)
            {
                //Console.WriteLine("CAUSES____________________________");
                //Console.WriteLine("Next STATE______________________________________________");
                //Console.WriteLine(changedStateCauses);
                states.Add(changedStateCauses);
                return states;
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
                        State changedStateReleases = new State(currentState);
                        states.Add(changedStateReleases.changeList(releasesIfRule.change));
                        //Console.WriteLine("Next STATE______________________________________________");
                        //Console.WriteLine(changedStateReleases);
                    }
                }
            }



            //output should be list of states coz of releasescan produce more than 1 state
            return states;
        }
    }
}
