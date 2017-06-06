using System.Collections.Generic;

namespace KRR.Logic.Rules
{
    public class ReleasesIf : IRule
    {
        public double probability;
        public string Name { get; set; }
        public List<Fluent> change { get; set; }
        public List<List<Fluent>> _if { get; set; }
        public Agent_Action agent_action { get; set; }
        public ReleasesIf(Agent_Action agent_action, List<Fluent> change, List<List<Fluent>> _if)
        {
            this.Name = "releases";
            this.change = change;
            this._if = _if;
            this.agent_action = agent_action;
            this.probability = 0.5;
        }
        public override string ToString()
        {
            string text = this.agent_action.agent.Name + "(" + agent_action.action.Name + ")" + " " + Name + " ";
           /* foreach (var v in change)
            {
                text += v.Name.ToString() + " " + v.IsTrue.ToString() + ", ";
            }
            /*text += " if ";
            foreach (List<Fluent> v in _if)
            {
                foreach
            }
            */
            return text;
        }
    }
}
