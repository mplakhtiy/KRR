using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KRR.Logic.Rules;

namespace KRR.Logic
{
    class Rule
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

        public List<State> checkRules(Agent_Action agentAction ,State currentState,State goal)
        {
           List<State> states = new List<State>();

            foreach(CausesIf causesIfRule in causesIfRules)
            {
                if (agentAction.isEqual(causesIfRule.agent_action))
                {
                    if (currentState.checkOrList(causesIfRule._if))
                    {
                        states.Add(currentState.changeList(causesIfRule.change));

                        return states;
                    }
                }
            }

            foreach (ReleasesIf releasesIfRule in releasesIfRules)
            {

            }
            foreach (Always alwaysRule in alwaysRules)
            {

            }
            foreach (Observable observableRule in observableRules)
            {

            }
            //output should be list of states coz of releasescan produce more than 1 state
            return states;
        }
    }
}
